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
    [Route("api/setting")]   
    [Authorize(Policy = "AuthRole")]   
    public class SubscriptionFeatureController : BaseApiController
    {
        private readonly Logger _log= LogManager.GetCurrentClassLogger();
        private readonly IManagerTenantSubscription _managerSubscription; 
        private IMetadataManager _iMetadataManager = new VPC.Framework.Business.MetadataManager.Contracts.MetadataManager();
        public SubscriptionFeatureController(IManagerTenantSubscription managerSubscription)
        {           
          _managerSubscription=managerSubscription;
        }  

        [HttpGet]
        [Route("features/{entityId}")]         
        public IActionResult GetFeatures(string entityId)
        {
            try
            {  
              var stopwatch = StopwatchLogger.Start(_log);              
              _log.Info("Called SubscriptionFeatureController Get features {0}=", JsonConvert.SerializeObject(entityId));
                entityId = _iMetadataManager.GetEntityContextByEntityName(entityId);                    
                var features=DataUtility.GetFeatureEntityWise(entityId);
                stopwatch.StopAndLog("End SubscriptionFeatureController Get features");
                return Ok(features);
            }
            catch (Exception ex)
            {
                _log.Error(ExceptionFormatter.SerializeToString(ex));
                return StatusCode((int)HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
            }
        } 

        [HttpGet]
        [Route("reports/{entityId}")]         
        public IActionResult GetReports(string entityId)
        {
            try
            {  
              var stopwatch = StopwatchLogger.Start(_log);              
              _log.Info("Called SubscriptionFeatureController get reports {0}=", JsonConvert.SerializeObject(entityId));   
                entityId = _iMetadataManager.GetEntityContextByEntityName(entityId);                   
                var features=DataUtility.GetReportEntityWise(entityId);
                stopwatch.StopAndLog("End SubscriptionFeatureController Get reports");
                return Ok(features);
            }
            catch (Exception ex)
            {
                _log.Error(ExceptionFormatter.SerializeToString(ex));
                return StatusCode((int)HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
            }
        } 

        [HttpGet]
        [Route("dashlets/{entityId}")]         
        public IActionResult GetDashlets(string entityId)
        {
            try
            {  
              var stopwatch = StopwatchLogger.Start(_log);              
              _log.Info("Called SubscriptionController Get dashlets {0}=", JsonConvert.SerializeObject(entityId));  
                entityId = _iMetadataManager.GetEntityContextByEntityName(entityId);                     
                var features=DataUtility.GetDashletEntityWise(entityId);
                stopwatch.StopAndLog("End SubscriptionFeatureController Get dashlets");
                return Ok(features);
            }
            catch (Exception ex)
            {
                _log.Error(ExceptionFormatter.SerializeToString(ex));
                return StatusCode((int)HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
            }
        } 

       
      
    }
}