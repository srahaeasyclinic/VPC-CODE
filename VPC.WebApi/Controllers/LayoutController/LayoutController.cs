using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NLog;
using VPC.Entities.EntityCore.Model.Storage;
using VPC.Framework.Business.MetadataManager.Contracts;
using VPC.WebApi.Utility;

namespace VPC.WebApi.Controllers.LayoutController {
    [Route ("api/meta-data")]
    public class LayoutController : BaseApiController {
        private readonly Logger _log = LogManager.GetCurrentClassLogger ();
        private readonly ILayoutManager _iLayoutManager;
        private readonly IJsonMessage _iJsonMessage;
        private readonly IMetadataManager _iMetadataManager;

        public LayoutController (ILayoutManager iLayoutManager, IJsonMessage iJsonMessage) {
            _iLayoutManager = iLayoutManager;
            _iJsonMessage = iJsonMessage;
            _iMetadataManager = new MetadataManager ();
        }

        [HttpGet]
        [Route ("{entityName}/layouts")]
        [ProducesResponseType (200, Type = typeof (List<LayoutModel>))]
        [ProducesResponseType (500)]
        [ProducesResponseType (404)]
        public IActionResult GetLayouts (string entityName, [FromQuery] string type) {
            try {
                var stopwatch = StopwatchLogger.Start (_log);
                _log.Info ("Called LayoutController GetLayouts");

                //var layout = new List<LayoutModel>();

                List<LayoutModel> layout = new List<LayoutModel> ();

                if (string.IsNullOrEmpty (type)) {
                    layout = _iLayoutManager.GetLayoutsByEntityName (TenantCode, entityName);
                } else {
                    try {
                        int entitytype = _iMetadataManager.GetTypeId (type);
                        layout = _iLayoutManager.GetLayoutsByEntityName (TenantCode, _iMetadataManager.GetEntityContextByEntityName (entityName, false), entitytype, false);
                    } catch (Exception ex) {
                        _log.Error (ExceptionFormatter.SerializeToString (ex));
                        return BadRequest ("Incorrect Type.");
                    }

                }
                stopwatch.StopAndLog ("GetLayouts of LayoutController");
                if (layout.Any ()) {
                    var settings = new JsonSerializerSettings ();
                    settings.NullValueHandling = NullValueHandling.Ignore;
                    settings.ContractResolver = new CamelCasePropertyNamesContractResolver ();
                    return Json (layout, settings);
                } else {
                    var settings = new JsonSerializerSettings ();
                    settings.NullValueHandling = NullValueHandling.Ignore;
                    settings.ContractResolver = new CamelCasePropertyNamesContractResolver ();
                    return Json (layout, settings);
                }

                // return NotFound("Layout not found");
            } catch (Exception ex) {
                _log.Error (ExceptionFormatter.SerializeToString (ex));
                return StatusCode ((int) HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
            }
        }

        [HttpGet]
        [Route ("{entityName}/layouts/{id:guid}")]
        [ProducesResponseType (200, Type = typeof (LayoutModel))]
        [ProducesResponseType (500)]
        [ProducesResponseType (404)]
        public IActionResult GetLayoutsDetailsById (Guid id) {
            try {
                var stopwatch = StopwatchLogger.Start (_log);
                _log.Info ("Called LayoutController GetLayoutsDetailsById");

                var layout = _iLayoutManager.GetLayoutsDetailsById (TenantCode, id);

                stopwatch.StopAndLog ("GetLayoutsDetailsById of LayoutController");

                if (layout != null) {
                    return _iJsonMessage.IgnoreNullableObject (layout);
                }

                return NotFound ("Layout not found");
            } catch (Exception ex) {
                _log.Error (ExceptionFormatter.SerializeToString (ex));
                return StatusCode ((int) HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
            }
        }

        [HttpGet]
        [Route ("{entityName}/layouts/default")]
        [ProducesResponseType (200, Type = typeof (LayoutModel))]
        [ProducesResponseType (500)]
        [ProducesResponseType (404)]
        public IActionResult GetDefaultLayout (string entityName, [FromQuery] string type, string subtype, string context) {
            try {
                var stopwatch = StopwatchLogger.Start (_log);
                _log.Info ("Called LayoutController GetLayoutsDetailsById");

                int entitytype;
                var contextType = 0;
                string errormessage = "";

                if (string.IsNullOrEmpty (type)) {
                    return BadRequest ("Type required.");
                } else {
                    try {
                        entitytype = _iMetadataManager.GetTypeId (type);
                    } catch (Exception ex) {
                        _log.Error (ExceptionFormatter.SerializeToString (ex));
                        return BadRequest ("Incorrect Type.");
                    }
                }

                if (entitytype == (int) LayoutType.Form) {

                    try {
                        contextType = _iMetadataManager.GetContextId (context);
                        if (contextType != (int) LayoutContext.QuickAdd) {
                            if (string.IsNullOrEmpty (context)) {
                                errormessage += " Context required.";
                            }
                            if (string.IsNullOrEmpty (subtype)) {
                                errormessage += "Sub Type required.";
                            }
                        } 
                    } catch (Exception ex) {
                        _log.Error (ExceptionFormatter.SerializeToString (ex));
                        return BadRequest ("Incorrect Context.");
                    }

                    // if (string.IsNullOrEmpty (subtype)) {
                    //     errormessage += "Sub Type required.";
                    // }
                    // else
                    // {
                    //     if(entityName == "user")
                    //         subtype = "Employee";
                    // }

                    // if (string.IsNullOrEmpty (context)) {
                    //     errormessage += " Context required.";
                    // } else {
                    //     try {
                    //         contextType = _iMetadataManager.GetContextId (context);
                    //     } catch (Exception ex) {
                    //         _log.Error (ExceptionFormatter.SerializeToString (ex));
                    //         return BadRequest ("Incorrect Context.");
                    //     }
                    // }

                    if (errormessage != "") {
                        return BadRequest (errormessage);
                    }
                }

                var layout = _iLayoutManager.GetDefaultLayoutForEntity (TenantCode, entityName, entitytype, subtype, contextType);
                //get all task configuration with entity name

                stopwatch.StopAndLog ("GetLayoutsDetailsById of LayoutController");

                if (layout != null) {
                    return _iJsonMessage.IgnoreNullableObject (layout);
                }
                return NotFound ("There is a configuration error. Default layout is missing for " + ((subtype != null) ? subtype + "." : entityName + "."));
            } catch (Exception ex) {
                _log.Error (ExceptionFormatter.SerializeToString (ex));
                return StatusCode ((int) HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
            }
        }

        [HttpPost]
        [Route ("{entityName}/layouts")]
        [ProducesResponseType (200)]
        [ProducesResponseType (500)]
        [ProducesResponseType (404)]
        public IActionResult SaveLayout (string entityName, [FromBody] LayoutModel layoutModel) {
            try {
                var stopwatch = StopwatchLogger.Start (_log);
                _log.Info ("Called LayoutController SaveLayout");

                var retVal = _iLayoutManager.Create (entityName, layoutModel, UserId, TenantCode);

                stopwatch.StopAndLog ("SaveLayout method of LayoutController");
                return Ok (retVal);
            } catch (Exception ex) {
                _log.Error (ExceptionFormatter.SerializeToString (ex));
                return StatusCode ((int) HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
            }
        }

        [HttpPatch]
        [Route ("{entityName}/layouts")]
        public IActionResult SetListLayoutDefault (string entityName, [FromBody] LayoutModel layoutModel) {
            try {
                var stopwatch = StopwatchLogger.Start (_log);
                _log.Info ("Called LayoutController SetListLayoutDefault");

                _iLayoutManager.SetListLayoutDefault (entityName, layoutModel, UserId, TenantCode);

                stopwatch.StopAndLog ("SetListLayoutDefault method of LayoutController");
                return Ok (true);
            } catch (Exception ex) {
                _log.Error (ExceptionFormatter.SerializeToString (ex));
                return StatusCode ((int) HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
            }
        }

        [HttpPut]
        [Route ("{entityName}/layouts/{id:guid}")]
        [ProducesResponseType (200, Type = typeof (LayoutModel))]
        [ProducesResponseType (500)]
        [ProducesResponseType (404)]
        public IActionResult UpdateLayoutDetails (string entityName, Guid id, [FromBody] LayoutModel templateModel) {
            try {
                var stopwatch = StopwatchLogger.Start (_log);
                _log.Info ("Called LayoutController UpdateLayoutDetails");

                if (templateModel == null) {
                    return BadRequest ("Layout required");
                }

                if (!Enum.IsDefined (typeof (LayoutType), templateModel.LayoutType)) {
                    return BadRequest ("Layout type required");
                }

                templateModel.ModifiedBy = UserId;

                if (templateModel.LayoutType == LayoutType.List) {
                    templateModel.Layout = JsonConvert.SerializeObject (templateModel.ListLayoutDetails, new JsonSerializerSettings {
                        ContractResolver = new CamelCasePropertyNamesContractResolver ()
                    });
                } else if (templateModel.LayoutType == LayoutType.Form) {
                    templateModel.Layout = JsonConvert.SerializeObject (templateModel.FormLayoutDetails, new JsonSerializerSettings {
                        ContractResolver = new CamelCasePropertyNamesContractResolver ()
                    });
                } else if (templateModel.LayoutType == LayoutType.View) {
                    templateModel.Layout = JsonConvert.SerializeObject (templateModel.ViewLayoutDetails, new JsonSerializerSettings {
                        ContractResolver = new CamelCasePropertyNamesContractResolver ()
                    });
                }

                _iLayoutManager.UpdateLayoutDetails (TenantCode, id, templateModel);

                stopwatch.StopAndLog ("UpdateLayoutDetails method of LayoutController");
                return Ok (HttpStatusCode.OK);
            } catch (Exception ex) {
                _log.Error (ExceptionFormatter.SerializeToString (ex));
                return StatusCode ((int) HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
            }
        }

        [HttpDelete]
        [Route ("{entityName}/layouts/{id:guid}")]
        [ProducesResponseType (200)]
        [ProducesResponseType (500)]
        [ProducesResponseType (404)]
        public IActionResult DeleteListLayout ([FromRoute] string entityName, Guid id) {
            try {
                var stopwatch = StopwatchLogger.Start (_log);
                _log.Info ("Called LayoutController DeleteListLayout with listlayoutId {0}=", id);

                if (id == Guid.Empty)
                    return BadRequest ("Layout id required !");

                _iLayoutManager.DeleteListLayout (TenantCode, id);
                stopwatch.StopAndLog ("DeleteListLayout method of LayoutController");
                return Ok (true);
            } catch (Exception ex) {
                _log.Error (ExceptionFormatter.SerializeToString (ex));
                return StatusCode ((int) HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
            }
        }
    
        [HttpPost]
        [Route ("{entityName}/layouts/{id:guid}/clone")]
        [ProducesResponseType (200)]
        [ProducesResponseType (500)]
        [ProducesResponseType (404)]
        public IActionResult CloneLayout ([FromRoute] string entityName, [FromRoute] Guid id, [FromBody] LayoutModel layoutModel) {
            try {
                var stopwatch = StopwatchLogger.Start (_log);
                _log.Info ("Called LayoutController CloneLayout");

                var retVal = _iLayoutManager.CloneLayout(entityName, id, layoutModel, UserId, TenantCode);

                stopwatch.StopAndLog ("CloneLayout method of LayoutController");
                return Ok (retVal);
            } catch (Exception ex) {
                _log.Error (ExceptionFormatter.SerializeToString (ex));
                return StatusCode ((int) HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
            }
        }
    }
}