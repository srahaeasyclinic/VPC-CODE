using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NLog;
using VPC.Entities.EntityCore.Model.Storage;
using VPC.Framework.Business.Initilize.Contracts;
using VPC.Framework.Business.MetadataManager.Contracts;
using VPC.WebApi.Utility;

namespace VPC.WebApi.Controllers.InitilizationController
{
    [Route("api/initialization")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public class InitilizationController : BaseApiController
    {
        private readonly Logger _log = LogManager.GetCurrentClassLogger();
        private readonly IInitilizeManager _initilizeManager;
        public InitilizationController(IInitilizeManager initilizeManager)
        {
            _initilizeManager = initilizeManager;
        }


         [HttpPost("ajay")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [ProducesResponseType(404)]
        public IActionResult Initialize()
        {
            try
            {
                ILayoutManager _iLayoutManager=new LayoutManager();
                var  userLayouts = _iLayoutManager.GetLayoutsByEntityName(TenantCode, "User");
                if(userLayouts.Count>0)
                {
                    var  roleLayout = _iLayoutManager.GetLayoutsByEntityName(TenantCode, "Role");
                    LayoutModel viewLayout=new LayoutModel();
                    if(roleLayout.Count>0)
                    {
                        viewLayout=(from roleLay in roleLayout where roleLay.LayoutType==LayoutType.View select roleLay).FirstOrDefault();
                    }

                    foreach(var userLayout in userLayouts)
                    {
                        if(userLayout.LayoutType==LayoutType.Form)
                        {
                            var details=_iLayoutManager.GetLayoutsDetailsById(TenantCode,userLayout.Id);
                            if(details.FormLayoutDetails!=null)
                            {
                                 foreach(var detail in details.FormLayoutDetails.Fields)
                                 {
                                     if(detail.DataType=="Section")
                                     {
                                         foreach(var innerField in detail.Fields)
                                         {
                                             if(innerField.DataType=="Role" && innerField.Name=="UserInRole")
                                             {
                                                 innerField.SelectedView=viewLayout.Id;

                                                  details.Layout = JsonConvert.SerializeObject(details.FormLayoutDetails, new JsonSerializerSettings
                                                    {
                                                        ContractResolver = new CamelCasePropertyNamesContractResolver()
                                                    });

                                                  _iLayoutManager.UpdateLayoutDetails(TenantCode,userLayout.Id,details);
                                             }
                                         }
                                      }
                                     else if(detail.DataType=="Tabs")
                                     {
                                         foreach(var innerField in detail.Fields)
                                         {
                                             if(innerField.DataType=="Role" && innerField.Name=="UserInRole")
                                             {
                                                 innerField.SelectedView=viewLayout.Id;

                                                  details.Layout = JsonConvert.SerializeObject(details.FormLayoutDetails, new JsonSerializerSettings
                                                    {
                                                        ContractResolver = new CamelCasePropertyNamesContractResolver()
                                                    });

                                                  _iLayoutManager.UpdateLayoutDetails(TenantCode,userLayout.Id,details);
                                             }

                                         }

                                      }
                                      else if(detail.DataType=="Tabs")
                                     {
                                         foreach(var innerField in detail.Fields)
                                         {
                                             if(innerField.DataType=="Role" && innerField.Name=="UserInRole")
                                             {
                                                 innerField.SelectedView=viewLayout.Id;

                                                  details.Layout = JsonConvert.SerializeObject(details.FormLayoutDetails, new JsonSerializerSettings
                                                    {
                                                        ContractResolver = new CamelCasePropertyNamesContractResolver()
                                                    });

                                                  _iLayoutManager.UpdateLayoutDetails(TenantCode,userLayout.Id,details);
                                             }

                                         }

                                      }
                                      
                                      else if(detail.DataType=="Role" && detail.Name=="UserInRole")
                                             {
                                                 detail.SelectedView=viewLayout.Id;

                                                  details.Layout = JsonConvert.SerializeObject(details.FormLayoutDetails, new JsonSerializerSettings
                                                    {
                                                        ContractResolver = new CamelCasePropertyNamesContractResolver()
                                                    });

                                                  _iLayoutManager.UpdateLayoutDetails(TenantCode,userLayout.Id,details);
                                             }


                                 }
                            }                          

                           
                        }
                    }

                

                }

                
                return Ok(true);
            }
            catch (FieldAccessException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _log.Error(ExceptionFormatter.SerializeToString(ex));
                return StatusCode((int)HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
            }
        }

        [HttpPost("tenants/{tenantCode}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [ProducesResponseType(404)]
        public IActionResult Initialize([FromRoute] Guid tenantCode)
        {
            try
            {
                var stopwatch = StopwatchLogger.Start(_log);
                _log.Info("Called InitilizationController Initialize with tenantCode {0}=", tenantCode);

                if (tenantCode != Guid.Empty)
                {
                    return BadRequest("tenant code required!");
                }

                //string tenantcode = "GCTT001";
                _initilizeManager.Initilize(tenantCode, new List<string> { "EN10003" }, UserId,Guid.Empty);

                stopwatch.StopAndLog("Initialize method of InitilizationController.");
                return Ok(true);
            }
            catch (FieldAccessException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _log.Error(ExceptionFormatter.SerializeToString(ex));
                return StatusCode((int)HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
            }
        }

         [HttpPost("workflows")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [ProducesResponseType(404)]
        public IActionResult InitializeRootTenantWorkFlow()
        {
            try
            {
                var stopwatch = StopwatchLogger.Start(_log);              
                _initilizeManager.InitializeRootTenantWorkFlow(TenantCode);
                stopwatch.StopAndLog("InitializeRootTenantWorkFlow method of InitilizationController.");
                return Ok(true);
            }
            catch (FieldAccessException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _log.Error(ExceptionFormatter.SerializeToString(ex));
                return StatusCode((int)HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
            }
        }

        [HttpPost("workflows/{entityName}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [ProducesResponseType(404)]
        public IActionResult InitializeRootTenantWorkFlow(string entityName)
        {
            try
            {
                var stopwatch = StopwatchLogger.Start(_log);              
                _initilizeManager.InitializeRootTenantWorkFlow(TenantCode,entityName);
                stopwatch.StopAndLog("InitializeRootTenantWorkFlow method of InitilizationController.");
                return Ok(true);
            }
            catch (FieldAccessException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _log.Error(ExceptionFormatter.SerializeToString(ex));
                return StatusCode((int)HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
            }
        }

    }
}