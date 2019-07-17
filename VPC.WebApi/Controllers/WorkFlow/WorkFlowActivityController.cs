using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using VPC.Entities.WorkFlow;
using VPC.Framework.Business.MetadataManager.Contracts;
using VPC.Framework.Business.WorkFlow.Attribute;
using VPC.Framework.Business.WorkFlow.Contracts;
using VPC.WebApi.Utility;


namespace VPC.WebApi.Controllers.WorkFlow
{
    [Route("api/workflow/activities")]
    public class WorkFlowActivityController : BaseApiController
    {
        private readonly Logger _log= LogManager.GetCurrentClassLogger();
         private readonly IManagerWorkFlowInnerStep _managerInnerSteps;
         private IMetadataManager _iMetadataManager;
         private readonly IManagerWorkFlow _managerWorkFlow;
         private IManagerWorkFlowTransition _managerTrasition; 
         private readonly IManagerWorkFlowStep _managerWorkFlowStep;       
       

         public WorkFlowActivityController(IManagerWorkFlowInnerStep managerInnerSteps,IMetadataManager iMetadataManager,IManagerWorkFlow managerWorkFlow,
         IManagerWorkFlowTransition managerTrasition,IManagerWorkFlowStep managerWorkFlowStep)
            {
                _managerInnerSteps=managerInnerSteps;     
                _iMetadataManager=iMetadataManager;    
                _managerWorkFlow=managerWorkFlow; 
                _managerTrasition=managerTrasition;
                _managerWorkFlowStep=managerWorkFlowStep;
            }        

        // [HttpGet]
        // [Route("{entityname}")]
        // public IActionResult Get([FromRoute] string entityname,[FromQuery] string subTypeName, Guid refId)
        // {
        //     try
        //     {               
        //     var stopwatch = StopwatchLogger.Start(_log);              
        //     _log.Info("Called WorkFlowActivityController Get {0}=", JsonConvert.SerializeObject(entityname));                 
                
        //     var innerPossibleSteps = new List<object>();
        //     var entityId= _iMetadataManager.GetEntityContextByEntityName(entityname); 
        //     var subTypeCode=_iMetadataManager.GetSubTypeId(entityname, subTypeName);
        //     var workFlow= _managerWorkFlow.GetWorkFlow(TenantCode,entityId,subTypeCode);
        //     WorkFlowTransition currentWorkflowItem = _managerTrasition.GetCurrentStepByRefId(TenantCode,refId);
        //     var itsCurrentStep=_managerWorkFlowStep.GetWorkFlowSteps(TenantCode,workFlow.WorkFlowId).FirstOrDefault(x => x.TransitionType.Id == currentWorkflowItem.WorkFlowStepId);
        //     List<WorkFlowInnerStepInfo> innerSteps = _managerInnerSteps.GetWorkFlowInnerStep(TenantCode,workFlow.WorkFlowId).FindAll(x => x.WorkFlowStepId == itsCurrentStep.WorkFlowStepId).ToList();

        //     foreach(var innerStep in innerSteps)
        //     {
        //           innerPossibleSteps.Add(new
        //                       {
        //                            StepId = innerStep.InnerStepId,
        //                            WorkFlowId = innerStep.WorkFlowId,
        //                            WorkFlowStepId=innerStep.WorkFlowStepId,
        //                            Key = WorkFlowHelper.GetKeyByContext(entityId, innerStep.TransitionType.Id),
        //                            Value = WorkFlowHelper.GetValueByContext(entityId, innerStep.TransitionType.Id),
        //                            //AssignedRoles = _reviewWorkFlow.GetsConfigurationRoleAssignments(tenantId, workFlowStep.WorkFlowId, workFlowStep.WorkFlowStepId, new Guid(WorkFlowConfigurationRoleTypeContext.AssignedRoles), new Guid(WorkFlowExtensionStepTypeContext.ForUser)).RoleId,
        //                           // TransitionLabelKey = ApiUtility.GetTransitionLabelKeyByContext(workFlowcontext, workFlowStep.Context),                                            
        //                           // userOperator.IsAssignmentMandatory
        //                       });
        //     }

        //     stopwatch.StopAndLog("End WorkFlowActivityController Get");
        //     return Ok(innerPossibleSteps);
        //     }
        //     catch (Exception ex)
        //     {
        //         _log.Error(ExceptionFormatter.SerializeToString(ex));
        //         return StatusCode((int)HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
        //     }
        // }
      
    }
}