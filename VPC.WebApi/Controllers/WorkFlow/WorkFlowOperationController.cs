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
using VPC.Framework.Business.WorkFlow;
using VPC.Framework.Business.WorkFlow.Attribute;
using VPC.Framework.Business.WorkFlow.Contracts;
using VPC.WebApi.Utility;


namespace VPC.WebApi.Controllers.WorkFlow
{
    [Route("api/workflow/operation/process")]
    public class WorkFlowOperationsController : BaseApiController
    {
        private readonly Logger _log= LogManager.GetCurrentClassLogger();
        private IManagerWorkFlowProcess _managerWorkFlowProcess;  
        private readonly IMetadataManager _iMetadataManager;
        private readonly IManagerWorkFlowProcessTask _managerWorkFlowProcessTask;
        public WorkFlowOperationsController(IMetadataManager iMetadataManager,IManagerWorkFlowProcess managerWorkFlowProcess,
         IManagerWorkFlowProcessTask managerWorkFlowProcessTask)
         {
             _managerWorkFlowProcess = managerWorkFlowProcess; 
             _iMetadataManager = iMetadataManager;  
             _managerWorkFlowProcessTask=managerWorkFlowProcessTask;           
         } 


       [HttpGet]
       [Route("{workFlowId:guid}")]
        public IActionResult Get(Guid workFlowId)
        {

            try
            {               
                var stopwatch = StopwatchLogger.Start(_log);              
                _log.Info("Called WorkFlowOperationsController Get by workFlowId{0}=", JsonConvert.SerializeObject(workFlowId)); 
                var workFlowOperationProcess=_managerWorkFlowProcess.GetWorkFlowProcess(TenantCode,workFlowId);
                stopwatch.StopAndLog("End WorkFlowOperationsController Get by workFlowId");
                return Ok(workFlowOperationProcess);
            }
            catch (Exception ex)
            {
                _log.Error(ExceptionFormatter.SerializeToString(ex));
                return StatusCode((int)HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
            }
        }

       [HttpGet]
       [Route("{operationName}/{entityName}")]
        public IActionResult Get(string operationName,string entityName)
        {

            try
            {               
                var stopwatch = StopwatchLogger.Start(_log);              
                _log.Info("Called WorkFlowProcessTaskController Get {0}=", JsonConvert.SerializeObject(operationName));                 
                 var entityId= _iMetadataManager.GetEntityContextByEntityName(entityName); 
                 var retVal=WorkFlowHelper.GetProcessorTitleByOperation(operationName,entityId);
                stopwatch.StopAndLog("End WorkFlowProcessTaskController Get");
                return Ok(retVal);
            }
            catch (Exception ex)
            {
                _log.Error(ExceptionFormatter.SerializeToString(ex));
                return StatusCode((int)HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
            }
        }

       [HttpGet]
       [Route("task/{workFlowOperationId:guid}")]
        public IActionResult Get([FromRoute] Guid workFlowOperationId,[FromQuery] string entityName)
        {

            try
            {               
                var stopwatch = StopwatchLogger.Start(_log);              
                _log.Info("Called WorkFlowProcessTaskController Get by workFlowOperationId{0}=", JsonConvert.SerializeObject(workFlowOperationId));                 
                 var tasks= _managerWorkFlowProcessTask.GetWorkFlowProcessTask_ByInnerStepId(TenantCode,workFlowOperationId); 

                         if(tasks.Count>0)
                            {
                                var entityId= _iMetadataManager.GetEntityContextByEntityName(entityName);
                                var itsProcessorTasks= WorkFlowHelper.GetProcessorTitleByOperationModule(entityId);
                                    foreach(var workFlowProcessTask in tasks)
                                        {
                                        var task=(from itsProcessorTask in itsProcessorTasks 
                                        where itsProcessorTask.Id==workFlowProcessTask.ProcessCode select itsProcessorTask).ToList();
                                        if(task.Count>0)
                                        {
                                            workFlowProcessTask.ProcessName=task[0].Key;
                                        }
                                    
                                        }
                            }

                stopwatch.StopAndLog("End WorkFlowProcessTaskController Get by innerStepId");
                return Ok(tasks);
            }
            catch (Exception ex)
            {
                _log.Error(ExceptionFormatter.SerializeToString(ex));
                return StatusCode((int)HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
            }
        }
       
        
    }
}