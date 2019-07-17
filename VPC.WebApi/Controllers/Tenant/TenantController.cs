using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NLog;
using VPC.Entities.Rule;
using VPC.Framework.Business.EntitySecurity.APIs;
using VPC.Framework.Business.MetadataManager.Contracts;
using VPC.Framework.Business.Rule.Contracts;
using VPC.WebApi.Utility;
using VPC.Framework.Business.DynamicQueryManager.Core.Enums;
using VPC.Entities.EntityCore.Model.Query;
using VPC.Entities.EntityCore.Model.Storage;

namespace VPC.WebApi.Controllers.Tenant
{
    [Route("api/tenants")]
    [ApiController]
    public class TenantController : BaseApiController
    {
        private readonly Logger _log = LogManager.GetCurrentClassLogger();
        private readonly IManageTenant _manageTenant;
        private readonly ILayoutManager _iILayoutManager;
        private IMetadataManager _iMetadataManager;
        private IManagerEntitySecurity _managerEntitySecurity;
        public TenantController(IManageTenant manageTenant, IMetadataManager iMetadataManager, ILayoutManager iILayoutManager, IManagerEntitySecurity managerEntitySecurity)
        {
            _manageTenant = manageTenant;
            _iMetadataManager = iMetadataManager;
            _iILayoutManager = iILayoutManager;
            _managerEntitySecurity = managerEntitySecurity;
        }
        [HttpGet]
        [Route("")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [ProducesResponseType(404)]
        public IActionResult Get()
        {
            try
            {
                var stopwatch = StopwatchLogger.Start(_log);
                _log.Info("Called TenantController Get All");
                var retVal = _manageTenant.GetTenantInfo(TenantCode);
                dynamic totalRow = retVal.Count;
                stopwatch.StopAndLog("End RuleController Get all");
                return Ok(new { retVal, totalRow });

            }
            catch (FieldAccessException fx)
            {
                _log.Error(ExceptionFormatter.SerializeToString(fx));
                return StatusCode((int)HttpStatusCode.BadRequest, ApiConstant.CustomErrorMessage);
            }
            catch (Exception ex)
            {
                _log.Error(ExceptionFormatter.SerializeToString(ex));
                return StatusCode((int)HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
            }
        }
    }
}