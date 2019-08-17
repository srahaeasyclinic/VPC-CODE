using System;
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
using VPC.Framework.Business.Task.Contracts;
using VPC.WebApi.AttributesHandler;
using VPC.WebApi.Model;
using VPC.WebApi.Utility;

namespace VPC.WebApi.Controllers.EntityController {
    [Route ("api/entities")]
    //[Authorize(Policy = "AuthRole")]
    public class TaskController : BaseApiController {
        private readonly Logger _log = LogManager.GetCurrentClassLogger ();
        private readonly ITaskManager _iTaskManager;

        public TaskController (ITaskManager iTaskManager) {
            _iTaskManager = iTaskManager;
        }

        [HttpPost ("{entityName}/tasks/{taskName}")]
        [ProducesResponseType (200)]
        [ProducesResponseType (500)]
        [ProducesResponseType (404)]
        //   [ValidateJObjectdata]
        public IActionResult CreateTask ([FromRoute] string entityName, string taskName, [FromBody] JObject payload, [FromQuery] Guid id) {
            try {
                var stopwatch = StopwatchLogger.Start (_log);
                _log.Info ("Called EntityController SaveResult");
                if (payload == null) return BadRequest ("Payload required");
                var result = (id == Guid.Empty) ? _iTaskManager.ExecuteTask (TenantCode, entityName, UserId, taskName, payload) : _iTaskManager.ExecuteTaskById (TenantCode, id, entityName, UserId, taskName, payload);

                stopwatch.StopAndLog ("SaveResult of EntityController");
                return Ok (new { result });
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

    }
}