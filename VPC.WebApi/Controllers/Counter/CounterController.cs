using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NLog;
using VPC.Entities.Common;
using VPC.Entities.Counter;
using VPC.Entities.SchedulerConfiguration;
using VPC.Framework.Business.Common;
using VPC.Framework.Business.Counter.Contracts;
using VPC.Framework.Business.SchedulerConfiguration;
using VPC.WebApi.Attribute;
using VPC.WebApi.Utility;
using VPC.Framework.Business.MetadataManager.Contracts;
namespace VPC.WebApi.Controllers.Counter
{
    [Route("api/counters")]
    public class CounterController : BaseApiController
    {
        private readonly Logger _log = LogManager.GetCurrentClassLogger();
        private readonly IManagerCounter _managerCounter;

        public CounterController(IManagerCounter managerCounter)
        {
            _managerCounter = managerCounter;

        }

        [HttpPost]
        [Route("")]
        public IActionResult Post([FromBody] CounterInfo info)
        {
            try
            {
                if (info == null)
                    return BadRequest("Invalid parameter");

                var stopwatch = StopwatchLogger.Start(_log);
                _log.Info("Called CounterController Post {0}=", JsonConvert.SerializeObject(info));
                info.UpdatedBy = UserId;
               
                var retVal = _managerCounter.Create(TenantCode, info);
                stopwatch.StopAndLog("End CounterController Post");
                return Ok(retVal);
            }
            catch (Exception ex)
            {
                _log.Error(ExceptionFormatter.SerializeToString(ex));
                return StatusCode((int)HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
            }
        }

        [HttpGet]
        [Route("{counterId:guid}")]
        public IActionResult Get(Guid counterId)
        {
            try
            {
                var stopwatch = StopwatchLogger.Start(_log);
                _log.Info("Called CounterController Get {0}=", JsonConvert.SerializeObject(counterId));
                var item = _managerCounter.GetCounter(TenantCode, counterId);
                stopwatch.StopAndLog("End CounterController Get");
                return Ok(item);
            }
            catch (Exception ex)
            {
                _log.Error(ExceptionFormatter.SerializeToString(ex));
                return StatusCode((int)HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
            }
        }

        [HttpGet]
        [Route("{entityName}")]
        public IActionResult Get(string entityName)
        {
            try
            {
                var stopwatch = StopwatchLogger.Start(_log);
                // _log.Info ("Called CounterController Get {0}=", JsonConvert.SerializeObject (counterId));
                var item = _managerCounter.GetCounters(TenantCode,entityName);
                stopwatch.StopAndLog("End CounterController Get");
                return Ok(item);
            }
            catch (Exception ex)
            {
                _log.Error(ExceptionFormatter.SerializeToString(ex));
                return StatusCode((int)HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
            }
        }

        [HttpDelete]
        [Route("{counterId:guid}")]
        public IActionResult Delete(Guid counterId)
        {
            try
            {
                var stopwatch = StopwatchLogger.Start(_log);
                _log.Info("Called CounterController Delete {0}=", JsonConvert.SerializeObject(counterId));
                var retVal = _managerCounter.Delete(TenantCode, counterId);
                stopwatch.StopAndLog("End CounterController Delete");
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
        public IActionResult Put([FromBody] CounterInfo info)
        {
            try
            {
                var stopwatch = StopwatchLogger.Start(_log);
                _log.Info("Called CounterController Put {0}=", JsonConvert.SerializeObject(info));
                info.UpdatedBy = UserId;
           
                var retVal = _managerCounter.Update(TenantCode, info);
                stopwatch.StopAndLog("End CounterController put");
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