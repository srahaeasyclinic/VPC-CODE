using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
// using VPC.Entities.EntityCore.Model.Storage;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using VPC.Entities.Common;
using VPC.Entities.EntityCore.Metadata;
using VPC.Entities.EntityCore.Model.Setting;
using VPC.Framework.Business.Common;
using VPC.Framework.Business.SettingsManager.Contracts;
using VPC.WebApi.Utility;
using VPC.WebApi.Model;

namespace VPC.WebApi.Controllers.SettingsController
{
    [Route("api/settings")]
    // [Authorize(Policy = "AuthFunction")]
    public class SettingController : BaseApiController
    {
        private readonly Logger _log = LogManager.GetCurrentClassLogger();
        private readonly IJsonMessage _iJsonMessage;
        private readonly ISettingManager _iSettingManager;

        public SettingController(ISettingManager iSettingManager, IJsonMessage iJsonMessage)
        {

            _iJsonMessage = iJsonMessage;
            _iSettingManager = iSettingManager;
        }

        [HttpGet]
        [Route("")]
        [ProducesResponseType(200, Type = typeof(List<SettingModel>))]
        [ProducesResponseType(500)]
        [ProducesResponseType(404)]
        public IActionResult GetSetting(int pageIndex = 1, int pageSize = 10)
        {
            try
            {
                var tenantId = TenantCode;
                if (tenantId == Guid.Empty)
                {
                    return BadRequest("Tenant is invalid!");
                }
                var stopwatch = StopwatchLogger.Start(_log);
                _log.Info("Called SettingController GetSetting");

                List<SettingModel> retobj = new List<SettingModel>();

                try
                {
                    var ret = _iSettingManager.GetSettings(TenantCode);
                    retobj = ret.Select(s => new SettingModel
                    {
                        Content = s.Content,
                        Id = Convert.ToString(s.Id),
                        Context = s.Context,
                        ContextName = s.ContextName,
                        UpdatedBy = Convert.ToString(s.UpdatedBy),
                        UpdatedOn = s.UpdatedOn
                    }).ToList();
                }
                catch (Exception ex)
                {
                    _log.Error(ExceptionFormatter.SerializeToString(ex));
                    return BadRequest(ex.Message);
                }

                stopwatch.StopAndLog("GetSetting of SettingController");

                int PageSize = pageSize;
                int CurrentPage = pageIndex;
                dynamic totalRow = (int)Math.Ceiling(retobj.Count / (double)PageSize);
                var items = retobj.Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();

                // Parameter is passed from Query string if it is null then it default Value will be pageSize:10  



                return Ok(new { items, totalRow });
            }
            catch (Exception ex)
            {
                _log.Error(ExceptionFormatter.SerializeToString(ex));
                return StatusCode((int)HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
            }
        }

        [HttpGet]
        [Route("{contexttype}")]
        [ProducesResponseType(200, Type = typeof(SettingModel))]
        [ProducesResponseType(500)]
        [ProducesResponseType(404)]
        public IActionResult GetSettingByContext(string contexttype)
        {
            try
            {
                var tenantId = TenantCode;
                SettingContextTypeEnum contextenum;
                if (tenantId == Guid.Empty)
                {
                    return BadRequest("Tenant is invalid!");
                }

                if (!Enum.TryParse(contexttype, out contextenum))
                {
                    return BadRequest("ContextType is invalid!");
                }

                var stopwatch = StopwatchLogger.Start(_log);
                _log.Info("Called SettingController GetSettingByContext");

                SettingModel retobj = new SettingModel();
                // var dd = _iSettingManager.GetSenderNameByContext(tenantId, contextenum);

                var result = _iSettingManager.GetSettingsByContext(TenantCode, contextenum); //(ContextType)contexttype

                stopwatch.StopAndLog("GetSettingByContext of SettingController");


                return Ok(result);
            }
            catch (Exception ex)
            {
                _log.Error(ExceptionFormatter.SerializeToString(ex));
                return StatusCode((int)HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
            }
        }


        [HttpGet]
        [Route("{id:guid}")]
        [ProducesResponseType(200, Type = typeof(SettingModel))]
        [ProducesResponseType(500)]
        [ProducesResponseType(404)]
        public IActionResult GetSettingById(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("Setting Id required!");
            }
            try
            {
                var stopwatch = StopwatchLogger.Start(_log);
                _log.Info("Called SettingController GetSettingById");

                var result = _iSettingManager.GetSettingsById(TenantCode, id);

                stopwatch.StopAndLog("GetSettingById of SettingController");


                //var settings = new JsonSerializerSettings();
                //settings.NullValueHandling = NullValueHandling.Ignore;
                //settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                //return Json(result, settings);
                return Ok(result);


            }
            catch (Exception ex)
            {
                _log.Error(ExceptionFormatter.SerializeToString(ex));
                return StatusCode((int)HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
            }
        }



        [HttpPost]
        //[Route("{entityName}")]
        [Route("")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [ProducesResponseType(404)]
        public IActionResult CreateSetting([FromBody]List<SettingModel> Item)
        {
            if (Item == null)
                return BadRequest("Invalid parameter");
            var isinserted = new List<bool>();
            var stopwatch = StopwatchLogger.Start(_log);
            _log.Info("Called SettingsController CreateSettings");
            try
            {

                //if (string.IsNullOrEmpty(Item.Content))
                //    return BadRequest("Invalid parameter.");
                //if (Convert.ToString(Item.Context) == "")
                //    return BadRequest("Invalid parameter");

                foreach (var item in Item)
                {
                    // var CehckduplicatesettingByContext = _iSettingManager.GetSettings(TenantCode)?.Where(w => w.Context == item.Context).FirstOrDefault();

                    // if (CehckduplicatesettingByContext != null)
                    // {
                    //     return BadRequest(string.Format("Duplicate context, please try with different context!, "));
                    // }

                    isinserted.Add(_iSettingManager.CreateSetting(TenantCode, Mapfield(item)));

                }

                stopwatch.StopAndLog("CreateSetting method of SettingController");
                return Ok(isinserted.Contains(false) ? HttpStatusCode.ExpectationFailed : HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                _log.Error(ExceptionFormatter.SerializeToString(ex));
                return StatusCode((int)HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
            }
        }

        [HttpPut]
        [Route("")]
        [ProducesResponseType(200, Type = typeof(MenuItem))]
        [ProducesResponseType(500)]
        [ProducesResponseType(404)]
        public IActionResult UpdateSetting([FromBody]List<SettingModel> Item)
        {
            if (Item == null)
                return BadRequest("Invalid parameter");
            var isinserted = new List<bool>();
            try
            {
                var stopwatch = StopwatchLogger.Start(_log);
                _log.Info("Called SettingsController UpdateSettings");

                foreach (var item in Item)
                {

                    isinserted.Add(_iSettingManager.UpdateSetting(TenantCode, Mapfield(item)));
                }
                //if (string.IsNullOrEmpty(Item.Content))
                //    return BadRequest("Invalid parameter.");
                //if (Item.Context == 0)
                //    return BadRequest("Invalid parameter");
                //if (Item.Id == "")
                //    return BadRequest("Invalid parameter");

                stopwatch.StopAndLog("UpdateSettings method of SettingsController");
                return Ok(isinserted.Contains(false) ? HttpStatusCode.ExpectationFailed : HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                _log.Error(ExceptionFormatter.SerializeToString(ex));
                return StatusCode((int)HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
            }
        }

        [HttpDelete]
        [Route("{id:guid}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [ProducesResponseType(404)]
        public IActionResult DeleteSetting([FromRoute] Guid id)
        {
            try
            {
                var stopwatch = StopwatchLogger.Start(_log);
                _log.Info("Called SettingsController DeleteSettings with menuId {0}=", id);

                if (id == Guid.Empty)
                    return BadRequest("Settings id required !");

                var isUpdated = _iSettingManager.DeleteSetting(TenantCode, id);

                stopwatch.StopAndLog("DeleteSettings method of SettingsController");
                return Ok(isUpdated ? HttpStatusCode.OK : HttpStatusCode.NotAcceptable);
            }
            catch (Exception ex)
            {
                _log.Error(ExceptionFormatter.SerializeToString(ex));
                return StatusCode((int)HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
            }
        }

        [HttpGet]
        [Route("context")]
        public IActionResult GetContextType()
        {
            try
            {
                var stopwatch = StopwatchLogger.Start(_log);
                _log.Info("Called SettingsController Get Context type");

                List<ItemNameInt> ContextTypes = ((SettingContextTypeEnum[])Enum.GetValues(typeof(SettingContextTypeEnum))).Select(c => new ItemNameInt()
                {
                    Id = (int)c,
                    Name = DataUtility.GetEnumDescription((SettingContextTypeEnum)(int)c)
                }).ToList();

                stopwatch.StopAndLog("End SettingsController Get Context Type");
                return Ok(ContextTypes);
            }
            catch (Exception ex)
            {
                _log.Error(ExceptionFormatter.SerializeToString(ex));
                return StatusCode((int)HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
            }
        }

        private Setting Mapfield(SettingModel Inputobj)
        {
            Setting obj = new Setting();
            obj.Content = Inputobj.Content;
            obj.Id = string.IsNullOrEmpty(Inputobj.Id) ? Guid.Empty : new Guid(Inputobj.Id);
            obj.Context = Inputobj.Context;
            obj.ContextName = Inputobj.ContextName;
            obj.UpdatedBy = UserId;
            obj.UpdatedOn = DateTime.UtcNow;
            return obj;
        }

    }
}