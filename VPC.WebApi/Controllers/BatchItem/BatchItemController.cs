using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NLog;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using VPC.Entities.BatchType;
using VPC.Entities.Common;
using VPC.Entities.SchedulerConfiguration;
using VPC.Entities.WorkFlow.Engine.Email;
using VPC.Framework.Business.BatchItems.APIs;
using VPC.Framework.Business.BatchType.Contracts;
using VPC.Framework.Business.Common;
using VPC.Framework.Business.MetadataManager.Contracts;
using VPC.Framework.Business.SchedulerConfiguration;
using VPC.WebApi.Attribute;
using VPC.WebApi.Utility;


namespace VPC.WebApi.Controllers.BatchType
{
   [Route("api/batche/items")]    
    public class BatchItemController : BaseApiController
    {
         private readonly Logger _log= LogManager.GetCurrentClassLogger();         
         private readonly IManagerBatchItem _managerBatchItem;
          private IMetadataManager _iMetadataManager;
    
      
        public BatchItemController(IManagerBatchItem managerBatchItem,IMetadataManager iMetadataManager)
        {             
            _managerBatchItem=managerBatchItem;    
            _iMetadataManager=  new VPC.Framework.Business.MetadataManager.Contracts.MetadataManager();
        }    

        [HttpGet]
        [Route("{batchTypeId:guid}")]
        public IActionResult Get(Guid batchTypeId,[FromQuery] BatchItemTypeEnum itemType)
        {
            try
            {               
                var stopwatch = StopwatchLogger.Start(_log); 
                _log.Info("Called BatchItemController Get {0}=", JsonConvert.SerializeObject(batchTypeId)); 
                var items = _managerBatchItem.GetBatchItemListByStatus(TenantCode,batchTypeId,itemType);   
                foreach(var item in items)                         
                {
                   item.EntityId = _iMetadataManager.GetEntityNameByEntityContext(item.EntityId);
                   item.RetryCount= item.RetryCount>0 ? (item.RetryCount-1) : item.RetryCount;
                }
                stopwatch.StopAndLog("End BatchItemController Get");
                return Ok(items);
            }
            catch (Exception ex)
            {
                _log.Error(ExceptionFormatter.SerializeToString(ex));
                return StatusCode((int)HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
            }
        }          

        
        // [HttpGet]
        // [Route("")]
         
        // public IActionResult Get()
        // {
        //     try
        //     {  
        //         var stopwatch = StopwatchLogger.Start(_log); 
        //          _log.Info("Called BatchTypeController Get All");  
        //         //  var allBatchTypes =DataUtility.GetAllBatchTypes();
        //         //  var savesBatches = _managerBatchType.GetBatchTypes(TenantCode);   
        //         //  foreach(var allBatchType in allBatchTypes)
        //         //  {
        //         //      var checkBatchExists=(from savesBatche in savesBatches where savesBatche.Context==allBatchType.Context select savesBatche).FirstOrDefault();
        //         //      if(checkBatchExists!=null)
        //         //      {
        //         //          allBatchType.BatchTypeId=checkBatchExists.BatchTypeId;
        //         //          allBatchType.Status=checkBatchExists.Status;
        //         //          allBatchType.IdleTime=checkBatchExists.IdleTime;
        //         //          allBatchType.Priority=checkBatchExists.Priority;
        //         //          allBatchType.ItemRetryCount=checkBatchExists.ItemRetryCount;
        //         //          allBatchType.ItemTimeout=checkBatchExists.ItemTimeout;
        //         //      }

        //         //  }
                 
        //         stopwatch.StopAndLog("End BatchTypeController Get all");
        //         return Ok();
        //     }
        //     catch (Exception ex)
        //     {
        //         _log.Error(ExceptionFormatter.SerializeToString(ex));
        //         return StatusCode((int)HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
        //     }
        // }

        // [HttpGet]
        // [Route("{batchTypeId:guid}")]
        // public IActionResult Get(Guid batchTypeId)
        // {
        //     try
        //     {               
        //         var stopwatch = StopwatchLogger.Start(_log); 
        //         _log.Info("Called BatchTypeController Get {0}=", JsonConvert.SerializeObject(batchTypeId)); 
        //         var item = _managerBatchType.GetBatchType(TenantCode,batchTypeId);                            
        //         stopwatch.StopAndLog("End BatchTypeController Get");
        //         return Ok(item);
        //     }
        //     catch (Exception ex)
        //     {
        //         _log.Error(ExceptionFormatter.SerializeToString(ex));
        //         return StatusCode((int)HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
        //     }
        // }              
      

        // [HttpPost]
        // [Route("")]
        // public IActionResult Post([FromBody] BatchTypeInfo info)
        // {
        //     try
        //     {   
        //         if (info==null) 
        //             return BadRequest("Invalid parameter");                   

        //         var stopwatch = StopwatchLogger.Start(_log);              
        //         _log.Info("Called BatchTypeController Post {0}=", JsonConvert.SerializeObject(info));                 
        //         var retVal = _managerBatchType.Create(TenantCode,info);
        //         stopwatch.StopAndLog("End BatchTypeController Post");
        //         return Ok(retVal);
        //     }
        //     catch (Exception ex)
        //     {
        //         _log.Error(ExceptionFormatter.SerializeToString(ex));
        //         return StatusCode((int)HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
        //     }
        // }

        // [HttpPut]
        // [Route("")]
        // public IActionResult Put([FromBody] BatchTypeInfo info)
        // {
        //      try
        //     {               
        //         var stopwatch = StopwatchLogger.Start(_log);              
        //         _log.Info("Called BatchTypeController Put {0}=", JsonConvert.SerializeObject(info));                 
        //         var retVal = _managerBatchType.Update(TenantCode,info);              
        //         stopwatch.StopAndLog("End BatchTypeController put");
        //         return Ok(retVal);
        //     }
        //     catch (Exception ex)
        //     {
        //         _log.Error(ExceptionFormatter.SerializeToString(ex));
        //         return StatusCode((int)HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
        //     }
        // }


        // [HttpPut]
        // [Route("item/nextRunTime")]
        // public IActionResult UpdateBatchItemNextRunTime([FromBody] VPC.Entities.BatchType.BatchType info)
        // {
        //      try
        //     {               
        //         var stopwatch = StopwatchLogger.Start(_log);              
        //         _log.Info("Called BatchTypeController UpdateBatchItemNextRunTime Put {0}=", JsonConvert.SerializeObject(info));                 
        //         var retVal = _managerBatchItem.BatchItemUpdateNextRunTime(TenantCode);              
        //         stopwatch.StopAndLog("End BatchTypeController UpdateBatchItemNextRunTime put");
        //         return Ok(retVal);
        //     }
        //     catch (Exception ex)
        //     {
        //         _log.Error(ExceptionFormatter.SerializeToString(ex));
        //         return StatusCode((int)HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
        //     }
        // }



        // [HttpDelete]
        // [Route("{batchTypeId:guid}")]
        // public IActionResult Delete(Guid batchTypeId)
        // {
        //      try
        //     {               
        //         var stopwatch = StopwatchLogger.Start(_log);              
        //         _log.Info("Called RoleConBatchTypeControllertroller Delete {0}=", JsonConvert.SerializeObject(batchTypeId));                 
        //         var retVal = _managerBatchType.Delete(TenantCode,batchTypeId);
        //         stopwatch.StopAndLog("End BatchTypeController Delete");
        //         return Ok(retVal);
        //     }
        //     catch (Exception ex)
        //     {
        //         _log.Error(ExceptionFormatter.SerializeToString(ex));
        //         return StatusCode((int)HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
        //     }
        // } 

        // [HttpPatch]
        // [Route("{batchTypeId:guid}")]
        // public IActionResult Patch(Guid batchTypeId)
        // {
        //      try
        //     {               
        //         var stopwatch = StopwatchLogger.Start(_log);              
        //         _log.Info("Called BatchTypeController Status {0}=", JsonConvert.SerializeObject(batchTypeId));                 
        //         var retVal = _managerBatchType.UpdateStatus(TenantCode,batchTypeId);
        //         stopwatch.StopAndLog("End BatchTypeController Status");
        //         return Ok(retVal);
        //     }
        //     catch (Exception ex)
        //     {
        //         _log.Error(ExceptionFormatter.SerializeToString(ex));
        //         return StatusCode((int)HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
        //     }
        // } 
 
       
    }
}