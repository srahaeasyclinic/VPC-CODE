using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NLog;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using VPC.Entities.WorkFlow;
using VPC.Framework.Business.MetadataManager.Contracts;
using VPC.Framework.Business.WorkFlow;
using VPC.Framework.Business.WorkFlow.Contracts;
using VPC.WebApi.Utility;
using System.Linq;
using VPC.Framework.Business.WorkFlow.Attribute;

namespace VPC.WebApi.Controllers.WorkFlow
{
    [Route("api/workflow/innerSteps/process/task")]
    public class WorkFlowProcessTaskController : BaseApiController
    {
        private readonly Logger _log= LogManager.GetCurrentClassLogger();
        private readonly IManagerWorkFlowProcessTask _managerWorkFlowProcessTask;
        private readonly IMetadataManager _iMetadataManager ;
        public WorkFlowProcessTaskController(IManagerWorkFlowProcessTask managerWorkFlowProcessTask,IMetadataManager iMetadataManager)
        {
            _managerWorkFlowProcessTask = managerWorkFlowProcessTask;  
             _iMetadataManager = iMetadataManager;            
        }
        
       [HttpGet]
       [Route("{fromStep:guid}/{toStep:guid}")]
        public IActionResult Get([FromRoute] Guid fromStep,[FromRoute] Guid toStep,[FromQuery] string entityName)
        {

            try
            {               
                var stopwatch = StopwatchLogger.Start(_log);              
                _log.Info("Called WorkFlowProcessTaskController Get {0}=", JsonConvert.SerializeObject(entityName));                 
                 var entityId= _iMetadataManager.GetEntityContextByEntityName(entityName); 
                 var retVal=WorkFlowHelper.GetProcessorTitleByTransition(fromStep,toStep,entityId);
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
       [Route("{innerStepId:guid}")]
        public IActionResult Get([FromRoute] Guid innerStepId,[FromQuery] string entityName)
        {

            try
            {               
                var stopwatch = StopwatchLogger.Start(_log);              
                _log.Info("Called WorkFlowProcessTaskController Get by innerStepId{0}=", JsonConvert.SerializeObject(innerStepId));                 
                 var tasks= _managerWorkFlowProcessTask.GetWorkFlowProcessTask_ByInnerStepId(TenantCode,innerStepId); 

                         if(tasks.Count>0)
                            {
                                var entityId= _iMetadataManager.GetEntityContextByEntityName(entityName);
                                var itsProcessorTasks= WorkFlowHelper.GetProcessorTitleByTransitionModule(entityId);
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


       [HttpPost]
       [Route("")]
        public IActionResult Post([FromBody] WorkFlowProcessTaskInfo processTask)
        {

            try
            {               
                var stopwatch = StopwatchLogger.Start(_log);              
                _log.Info("Called WorkFlowProcessTaskController Post {0}=", JsonConvert.SerializeObject(processTask));                 
                var retVal = _managerWorkFlowProcessTask.CreateWorkFlowProcessTask(TenantCode,processTask);
                stopwatch.StopAndLog("End WorkFlowProcessTaskController Post");
                return Ok(retVal);
            }
            catch (Exception ex)
            {
                _log.Error(ExceptionFormatter.SerializeToString(ex));
                return StatusCode((int)HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
            }
        }

        [HttpDelete]
        [Route("{workFlowProcessStepId:guid}")]
        public IActionResult Delete(Guid workFlowProcessStepId)
        {
            try
            {               
                var stopwatch = StopwatchLogger.Start(_log);              
                _log.Info("Called WorkFlowProcessTaskController Delete {0}=", JsonConvert.SerializeObject(workFlowProcessStepId));                 
                var retVal = _managerWorkFlowProcessTask.DeleteWorkFlowProcessTask(TenantCode,workFlowProcessStepId);
                stopwatch.StopAndLog("End WorkFlowProcessTaskController Delete");
                return Ok(retVal);
            }
            catch (Exception ex)
            {
                _log.Error(ExceptionFormatter.SerializeToString(ex));
                return StatusCode((int)HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
            }
        }

        [HttpPut]
        [Route("{id:guid}")]
        public IActionResult Put(Guid id,[FromBody] List<WorkFlowProcessTaskInfo> processTasks)
        {
            try
            {               
                var stopwatch = StopwatchLogger.Start(_log);              
                _log.Info("Called WorkFlowProcessTaskController Put {0}=", JsonConvert.SerializeObject(processTasks));                 
                var retVal = _managerWorkFlowProcessTask.MoveUpDownWorkFlowProcessTask(TenantCode,processTasks);
                stopwatch.StopAndLog("End WorkFlowProcessTaskController Put");
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