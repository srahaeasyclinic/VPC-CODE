using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using NLog;
using VPC.Entities.EntityCore.Model.PickListItem;
using VPC.Entities.EntityCore.Model.Query;
using VPC.Entities.EntityCore.Model.Storage;
using VPC.Framework.Business.DynamicQueryManager.Core.Enums;
using VPC.Framework.Business.MetadataManager.Contracts;
using VPC.WebApi.AttributesHandler;
using VPC.WebApi.Utility;

namespace VPC.WebApi.Controllers.PicklistItem {
    [Route ("api/picklists")]
    [ProducesResponseType (200, Type = typeof (PicklistObject))]
    [ProducesResponseType (500)]
    public class PicklistController : BaseApiController {
        private readonly Logger _log = LogManager.GetCurrentClassLogger ();
        private readonly IPicklistManager _picklistManager;
        private readonly IJsonMessage _iJsonMessage;
        private readonly ILayoutManager _iLayoutManager;
        private readonly IMetadataManager _iMetadataManager;

        public PicklistController (IPicklistManager picklistManager, IJsonMessage iJsonMessage, ILayoutManager iLayoutManager, IMetadataManager iMetadataManager) {
            _picklistManager = picklistManager;
            _iJsonMessage = iJsonMessage;
            _iLayoutManager = iLayoutManager;
            _iMetadataManager = iMetadataManager;
        }

        #region Pick list meta-data

        [HttpGet]
        [Route ("")]
        [ProducesResponseType (200, Type = typeof (List<PicklistObject>))]
        [ProducesResponseType (500)]
        [ProducesResponseType (404)]
        public IActionResult GetAllPicklists () {
            try {
                var stopwatch = StopwatchLogger.Start (_log);
                _log.Info ("Called PicklistController GetAllPicklists ");
                var retVal = _picklistManager.GetAllPicklists (TenantCode).OrderBy (x => x.Name);
                stopwatch.StopAndLog ("GetAllPicklists method of PicklistController.");
                return Ok (retVal);
            } catch (Exception ex) {
                _log.Error (ExceptionFormatter.SerializeToString (ex));
                return StatusCode ((int) HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
            }
        }

        [HttpGet]
        [Route ("{name}/values")]
        [ProducesResponseType (200, Type = typeof (PicklistObject))]
        [ProducesResponseType (500)]
        [ProducesResponseType (404)]
        public IActionResult GetPickListValueByPicklistName ([FromRoute] string name, [FromQuery] string fields, string searchText, string orderBy, string filters, int pageIndex = 1, int pageSize = 100) {
            try {
                var stopwatch = StopwatchLogger.Start (_log);
                _log.Info ("Called PicklistController GetPickListValueByPicklistName");
                _log.Info ("Called PicklistController GetPickListValueByPicklistName with fields {0}=", fields);
                _log.Info ("Called PicklistController GetPickListValueByPicklistName with name {0}=", name);
                _log.Info ("Called PicklistController GetPickListValueByPicklistName with searchText {0}=", searchText);
                _log.Info ("Called PicklistController GetPickListValueByPicklistName with orderBy {0}=", orderBy);
                _log.Info ("Called PicklistController GetPickListValueByPicklistName with filters {0}=", filters);
                _log.Info ("Called PicklistController GetPickListValueByPicklistName with pageIndex {0}=", pageIndex);
                _log.Info ("Called PicklistController GetPickListValueByPicklistName with pageSize {0}=", pageSize);

                var queryFilter = ApiHelper.GetQueryFilter (filters, TenantCode, IsSystemAdmin, name);
                var queryContext = new QueryContext { Fields = fields, Filters = queryFilter, PageSize = pageSize, PageIndex = pageIndex, OrderBy = orderBy };

                var defaultLayout = (!string.IsNullOrEmpty (fields)) ? _iLayoutManager.GetDefaultPicklistLayout (TenantCode, name, LayoutType.List, 0) : _iLayoutManager.GetDefaultPicklistLayout (TenantCode, name, LayoutType.View, 0);

                if (!string.IsNullOrEmpty (searchText)) {

                    var freeTextSearch = defaultLayout?.ListLayoutDetails?.SearchProperties?.FirstOrDefault (t => t.Name.Equals ("FreeTextSearch"));
                    if (freeTextSearch?.Properties != null && freeTextSearch.Properties.Any ()) {
                        queryContext.FreeTextSearch = new List<QueryFilter> ();
                        foreach (var prop in freeTextSearch.Properties) {
                            var filter = new QueryFilter {
                                FieldName = prop.Name,
                                Operator = Comparison.Like.ToString (),
                                Value = searchText
                            };
                            queryContext.FreeTextSearch.Add (filter);
                        }
                    }
                }

                var result = _picklistManager.GetPicklistValues (TenantCode, name, queryContext);
                if (result != null && result.Rows.Count > 0) {
                    dynamic totalRow = (result.Columns.Contains ("totalRow")) ? result.Rows[0]["totalRow"] : result.Rows.Count;
                    ApiHelper.MapDynamicColumn (result.Columns);
                    return Ok (new { result, totalRow });
                }
                stopwatch.StopAndLog ("GetPickListValueByPicklistName method of PicklistController.");
                return Ok (new { result = new List<string> (), totalRow = 0 });

            } catch (Exception ex) {
                _log.Error (ExceptionFormatter.SerializeToString (ex));
                return StatusCode ((int) HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
            }
        }

        [HttpGet]
        [Route ("{name}/values/{id:guid}")]
        [ProducesResponseType (200, Type = typeof (PicklistObject))]
        [ProducesResponseType (500)]
        [ProducesResponseType (404)]
        public IActionResult GetPicklistValueById ([FromRoute] string name, Guid id) {
            try {
                var stopwatch = StopwatchLogger.Start (_log);
                _log.Info ("Called PicklistController GetPicklistValueById");
                _log.Info ("Called PicklistController GetPicklistValueById with name {0}=", name);
                _log.Info ("Called PicklistController GetPickListValueByPiGetPicklistValueByIdcklistName with id {0}=", id.ToString ());
                if (string.IsNullOrEmpty (name)) {
                    return BadRequest ("Entity name required!");
                }
                if (id == Guid.Empty) {
                    return BadRequest ("Id required!");
                }
                var result = _picklistManager.GetPicklistValueDetails (TenantCode, name, id, LayoutType.Form, LayoutContext.Edit);
                if (result != null && result.Rows.Count > 0) {
                    ApiHelper.MapDynamicColumn (result.Columns);
                    var json = new JObject (result.Columns.Cast<DataColumn> ().Select (c => new JProperty (c.ColumnName, JToken.FromObject (result.Rows[0][c]))));
                    return Ok (json);
                }
                stopwatch.StopAndLog ("GetPicklistValueById method of PicklistController.");
                return NotFound ("Data not found");
            } catch (Exception ex) {
                _log.Error (ExceptionFormatter.SerializeToString (ex));
                return StatusCode ((int) HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
            }
        }

        [HttpPost]
        [Route ("{name}/values")]
        [ProducesResponseType (200)]
        [ProducesResponseType (500)]
        [ProducesResponseType (404)]
        [ValidatePicklist]
        public IActionResult SavePicklistValue ([FromRoute] string name, [FromQuery] string subType, [FromBody] JObject value) {
            try {
                var stopwatch = StopwatchLogger.Start (_log);
                _log.Info ("Called PicklistController SavePicklistValue");
                _log.Info ("Called PicklistController SavePicklistValue with value {0}=", JsonConvert.SerializeObject (value));
                _log.Info ("Called PicklistController SavePicklistValue with name {0}=", name);
                _log.Info ("Called PicklistController SavePicklistValue with subType {0}=", subType);
                var result = _picklistManager.SavePicklistValue (TenantCode, UserId, name, value);
                stopwatch.StopAndLog ("SavePicklistValue method of PicklistController.");
                return Ok (result);
            } catch (FieldAccessException ex) {
                _log.Error (ExceptionFormatter.SerializeToString (ex));
                return BadRequest (ex.Message);
            } catch (Exception ex) {
                _log.Error (ExceptionFormatter.SerializeToString (ex));
                return StatusCode ((int) HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
            }
        }

        [HttpPut]
        [Route ("{name}/values/{id:guid}")]
        [ProducesResponseType (200, Type = typeof (PicklistObject))]
        [ProducesResponseType (500)]
        [ProducesResponseType (404)]
        [ValidatePicklist]
        public IActionResult UpdatePicklistValue ([FromRoute] string name, Guid id, [FromBody] JObject value) {
            try {
                var stopwatch = StopwatchLogger.Start (_log);
                _log.Info ("Called PicklistController UpdatePicklistValue");
                _log.Info ("Called PicklistController UpdatePicklistValue with value {0}=", JsonConvert.SerializeObject (value));
                _log.Info ("Called PicklistController UpdatePicklistValue with name {0}=", name);
                _log.Info ("Called PicklistController UpdatePicklistValue with fields {0}=", id);

                if (string.IsNullOrEmpty (name)) {
                    return BadRequest ("Entity name required!");
                }
                if (id == Guid.Empty) {
                    return BadRequest ("Id required!");
                }

                var result = _picklistManager.UpdatePicklistValueDetails (TenantCode, name, id, value);

                stopwatch.StopAndLog ("UpdatePicklistValue method of PicklistController.");
                return Ok (result);
            } catch (Exception ex) {
                _log.Error (ExceptionFormatter.SerializeToString (ex));
                return StatusCode ((int) HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
            }
        }

        [HttpDelete]
        [Route ("{name}/values/{id:guid}")]
        [ProducesResponseType (200)]
        [ProducesResponseType (500)]
        [ProducesResponseType (404)]
        public IActionResult DeletePickListValue ([FromRoute] string name, Guid id) {
            try {
                var stopwatch = StopwatchLogger.Start (_log);
                _log.Info ("Called PicklistController DeletePickListValue");
                _log.Info ("Called PicklistController DeletePickListValue with fields {0}=", id);
                _log.Info ("Called PicklistController UpdatePicklistValue with name {0}=", name);

                if (string.IsNullOrEmpty (name)) {
                    return BadRequest ("Entity name required!");
                }
                if (id == Guid.Empty) {
                    return BadRequest ("Id required!");
                }

                var result = _picklistManager.DeletePickListValueById (TenantCode, name, id);
                stopwatch.StopAndLog ("DeletePickListValue method of PicklistController.");
                return Ok (result);
            } catch (Exception ex) {
                _log.Error (ExceptionFormatter.SerializeToString (ex));
                return StatusCode ((int) HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
            }
        }
        #endregion

        #region Picklist layout

        [HttpGet]
        [Route ("{name}/layouts")]
        [ProducesResponseType (200, Type = typeof (List<LayoutModel>))]
        [ProducesResponseType (500)]
        [ProducesResponseType (404)]
        public IActionResult GetPickListLayoutByPicklistName ([FromRoute] string name, [FromQuery] string type) {
            try {
                var stopwatch = StopwatchLogger.Start (_log);
                _log.Info ("Called PicklistController GetPickListLayoutByPicklistName");
                _log.Info ("Called PicklistController UpdatePicklistValue with name {0}=", name);

                if (string.IsNullOrEmpty (name)) {
                    return BadRequest ("Entity name required!");
                }

                if (string.IsNullOrEmpty (type)) {
                    var result = _iLayoutManager.GetLayoutsByPicklistName (TenantCode, name);
                    stopwatch.StopAndLog ("GetPickListLayoutByPicklistName method of PicklistController.");
                    return Ok (result);
                } else {
                    var result = _iLayoutManager.GetLayoutsByEntityName (TenantCode, _iMetadataManager.GetEntityContextByEntityName (name, true), _iMetadataManager.GetTypeId (type), true);
                    stopwatch.StopAndLog ("GetPickListLayoutByPicklistName method of PicklistController.");
                    return Ok (result);
                }

            } catch (Exception ex) {
                _log.Error (ExceptionFormatter.SerializeToString (ex));
                return StatusCode ((int) HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
            }
        }

        [HttpPost]
        [Route ("{name}/layouts")]
        public IActionResult SavePickListLayout ([FromRoute] string name, [FromBody] LayoutModel layoutModel) {
            try {
                var stopwatch = StopwatchLogger.Start (_log);
                _log.Info ("Called PicklistController SavePickListLayout");
                _log.Info ("Called PicklistController GetPickListValueByPicklistName with LayoutModel {0}=", JsonConvert.SerializeObject (layoutModel));
                _log.Info ("Called PicklistController UpdatePicklistValue with name {0}=", name);

                if (string.IsNullOrEmpty (name)) {
                    return BadRequest ("Entity name required!");
                }

                if (layoutModel == null) {
                    return BadRequest ("Layout detail required!");
                }

                var retVal = _iLayoutManager.CreatePicklistLayout (name, layoutModel, UserId, TenantCode);
                stopwatch.StopAndLog ("SavePickListLayout method of PicklistController.");
                return Ok (retVal);

            } catch (Exception ex) {
                _log.Error (ExceptionFormatter.SerializeToString (ex));
                return StatusCode ((int) HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
            }
        }

        [HttpPut]
        [ProducesResponseType (200)]
        [ProducesResponseType (500)]
        [ProducesResponseType (404)]
        [Route ("{name}/layouts/{id:guid}")]
        public IActionResult UpdatePicklistLayout ([FromRoute] string name, [FromRoute] Guid id, [FromBody] LayoutModel layout) {
            try {
                var stopwatch = StopwatchLogger.Start (_log);
                _log.Info ("Called PicklistController UpdatePicklistLayout with LayoutModel {0}=", JsonConvert.SerializeObject (layout));
                _log.Info ("Called PicklistController UpdatePicklistLayout with name {0}=", name);
                _log.Info ("Called PicklistController UpdatePicklistLayout with id {0}=", id);

                #region API validation
                if (string.IsNullOrEmpty (name)) {
                    return BadRequest ("Picklist name required");
                }
                if (id == Guid.Empty) {
                    return BadRequest ("Picklist id required");
                }
                if (layout == null) {
                    return BadRequest ("Layout required");
                }

                if (!Enum.IsDefined (typeof (LayoutType), layout.LayoutType)) {
                    return BadRequest ("Layout type required");
                }
                #endregion

                layout.ModifiedBy = UserId;

                if (layout.LayoutType == LayoutType.List) {
                    layout.Layout = JsonConvert.SerializeObject (layout.ListLayoutDetails, new JsonSerializerSettings {
                        ContractResolver = new CamelCasePropertyNamesContractResolver ()
                    });
                } else if (layout.LayoutType == LayoutType.Form) {
                    layout.Layout = JsonConvert.SerializeObject (layout.FormLayoutDetails, new JsonSerializerSettings {
                        ContractResolver = new CamelCasePropertyNamesContractResolver ()
                    });
                } else if (layout.LayoutType == LayoutType.View) {
                    layout.Layout = JsonConvert.SerializeObject (layout.ViewLayoutDetails, new JsonSerializerSettings {
                        ContractResolver = new CamelCasePropertyNamesContractResolver ()
                    });
                }

                _iLayoutManager.UpdatePicklistLayout (TenantCode, id, layout);

                stopwatch.StopAndLog ("UpdatePicklistLayout method of PicklistController.");
                return Ok (HttpStatusCode.OK);
            } catch (Exception ex) {
                _log.Error (ExceptionFormatter.SerializeToString (ex));
                return StatusCode ((int) HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
            }
        }

        [HttpPatch]
        [Route ("{name}/layouts")]
        public IActionResult SetPickListLayoutDefault ([FromRoute] string name, [FromBody] LayoutModel layout) {
            try {
                var stopwatch = StopwatchLogger.Start (_log);
                _log.Info ("Called PicklistController SetPickListLayoutDefault");
                _log.Info ("Called PicklistController SetPickListLayoutDefault with LayoutModel {0}=", JsonConvert.SerializeObject (layout));
                _log.Info ("Called PicklistController SetPickListLayoutDefault with name {0}=", name);

                if (string.IsNullOrEmpty (name)) {
                    return BadRequest ("Picklist name required");
                }
                if (layout == null) {
                    return BadRequest ("Layout required");
                }

                _iLayoutManager.SetPicklistLayoutDefault (name, layout, UserId, TenantCode);
                stopwatch.StopAndLog ("SetPickListLayoutDefault method of PicklistController.");
                return Ok (true);

            } catch (Exception ex) {
                _log.Error (ExceptionFormatter.SerializeToString (ex));
                return StatusCode ((int) HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
            }
        }

        [HttpDelete]
        [Route ("{name}/layouts/{id:guid}")]
        [ProducesResponseType (200)]
        [ProducesResponseType (500)]
        public IActionResult DeletePicklistLayout ([FromRoute] Guid id) {
            try {
                var stopwatch = StopwatchLogger.Start (_log);
                _log.Info ("Called PicklistController DeletePicklistLayout with picklistId {0}=", id);

                if (id == Guid.Empty)
                    return BadRequest ("Picklist id required !");

                _iLayoutManager.DeletePicklistLayout (TenantCode, id);
                stopwatch.StopAndLog ("DeletePicklistLayout method of PicklistController");
                return Ok (true);
            } catch (Exception ex) {
                _log.Error (ExceptionFormatter.SerializeToString (ex));
                return StatusCode ((int) HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
            }
        }

        [HttpGet]
        [Route ("layouts/{id:guid}")]
        public IActionResult GetLayoutsDetailsById (Guid id) {
            try {
                var stopwatch = StopwatchLogger.Start (_log);
                _log.Info ("Called PicklistController GetLayoutsDetailsById");

                if (id == Guid.Empty)
                    return BadRequest ("Picklist id required !");

                var layout = _iLayoutManager.GetPicklistLayoutDetailsById (TenantCode, id);

                stopwatch.StopAndLog ("GetLayoutsDetailsById of PicklistController");
                if (layout != null) {
                    return _iJsonMessage.IgnoreNullableObject (layout);
                }

                return NotFound ("No default layout found.");
            } catch (Exception ex) {
                _log.Error (ExceptionFormatter.SerializeToString (ex));
                return StatusCode ((int) HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
            }
        }

        [HttpGet]
        [ProducesResponseType (200, Type = typeof (LayoutModel))]
        [ProducesResponseType (500)]
        [ProducesResponseType (404)]
        [Route ("{name}/layouts/types/{type:int}/contexts/{context:int}")]
        public IActionResult GetDefaultLayout (string name, int type, int context) {

            try {
                var stopwatch = StopwatchLogger.Start (_log);
                _log.Info ("Called PicklistController GetDefaultLayout with type {0}=", type);
                _log.Info ("Called PicklistController GetDefaultLayout with name {0}=", name);
                _log.Info ("Called PicklistController GetDefaultLayout with context {0}=", context);

                if (string.IsNullOrEmpty (name)) {
                    return BadRequest ("Entity name required !");
                }

                if (type == 0) {
                    return BadRequest ("Layout type required !");
                }

                var layout = _iLayoutManager.GetDefaultPicklistLayout (TenantCode, name, (LayoutType) type, context);

                stopwatch.StopAndLog ("GetDefaultLayout of PicklistController");
                if (layout != null) {
                    return _iJsonMessage.IgnoreNullableObject (layout);
                }

                return NotFound ("No default layout found.");

            } catch (Exception ex) {
                _log.Error (ExceptionFormatter.SerializeToString (ex));
                return StatusCode ((int) HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
            }
        }

        #endregion

        #region Private helper functions

        //private List<QueryFilter> GetQueryFilter(string data)
        //{
        //    var queryFilters = new List<QueryFilter>();
        //    if (string.IsNullOrEmpty(data)) return queryFilters;
        //    string[] filters = data.Split('|');
        //    if (filters != null && filters.Any())
        //    {
        //        for (int i = 0; i < filters.Length; i++)
        //        {
        //            string[] filterdata = filters[i].Split(',');
        //            if (filterdata != null && filterdata.Any() && filterdata.Count() == 2)
        //            {
        //                var queryFilter = new QueryFilter
        //                {
        //                    FieldName = filterdata[0],
        //                    Operator = "Equal",
        //                    Value = filterdata[1]
        //                };
        //                queryFilters.Add(queryFilter);
        //            }
        //        }
        //    }
        //    return queryFilters;
        //}

        #endregion
    }
}