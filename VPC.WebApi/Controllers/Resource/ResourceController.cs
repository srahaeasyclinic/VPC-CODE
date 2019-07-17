using System;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NLog;
using VPC.Framework.Business.MetadataManager.Contracts;
using VPC.Framework.Business.ResourceManager.Contracts;
using VPC.WebApi.Utility;

namespace VPC.WebApi.Controllers.Resource {
    [Route ("api/resources")]
    public class ResourceController : BaseApiController {

        private readonly Logger _log = LogManager.GetCurrentClassLogger ();
        private readonly IResourceManager _resourceManager;
        private readonly ILayoutManager _iILayoutManager;

        public ResourceController (IResourceManager resourceManager, ILayoutManager layoutManager) {
            _resourceManager = resourceManager;
            _iILayoutManager = layoutManager;
        }

        [HttpGet]
        [Route ("")]
        [ApiExplorerSettings (IgnoreApi = true)]
        [NonAction]
        public IActionResult GetAllResources () {
            try {

                var stopwatch = StopwatchLogger.Start (_log);
                _log.Info ("Called ResourceController GetAllResources");

                var retVal = _resourceManager.GetResources (TenantCode).ToDictionary (x => x.Key, x => x.Value);

                stopwatch.StopAndLog ("GetAllResources of ResourceController");
                return this.Ok (retVal);
            } catch (Exception ex) {
                _log.Error (ExceptionFormatter.SerializeToString (ex));
                return StatusCode ((int) HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
            }
        }

        [HttpGet]
        [Route ("")]
        public IActionResult GetAll ([FromQuery] int pageIndex, int pageSize, string orderBy, string language = null) {
            try {

                var stopwatch = StopwatchLogger.Start (_log);
                _log.Info ("Called ResourceController GetAllResources");

                int totalRowCount = 0;
                var resources = _resourceManager.GetResourcesDetails (language, TenantCode, pageIndex, pageSize, orderBy, ref totalRowCount);
                var totalRow = totalRowCount;
                stopwatch.StopAndLog ("End ResourceController GetAllResources");
                return Ok (new { resources, totalRow });

            } catch (Exception ex) {
                _log.Error (ExceptionFormatter.SerializeToString (ex));
                return StatusCode ((int) HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
            }
        }

        [HttpGet]
        [Route ("all")]
        [CustomJsonFormatter ("pascal")]
        public IActionResult GetAll ([FromQuery] string language) {
            try {
                var stopwatch = StopwatchLogger.Start (_log);
                _log.Info ("Called ResourceController GetAllResources");

                var retVal = _resourceManager.GetResources (TenantCode, language).ToDictionary (x => x.Key, x => x.Value);
                stopwatch.StopAndLog ("GetAllResources of ResourceController");
                return this.Ok (retVal);
            } catch (Exception ex) {
                _log.Error (ExceptionFormatter.SerializeToString (ex));
                return StatusCode ((int) HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
            }
        }

        [HttpGet]
        [Route ("all/{key}/{language}")]
        public IActionResult GetAllByKeyLanguage (string key, string language) {
            try {
                var stopwatch = StopwatchLogger.Start (_log);
                _log.Info ("Called ResourceController GetAllResources");

                var retVal = _resourceManager.GetResourcesByKeyAndLanguage (TenantCode, key, language);
                stopwatch.StopAndLog ("GetAllResourcesByKeyAndLanguage of ResourceController");
                return this.Ok (retVal);
            } catch (Exception ex) {
                _log.Error (ExceptionFormatter.SerializeToString (ex));
                return StatusCode ((int) HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
            }
        }

        [HttpGet]
        [Route ("{key}/{language}")]
        public IActionResult GetDuplicateResourceKey (string key, string language) {
            try {
                var stopwatch = StopwatchLogger.Start (_log);
                _log.Info ("Called ResourceController GetDuplicateResourceKey");

                var retVal = _resourceManager.GetDuplicateResourceKey (TenantCode, key, language);
                stopwatch.StopAndLog ("GetDuplicateResourceKey of ResourceController");
                return this.Ok (retVal);
            } catch (Exception ex) {
                _log.Error (ExceptionFormatter.SerializeToString (ex));
                return StatusCode ((int) HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
            }
        }

        [HttpPost]
        [Route ("")]
        public IActionResult Post ([FromBody] VPC.Entities.EntityCore.Model.Resource.Resource resource) {
            try {
                if (resource == null)
                    return BadRequest ("Invalid parameter");
                if (string.IsNullOrEmpty (resource.Key))
                    return BadRequest ("Invalid parameter");
                if (string.IsNullOrEmpty (resource.Value))
                    return BadRequest ("Invalid parameter");

                var stopwatch = StopwatchLogger.Start (_log);
                _log.Info ("Called ResourceController Post {0}=", JsonConvert.SerializeObject (resource));

                string strMsg = string.Empty;

                var retVal = _resourceManager.Create (TenantCode, resource, ref strMsg);

                if (!String.IsNullOrEmpty (strMsg) && retVal == false) {
                    return StatusCode ((int) HttpStatusCode.AlreadyReported, strMsg);
                }

                stopwatch.StopAndLog ("End ResourceController Post");
                return Ok (retVal);
            } catch (Exception ex) {
                _log.Error (ExceptionFormatter.SerializeToString (ex));
                return StatusCode ((int) HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
            }
        }

        [HttpPut]
        [Route ("{resourceId}")]
        public IActionResult Put (Guid resourceId, [FromBody] VPC.Entities.EntityCore.Model.Resource.Resource resource) {
            try {
                var stopwatch = StopwatchLogger.Start (_log);
                _log.Info ("Called ResourceController Put {0}=", JsonConvert.SerializeObject (resource));

                string strMsg = string.Empty;

                var retVal = _resourceManager.Update (resourceId, TenantCode, resource, ref strMsg);

                if (!String.IsNullOrEmpty (strMsg) && retVal == false) {
                    return StatusCode ((int) HttpStatusCode.AlreadyReported, strMsg);
                }

                stopwatch.StopAndLog ("End ResourceController Put");
                return Ok (retVal);
            } catch (Exception ex) {
                _log.Error (ExceptionFormatter.SerializeToString (ex));
                return StatusCode ((int) HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
            }
        }

        [HttpDelete]
        [Route ("{resourceKey}")]
        public IActionResult Delete (string resourceKey) {
            try {
                var stopwatch = StopwatchLogger.Start (_log);
                _log.Info ("Called ResourceController Delete {0}=", JsonConvert.SerializeObject (resourceKey));

                var retVal = _resourceManager.Delete (TenantCode, resourceKey);
                stopwatch.StopAndLog ("End ResourceController Delete");
                return Ok (retVal);
            } catch (Exception ex) {
                _log.Error (ExceptionFormatter.SerializeToString (ex));
                return StatusCode ((int) HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
            }
        }


         [HttpGet]
        [Route ("clear-cache")]
        public IActionResult Clear (Guid tenantId) {
            try {
                var retVal = _resourceManager.ClearResourceCache(tenantId);
                return this.Ok (retVal);
            } catch (Exception ex) {
                _log.Error (ExceptionFormatter.SerializeToString (ex));
                return StatusCode ((int) HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
            }
        }


    }
}