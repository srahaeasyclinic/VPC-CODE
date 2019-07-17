using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NLog;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using VPC.Entities.EntitySecurity;
using VPC.Entities.Role;
using VPC.Entities.TenantSubscription;
using VPC.Entities.WorkFlow;
using VPC.Framework.Business.Common;
using VPC.Framework.Business.EntitySecurity.APIs;
using VPC.Framework.Business.MetadataManager.Contracts;
using VPC.Framework.Business.Role.APIs;
using VPC.Framework.Business.TenantSubscription.Contracts;
using VPC.Framework.Business.WorkFlow.Contracts;
using VPC.WebApi.Controllers.WorkFlow;
using VPC.WebApi.Utility;


namespace VPC.WebApi.Controllers.EntitySecurity
{
    [Route("api/subscriptions")]   
  //  [Authorize(Policy = "AuthRole")]   
    public class SubscriptionController : BaseApiController
    {
        private readonly Logger _log= LogManager.GetCurrentClassLogger();
        private readonly IManagerTenantSubscription _managerSubscription; 
        public SubscriptionController(IManagerTenantSubscription managerSubscription)
        {           
          _managerSubscription=managerSubscription;
        }  

        [HttpGet]
        [Route("")]         
        public IActionResult Get()
        {
            try
            {  
                var stopwatch = StopwatchLogger.Start(_log); 
                 _log.Info("Called SubscriptionController Get All");                   
                var retVal = _managerSubscription.TenantSubscriptions(TenantCode);    
                stopwatch.StopAndLog("End SubscriptionController Get all");
                return Ok(retVal);
            }
            catch (Exception ex)
            {
                _log.Error(ExceptionFormatter.SerializeToString(ex));
                return StatusCode((int)HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
            }
        }   

        [HttpGet]
        [Route("{subscriptionId:guid}")]         
        public IActionResult Get(Guid subscriptionId)
        {
            try
            {  
               if (subscriptionId==Guid.Empty) 
                    return BadRequest("Invalid parameter");

                var stopwatch = StopwatchLogger.Start(_log); 
                 _log.Info("Called SubscriptionController Get");                   
                var retVal = _managerSubscription.TenantSubscription(TenantCode,subscriptionId);    
                stopwatch.StopAndLog("End SubscriptionController Get");
                return Ok(retVal);
            }
            catch (Exception ex)
            {
                _log.Error(ExceptionFormatter.SerializeToString(ex));
                return StatusCode((int)HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
            }
        }   

        [HttpGet]
        [Route("duration")]         
        public IActionResult GetDuration()
        {
            try
            {  
              var stopwatch = StopwatchLogger.Start(_log); 
              _log.Info("Called SubscriptionController Get All");                   
           
               var mainObj = new List<object>();     
               Dictionary<int, string> routeRx = DataUtility.GetIntFieldNameFromEnum(typeof (SubscriptionDuration));
               foreach (var route in routeRx.OrderBy(u => u.Key))
                  {            
                   mainObj.Add(new
                        {
                            id = route.Key.ToString(CultureInfo.InvariantCulture),
                            name = DataUtility.GetEnumDescription((SubscriptionDuration) route.Key)
                      });
                  } 
                stopwatch.StopAndLog("End SubscriptionController Get all");
                return Ok(mainObj);
            }
            catch (Exception ex)
            {
                _log.Error(ExceptionFormatter.SerializeToString(ex));
                return StatusCode((int)HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
            }
        }  
 

        [HttpPost]
        [Route("")]
        public IActionResult Post([FromBody] TenantSubscriptionInfo info)
        {
            try
            {   
                if (info==null) 
                    return BadRequest("Invalid parameter");
                if (string.IsNullOrEmpty(info.Name) )
                    return BadRequest("Invalid parameter.");
                if (info.Group==null) 
                    return BadRequest("Invalid parameter");
                if (info.Group.Id==Guid.Empty) 
                    return BadRequest("Invalid parameter");

                var stopwatch = StopwatchLogger.Start(_log);              
                _log.Info("Called SubscriptionController Post {0}=", JsonConvert.SerializeObject(info));                 
                var subscriptionId = _managerSubscription.Create(TenantCode,info);
                stopwatch.StopAndLog("End SubscriptionController Post");
                return Ok(subscriptionId);
            }
            catch (Exception ex)
            {
                _log.Error(ExceptionFormatter.SerializeToString(ex));
                return StatusCode((int)HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
            }
        }

        [HttpPut]
        [Route("")]
        public IActionResult Put([FromBody] TenantSubscriptionInfo info)
        {
            try
            {   
                if (info==null) 
                    return BadRequest("Invalid parameter");
                if (string.IsNullOrEmpty(info.Name) )
                    return BadRequest("Invalid parameter.");
                if (info.Group==null) 
                    return BadRequest("Invalid parameter");
                if (info.Group.Id==Guid.Empty) 
                    return BadRequest("Invalid parameter");

                var stopwatch = StopwatchLogger.Start(_log);              
                _log.Info("Called SubscriptionController Put {0}=", JsonConvert.SerializeObject(info));                 
                var retVal = _managerSubscription.Update(TenantCode,info);
                stopwatch.StopAndLog("End SubscriptionController Put");
                return Ok(retVal);
            }
            catch (Exception ex)
            {
                _log.Error(ExceptionFormatter.SerializeToString(ex));
                return StatusCode((int)HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
            }
        }

        [HttpDelete]
        [Route("{subscriptionId:guid}")]   
        public IActionResult Delete(Guid subscriptionId)
        {
            try
            {                 
                if (subscriptionId==Guid.Empty) 
                    return BadRequest("Invalid parameter");

                var stopwatch = StopwatchLogger.Start(_log);              
                _log.Info("Called SubscriptionController Delete {0}=", JsonConvert.SerializeObject(subscriptionId));                 
                var retVal = _managerSubscription.Delete(TenantCode,subscriptionId);
                stopwatch.StopAndLog("End SubscriptionController Delete");
                return Ok(retVal);
            }
            catch (Exception ex)
            {
                _log.Error(ExceptionFormatter.SerializeToString(ex));
                return StatusCode((int)HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
            }
        }

        [HttpPatch]
        [Route("{subscriptionId:guid}")]   
        public IActionResult Patch(Guid subscriptionId)
        {
            try
            {                 
                if (subscriptionId==Guid.Empty) 
                    return BadRequest("Invalid parameter");

                var stopwatch = StopwatchLogger.Start(_log);              
                _log.Info("Called SubscriptionController Patch {0}=", JsonConvert.SerializeObject(subscriptionId));                 
                var retVal = _managerSubscription.Status(TenantCode,subscriptionId);
                stopwatch.StopAndLog("End SubscriptionController Patch");
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