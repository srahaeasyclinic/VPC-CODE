using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NLog;
using VPC.Entities.EntityCore.Metadata;
using VPC.Framework.Business.Menu.Contracts;
using VPC.Framework.Business.MetadataManager.Contracts;
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

        public MenuController(IMenuManager iMenuManager, IJsonMessage iJsonMessage)
        {
            _iMenuManager = iMenuManager;
            _iJsonMessage = iJsonMessage;
            _iMetadataManager = new MetadataManager();
        }

        [HttpGet]
        [Route("")]
        [ProducesResponseType(200, Type = typeof(List<MenuItem>))]
        [ProducesResponseType(500)]
        [ProducesResponseType(404)]
        public IActionResult GetMenu([FromQuery] int pageIndex, int pageSize, string groupName)
        {
            var entityName = "";

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

                    foreach (var menuItem in menus)
                    {
                        if (menuItem.ReferenceEntityId != null && menuItem.ReferenceEntityId != "")
                        {
                            if (menuItem != null && menuItem.MenuTypeId == 1)
                            {
                                entityName = _iMetadataManager.GetEntityNameByEntityContext(menuItem.ReferenceEntityId, false);
                            }
                            else if (menuItem != null && menuItem.MenuTypeId == 2)
                            {
                                entityName = _iMetadataManager.GetEntityNameByEntityContext(menuItem.ReferenceEntityId, true);
                            }
                            menuItem.ReferenceEntityId = entityName;
                        }

                    }
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

                var retVal = _iMenuManager.CreateMenu(menuItem, UserId, TenantCode);

                stopwatch.StopAndLog("CreateMenu method of MenuController");
                return Ok(retVal);
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

                _iMenuManager.UpdateMenu(TenantCode, id, menuItem);

                stopwatch.StopAndLog("UpdateMenu method of MenuController");
                return Ok(HttpStatusCode.OK);
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
                stopwatch.StopAndLog("DeleteMenu method of MenuController");
                return Ok(true);
            }
            catch (Exception ex)
            {
                _log.Error(ExceptionFormatter.SerializeToString(ex));
                return StatusCode((int)HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
            }
        }

    }
}