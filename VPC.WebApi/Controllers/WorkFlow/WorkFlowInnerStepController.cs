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
using VPC.Framework.Business.WorkFlow.Contracts;
using VPC.WebApi.Utility;


namespace VPC.WebApi.Controllers.WorkFlow
{
    [Route("api/workflow/innerSteps")]
    public class WorkFlowInnerStepController : BaseApiController
    {
        private readonly Logger _log= LogManager.GetCurrentClassLogger();
        private readonly IManagerWorkFlowInnerStep _managerWorkFlowInnerSteps;
        private readonly IMetadataManager _iMetadataManager ;
        public WorkFlowInnerStepController(IManagerWorkFlowInnerStep managerWorkFlowInnerSteps,IMetadataManager iMetadataManager)
        {
            _managerWorkFlowInnerSteps = managerWorkFlowInnerSteps; 
            _iMetadataManager = iMetadataManager;               
        }

        

        [HttpPost]
        [Route("")]
        public IActionResult Post([FromBody] WorkFlowInnerStepInfo workFlowInnerStep)
        {
            try
            {               
                var stopwatch = StopwatchLogger.Start(_log);              
                _log.Info("Called WorkFlowInnerStepController Post {0}=", JsonConvert.SerializeObject(workFlowInnerStep));                 
                var retVal = _managerWorkFlowInnerSteps.CreateWorkFlowInnerStep(TenantCode,workFlowInnerStep);
                stopwatch.StopAndLog("End WorkFlowInnerStepController Post");
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
        public IActionResult Put([FromBody] List<WorkFlowInnerStepInfo> workFlowInnerSteps)
        {
            try
            {               
                var stopwatch = StopwatchLogger.Start(_log);              
                _log.Info("Called WorkFlowInnerStepController Put {0}=", JsonConvert.SerializeObject(workFlowInnerSteps));                 
                var retVal = _managerWorkFlowInnerSteps.MoveUpDownWorkFlowInnerStep(TenantCode,workFlowInnerSteps);
                stopwatch.StopAndLog("End WorkFlowInnerStepController Put");
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
        public IActionResult Delete(Guid id)
        { 
           try
            {               
                var stopwatch = StopwatchLogger.Start(_log);              
                _log.Info("Called WorkFlowInnerStepController Delete {0}=", JsonConvert.SerializeObject(id));                 
                var retVal = _managerWorkFlowInnerSteps.DeleteWorkFlowInnerStep(TenantCode,id);
                stopwatch.StopAndLog("End WorkFlowInnerStepController Delete");
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