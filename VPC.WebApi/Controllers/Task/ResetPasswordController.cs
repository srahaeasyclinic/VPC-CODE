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
using VPC.Framework.Business.WorkFlow.Contracts;
using VPC.WebApi.Utility;


namespace VPC.WebApi.Controllers.WorkFlow
{
    [Route("api/user")]
    public class ResetPasswordController : BaseApiController
    {
        private readonly Logger _log= LogManager.GetCurrentClassLogger();
        private readonly  SqlMembershipProvider _sqlMembership;
        public ResetPasswordController(SqlMembershipProvider sqlMembership)
        {
            _sqlMembership=sqlMembership; 
        }   

        [HttpPost]
        [Route("resetPassword")]
        public IActionResult Post([FromBody] ItemName item)
        {
            try
            {               
                var stopwatch = StopwatchLogger.Start(_log);              
                _log.Info("Called WorkFlowController Get {0}=", JsonConvert.SerializeObject(item));                 
                
                var retVal = _sqlMembership.ResetPassword(TenantCode,item.Id);
                stopwatch.StopAndLog("End WorkFlowController Get");
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