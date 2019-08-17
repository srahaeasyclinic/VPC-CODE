using System;
using System.Collections.Generic;
using System.Net;
using System.Transactions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NLog;
using VPC.Entities.EntityCore.Metadata;
using VPC.Framework.Business.Menu.Contracts;
using VPC.Framework.Business.MetadataManager.Contracts;
using VPC.Framework.Business.ResourceManager.Contracts;
using VPC.WebApi.Utility;

namespace VPC.WebApi.Controllers.Menu
{
    [Route("api/menu")]
    public class MenuController : BaseApiController
    {
        private readonly Logger _log = LogManager.GetCurrentClassLogger();
        private readonly IMenuManager _iMenuManager;
        private readonly IJsonMessage _iJsonMessage;
        private readonly IMetadataManager _iMetadataManager;
        private readonly IResourceManager _resourceManager;

        public MenuController(IMenuManager iMenuManager, IJsonMessage iJsonMessage, IResourceManager resourceManager)
        {
            _iMenuManager = iMenuManager;
            _iJsonMessage = iJsonMessage;
            _iMetadataManager = new MetadataManager();
            _resourceManager = resourceManager;
        }

        [HttpGet]
        [Route("")]
        [ProducesResponseType(200, Type = typeof(List<MenuItem>))]
        [ProducesResponseType(500)]
        [ProducesResponseType(404)]
        public IActionResult GetMenu([FromQuery] int pageIndex, int pageSize, string groupName)
        {

            try
            {
                var stopwatch = StopwatchLogger.Start(_log);
                _log.Info("Called MenuController GetMenu");

                List<MenuItem> menus = new List<MenuItem>();

                try
                {
                    //var tenantId = new Guid("1C083115-7DB3-4B92-A449-D57FD1D2A52A");
                    menus = _iMenuManager.GetMenu(TenantCode, groupName, pageIndex, pageSize);
                    //menus = _iMenuManager.GetMenu(tenantId, groupName, pageIndex, pageSize);


                }
                catch (Exception ex)
                {
                    _log.Error(ExceptionFormatter.SerializeToString(ex));
                    return BadRequest("Incorrect Type.");
                }

                stopwatch.StopAndLog("GetMenu of MenuController");

                var settings = new JsonSerializerSettings();
                settings.NullValueHandling = NullValueHandling.Ignore;
                settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                return Json(menus, settings);
            }
            catch (Exception ex)
            {
                _log.Error(ExceptionFormatter.SerializeToString(ex));
                return StatusCode((int)HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
            }
        }

        [HttpGet]
        [Route("all")]
        [ProducesResponseType(200, Type = typeof(List<MenuItem>))]
        [ProducesResponseType(500)]
        [ProducesResponseType(404)]
        public IActionResult GetAllMenu()
        {
            try
            {
                var stopwatch = StopwatchLogger.Start(_log);
                _log.Info("Called MenuController GetMenu");

                List<MenuItem> Objmenu = new List<MenuItem>();

                try
                {

                    Objmenu = _iMenuManager.GetMenuBytenant(TenantCode);

                }
                catch (Exception ex)
                {
                    _log.Error(ExceptionFormatter.SerializeToString(ex));
                    return BadRequest("Incorrect Type.");
                }

                stopwatch.StopAndLog("GetMenu of MenuController");

                var settings = new JsonSerializerSettings();
                settings.NullValueHandling = NullValueHandling.Ignore;
                settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                return Json(Objmenu, settings);
            }
            catch (Exception ex)
            {
                _log.Error(ExceptionFormatter.SerializeToString(ex));
                return StatusCode((int)HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
            }
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ProducesResponseType(200, Type = typeof(MenuItem))]
        [ProducesResponseType(500)]
        [ProducesResponseType(404)]
        public IActionResult GetMenuById(Guid id)
        {
            var entityName = "";

            try
            {
                var stopwatch = StopwatchLogger.Start(_log);
                _log.Info("Called MenuController GetMenuById");

                var menu = _iMenuManager.GetMenuById(TenantCode, id);

                if (menu != null && menu.ReferenceEntityId != null && menu.ReferenceEntityId != "")
                {
                    if (menu.MenuTypeId == 1)
                    {
                        entityName = _iMetadataManager.GetEntityNameByEntityContext(menu.ReferenceEntityId, false);
                    }
                    else if (menu.MenuTypeId == 2)
                    {
                        entityName = _iMetadataManager.GetEntityNameByEntityContext(menu.ReferenceEntityId, true);
                    }
                    menu.ReferenceEntityId = entityName;
                }

                stopwatch.StopAndLog("GetMenuById of MenuController");

                if (menu != null)
                {
                    var settings = new JsonSerializerSettings();
                    settings.NullValueHandling = NullValueHandling.Ignore;
                    settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    return Json(menu, settings);
                }

                return NotFound("Menu not found");
            }
            catch (Exception ex)
            {
                _log.Error(ExceptionFormatter.SerializeToString(ex));
                return StatusCode((int)HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
            }
        }

        // [HttpGet]
        // [Route("menu-group/{name}")]
        // [ProducesResponseType(200, Type = typeof(List<MenuItem>))]
        // [ProducesResponseType(500)]
        // [ProducesResponseType(404)]
        // public IActionResult GetMenuByGroupName(string name)
        // {
        //    var entityName = "";

        //     try
        //     {
        //         var stopwatch = StopwatchLogger.Start(_log);
        //         _log.Info("Called MenuController GetMenuByGroupName");

        //         List<MenuItem> menus = new List<MenuItem>();

        //         try
        //         {
        //             //var tenantId = new Guid("1C083115-7DB3-4B92-A449-D57FD1D2A52A");
        //             menus = _iMenuManager.GetMenuByGroupName(TenantCode, name);
        //             //menus = _iMenuManager.GetMenu(tenantId, pageIndex, pageSize);

        //             foreach(var menuItem in menus)
        //             {
        //                 if(menuItem.ReferenceEntityId != null && menuItem.ReferenceEntityId != "")
        //                 {
        //                     if(menuItem.MenuTypeId != null && menuItem.MenuTypeId == 1)
        //                     {
        //                         entityName = _iMetadataManager.GetEntityNameByEntityContext(menuItem.ReferenceEntityId, false);
        //                     }
        //                     else if(menuItem.MenuTypeId != null && menuItem.MenuTypeId == 2)
        //                     {
        //                         entityName = _iMetadataManager.GetEntityNameByEntityContext(menuItem.ReferenceEntityId, true);
        //                     }
        //                     menuItem.ReferenceEntityId = entityName;                            
        //                 }

        //             }
        //         }
        //         catch (Exception ex)
        //         {
        //             _log.Error(ExceptionFormatter.SerializeToString(ex));
        //             return BadRequest("Incorrect Type.");
        //         }

        //         stopwatch.StopAndLog("GetMenuByGroupName of MenuController");

        //         var settings = new JsonSerializerSettings();
        //         settings.NullValueHandling = NullValueHandling.Ignore;
        //         settings.ContractResolver = new CamelCasePropertyNamesContractResolver();                    
        //         return Json(menus, settings);              
        //     }
        //     catch (Exception ex)
        //     {
        //         _log.Error(ExceptionFormatter.SerializeToString(ex));
        //         return StatusCode((int)HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
        //     }
        // }

        [HttpPost]
        [Route("")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [ProducesResponseType(404)]
        public IActionResult CreateMenu([FromBody]MenuItem menuItem)
        {
            try
            {
                var stopwatch = StopwatchLogger.Start(_log);
                _log.Info("Called MenuController CreateMenu");

                if (menuItem != null && menuItem.ReferenceEntityId != null)
                {
                    if (menuItem.MenuTypeId == 1)
                    {
                        menuItem.ReferenceEntityId = _iMetadataManager.GetEntityContextByEntityName(menuItem.ReferenceEntityId, false);
                    }
                    else if (menuItem.MenuTypeId == 2)
                    {
                        menuItem.ReferenceEntityId = _iMetadataManager.GetEntityContextByEntityName(menuItem.ReferenceEntityId, true);
                    }
                }

                using (TransactionScope ts = new TransactionScope ()) {
                    //create menu
                    var retVal = _iMenuManager.CreateMenu (menuItem, UserId, TenantCode);
                    _iMenuManager.ClearMenuCache (TenantCode);

                    //create resource
                    //get language key value
                    string langKey = "";
                    string langValue = "";
                    var retLan = _resourceManager.GetDefaultLanguageByTenant (TenantCode);
                    if (retLan != null && retLan.Key != null) {
                        langKey = Convert.ToString (retLan.Key);
                    }
                    if (retLan != null && retLan.Text != null) {
                        langValue = Convert.ToString (retLan.Text);
                    }

                    //check resource by key
                    var retRes = _resourceManager.GetResourcesByKey (TenantCode, menuItem.Menucode);
                    if (retRes.Count == 0) {
                        string msg = "";
                        _resourceManager.Create (TenantCode, new Entities.EntityCore.Model.Resource.Resource (menuItem.Menucode, menuItem.Name, langKey, langValue, menuItem.ReferenceEntityId, false), UserId, ref msg);
                    }

                    ts.Complete ();

                    stopwatch.StopAndLog ("CreateMenu method of MenuController");
                    return Ok (retVal);
                }
            }
            catch (Exception ex)
            {
                _log.Error(ExceptionFormatter.SerializeToString(ex));
                return StatusCode((int)HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
            }
        }

        [HttpPut]
        [Route("{id:guid}")]
        [ProducesResponseType(200, Type = typeof(MenuItem))]
        [ProducesResponseType(500)]
        [ProducesResponseType(404)]
        public IActionResult UpdateMenu(Guid id, [FromBody]MenuItem menuItem)
        {
            try
            {
                var stopwatch = StopwatchLogger.Start(_log);
                _log.Info("Called MenuController UpdateMenu");

                if (menuItem == null)
                {
                    return BadRequest("Menu required");
                }

                if (menuItem != null && menuItem.ReferenceEntityId != null)
                {
                    //menuItem.ReferenceEntityId = _iMetadataManager.GetEntityContextByEntityName(menuItem.ReferenceEntityId, false);
                    if (menuItem.MenuTypeId == 1)
                    {
                        menuItem.ReferenceEntityId = _iMetadataManager.GetEntityContextByEntityName(menuItem.ReferenceEntityId, false);
                    }
                    else if (menuItem.MenuTypeId == 2)
                    {
                        menuItem.ReferenceEntityId = _iMetadataManager.GetEntityContextByEntityName(menuItem.ReferenceEntityId, true);
                    }
                }

                menuItem.ModifiedBy = UserId;

                using (TransactionScope ts = new TransactionScope ()) {
                    //update resource
                    //get language key value
                    string langKey = "";
                    string langValue = "";
                    var retLan = _resourceManager.GetDefaultLanguageByTenant (TenantCode);
                    if (retLan != null && retLan.Key != null) {
                        langKey = Convert.ToString (retLan.Key);
                    }
                    if (retLan != null && retLan.Text != null) {
                        langValue = Convert.ToString (retLan.Text);
                    }

                    //get resource id
                    var retVal = _resourceManager.GetResourcesByMenuId (TenantCode, id);

                    if (retVal != null && retVal.Count > 0) {
                        if (retVal[0].Key != null) {
                            //if same key do nothing
                            if (retVal[0].Key.ToLower () != menuItem.Menucode.ToLower ()) {
                                //else delete
                                _resourceManager.DeleteByKey (TenantCode, retVal[0].Key);

                                //create resource
                                string msg = "";
                                _resourceManager.Create (TenantCode, new Entities.EntityCore.Model.Resource.Resource (menuItem.Menucode, menuItem.Name, langKey, langValue, menuItem.ReferenceEntityId, false), UserId, ref msg);
                            }
                        }
                    }                  

                     //update menu
                    _iMenuManager.UpdateMenu (TenantCode, id, menuItem);
                    _iMenuManager.ClearMenuCache (TenantCode);

                    ts.Complete ();
                }

                stopwatch.StopAndLog ("UpdateMenu method of MenuController");
                return Ok (HttpStatusCode.OK);
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
        public IActionResult DeleteMenu([FromRoute] Guid id)
        {
            try
            {
                var stopwatch = StopwatchLogger.Start(_log);
                _log.Info("Called MenuController DeleteMenu with menuId {0}=", id);

                if (id == Guid.Empty)
                    return BadRequest("Menu id required !");

                _iMenuManager.DeleteMenu(TenantCode, id);
                _iMenuManager.ClearMenuCache(TenantCode);
                stopwatch.StopAndLog("DeleteMenu method of MenuController");
                return Ok(true);
            }
            catch (Exception ex)
            {
                _log.Error(ExceptionFormatter.SerializeToString(ex));
                return StatusCode((int)HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
            }
        }

        [HttpGet]
        [Route("clear-cache")]
        public IActionResult Clear()
        {
            try
            {
                var retVal = _iMenuManager.ClearMenuCache(TenantCode);
                return this.Ok(retVal);
            }
            catch (Exception ex)
            {
                _log.Error(ExceptionFormatter.SerializeToString(ex));
                return StatusCode((int)HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
            }
        }

        [HttpPost]
        [Route("initilizationmenu")]
        public IActionResult initilizationMenu(Guid rootTenantCode, Guid initilizedTenantCode)
        {
            try
            {
                _iMenuManager.InitilizeParentMenuFromAPI(rootTenantCode, initilizedTenantCode,UserId);
                return this.Ok();
            }
            catch (Exception ex)
            {
                _log.Error(ExceptionFormatter.SerializeToString(ex));
                return StatusCode((int)HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
            }
        }

        [HttpPost]
        [Route ("initilizationresource")]
        public IActionResult initilizationResource (Guid initilizedTenantCode) {
            try {
                //get language key value
                string langKey = "";
                string langValue = "";
                var retLan = _resourceManager.GetDefaultLanguageByTenant (initilizedTenantCode);
                if (retLan != null && retLan.Key != null) {
                    langKey = Convert.ToString (retLan.Key);
                }
                if (retLan != null && retLan.Text != null) {
                    langValue = Convert.ToString (retLan.Text);
                }

                List<MenuItem> allmenus = _iMenuManager.GetMenuBytenant (initilizedTenantCode);

                if (allmenus != null && allmenus.Count > 0) {
                    foreach (var item in allmenus) {
                        if (item.Menucode != null && item.Menucode != "") {
                            var retRes = _resourceManager.GetResourcesByKey (initilizedTenantCode, item.Menucode);
                            if (retRes.Count == 0) {
                                string msg = "";
                                _resourceManager.Create (initilizedTenantCode, new Entities.EntityCore.Model.Resource.Resource (item.Menucode, item.Name, langKey, langValue, "", false), UserId, ref msg);
                            }
                        }

                    }
                }

                return this.Ok ();
                
            } catch (Exception ex) {
                _log.Error (ExceptionFormatter.SerializeToString (ex));
                return StatusCode ((int) HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
            }
        }
    }
}