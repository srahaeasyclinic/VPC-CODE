using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NLog;
using System;
using System.Net;
using System.Linq;
using VPC.Entities.WorkFlow;
using VPC.Framework.Business.MetadataManager.Contracts;
using VPC.Framework.Business.WorkFlow.Contracts;
using VPC.WebApi.Utility;
using System.Collections.Generic;

namespace VPC.WebApi.Controllers.WorkFlow
{
    [Route("api/workflow")]
    public class WorkFlowController : BaseApiController
    {
        private readonly Logger _log= LogManager.GetCurrentClassLogger();
         private readonly IManagerWorkFlow _managerWorkFlow;//=new ManagerWorkFlow();
        IMetadataManager iMetadataManager = new VPC.Framework.Business.MetadataManager.Contracts.MetadataManager(); 
        public WorkFlowController(IManagerWorkFlow managerWorkFlow)
        {
            _managerWorkFlow = managerWorkFlow;             
        }             

        [HttpGet]
        [Route("{entityname}/{subTypeName}")]
        public IActionResult Get([FromRoute] string entityname,[FromRoute] string subTypeName)
        {
            try
            {               
                var stopwatch = StopwatchLogger.Start(_log);              
                _log.Info("Called WorkFlowController Get {0}=", JsonConvert.SerializeObject(entityname));                 
                
                var retVal = _managerWorkFlow.GetWorkFlowDetail(TenantCode,entityname,subTypeName);
                stopwatch.StopAndLog("End WorkFlowController Get");
                return Ok(retVal);
            }
            catch (Exception ex)
            {
                _log.Error(ExceptionFormatter.SerializeToString(ex));
                return StatusCode((int)HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
            }
        }

        [HttpGet]
        [Route("{entityname}")]
        public IActionResult Get([FromRoute] string entityname)
        {
            try
            {  

              var details = iMetadataManager.GetVersionControlName(entityname);
                if(!string.IsNullOrEmpty(details))
                    entityname=details;
                
                             
                var stopwatch = StopwatchLogger.Start(_log);              
                _log.Info("Called WorkFlowController Get all by entity name {0}=", JsonConvert.SerializeObject(entityname));                 
                
                var retVal = _managerWorkFlow.GetAllWorkFlowDetails(TenantCode,entityname);
                stopwatch.StopAndLog("End WorkFlowController Get all by entity name");
                return Ok(retVal);
            }
            catch (Exception ex)
            {
                _log.Error(ExceptionFormatter.SerializeToString(ex));
                return StatusCode((int)HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
            }
        }
      
        [HttpGet]
        [Route("currentWorkFlows/{entityname}")]
        public IActionResult GetCurrentUserWorkFlow([FromRoute] string entityname)
        {
            try
            {               
                // var details = iMetadataManager.GetEntitityByName(entityname);
                // if(details!=null && details.VersionControl !=null )
                // entityname=details.VersionControl.Name;

                var stopwatch = StopwatchLogger.Start(_log);              
                _log.Info("Called WorkFlowController Get all by entity name {0}=", JsonConvert.SerializeObject(entityname));                 
                var workFlows=SecurityCache.WorkFlow;
                List<WorkFlowInfo> list=new List<WorkFlowInfo>();
                if(workFlows.Count>0)
                {                   
                    list = (from workFlow in workFlows where workFlow.EntityId.ToLower() == entityname.ToLower() select workFlow).ToList();
                }
              
                stopwatch.StopAndLog("End WorkFlowController Get all by entity name");
                return Ok(list);
            }
            catch (Exception ex)
            {
                _log.Error(ExceptionFormatter.SerializeToString(ex));
                return StatusCode((int)HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
            }
        }
      

        // [HttpPost]
        // [Route("")]
        // public IActionResult Post()
        // {
        //     try
        //     {               
        //        // var stopwatch = StopwatchLogger.Start(_log);              
        //        // _log.Info("Called WorkFlowController Get {0}=", JsonConvert.SerializeObject(entityId));                 
        //         var retVal = _managerWorkFlow.InitWorkFlow(TenantCode);
        //       //  stopwatch.StopAndLog("End WorkFlowController Get");
        //         return Ok(retVal);
        //     }
        //     catch (Exception ex)
        //     {
        //         _log.Error(ExceptionFormatter.SerializeToString(ex));
        //         return StatusCode((int)HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
        //     }
        // }

        // [HttpPost]
        // [Route("operation")]
        // public IActionResult Operation()
        // {
        //     try
        //     {               
        //         var operationWapper = new OperationWapper
        //         {
        //             OperationType=WorkFlowOperationType.Create,
        //             Data = null
        //         };  
        //         var process=new WorkFlowProcessProperties
        //                         {
        //                             EntityName="User",
        //                             SubTypeCode="Employee",
        //                            // RoleIds = RoleIds,
        //                             UserId = UserId,
        //                             IsSuperAdmin = IsSuperAdmin,
        //                            // LocationId = CurrentLocationInfo.LocationId,
        //                            // Subscriptions = SubscriptionsV1
        //                         };
        //         var retVal = _managerWorkFlow.ManageOperation(TenantCode,operationWapper,process);
              
        //         return Ok(retVal);
        //     }
        //     catch (Exception ex)
        //     {
        //         _log.Error(ExceptionFormatter.SerializeToString(ex));
        //         return StatusCode((int)HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
        //     }
        // }

         [HttpPatch]
        [Route("transition")]
        public IActionResult Transition([FromBody] TransitionWapper wapper)
        {
            try
             {  
                //  var details = iMetadataManager.GetEntitityByName(wapper.EntityName);
                //      if(details!=null && details.VersionControl !=null )
                //          wapper.EntityName=details.VersionControl.Name;

                 wapper.UserId=UserId;
                 var retVal = _managerWorkFlow.ManageTransition(TenantCode,wapper,IsSuperAdmin);              
                 return Ok(retVal);
            }
            catch (Exception ex)
            {
                _log.Error(ExceptionFormatter.SerializeToString(ex));
                return StatusCode((int)HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
            }
        } 

        // [HttpPost]
        // [Route("transition")]
        // public IActionResult Transition([FromBody] TransitionWapper wapper)
        // {
        //     try
        //      {              
        //         var transition = new WorkFlowTransition {                       
        //                 WorkFlowStepId =wapper.StepId,
        //                 RefId = wapper.RefId 
        //             };

        //         var process=new WorkFlowProcessProperties
        //                         {
        //                             EntityName=wapper.EntityName,
        //                             SubTypeCode=wapper.SubTypeName,
        //                            // RoleIds = RoleIds,
        //                             UserId = UserId,
        //                             IsSuperAdmin = IsSuperAdmin,
        //                            // LocationId = CurrentLocationInfo.LocationId,
        //                            // Subscriptions = SubscriptionsV1
        //                         };
        //         var retVal = _managerWorkFlow.ManageTransition(TenantCode,transition,process);
              
        //         return Ok(retVal);
        //     }
        //     catch (Exception ex)
        //     {
        //         _log.Error(ExceptionFormatter.SerializeToString(ex));
        //         return StatusCode((int)HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
        //     }
        // }       
        
    }
}