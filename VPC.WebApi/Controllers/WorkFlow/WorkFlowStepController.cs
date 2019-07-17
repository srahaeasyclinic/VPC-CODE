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
    [Route("api/workflow/steps")]
    public class WorkFlowStepController : BaseApiController
    {
        private readonly Logger _log= LogManager.GetCurrentClassLogger();
        private readonly IManagerWorkFlowStep _managerWorkFlowSteps;
        private readonly IMetadataManager _iMetadataManager ;
        private readonly IManagerWorkFlowInnerStep _managerWorkFlowInnerSteps;

        private readonly IManagerWorkFlowRole _managerWorkFlowRole;
        public WorkFlowStepController(IManagerWorkFlowStep managerWorkFlowSteps,IMetadataManager iMetadataManager,IManagerWorkFlowInnerStep managerWorkFlowInnerSteps,
        IManagerWorkFlowRole managerWorkFlowRole)
        {
            _managerWorkFlowSteps = managerWorkFlowSteps; 
            _iMetadataManager = iMetadataManager;   
            _managerWorkFlowInnerSteps=managerWorkFlowInnerSteps; 
            _managerWorkFlowRole=managerWorkFlowRole;        
        }       

        [HttpGet]
        [Route("{entityname}")]
        public IActionResult Get([FromRoute] string entityname,[FromQuery]  Guid transitionType,[FromQuery] Guid workFlowId)
        {
            try
            {               
                var stopwatch = StopwatchLogger.Start(_log);              
                _log.Info("Called WorkFlowStepController Get {0}=", JsonConvert.SerializeObject(entityname));    
                var entityId= _iMetadataManager.GetEntityContextByEntityName(entityname);             
                var retVal = WorkFlowHelper.GetAllSteps(entityId);                
                var itsSavedInnerSteps=_managerWorkFlowInnerSteps.GetWorkFlowInnerStep_ByStepTransactionType(TenantCode,transitionType, workFlowId);
                if(retVal.Count>0)
                {
                    retVal=(from retV in retVal where retV.Id  != transitionType select retV ).ToList();
                }
                List<WorkFlowResource> filteredInnerSteps=new List<WorkFlowResource>();
                foreach(var retV in retVal)
                {
                    var checkExistance=(from itsSavedInnerStep in itsSavedInnerSteps where itsSavedInnerStep.TransitionType.Id==retV.Id select itsSavedInnerStep).ToList();
                    if(checkExistance.Count==0)
                    {
                      filteredInnerSteps.Add(retV);  
                    }
                }

                stopwatch.StopAndLog("End WorkFlowStepController Get");
                return Ok(filteredInnerSteps);
            }
            catch (Exception ex)
            {
                _log.Error(ExceptionFormatter.SerializeToString(ex));
                return StatusCode((int)HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
            }
        }
      
        [HttpPost]
        [Route("")]
        public IActionResult Post([FromBody] WorkFlowStepInfo workFlowStep)
        {
            try
            {               
                var stopwatch = StopwatchLogger.Start(_log);              
                _log.Info("Called WorkFlowStepController Post {0}=", JsonConvert.SerializeObject(workFlowStep));                 
                var retVal = _managerWorkFlowSteps.CreateWorkFlowStep(TenantCode,workFlowStep);
                stopwatch.StopAndLog("End WorkFlowStepController Post");
                return Ok(retVal);
            }
            catch (Exception ex)
            {
                _log.Error(ExceptionFormatter.SerializeToString(ex));
                return StatusCode((int)HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
            }
        }

        [HttpPut]
        [Route("")]
        public IActionResult Put([FromBody] WorkFlowStepInfo workFlowStep)
        {
            try
            {               
                var stopwatch = StopwatchLogger.Start(_log);              
                _log.Info("Called WorkFlowStepController Put {0}=", JsonConvert.SerializeObject(workFlowStep));                 
                var retVal = _managerWorkFlowSteps.UpdateWorkFlowSteps(TenantCode,workFlowStep);
                stopwatch.StopAndLog("End WorkFlowStepController Put");
                return Ok(retVal);
            }
            catch (Exception ex)
            {
                _log.Error(ExceptionFormatter.SerializeToString(ex));
                return StatusCode((int)HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
            }
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public IActionResult Delete(Guid id,[FromQuery] Guid workFlowId)
        { 
          try
            {               
                var stopwatch = StopwatchLogger.Start(_log);              
                _log.Info("Called WorkFlowStepController Delete {0}=", JsonConvert.SerializeObject(id));                 
                var retVal = _managerWorkFlowSteps.DeleteWorkFlowSteps(TenantCode,id,workFlowId);
                stopwatch.StopAndLog("End WorkFlowStepController Delete");
                return Ok(retVal);
            }
            catch (Exception ex)
            {
                _log.Error(ExceptionFormatter.SerializeToString(ex));
                return StatusCode((int)HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
            }
        }

        [HttpPut]
        [Route("sequence")]
        public IActionResult Put([FromBody] List<WorkFlowStepInfo> workFlowSteps)
        {
            try
            {               
                var stopwatch = StopwatchLogger.Start(_log);              
                _log.Info("Called WorkFlowStepController Patch {0}=", JsonConvert.SerializeObject(workFlowSteps));                 
                var retVal = _managerWorkFlowSteps.MoveUpDownWorkFlowSteps(TenantCode,workFlowSteps);
                stopwatch.StopAndLog("End WorkFlowStepController Patch");
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