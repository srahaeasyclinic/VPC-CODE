using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NLog;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using VPC.Entities.EntitySecurity;
using VPC.Entities.WorkFlow;
using VPC.Framework.Business.EntitySecurity.APIs;
using VPC.Framework.Business.WorkFlow.Contracts;
using VPC.WebApi.Utility;


namespace VPC.WebApi.Controllers.WorkFlow
{
    [Route("api/workflow/steps/roles")]
    public class WorkFlowRoleController : BaseApiController
    {
        private readonly Logger _log= LogManager.GetCurrentClassLogger();
        private readonly IManagerWorkFlowRole _managerWorkFlowRole;
        private readonly IManagerWorkFlowSecurity _managerWorkFlowSecurity;

         private ISecurityCacheManager _securityCacheManager;
        public WorkFlowRoleController(IManagerWorkFlowRole managerWorkFlowRole,IManagerWorkFlowSecurity managerWorkFlowSecurity,
        ISecurityCacheManager securityCacheManager)
        {
            _managerWorkFlowRole = managerWorkFlowRole;   
            _managerWorkFlowSecurity=managerWorkFlowSecurity; 
              _securityCacheManager=securityCacheManager;         
        }  

        [HttpGet]
        [Route("{entityname}")]
        public IActionResult Get(string entityname)
        {
            try
            {               
                var stopwatch = StopwatchLogger.Start(_log);              
                _log.Info("Called WorkFlowRoleController Get WorkFlow Security {0}=", JsonConvert.SerializeObject(entityname));  
                var retVal = _managerWorkFlowSecurity.GetWorkFlowSecurity(TenantCode,entityname);
                stopwatch.StopAndLog("End WorkFlowRoleController Get Get WorkFlow Security ");
                return Ok(retVal);
            }
            catch (Exception ex)
            {
                _log.Error(ExceptionFormatter.SerializeToString(ex));
                return StatusCode((int)HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
            }
        }     

        [HttpPost]
        [Route("")]
        public IActionResult Post([FromBody] WorkFlowRoleInfo workFlowRole)
        {
            try
            {               
                var stopwatch = StopwatchLogger.Start(_log);              
                _log.Info("Called WorkFlowRoleController Post {0}=", JsonConvert.SerializeObject(workFlowRole));                 
                var retVal = _managerWorkFlowRole.CreateWorkFlowRole(TenantCode,workFlowRole);
                 _securityCacheManager.Clear(TenantCode,UserId,EntityCacheType.WorkFlow);
                stopwatch.StopAndLog("End WorkFlowRoleController Post");
                return Ok(retVal);
            }
            catch (Exception ex)
            {
                _log.Error(ExceptionFormatter.SerializeToString(ex));
                return StatusCode((int)HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
            }
        }
        

        [HttpDelete]
        [Route("{workFlowStepId:guid}")]
        public IActionResult Delete(Guid workFlowStepId,[FromQuery] Guid roleId,Guid workFlowId,WorkFlowRoleType type)
        { 
          try
            {               
                var stopwatch = StopwatchLogger.Start(_log);              
                _log.Info("Called WorkFlowRoleController Delete {0}=", JsonConvert.SerializeObject(workFlowStepId));                 
                var retVal = _managerWorkFlowRole.DeleteWorkFlowRole(TenantCode,workFlowStepId,roleId,workFlowId,type);
                 _securityCacheManager.Clear(TenantCode,UserId,EntityCacheType.WorkFlow);
                stopwatch.StopAndLog("End WorkFlowRoleController Delete");
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