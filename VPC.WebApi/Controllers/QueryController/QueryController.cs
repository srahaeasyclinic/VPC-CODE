using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using VPC.Entities.EntityCore.Model.Query;
using VPC.Framework.Business.DynamicQueryManager.Contracts;
using VPC.Framework.Business.EntityResourceManager.Contracts;
using VPC.Framework.Business.MetadataManager.Contracts;
using VPC.WebApi.Utility;
using NLog;

namespace VPC.WebApi.Controllers.EntityResourceController {
    [Route ("api/query")]
    public class QueryController : BaseApiController {

        private readonly Logger _log = LogManager.GetCurrentClassLogger();
        private readonly IEntityResourceManager _iEntityResourceManager;
        private readonly IPicklistManager _iPicklistManager;
        public QueryController (IEntityResourceManager iEntityResourceManager, IPicklistManager iPicklistManager) {
            _iEntityResourceManager = iEntityResourceManager;
            _iPicklistManager = iPicklistManager;
        }

        [HttpGet ("{entityName}")]
        public IActionResult GetQuery (string entityName, [FromQuery] string fields, string orderBy, string filter, int maxCount, int pageIndex, int pageSize) {
            try {
                var stopwatch = StopwatchLogger.Start(_log);
                _log.Info("Called QueryController GetQuery");

                if (string.IsNullOrEmpty (fields)) return BadRequest ("Field required");
                var tenantId = TenantCode;

                var queryContext = new QueryContext {
                    Fields = fields,
                    PageSize = pageSize,
                    PageIndex = pageIndex,
                    MaxResult = maxCount,
                };
                if (!string.IsNullOrEmpty (filter)) {
                    queryContext.Filters = CreateFilterObjFromString (filter);
                }
                if (!string.IsNullOrEmpty (orderBy)) {
                    queryContext.OrderBy = orderBy;
                }
                var query = _iEntityResourceManager.GetQueryResult (tenantId, entityName, queryContext);

                stopwatch.StopAndLog("GetQuery of QueryController");                
                return Ok (new { query });
            } catch (FieldAccessException fx) {
                //return BadRequest (fx.Message);
                _log.Error(ExceptionFormatter.SerializeToString(fx));
                return StatusCode((int)HttpStatusCode.BadRequest, ApiConstant.CustomErrorMessage);
            } catch (Exception ex) {
                //return StatusCode ((int) HttpStatusCode.InternalServerError, ex.Message);
                _log.Error(ExceptionFormatter.SerializeToString(ex));
                return StatusCode((int)HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
            }
        }

        private static List<QueryFilter> CreateFilterObjFromString (string filter) {
            var customFilters = new List<QueryFilter> ();
            var filterArr = filter.Split ('|');
            if (filterArr.Any ()) {

                foreach (var item in filterArr) {
                    var data = item.Split (',');
                    if (data.Any () && data.Count ().Equals (3)) {
                        var qF = new QueryFilter ();
                        qF.FieldName = data[0];
                        qF.Operator = data[1];
                        qF.Value = data[2];
                        customFilters.Add (qF);
                    }
                }

            };
            return customFilters;

        }

        [HttpGet ("{entityName}/execute")]
        public IActionResult ExecuteQuery (string entityName, [FromQuery] string fields, string orderBy, string filter, int maxCount, int pageIndex, int pageSize) {
            try
            {
                var stopwatch = StopwatchLogger.Start(_log);
                _log.Info("Called QueryController ExecuteQuery");

                if (string.IsNullOrEmpty(fields)) return BadRequest("Field required");
                var tenantId = TenantCode;

                var queryContext = new QueryContext
                {
                    Fields = fields,
                    PageSize = pageSize,
                    PageIndex = pageIndex,
                    MaxResult = maxCount,
                };
                if (!string.IsNullOrEmpty(filter))
                {
                    queryContext.Filters = CreateFilterObjFromString(filter);
                }
                if (!string.IsNullOrEmpty(orderBy))
                {
                    queryContext.OrderBy = orderBy;
                }

                var result = _iEntityResourceManager.GetResult(tenantId, entityName, queryContext);

                stopwatch.StopAndLog("ExecuteQuery of QueryController");

                if (result == null || result.Rows.Count <= 0)
                    return Ok(new { result = new List<string>(), totalRow = 0 });
                dynamic totalRow = result.Rows[0]["totalRow"];
                ApiHelper.MapDynamicColumn(result.Columns);
                return Ok(new { result, totalRow });
            }
            catch (FieldAccessException fx) {
                //return BadRequest (fx.Message);
                _log.Error(ExceptionFormatter.SerializeToString(fx));
                return StatusCode((int)HttpStatusCode.BadRequest, ApiConstant.CustomErrorMessage);

            } catch (Exception ex) {
                //return StatusCode ((int) HttpStatusCode.InternalServerError, ex.Message);
                _log.Error(ExceptionFormatter.SerializeToString(ex));
                return StatusCode((int)HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
            }
        }

        
    }
}