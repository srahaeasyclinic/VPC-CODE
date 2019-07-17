using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NLog;
using System;
using System.Collections.Generic;
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
    [Route("api/subscription/entity/details")]   
    [Authorize(Policy = "AuthRole")]   
    public class SubscriptionEntityDetailController : BaseApiController
    {
        private readonly Logger _log= LogManager.GetCurrentClassLogger();
        private readonly IManagerTenantSubscriptionEntityDetail _managerEntityDetail; 
        public SubscriptionEntityDetailController(IManagerTenantSubscriptionEntityDetail managerEntityDetail)
        {           
          _managerEntityDetail=managerEntityDetail;
        }  

        [HttpGet] 
        [Route("{subscriptionEntityId:guid}")]        
        public IActionResult Get(Guid subscriptionEntityId)
        {
            try
            {  
                var stopwatch = StopwatchLogger.Start(_log); 
                 _log.Info("Called SubscriptionEntityDetailController Get All");                   
                var retVal = _managerEntityDetail.TenantSubscriptionEntityDetails(TenantCode,subscriptionEntityId);    
                stopwatch.StopAndLog("End SubscriptionEntityDetailController Get all");
                return Ok(retVal);
            }
            catch (Exception ex)
            {
                _log.Error(ExceptionFormatter.SerializeToString(ex));
                return StatusCode((int)HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
            }
        }   

        [HttpGet]
        [Route("{subscriptionEntityId:guid}/{subscriptionEntityDetailId:guid}")]         
        public IActionResult Get(Guid subscriptionEntityId,Guid subscriptionEntityDetailId)
        {
            try
            {  
               if (subscriptionEntityId==Guid.Empty) 
                    return BadRequest("Invalid parameter");

                if (subscriptionEntityDetailId==Guid.Empty) 
                    return BadRequest("Invalid parameter");

                var stopwatch = StopwatchLogger.Start(_log); 
                 _log.Info("Called SubscriptionEntityDetailController Get");                   
                var retVal = _managerEntityDetail.TenantSubscriptionEntityDetail(TenantCode,subscriptionEntityDetailId);    
                stopwatch.StopAndLog("End SubscriptionEntityDetailController Get");
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
        public IActionResult Post([FromBody] TenantSubscriptionEntityDetailInfo info)
        {
            try
            {   
                if (info==null) 
                    return BadRequest("Invalid parameter");               

                var stopwatch = StopwatchLogger.Start(_log);              
                _log.Info("Called SubscriptionEntityDetailController Post {0}=", JsonConvert.SerializeObject(info));                 
                var subscriptionEntityDetailId = _managerEntityDetail.Create(TenantCode,info);
                stopwatch.StopAndLog("End SubscriptionEntityDetailController Post");
                return Ok(subscriptionEntityDetailId);
            }
            catch (Exception ex)
            {
                _log.Error(ExceptionFormatter.SerializeToString(ex));
                return StatusCode((int)HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
            }
        }

        [HttpPut]
        [Route("")]
        public IActionResult Put([FromBody] TenantSubscriptionEntityDetailInfo info)
        {
            try
            {   
                if (info==null) 
                    return BadRequest("Invalid parameter");              

                var stopwatch = StopwatchLogger.Start(_log);              
                _log.Info("Called SubscriptionEntityDetailController Put {0}=", JsonConvert.SerializeObject(info));                 
                var retVal = _managerEntityDetail.Update(TenantCode,info);
                stopwatch.StopAndLog("End SubscriptionEntityDetailController Put");
                return Ok(retVal);
            }
            catch (Exception ex)
            {
                _log.Error(ExceptionFormatter.SerializeToString(ex));
                return StatusCode((int)HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
            }
        }

        [HttpDelete]
          [Route("{subscriptionEntityDetailId:guid}")]   
        public IActionResult Delete(Guid subscriptionEntityDetailId)
        {
            try
            {                 
                if (subscriptionEntityDetailId==Guid.Empty) 
                    return BadRequest("Invalid parameter");

                var stopwatch = StopwatchLogger.Start(_log);              
                _log.Info("Called SubscriptionEntityDetailController Delete {0}=", JsonConvert.SerializeObject(subscriptionEntityDetailId));                 
                var retVal = _managerEntityDetail.Delete(TenantCode,subscriptionEntityDetailId);
                stopwatch.StopAndLog("End SubscriptionEntityDetailController Delete");
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