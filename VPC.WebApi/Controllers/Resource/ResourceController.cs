using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NLog;
using VPC.Framework.Business.MetadataManager.Contracts;
using VPC.Framework.Business.ResourceManager.Contracts;
using VPC.WebApi.Utility;
using VPC.Entities.EntityCore.Model.Resource;
using VPC.Framework.Business.Tenant;
using VPC.Metadata.Business.Entity.Infrastructure;

namespace VPC.WebApi.Controllers.Resource
{
    [Route("api/resources")]
    public class ResourceController : BaseApiController
    {

        private readonly Logger _log = LogManager.GetCurrentClassLogger();
        private readonly IResourceManager _resourceManager;
        private readonly ILayoutManager _iILayoutManager;


        public ResourceController(IResourceManager resourceManager, ILayoutManager layoutManager)
        {
            _resourceManager = resourceManager;
            _iILayoutManager = layoutManager;
        }

        [HttpGet]
        [Route("")]
        [ApiExplorerSettings(IgnoreApi = true)]
        [NonAction]
        public IActionResult GetAllResources()
        {
            try
            {

                var stopwatch = StopwatchLogger.Start(_log);
                _log.Info("Called ResourceController GetAllResources");

                var retVal = _resourceManager.GetResources(TenantCode).ToDictionary(x => x.Key, x => x.Value);

                stopwatch.StopAndLog("GetAllResources of ResourceController");
                return this.Ok(retVal);
            }
            catch (Exception ex)
            {
                _log.Error(ExceptionFormatter.SerializeToString(ex));
                return StatusCode((int)HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
            }
        }

        [HttpGet]
        [Route("defaultlanguage")]
        public IActionResult GetDefaultLanguage()
        {
            try
            {

                var stopwatch = StopwatchLogger.Start(_log);
                _log.Info("Called ResourceController GetDefaultLanguage");

                var retVal = _resourceManager.GetDefaultLanguageByTenant(TenantCode);

                stopwatch.StopAndLog("GetDefaultLanguage of ResourceController");
                return this.Ok(retVal);
            }
            catch (Exception ex)
            {
                _log.Error(ExceptionFormatter.SerializeToString(ex));
                return StatusCode((int)HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
            }
        }

        [HttpGet]
        [Route("")]
        public IActionResult GetAll([FromQuery] int pageIndex, int pageSize, string orderBy, string language = null)
        {
            try
            {

                var stopwatch = StopwatchLogger.Start(_log);
                _log.Info("Called ResourceController GetAllResources");

                int totalRowCount = 0;
                var resources = _resourceManager.GetResourcesDetails(language, TenantCode, pageIndex, pageSize, orderBy, ref totalRowCount);
                var totalRow = totalRowCount;
                stopwatch.StopAndLog("End ResourceController GetAllResources");
                return Ok(new { resources, totalRow });

            }
            catch (Exception ex)
            {
                _log.Error(ExceptionFormatter.SerializeToString(ex));
                return StatusCode((int)HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
            }
        }

        [HttpGet]
        [Route("all")]
        [CustomJsonFormatter("pascal")]
        public IActionResult GetAll([FromQuery] string language)
        {
            try
            {
                var stopwatch = StopwatchLogger.Start(_log);
                _log.Info("Called ResourceController GetAllResources");
                ;
                var retVal = _resourceManager.GetResources(TenantCode, language).ToDictionary(x => x.Key, x => x.Value);
                stopwatch.StopAndLog("GetAllResources of ResourceController");
                return this.Ok(retVal);
            }
            catch (Exception ex)
            {
                _log.Error(ExceptionFormatter.SerializeToString(ex));
                return StatusCode((int)HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
            }
        }

        [HttpGet]
        [Route("all/{key}/{language}")]
        public IActionResult GetAllByKeyLanguage(string key, string language)
        {
            try
            {
                var stopwatch = StopwatchLogger.Start(_log);
                _log.Info("Called ResourceController GetAllResources");

                var retVal = _resourceManager.GetResourcesByKeyAndLanguage(TenantCode, key, language);
                stopwatch.StopAndLog("GetAllResourcesByKeyAndLanguage of ResourceController");
                return this.Ok(retVal);
            }
            catch (Exception ex)
            {
                _log.Error(ExceptionFormatter.SerializeToString(ex));
                return StatusCode((int)HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
            }
        }

        [HttpGet]
        [Route("{key}/{language}")]
        public IActionResult GetDuplicateResourceKey(string key, string language)
        {
            try
            {
                var stopwatch = StopwatchLogger.Start(_log);
                _log.Info("Called ResourceController GetDuplicateResourceKey");

                var retVal = _resourceManager.GetDuplicateResourceKey(TenantCode, key, language);
                stopwatch.StopAndLog("GetDuplicateResourceKey of ResourceController");
                return this.Ok(retVal);
            }
            catch (Exception ex)
            {
                _log.Error(ExceptionFormatter.SerializeToString(ex));
                return StatusCode((int)HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
            }
        }

        [HttpPost]
        [Route("")]
        public IActionResult Post([FromBody] VPC.Entities.EntityCore.Model.Resource.Resource resource)
        {
            try
            {
                if (resource == null)
                    return BadRequest("Invalid parameter");
                if (string.IsNullOrEmpty(resource.Key))
                    return BadRequest("Invalid parameter");
                if (string.IsNullOrEmpty(resource.Value))
                    return BadRequest("Invalid parameter");

                var stopwatch = StopwatchLogger.Start(_log);
                _log.Info("Called ResourceController Post {0}=", JsonConvert.SerializeObject(resource));

                string strMsg = string.Empty;

                var retVal = _resourceManager.Create(TenantCode, resource, UserId, ref strMsg);

                if (!String.IsNullOrEmpty(strMsg) && retVal == false)
                {
                    var error = new
                    {
                        message = strMsg,
                        status = Microsoft.AspNetCore.Http.StatusCodes.Status208AlreadyReported
                    };
                    return (StatusCode((int)ErrorCodeEnum.Duplicate_Data)); //StatusCode((int)HttpStatusCode.AlreadyReported,new ObjectResult(error)
                }

                stopwatch.StopAndLog("End ResourceController Post");
                return Ok(retVal);
            }
            catch (Exception ex)
            {
                _log.Error(ExceptionFormatter.SerializeToString(ex));
                return StatusCode((int)HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
            }
        }

        [HttpPut]
        [Route("{resourceId}")]
        public IActionResult Put(Guid resourceId, [FromBody] VPC.Entities.EntityCore.Model.Resource.Resource resource)
        {
            try
            {
                var stopwatch = StopwatchLogger.Start(_log);
                _log.Info("Called ResourceController Put {0}=", JsonConvert.SerializeObject(resource));

                string strMsg = string.Empty;

                var retVal = _resourceManager.Update(resourceId, TenantCode, resource, UserId, ref strMsg);

                if (!String.IsNullOrEmpty(strMsg) && retVal == false)
                {
                    return StatusCode((int)HttpStatusCode.AlreadyReported, strMsg);
                }

                stopwatch.StopAndLog("End ResourceController Put");
                return Ok(retVal);
            }
            catch (Exception ex)
            {
                _log.Error(ExceptionFormatter.SerializeToString(ex));
                return StatusCode((int)HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
            }
        }

        [HttpDelete]
        [Route("{resourceKey}")]
        public IActionResult Delete(string resourceKey)
        {
            try
            {
                var stopwatch = StopwatchLogger.Start(_log);
                _log.Info("Called ResourceController Delete {0}=", JsonConvert.SerializeObject(resourceKey));
                bool retVal = false;
                Guid resourceId;
                if (Guid.TryParse(resourceKey, out resourceId))
                {
                    if (resourceId != null)
                    {
                        retVal = _resourceManager.Delete(TenantCode, resourceId);
                        stopwatch.StopAndLog("End ResourceController Delete");

                    }
                }
                else
                {
                    retVal = _resourceManager.DeleteByKey(TenantCode, resourceKey);
                    stopwatch.StopAndLog("End ResourceController Delete");
                }

                return Ok(retVal);
            }
            catch (Exception ex)
            {
                _log.Error(ExceptionFormatter.SerializeToString(ex));
                return StatusCode((int)HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
            }
        }


        [HttpGet]
        [Route("clear-cache")]
        public IActionResult Clear(Guid tenantId)
        {
            try
            {
                var retVal = _resourceManager.ClearResourceCache(tenantId);
                return this.Ok(retVal);
            }
            catch (Exception ex)
            {
                _log.Error(ExceptionFormatter.SerializeToString(ex));
                return StatusCode((int)HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
            }
        }

        [HttpPost]
        [Route("reset")]
        public IActionResult ResetResources()
        {
            try
            {
                List<Entities.EntityCore.Model.Resource.Resource> staticResourcesTenant = new List<Entities.EntityCore.Model.Resource.Resource>();

                // var resoucesDic = GetResouceDictionaryFromJsonFile();
                // foreach (string key in resoucesDic.Keys)
                //     staticResourcesTenant.Add(new Entities.EntityCore.Model.Resource.Resource(key, resoucesDic[key].ToString()));
                return this.Ok(_resourceManager.ResetResources(TenantCode, staticResourcesTenant));
            }
            catch (Exception ex)
            {
                _log.Error(ExceptionFormatter.SerializeToString(ex));
                return StatusCode((int)HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
            }
        }

        [HttpPost]
        [Route("repair")]

        public IActionResult RepairResources()
        {
            Dictionary<string, string> resourceStatic = new Dictionary<string, string>();
            //Dictionary<string, string> resourceRootTenant = new Dictionary<string, string>();

            try
            {
                var stopwatch = StopwatchLogger.Start(_log);
                _log.Info("Called ResourceController Repair Resources");

                var resourceTenant = _resourceManager.GetResources(TenantCode).ToDictionary(x => x.Key, x => x.Value); //, language
                resourceStatic = this.GetResouceDictionaryFromJsonFile();
                string msg = "";
                foreach (string key in resourceStatic.Keys)
                {
                    if (!resourceTenant.ContainsKey(key))
                        _resourceManager.Create(TenantCode, new Entities.EntityCore.Model.Resource.Resource(key, resourceStatic[key].ToString()), UserId, ref msg);
                }
                //var resourceRootTenant1 = _resourceManager.GetResourcesForRepair(TenantCode);
                //resourceRootTenant = resourceRootTenant1.ToDictionary(x => x.Key, x => x.Value); //, language
                //foreach (string key in resourceRootTenant.Keys)
                //{
                //    if (!resourceTenant.ContainsKey(key))
                //        _resourceManager.Create(TenantCode, new Entities.EntityCore.Model.Resource.Resource(key, resourceRootTenant[key].ToString()), UserId, ref msg);
                //}

                var resourceRootTenant = _resourceManager.GetResourcesForRepair(TenantCode);

                foreach (var item in resourceRootTenant)
                {
                    if (!resourceTenant.ContainsKey(item.Key))
                        _resourceManager.Create(TenantCode, new Entities.EntityCore.Model.Resource.Resource(item.Key, item.Value.ToString(), item.Language, item.LanguageName, item.EntityCode, false), UserId, ref msg);
                }
                stopwatch.StopAndLog("Repair Resources of Tenant in ResourceController");
                return this.Ok(true);

            }
            catch (Exception ex)
            {
                _log.Error(ExceptionFormatter.SerializeToString(ex));
                return StatusCode((int)HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
            }
        }

        private Dictionary<string, string> GetResouceDictionaryFromJsonFile()
        {
            try
            {
                String fileName = @"Resources\resource.json";
                Dictionary<string, string> jsonDic = new Dictionary<string, string>();

                using (System.IO.TextReader reader = new StreamReader(fileName))
                {
                    using (StreamReader sr = new StreamReader(fileName))
                    {
                        var json = sr.ReadToEnd();
                        jsonDic = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
                        return jsonDic;
                    }
                }
            }
            catch (Exception ex)
            {
                _log.Error(ExceptionFormatter.SerializeToString(ex));
                return null;
            }
        }
    }
}