﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using NLog;
using VPC.Entities.EntityCore.Model.Query;
using VPC.Entities.EntityCore.Model.Storage;
using VPC.Entities.WorkFlow.Engine;
using VPC.Entities.WorkFlow.Engine.Email;
using VPC.Framework.Business.DynamicQueryManager.Contracts;
using VPC.Framework.Business.DynamicQueryManager.Core.Enums;
using VPC.Framework.Business.EntityResourceManager.Contracts;
using VPC.Framework.Business.Initilize.Contracts;
using VPC.Framework.Business.MetadataManager.Contracts;
using VPC.WebApi.AttributesHandler;
using VPC.WebApi.Model;
using VPC.WebApi.Utility;

namespace VPC.WebApi.Controllers.EntityController {
    [Route ("api/entities")]
    //[Authorize(Policy = "AuthRole")]
    public class EntityController : BaseApiController {
        private readonly Logger _log = LogManager.GetCurrentClassLogger ();
        private readonly IEntityResourceManager _iEntityResourceManager;
        private readonly ILayoutManager _iILayoutManager;
        private readonly IMetadataManager _iMetadataManager;
        private readonly IJsonMessage _iJsonMessage;
        private readonly IInitilizeManager _initilizeManager;
        private readonly IMatcher _iMatcher;

        private readonly IInsertHelper _iEntityQueryManagerV1;

        public EntityController (IEntityResourceManager iEntityResourceManager, ILayoutManager iILayoutManager, IMetadataManager iMetadataManager, IJsonMessage iJsonMessage, IInitilizeManager initilizeManager, IMatcher iMatcher, IInsertHelper iEntityQueryManagerV1) {
            _iEntityResourceManager = iEntityResourceManager;
            _iILayoutManager = iILayoutManager;
            _iMetadataManager = iMetadataManager;
            _iJsonMessage = iJsonMessage;
            _initilizeManager = initilizeManager;
            _iMatcher = iMatcher;
            _iEntityQueryManagerV1 = iEntityQueryManagerV1;
        }

        [HttpGet ("{entityName}")]
        [ProducesResponseType (200)]
        [ProducesResponseType (500)]
        [ProducesResponseType (404)]
        public IActionResult GetResults (string entityName, [FromQuery] string fields, string searchText, string orderBy, string filters, int pageIndex, int pageSize) {
            try {
                var stopwatch = StopwatchLogger.Start (_log);
                _log.Info ("Called EntityController GetResults");
                var tenantId = TenantCode;
                var queryFilter = ApiHelper.GetQueryFilter (filters, tenantId, IsSystemAdmin, entityName);
                var queryContext = new QueryContext { Fields = fields, Filters = queryFilter, PageSize = pageSize, PageIndex = pageIndex, OrderBy = orderBy };

                if (!string.IsNullOrEmpty (searchText)) {
                    var defaultLayout = _iILayoutManager.GetDefaultLayoutForEntity (TenantCode, entityName, (int) LayoutType.List, "", 0);
                    if (defaultLayout != null && defaultLayout.ListLayoutDetails != null && defaultLayout.ListLayoutDetails.SearchProperties != null) {
                        var freeTextSearch = defaultLayout.ListLayoutDetails.SearchProperties.FirstOrDefault (t => t.Name.Equals ("FreeTextSearch"));
                        if (freeTextSearch != null && freeTextSearch.Properties != null && freeTextSearch.Properties.Any ()) {
                            queryContext.FreeTextSearch = new List<QueryFilter> ();
                            foreach (var prop in freeTextSearch.Properties) {
                                var filter = new QueryFilter ();
                                filter.FieldName = prop.Name;
                                filter.Operator = Comparison.Like.ToString ();
                                filter.Value = searchText;
                                queryContext.FreeTextSearch.Add (filter);
                            }
                        }
                    }
                }
                var result = _iEntityResourceManager.GetResult (tenantId, entityName, queryContext);
                stopwatch.StopAndLog ("GetResults of EntityController");

                if (result == null || result.Rows.Count <= 0)
                    return Ok (new { result = new List<string> (), totalRow = 0 });
                if (result.Columns.Contains ("Context")) {
                    result = SetEntityNamebyCode (entityName, result); //Added to get Entityname by Entity code for Email/SMSTemplate
                }
                dynamic totalRow = (result.Columns.Contains ("totalRow")) ? result.Rows[0]["totalRow"] : result.Rows.Count;
                ApiHelper.MapDynamicColumn (result.Columns);
                return Ok (new { result, totalRow });
            } catch (FieldAccessException fx) {
                _log.Error (ExceptionFormatter.SerializeToString (fx));
                return StatusCode ((int) HttpStatusCode.BadRequest, fx.Message);
            } catch (Exception ex) {
                //return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
                _log.Error (ExceptionFormatter.SerializeToString (ex));
                return StatusCode ((int) HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route ("{entityName}/{id:guid}")]
        [ProducesResponseType (200)]
        [ProducesResponseType (500)]
        [ProducesResponseType (404)]
        public IActionResult GetEntityDetails ([FromRoute] string entityName, Guid id, [FromQuery] string subType) {
            var tenantId = TenantCode;

            try {
                var stopwatch = StopwatchLogger.Start (_log);
                _log.Info ("Called EntityController GetEntityDetails with entityName {0}=", entityName);
                _log.Info ("Called EntityController GetEntityDetails with subType {0}=", subType);

                if (string.IsNullOrEmpty (entityName)) {
                    return BadRequest ("Entity name required!");
                }
                if (id == Guid.Empty) {
                    return BadRequest ("Id required!");
                }
                if (string.IsNullOrEmpty (subType)) {
                    return BadRequest ("Sub type required!");
                }

                var result = _iEntityResourceManager.GetResultDetailsFromDefaultLayout (tenantId, entityName, id, LayoutType.Form, subType, LayoutContext.Edit);
                GetDetailEntity (entityName, id, subType, tenantId, result);

                if (result == null || result.Rows.Count <= 0)
                    return NotFound ("Data not found");

                if (result.Columns.Contains ("Context")) {
                    result = SetEntityNamebyCode (entityName, result); //Added to get Entityname by Entity code for Email/SMSTemplate
                }

                ApiHelper.MapDynamicColumn (result.Columns);
                stopwatch.StopAndLog ("GetEntityDetails method of EntityController.");

                var computedField = _iILayoutManager.GetComputedFields (tenantId, entityName, LayoutType.Form, subType, (int) LayoutContext.Edit);

                var resultModel = new ExpandoObject () as IDictionary<string, Object>;
                var receiversWithValue = new List<ComputedField> ();
                foreach (DataColumn item in result.Columns) {
                    resultModel.Add (item.ColumnName, result.Rows[0][item.ColumnName]);
                    if (computedField.Any ()) {
                        // var broadcuster = computedField.FirstOrDefault(t => !string.IsNullOrEmpty(t.BroadcastingTypes) && t.Name.ToLower().Equals(item.ColumnName.ToLower()));
                        // if (broadcuster != null)
                        // {
                        //     var receiver = computedField.FirstOrDefault(
                        //         t => !string.IsNullOrEmpty(t.ReceivingTypes) &&
                        //         t.ReceivingTypes.ToLower().Equals(broadcuster.BroadcastingType.ToLower())
                        //         );
                        //     if (receiver != null)
                        //     {
                        //         receiver.Value = result.Rows[0][item.ColumnName];
                        //         var computed = new ComputedField();
                        //         computed.FieldName = receiver.Name;
                        //         computed.Value = receiver.Value;
                        //         computed.MethodName = broadcuster.BroadcastingType;
                        //         receiversWithValue.Add(computed);
                        //     }
                        // }
                    }
                }
                if (receiversWithValue.Any ()) {
                    //computed manager call
                }
                return Ok (resultModel);
            } catch (Exception ex) {
                _log.Error (ExceptionFormatter.SerializeToString (ex));
                return StatusCode ((int) HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
            }
        }

        private void GetDetailEntity (string entityName, Guid id, string subType, Guid tenantId, DataTable result) {
            var detailEntities = _iILayoutManager.GetDetailEntitiesFromDefaultLayoutForEntity (tenantId, entityName, (int) LayoutType.Form, subType, (int) LayoutContext.Edit);
            if (detailEntities != null && detailEntities.Any ()) {
                foreach (var detail in detailEntities) {
                    Guid targetId = Guid.Parse (detail.Value); //value from selected view..
                    var viewLayout = _iILayoutManager.GetLayoutsDetailsById (tenantId, targetId);
                    var fields = string.Empty;
                    if (viewLayout != null && viewLayout.ViewLayoutDetails != null && viewLayout.ViewLayoutDetails.Fields.Any ()) {
                        foreach (var item in viewLayout.ViewLayoutDetails.Fields) {
                            fields += item.Name + ",";
                        }
                        fields = fields.TrimEnd (',');
                    }
                    if (fields == string.Empty) {
                        continue;
                    }
                    var relatedColumn = _iMetadataManager.GetRelatedColumnNameOfDetailEntity (entityName, detail.Key);
                    var filters = new List<QueryFilter> ();
                    if (relatedColumn != null) {
                        var queryFilter = new QueryFilter ();
                        queryFilter.FieldName = relatedColumn.FieldName;
                        queryFilter.Operator = "=";
                        queryFilter.Value = id.ToString ();
                        filters.Add (queryFilter);
                    }
                    var queryContext = new QueryContext { Fields = fields, Filters = filters, PageSize = 10, PageIndex = 1 };
                    var detailEntity = _iEntityResourceManager.GetResult (tenantId, detail.Key, queryContext);
                    if (detailEntity == null || detailEntity.Rows.Count <= 0) continue;
                    dynamic detailEntityRowCount = detailEntity.Rows[0]["totalRow"];
                    //newly introduced columns
                    result.Columns.Add (detail.Key, typeof (DataTable));
                    result.Columns.Add (detail.Key + "Count", typeof (int));
                    // add value to newly introduced columns
                    foreach (DataRow row in result.Rows) {
                        row[detail.Key] = detailEntity;
                        row[detail.Key + "Count"] = detailEntityRowCount;
                    }
                    ApiHelper.MapDynamicColumn (detailEntity.Columns);
                }
            }
        }

        [HttpPost ("{entityName}")]
        [ProducesResponseType (200)]
        [ProducesResponseType (500)]
        [ProducesResponseType (404)]
        //   [ValidateJObjectdata]
        public IActionResult SaveResult ([FromRoute] string entityName, [FromQuery] string subType, [FromBody] JObject value) {
            try {
                var stopwatch = StopwatchLogger.Start (_log);
                _log.Info ("Called EntityController SaveResult");
                if (value == null) return BadRequest ("Value required");
                if (string.IsNullOrEmpty (subType)) return BadRequest ("sub type required");
                var result = _iEntityResourceManager.SaveResult (TenantCode, UserId, entityName, value, subType);
                stopwatch.StopAndLog ("SaveResult of EntityController");
                return Ok (new { result });
                // var result1 = _iEntityResourceManager.SaveResult(TenantCode, UserId, entityName, value, subType);
                //var id = Guid.NewGuid ();
                //var query = _iEntityQueryManagerV1.BuildInsertQuery (id, TenantCode, entityName, value, subType, UserId);
                // var col = _iEntityQueryManagerV1.MatchTest(TenantCode, entityName, value, subType, UserId);
                //return Ok (new { query });
                //  return Ok(new {col, query });
            } catch (FieldAccessException ex) {
                //return BadRequest(ex.Message);
                _log.Error (ExceptionFormatter.SerializeToString (ex));
                return StatusCode ((int) HttpStatusCode.BadRequest, ApiConstant.CustomErrorMessage);
            } catch (Exception ex) {
                //return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
                _log.Error (ExceptionFormatter.SerializeToString (ex));
                return StatusCode ((int) HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
            }
        }

        [HttpPut ("{entityName}/{id:guid}")]
        [ProducesResponseType (200)]
        [ProducesResponseType (500)]
        [ProducesResponseType (404)]
        [ValidateJObjectdata]
        public IActionResult UpdateResult ([FromRoute] string entityName, Guid id, [FromQuery] string subType, [FromBody] JObject value) {
            try {
                var stopwatch = StopwatchLogger.Start (_log);
                _log.Info ("Called EntityController UpdateResult");
                if (value == null) return BadRequest ("Value required");
                if (string.IsNullOrEmpty (subType)) return BadRequest ("sub type required");
                var result = _iEntityResourceManager.UpdateResult (TenantCode, UserId, id, entityName, value, subType);
                stopwatch.StopAndLog ("UpdateResult of EntityController");
                return Ok (result);
            } catch (FieldAccessException ex) {
                //return BadRequest(ex.Message);
                _log.Error (ExceptionFormatter.SerializeToString (ex));
                return StatusCode ((int) HttpStatusCode.BadRequest, ApiConstant.CustomErrorMessage);
            } catch (Exception ex) {
                //return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
                _log.Error (ExceptionFormatter.SerializeToString (ex));
                return StatusCode ((int) HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
            }
        }

        [HttpDelete ("{entityName}/{id:guid}")]
        [ProducesResponseType (200)]
        [ProducesResponseType (500)]
        [ProducesResponseType (404)]
        public IActionResult DeleteResult ([FromRoute] string entityName, Guid id, string subType) {
            try {
                var stopwatch = StopwatchLogger.Start (_log);
                _log.Info ("Called EntityController DeleteResult");
                var result = _iEntityResourceManager.DeleteResult (TenantCode, UserId, id, entityName, subType);
                stopwatch.StopAndLog ("DeleteResult of EntityController");
                return Ok (result);
            } catch (FieldAccessException fx) {
                //return BadRequest(fx.Message);
                _log.Error (ExceptionFormatter.SerializeToString (fx));
                return StatusCode ((int) HttpStatusCode.BadRequest, ApiConstant.CustomErrorMessage);
            } catch (Exception ex) {
                //return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
                _log.Error (ExceptionFormatter.SerializeToString (ex));
                return StatusCode ((int) HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
            }
        }

        [HttpGet]
        [Route ("{entityName}/{id:guid}/{detailEntityName}")]
        [ProducesResponseType (200)]
        [ProducesResponseType (500)]
        [ProducesResponseType (404)]
        public IActionResult DetailEntity_GetEntityDetails ([FromRoute] string entityName, Guid id, string detailEntityName, [FromQuery] string subType, string orderBy, int pageIndex, int pageSize) {
            try {
                Guid targetId = Guid.NewGuid ();
                var tenantId = TenantCode;
                var stopwatch = StopwatchLogger.Start (_log);
                _log.Info ("Called EntityController DetailEntity_GetEntityDetails");
                var defaultLayout = _iILayoutManager.GetDefaultLayoutForEntity (tenantId, entityName, (int) LayoutType.Form, subType, (int) LayoutContext.Edit);
                if (defaultLayout != null && defaultLayout.FormLayoutDetails != null && defaultLayout.FormLayoutDetails.Fields != null) {
                    targetId = _iEntityResourceManager.GetSelectedView (defaultLayout.FormLayoutDetails.Fields);
                }

                var viewLayout = _iILayoutManager.GetLayoutsDetailsById (tenantId, targetId);
                var fields = string.Empty;

                if (viewLayout != null && viewLayout.ViewLayoutDetails != null && viewLayout.ViewLayoutDetails.Fields.Any ()) {
                    foreach (var item in viewLayout.ViewLayoutDetails.Fields) {
                        fields += item.Name + ",";
                    }
                    fields = fields.TrimEnd (',');
                }

                var relatedColumn = _iMetadataManager.GetRelatedColumnNameOfDetailEntity (entityName, detailEntityName);
                var filters = new List<QueryFilter> ();
                if (relatedColumn != null) {
                    var queryFilter = new QueryFilter ();
                    queryFilter.FieldName = relatedColumn.FieldName;
                    queryFilter.Operator = relatedColumn.FieldName;
                    queryFilter.Value = id.ToString ();
                    filters.Add (queryFilter);
                }
                var queryContext = new QueryContext { Fields = fields, Filters = filters, PageSize = pageSize, PageIndex = pageIndex, OrderBy = orderBy };
                var result = _iEntityResourceManager.GetResult (tenantId, detailEntityName, queryContext);

                stopwatch.StopAndLog ("DetailEntity_GetEntityDetails of EntityController");

                if (result == null || result.Rows.Count <= 0)
                    return Ok (new { result = new List<string> (), totalRow = 0 });
                dynamic totalRow = result.Rows[0]["totalRow"];
                ApiHelper.MapDynamicColumn (result.Columns);
                return Ok (new { result, totalRow });
                // stopwatch.StopAndLog ("GetAllPicklistValues method of PicklistController.");
            } catch (Exception ex) {
                _log.Error (ExceptionFormatter.SerializeToString (ex));
                return StatusCode ((int) HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
            }
        }

        [HttpGet]
        [Route ("{entityName}/{id:guid}/{detailEntityName}/{detailId:guid}")]
        public IActionResult GetDetailEntity_DetailsById ([FromRoute] string entityName, Guid id, string detailEntityName, Guid detailId) {
            try {
                var tenantId = TenantCode;
                var stopwatch = StopwatchLogger.Start (_log);
                _log.Info ("Called EntityController GetDetailEntity_DetailsById");
                var result = _iEntityResourceManager.GetResultDetailsFromDefaultLayout (tenantId, detailEntityName, detailId, (LayoutType) LayoutType.Form, "Standard", LayoutContext.Edit);
                if (result == null || result.Rows.Count <= 0)
                    return NotFound ("Data not found");
                ApiHelper.MapDynamicColumn (result.Columns);
                stopwatch.StopAndLog ("GetDetailEntity_DetailsById method of EntityController.");

                var json = new JObject (result.Columns.Cast<DataColumn> ().Select (c => new JProperty (c.ColumnName, JToken.FromObject (result.Rows[0][c]))));
                return Ok (json);

            } catch (Exception ex) {
                _log.Error (ExceptionFormatter.SerializeToString (ex));
                return StatusCode ((int) HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
            }
        }

        [HttpPost ("{entityName}/{id:guid}/{detailEntityName}")]
        [ProducesResponseType (200)]
        [ProducesResponseType (500)]
        [ProducesResponseType (404)]
        public IActionResult DetailEntity_SaveResult ([FromRoute] string entityName, Guid id, string detailEntityName, [FromQuery] string subType, [FromBody] JObject value) {
            try {
                var stopwatch = StopwatchLogger.Start (_log);
                _log.Info ("Called EntityController DetailEntity_SaveResult");

                if (value == null) return BadRequest ("Value required");
                if (string.IsNullOrEmpty (subType)) return BadRequest ("sub type required");
                var tenantId = TenantCode;
                var relatedColumn = _iMetadataManager.GetRelatedColumnNameOfDetailEntity (entityName, detailEntityName);
                if (relatedColumn != null) {
                    value.Add (relatedColumn.FieldName, id);
                }
                var result = _iEntityResourceManager.SaveResult (tenantId, UserId, detailEntityName, value, string.Empty);

                stopwatch.StopAndLog ("DetailEntity_SaveResult of EntityController");

                return Ok (result);
            } catch (FieldAccessException ex) {
                //return BadRequest(ex.Message);
                _log.Error (ExceptionFormatter.SerializeToString (ex));
                return StatusCode ((int) HttpStatusCode.BadRequest, ApiConstant.CustomErrorMessage);
            } catch (Exception ex) {
                //return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
                _log.Error (ExceptionFormatter.SerializeToString (ex));
                return StatusCode ((int) HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
            }
        }

        [HttpPut]
        [Route ("{entityName}/{id:guid}/{detailEntityName}/{detailId:guid}")]
        [ProducesResponseType (200)]
        [ProducesResponseType (500)]
        [ProducesResponseType (404)]
        public IActionResult DetailEntity_UpdateResult ([FromRoute] string entityName, Guid id, string detailEntityName, Guid detailId, [FromQuery] string subType, [FromBody] JObject value) {
            try {
                var stopwatch = StopwatchLogger.Start (_log);
                _log.Info ("Called EntityController DetailEntity_UpdateResult");

                if (value == null) return BadRequest ("Value required");
                if (string.IsNullOrEmpty (subType)) return BadRequest ("sub type required");
                var result = _iEntityResourceManager.UpdateResult (TenantCode, UserId, detailId, detailEntityName, value, subType);
                stopwatch.StopAndLog ("DetailEntity_UpdateResult of EntityController");
                return Ok (result);
            } catch (FieldAccessException ex) {
                _log.Error (ExceptionFormatter.SerializeToString (ex));
                return StatusCode ((int) HttpStatusCode.BadRequest, ApiConstant.CustomErrorMessage);
            } catch (Exception ex) {
                //return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
                _log.Error (ExceptionFormatter.SerializeToString (ex));
                return StatusCode ((int) HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
            }
        }

        private DataTable SetEntityNamebyCode (string entityname, DataTable resultdt) {
            var targetentity = new List<string> (new string[] { "emailtemplate", "smstemplate" });
            if (targetentity.Contains (entityname.ToLower ())) {
                if (resultdt.Columns.Contains ("Context")) {
                    int x = 0;
                    foreach (DataRow myRow in resultdt.Rows) {
                        myRow["Context"] = _iMetadataManager.GetEntityNameByEntityContext (Convert.ToString (myRow["Context"]), false); //changes the ContextType;;
                        x++;
                    }
                }
            }
            return resultdt;
        }
    }
}