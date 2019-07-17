using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using VPC.Entities.Common;
using VPC.Entities.SchedulerConfiguration;
using VPC.Framework.Business.Common;
using VPC.Framework.Business.SchedulerConfiguration;
using VPC.WebApi.Attribute;
using VPC.WebApi.Utility;


namespace VPC.WebApi.Controllers.Scheduler
{
   [Route("api/schedulerConfigurations")]    
    public class SchedulerController : BaseApiController
    {
        private readonly Logger _log= LogManager.GetCurrentClassLogger();
        private readonly IManagerConfigureScheduler _managerConfigureScheduler;       
        public SchedulerController(IManagerConfigureScheduler managerConfigureScheduler)
        {
            _managerConfigureScheduler = managerConfigureScheduler;            
        }  

        [HttpGet]
        [Route("{batchId:guid}")]         
        public IActionResult Get(Guid batchId)
        {
            try
            {  
                var stopwatch = StopwatchLogger.Start(_log); 
                 _log.Info("Called SchedulerController Get By BatchId");                 
                 var scheduler = _managerConfigureScheduler.GetConfigureScheduler(TenantCode,batchId); 
                stopwatch.StopAndLog("End SchedulerController Get By BatchId");
                return Ok(scheduler);
            }
            catch (Exception ex)
            {
                _log.Error(ExceptionFormatter.SerializeToString(ex));
                return StatusCode((int)HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
            }
        }  

        [HttpPost]
        [Route("")]
        public IActionResult Post([FromBody] SchedulerInfo info)
        {
            try
            {   
                if (info==null) 
                    return BadRequest("Invalid parameter");                   

                var stopwatch = StopwatchLogger.Start(_log);              
                _log.Info("Called SchedulerController Post {0}=", JsonConvert.SerializeObject(info));                 
                var schedulerId = _managerConfigureScheduler.ConfigureScheduler(TenantCode,info);
                stopwatch.StopAndLog("End SchedulerController Post");
                return Ok(schedulerId);
            }
            catch (Exception ex)
            {
                _log.Error(ExceptionFormatter.SerializeToString(ex));
                return StatusCode((int)HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
            }
        }

        
        [HttpGet]
        [Route("unit")]         
        public IActionResult GetUnit()
        {
            try
            {                
               var mainObjs = new List<ItemNameInt>();     
               Dictionary<int, string> schedulerUnits = DataUtility.GetIntFieldNameFromEnum(typeof (SchedulerUnit));
               foreach (var schedulerUnit in schedulerUnits.OrderBy(u => u.Key))
                  {            
                    mainObjs.Add(new ItemNameInt
                        {
                            Id = schedulerUnit.Key,
                            Name = DataUtility.GetEnumDescription((SchedulerUnit) schedulerUnit.Key)
                      });
                  }               
                return Ok(mainObjs);
            }
            catch (Exception ex)
            {
                _log.Error(ExceptionFormatter.SerializeToString(ex));
                return StatusCode((int)HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
            }
        } 
      

        [HttpGet]
        [Route("weekdays")]         
        public IActionResult GetWeekDays()
        {
            try
            {                
               var mainObjs = new List<ItemNameInt>();     
               Dictionary<int, string> schedulerWeekDays = DataUtility.GetIntFieldNameFromEnum(typeof (SchedulerWeekDays));
               foreach (var schedulerWeekDay in schedulerWeekDays.OrderBy(u => u.Key))
                  {            
                    mainObjs.Add(new ItemNameInt
                        {
                            Id = schedulerWeekDay.Key,
                            Name = DataUtility.GetEnumDescription((SchedulerWeekDays) schedulerWeekDay.Key)
                      });
                  }               
                return Ok(mainObjs);
            }
            catch (Exception ex)
            {
                _log.Error(ExceptionFormatter.SerializeToString(ex));
                return StatusCode((int)HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
            }
        } 

        [HttpGet]
        [Route("months")]         
        public IActionResult GetMonths()
        {
            try
            {                
               var mainObjs = new List<ItemNameInt>();     
               Dictionary<int, string> schedulerMonths = DataUtility.GetIntFieldNameFromEnum(typeof (SchedulerMonths));
               foreach (var schedulerMonth in schedulerMonths.OrderBy(u => u.Key))
                  {            
                    mainObjs.Add(new ItemNameInt
                        {
                            Id = schedulerMonth.Key,
                            Name = DataUtility.GetEnumDescription((SchedulerMonths) schedulerMonth.Key)
                      });
                  }               
                return Ok(mainObjs);
            }
            catch (Exception ex)
            {
                _log.Error(ExceptionFormatter.SerializeToString(ex));
                return StatusCode((int)HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
            }
        } 
        
         [HttpGet]
        [Route("hours")]         
        public IActionResult GetHours()
        {
            try
            {                
               var mainObjs = new List<ItemNameInt>();     
               Dictionary<int, string> syncStartTimes = DataUtility.GetIntFieldNameFromEnum(typeof (SyncStartTime));
               foreach (var syncStartTime in syncStartTimes.OrderBy(u => u.Key))
                  {            
                    mainObjs.Add(new ItemNameInt
                        {
                            Id = syncStartTime.Key,
                            Name = DataUtility.GetEnumDescription((SyncStartTime) syncStartTime.Key)
                      });
                  }               
                return Ok(mainObjs);
            }
            catch (Exception ex)
            {
                _log.Error(ExceptionFormatter.SerializeToString(ex));
                return StatusCode((int)HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
            }
        } 
        
    }
}