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
    [Route("api/subscription/entities")]   
    [Authorize(Policy = "AuthRole")]   
    public class SubscriptionEntityController : BaseApiController
    {
        private readonly Logger _log= LogManager.GetCurrentClassLogger();
        private readonly IManagerTenantSubscriptionEntity _managerSubscriptionEntity; 
        private IMetadataManager _iMetadataManager = new VPC.Framework.Business.MetadataManager.Contracts.MetadataManager();
        public SubscriptionEntityController(IManagerTenantSubscriptionEntity managerSubscriptionEntity)
        {           
          _managerSubscriptionEntity=managerSubscriptionEntity;
          
        }  

        [HttpGet] 
        [Route("{subscriptionId:guid}")]        
        public IActionResult Get(Guid subscriptionId)
        {
            try
            {  
                var stopwatch = StopwatchLogger.Start(_log); 
                 _log.Info("Called SubscriptionEntityController Get All");                   
                var retVal = _managerSubscriptionEntity.TenantSubscriptionEntities(TenantCode,subscriptionId);  
                foreach(var ret in retVal)  
                {
                        ret.EntityId = _iMetadataManager.GetEntityNameByEntityContext(ret.EntityId,false); 
                }
                
                stopwatch.StopAndLog("End SubscriptionEntityController Get all");
                return Ok(retVal);
            }
            catch (Exception ex)
            {
                _log.Error(ExceptionFormatter.SerializeToString(ex));
                return StatusCode((int)HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
            }
        }   

        [HttpGet]
        [Route("{subscriptionId:guid}/{subscriptionEntityId:guid}")]         
        public IActionResult Get(Guid subscriptionId,Guid tenantSubscriptionEntityId)
        {
            try
            {  
               if (subscriptionId==Guid.Empty) 
                    return BadRequest("Invalid parameter");

                if (tenantSubscriptionEntityId==Guid.Empty) 
                    return BadRequest("Invalid parameter");

                var stopwatch = StopwatchLogger.Start(_log); 
                 _log.Info("Called SubscriptionEntityController Get");                   
                var retVal = _managerSubscriptionEntity.TenantSubscriptionEntity(TenantCode,tenantSubscriptionEntityId);    
                stopwatch.StopAndLog("End SubscriptionEntityController Get");
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
        public IActionResult Post([FromBody] TenantSubscriptionEntityInfo info)
        {
            try
            {   
                if (info==null) 
                    return BadRequest("Invalid parameter");               

                var stopwatch = StopwatchLogger.Start(_log);              
                _log.Info("Called SubscriptionEntityController Post {0}=", JsonConvert.SerializeObject(info));  
                info.EntityId = _iMetadataManager.GetEntityContextByEntityName(info.EntityId);               
                var subscriptionEntityId = _managerSubscriptionEntity.Create(TenantCode,info);
                stopwatch.StopAndLog("End SubscriptionEntityController Post");
                return Ok(subscriptionEntityId);
            }
            catch (Exception ex)
            {
                _log.Error(ExceptionFormatter.SerializeToString(ex));
                return StatusCode((int)HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
            }
        }

        [HttpPut]
        [Route("")]
        public IActionResult Put([FromBody] TenantSubscriptionEntityInfo info)
        {
            try
            {   
                if (info==null) 
                    return BadRequest("Invalid parameter");              

                var stopwatch = StopwatchLogger.Start(_log);              
                _log.Info("Called SubscriptionEntityController Put {0}=", JsonConvert.SerializeObject(info)); 
                info.EntityId = _iMetadataManager.GetEntityContextByEntityName(info.EntityId);                 
                var retVal = _managerSubscriptionEntity.Update(TenantCode,info);
                stopwatch.StopAndLog("End SubscriptionEntityController Put");
                return Ok(retVal);
            }
            catch (Exception ex)
            {
                _log.Error(ExceptionFormatter.SerializeToString(ex));
                return StatusCode((int)HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
            }
        }

        [HttpDelete]
        [Route("{subscriptionEntityId:guid}")]   
        public IActionResult Delete(Guid subscriptionEntityId)
        {
            try
            {                 
                if (subscriptionEntityId==Guid.Empty) 
                    return BadRequest("Invalid parameter");

                var stopwatch = StopwatchLogger.Start(_log);              
                _log.Info("Called SubscriptionEntityController Delete {0}=", JsonConvert.SerializeObject(subscriptionEntityId));                 
                var retVal = _managerSubscriptionEntity.Delete(TenantCode,subscriptionEntityId);
                stopwatch.StopAndLog("End SubscriptionEntityController Delete");
                return Ok(retVal);
            }
            catch (Exception ex)
            {
                _log.Error(ExceptionFormatter.SerializeToString(ex));
                return StatusCode((int)HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
            }
        }

        [HttpGet]
        [Route("limitCount")]         
        public IActionResult GetLimitTypes()
        {
            try
            {  
              var stopwatch = StopwatchLogger.Start(_log); 
              _log.Info("Called SubscriptionEntityController Get All");                   
           
               var mainObj = new List<object>();     
               Dictionary<int, string> routeRx = DataUtility.GetIntFieldNameFromEnum(typeof (LimitTypes));
               foreach (var route in routeRx.OrderBy(u => u.Key))
                  {            
                   mainObj.Add(new
                        {
                            id = route.Key.ToString(CultureInfo.InvariantCulture),
                            name = DataUtility.GetEnumDescription((LimitTypes) route.Key)
                      });
                  } 
                stopwatch.StopAndLog("End SubscriptionEntityController Get all");
                return Ok(mainObj);
            }
            catch (Exception ex)
            {
                _log.Error(ExceptionFormatter.SerializeToString(ex));
                return StatusCode((int)HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
            }
        }  
 
    }
}