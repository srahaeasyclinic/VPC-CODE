using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NLog;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using VPC.Entities.Common;
using VPC.Entities.WorkFlow;
using VPC.Framework.Business.Credential;
using VPC.Framework.Business.MetadataManager.Contracts;
using VPC.Framework.Business.Task.UserTask;
using VPC.Framework.Business.WorkFlow.Contracts;
using VPC.WebApi.Utility;


namespace VPC.WebApi.Controllers.WorkFlow
{
    [Route("api/user")]
    public class UserTaskController : BaseApiController
    {
        private readonly Logger _log= LogManager.GetCurrentClassLogger();
        private readonly  IUserTaskManager _userTaskManager;
        public UserTaskController(IUserTaskManager userTaskManager)
        {
            _userTaskManager=userTaskManager; 
        }   

        [HttpPut]
        [Route("tasks/resetPassword")]
        public IActionResult ResetPassword([FromBody] ItemName info)
        {
            try
            { 
                var retVal = _userTaskManager.ResetPassword(TenantCode,info.Id);             
                return Ok(retVal);
            }
            catch (Exception ex)
            {
                _log.Error(ExceptionFormatter.SerializeToString(ex));
                return StatusCode((int)HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
            }
        }  

        [HttpPut]
        [Route("tasks/userExport")]
        public IActionResult UserExport([FromBody] ItemName info)
        {
            try
            { 
                var retVal = _userTaskManager.ImportUser(TenantCode,info.Id);             
                return Ok(retVal);
            }
            catch (Exception ex)
            {
                _log.Error(ExceptionFormatter.SerializeToString(ex));
                return StatusCode((int)HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
            }
        }        
     
        
    }
}