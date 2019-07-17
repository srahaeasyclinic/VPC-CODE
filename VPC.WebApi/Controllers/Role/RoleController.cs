using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using VPC.Entities.Common;
using VPC.Entities.Common.Functions;
using VPC.Entities.EntityCore;
using VPC.Entities.EntitySecurity;
using VPC.Entities.Role;
using VPC.Entities.WorkFlow;
using VPC.Framework.Business.Common;
using VPC.Framework.Business.EntitySecurity.APIs;
using VPC.Framework.Business.MetadataManager.Contracts;
using VPC.Framework.Business.Role.APIs;
using VPC.Framework.Business.WorkFlow.Contracts;
using VPC.WebApi.Attribute;
using VPC.WebApi.Utility;


namespace VPC.WebApi.Controllers.WorkFlow
{
   [Route("api/role")]
//    [AddHeaderFunction(OrgnizationFunctionContexts.Role)]
   //[Authorize(Policy = "AuthFunction")]   
    public class RoleController : BaseApiController
    {
        private readonly Logger _log= LogManager.GetCurrentClassLogger();
         private readonly IManagerRole _managerRole; 
         private IMetadataManager _iMetadataManager ;
         private IManagerEntitySecurity _managerEntitySecurity;
        public RoleController(IManagerRole managerRole,IMetadataManager iMetadataManager,IManagerEntitySecurity managerEntitySecurity)
        {
            _managerRole = managerRole;   
            _iMetadataManager=iMetadataManager;   
            _managerEntitySecurity=managerEntitySecurity;       
        }             

        
        [HttpGet]
        [Route("")]
         
        public IActionResult Get()
        {
            try
            {  
                var stopwatch = StopwatchLogger.Start(_log); 
                 _log.Info("Called RoleController Get All");                   
                var retVal = _managerRole.Roles(TenantCode);    
                stopwatch.StopAndLog("End RoleController Get all");
                return Ok(retVal);
            }
            catch (Exception ex)
            {
                _log.Error(ExceptionFormatter.SerializeToString(ex));
                return StatusCode((int)HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
            }
        }

        [HttpGet]
        [Route("{roleId:guid}")]
        public IActionResult Get(Guid roleId)
        {
            try
            {               
                var stopwatch = StopwatchLogger.Start(_log); 
                _log.Info("Called RoleController Get {0}=", JsonConvert.SerializeObject(roleId)); 
                var roleItem = _managerRole.Role(TenantCode,roleId);                            
                stopwatch.StopAndLog("End RoleController Get");
                return Ok(roleItem);
            }
            catch (Exception ex)
            {
                _log.Error(ExceptionFormatter.SerializeToString(ex));
                return StatusCode((int)HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
            }
        }
      


        // [HttpGet]
        // [Route("details/{roleId:Guid}")]
        // public IActionResult GetDetails(Guid roleId)
        // {
        //     try
        //     {               
        //         var stopwatch = StopwatchLogger.Start(_log); 
        //         _log.Info("Called RoleController Get {0}=", JsonConvert.SerializeObject(roleId));     
                

        //         var tenantCode = TenantCode;
        //         var userId = UserId;
        //         var isSuperAdmin = IsSuperAdmin;
        //         var allEntities =  _iMetadataManager.GetEntities(false);               
        //         var roleItem = _managerRole.Role(TenantCode,roleId);
        //         roleItem.Entity=new List<RoleMapperEntityInfo>();
        //         var entitySecurities=_managerEntitySecurity.GetEntitySecurity(tenantCode,roleId);
        //         var accessLevel = _managerEntitySecurity.GetAccessLevel();
        //         var operationLevel = _managerEntitySecurity.GetOperationLevel(); 
        //         foreach(var allEnt in allEntities)
        //         {                         
        //             var mapEntity=new RoleMapperEntityInfo{ 
        //                 Name=allEnt.Name,
        //                 DisplayName=allEnt.DisplayName,
        //                 Data=(from entitySecuritie in entitySecurities where entitySecuritie.EntityId==allEnt.Name select entitySecuritie).FirstOrDefault(),
        //                 AccessLevel = RoleMapper.GetAccessLevels(accessLevel,new Guid(AccessLevelGuid.RoleEntity)),
        //                 OperationLevel = RoleMapper.GetAccessLevels(operationLevel,new Guid(AccessLevelGuid.RoleEntity))
        //                     };

        //                     roleItem.Entity.Add(mapEntity);                      
        //         }  
        //         stopwatch.StopAndLog("End RoleController Get");
        //         return Ok(roleItem);
        //     }
        //     catch (Exception ex)
        //     {
        //         _log.Error(ExceptionFormatter.SerializeToString(ex));
        //         return StatusCode((int)HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
        //     }
        // }
      

        [HttpPost]
        [Route("")]
        public IActionResult Post([FromBody] RoleInfo role)
        {
            try
            {   
                     if (role==null) 
                         return BadRequest("Invalid parameter");
                     if (string.IsNullOrEmpty(role.Name) )
                         return BadRequest("Invalid parameter.");
                     if (role.RoleType==0) 
                         return BadRequest("Invalid parameter");

                var stopwatch = StopwatchLogger.Start(_log);              
                _log.Info("Called RoleController Post {0}=", JsonConvert.SerializeObject(role)); 
                role.AuditDetail=new AuditDetail{ModifiedBy=UserId};            
                var retVal = _managerRole.Create(TenantCode,role);
                stopwatch.StopAndLog("End RoleController Post");
                return Ok(retVal);
            }
            catch (Exception ex)
            {
                _log.Error(ExceptionFormatter.SerializeToString(ex));
                return StatusCode((int)HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
            }
        }

        [HttpPut]
        [Route("")]
        public IActionResult Put([FromBody] RoleInfo role)
        {
             try
            {               
                var stopwatch = StopwatchLogger.Start(_log);              
                _log.Info("Called RoleController Put {0}=", JsonConvert.SerializeObject(role));                 
                var retVal = _managerRole.Update(TenantCode,role);              
                stopwatch.StopAndLog("End RoleController put");
                return Ok(retVal);
            }
            catch (Exception ex)
            {
                _log.Error(ExceptionFormatter.SerializeToString(ex));
                return StatusCode((int)HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
            }
        }

        [HttpDelete]
        [Route("{roleId:guid}")]
        public IActionResult Delete(Guid roleId)
        {
             try
            {               
                var stopwatch = StopwatchLogger.Start(_log);              
                _log.Info("Called RoleController Delete {0}=", JsonConvert.SerializeObject(roleId));                 
                var retVal = _managerRole.Delete(TenantCode,roleId);
                stopwatch.StopAndLog("End RoleController Delete");
                return Ok(retVal);
            }
            catch (Exception ex)
            {
                _log.Error(ExceptionFormatter.SerializeToString(ex));
                return StatusCode((int)HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
            }
        }  

        [HttpGet]
        [Route("type")]
        public IActionResult GetRoleType()
        {
            try
            {               
                var stopwatch = StopwatchLogger.Start(_log); 
                _log.Info("Called RoleController Get Role type");     
              
                 List<ItemNameInt> roleTypes = ((RoleTypeEnum[])Enum.GetValues(typeof(RoleTypeEnum))).Select(c => new ItemNameInt() { 
                     Id = (int)c, 
                     Name = DataUtility.GetEnumDescription((RoleTypeEnum)(int)c)
                     }).ToList();
                stopwatch.StopAndLog("End RoleController Get Role Type");
                return Ok(roleTypes);
            }
            catch (Exception ex)
            {
                _log.Error(ExceptionFormatter.SerializeToString(ex));
                return StatusCode((int)HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
            }
        }   
        
    }
}