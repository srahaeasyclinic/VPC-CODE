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

namespace VPC.WebApi.Controllers.Rule
{
    [Route("api/rules")]
    // [Authorize(Policy = "AuthFunction")]
    //[EnableCors("AllowMyOrigin")]
    public class RulesController : BaseApiController
    {
        private readonly Logger _log = LogManager.GetCurrentClassLogger();
        private readonly IManageRule _manageRule;
        private readonly ILayoutManager _iILayoutManager;
        private IMetadataManager _iMetadataManager;
        private IManagerEntitySecurity _managerEntitySecurity;
        public RulesController(IManageRule managerRole, IMetadataManager iMetadataManager, ILayoutManager iILayoutManager, IManagerEntitySecurity managerEntitySecurity)
        {
            _manageRule = managerRole;
            _iMetadataManager = iMetadataManager;
            _iILayoutManager = iILayoutManager;
            _managerEntitySecurity = managerEntitySecurity;
        }
        [HttpGet]
        [Route("{entityName}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [ProducesResponseType(404)]
        public IActionResult Get(string entityName, [FromQuery] string fields, string searchText, string orderBy, string filters, int pageIndex, int pageSize)
        {
            try
            {
                var stopwatch = StopwatchLogger.Start(_log);
                _log.Info("Called RuleController Get All");
                var tenantId = TenantCode;



                var queryFilter = ApiHelper.GetQueryFilter(filters, tenantId, IsSystemAdmin, entityName);
                var queryContext = new Entities.EntityCore.Model.Query.QueryContext { Fields = fields, Filters = queryFilter, PageSize = pageSize, PageIndex = pageIndex, OrderBy = orderBy };

                if (!string.IsNullOrEmpty(searchText))
                {
                    var defaultLayout = _iILayoutManager.GetDefaultLayoutForEntity(TenantCode, entityName, (int)LayoutType.List, "", 0);
                    if (defaultLayout != null && defaultLayout.ListLayoutDetails != null && defaultLayout.ListLayoutDetails.SearchProperties != null)
                    {
                        var freeTextSearch = defaultLayout.ListLayoutDetails.SearchProperties.FirstOrDefault(t => t.Name.Equals("FreeTextSearch"));
                        if (freeTextSearch != null && freeTextSearch.Properties != null && freeTextSearch.Properties.Any())
                        {
                            queryContext.FreeTextSearch = new List<QueryFilter>();
                            foreach (var prop in freeTextSearch.Properties)
                            {
                                var filter = new QueryFilter();
                                filter.FieldName = prop.Name;
                                filter.Operator = Comparison.Like.ToString();
                                filter.Value = searchText;
                                queryContext.FreeTextSearch.Add(filter);
                            }
                        }
                    }
                }

                string entityId = null;
                if (!String.IsNullOrEmpty(entityName))
                {
                    entityId = _iMetadataManager.GetEntityContextByEntityName(entityName);
                }


                var retVal = _manageRule.GetAllRules(TenantCode, entityId);                
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

        [HttpGet]
        [Route("{entityName}/{Id:guid}")]
        public IActionResult Get(Guid Id, string entityName)
        {
            try
            {
                var stopwatch = StopwatchLogger.Start(_log);
                _log.Info("Called RuleController Get {0}=", JsonConvert.SerializeObject(Id));

                string entityId = null;
                if (!String.IsNullOrEmpty(entityName))
                {
                    entityId = _iMetadataManager.GetEntityContextByEntityName(entityName);
                }
                var ruleItem = _manageRule.GetRuleById(TenantCode, Id, entityId);
                stopwatch.StopAndLog("End RuleController Get");
                return Ok(ruleItem);
            }
            catch (Exception ex)
            {
                _log.Error(ExceptionFormatter.SerializeToString(ex));
                return StatusCode((int)HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
            }
        }



        [HttpPost]
        [Route("{entityName}")]
        public IActionResult Post([FromBody] RuleInfo rule, string entityName)
        {
            try
            {
                if (rule == null)
                    return BadRequest("Invalid parameter");
                if (string.IsNullOrEmpty(rule.RuleName))
                    return BadRequest("Invalid parameter.");
                if (rule.RuleType == 0)
                    return BadRequest("Invalid parameter");

                var stopwatch = StopwatchLogger.Start(_log);
                _log.Info("Called RuleController Post {0}=", JsonConvert.SerializeObject(rule));

                
                if (!String.IsNullOrEmpty(entityName))
                {
                    rule.EntityId = _iMetadataManager.GetEntityContextByEntityName(entityName);
                }
                string strMsg = string.Empty;
                var retVal = _manageRule.Create(TenantCode, rule, ref strMsg);

                if (!String.IsNullOrEmpty(strMsg) && retVal == false)
                {
                    return StatusCode((int)HttpStatusCode.AlreadyReported, strMsg);
                }


                stopwatch.StopAndLog("End RuleController Post");
                return Ok(retVal);
            }
            catch (Exception ex)
            {
                _log.Error(ExceptionFormatter.SerializeToString(ex));
                return StatusCode((int)HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
            }
        }

        [HttpPut]
        [Route("{entityName}")]
        public IActionResult Put([FromBody] RuleInfo rule, string entityName)
        {
            try
            {
                var stopwatch = StopwatchLogger.Start(_log);
                _log.Info("Called RuleController Put {0}=", JsonConvert.SerializeObject(rule));

                if (!String.IsNullOrEmpty(rule.EntityName))
                {
                    rule.EntityId = _iMetadataManager.GetEntityContextByEntityName(entityName);
                }

                string strMsg = string.Empty;
                var retVal = _manageRule.Update(TenantCode, rule, ref strMsg);

                if (!String.IsNullOrEmpty(strMsg) && retVal == false)
                {
                    return StatusCode((int)HttpStatusCode.AlreadyReported, strMsg);
                }

                stopwatch.StopAndLog("End RuleController put");
                return Ok(retVal);
            }
            catch (Exception ex)
            {
                _log.Error(ExceptionFormatter.SerializeToString(ex));
                return StatusCode((int)HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
            }
        }

        [HttpDelete]
        [Route("{entityName}/{Id:guid}")]
        public IActionResult Delete(string entityName, Guid Id)
        {
            try
            {
                var stopwatch = StopwatchLogger.Start(_log);
                _log.Info("Called RuleController Delete {0}=", JsonConvert.SerializeObject(Id));
                var retVal = _manageRule.Delete(TenantCode, Id);
                stopwatch.StopAndLog("End RuleController Delete");
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