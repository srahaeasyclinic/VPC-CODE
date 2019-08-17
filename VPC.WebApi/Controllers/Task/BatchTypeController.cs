using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query;
using Newtonsoft.Json;
using NLog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using VPC.Entities.Common;
using VPC.Framework.Business.BatchItems.APIs;
using VPC.Framework.Business.DynamicQueryManager.Contracts;
using VPC.WebApi.Utility;


namespace VPC.WebApi.Controllers.WorkFlow
{
    [Route("api/batchtype")]
    public class BatchTypeController : BaseApiController
    {
        private readonly Logger _log= LogManager.GetCurrentClassLogger();
        private readonly IManagerBatchItem _managerBatchItem;
        public BatchTypeController(IManagerBatchItem managerBatchItem)
        {
            _managerBatchItem=managerBatchItem; 
        }   

        [HttpPut]
        [Route("tasks/runNow")]
        public IActionResult Put([FromBody] ItemName info)
        {
            try
            {                    
                var retVal = _managerBatchItem.BatchItemUpdateNextRunTime(TenantCode,info.Id); 
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