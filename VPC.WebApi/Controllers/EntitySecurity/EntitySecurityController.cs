using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using VPC.Entities.EntitySecurity;
using VPC.Entities.Role;
using VPC.Entities.WorkFlow;
using VPC.Framework.Business.Common;
using VPC.Framework.Business.EntitySecurity.APIs;
using VPC.Framework.Business.MetadataManager.Contracts;
using VPC.Framework.Business.Role.APIs;
using VPC.Framework.Business.WorkFlow.Contracts;
using VPC.WebApi.Controllers.WorkFlow;
using VPC.WebApi.Utility;


namespace VPC.WebApi.Controllers.EntitySecurity
{
    [Route("api/entitySecurity")]
    public class EntitySecurityController : BaseApiController
    {
        private readonly Logger _log= LogManager.GetCurrentClassLogger();
        private readonly IManagerRole _managerRole; 
        private IMetadataManager _iMetadataManager ;
        private IManagerEntitySecurity _managerEntitySecurity;
        private ISecurityCacheManager _securityCacheManager;
        public EntitySecurityController(IMetadataManager iMetadataManager,IManagerEntitySecurity managerEntitySecurity,IManagerRole managerRole,
        ISecurityCacheManager securityCacheManager)
        {           
            _iMetadataManager=iMetadataManager;   
            _managerEntitySecurity=managerEntitySecurity;     
            _managerRole=managerRole;  
            _securityCacheManager=securityCacheManager;
        }             



        [HttpGet]
        [Route("{entityname}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [ProducesResponseType(404)]
        public IActionResult Get(string entityname,[FromQuery] Guid? roleId)
        {
            try
            {               
                var stopwatch = StopwatchLogger.Start(_log); 
                _log.Info("Called EntitySecurityController Get {0}=", JsonConvert.SerializeObject(entityname));  
                var entityId= _iMetadataManager.GetEntityContextByEntityName(entityname); 
                var roles=_managerRole.Roles(TenantCode,roleId);
                var entitySecurities=_managerEntitySecurity.GetEntitySecurities(TenantCode,entityId,roleId); 
                var accessLevel = _managerEntitySecurity.GetAccessLevel();
                var operationLevel = _managerEntitySecurity.GetOperationLevel();  

                foreach(var role in roles)
                {
                    var itsEntitySecurity=(from entitySecurity in entitySecurities where entitySecurity.RoleId==role.RoleId select entitySecurity).FirstOrDefault();
                    
                    if(itsEntitySecurity!=null)
                       itsEntitySecurity.EntityId=string.Empty;

                    role.Entity=new RoleMapperEntityInfo(){
                           Data=itsEntitySecurity!=null ? itsEntitySecurity : new EntitySecurityInfo(){RoleId=role.RoleId},
                           AccessLevel = RoleMapper.GetAccessLevels(accessLevel,new Guid(AccessLevelGuid.RoleEntity)),
                           OperationLevel = RoleMapper.GetAccessLevels(operationLevel,new Guid(AccessLevelGuid.RoleEntity))                         

                    };
                }

                stopwatch.StopAndLog("End EntitySecurityController Get");
                return Ok(roles);
            }
            catch (Exception ex)
            {
                _log.Error(ExceptionFormatter.SerializeToString(ex));
                return StatusCode((int)HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
            }
        }
      

        [HttpPost]
        [Route("{entityname}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [ProducesResponseType(404)]
        public IActionResult Post(string entityname,[FromBody] EntitySecurityInfo entitySecurity)
        {
            try
            {   
                if (entitySecurity==null) 
                    return BadRequest("Invalid parameter");  

                var stopwatch = StopwatchLogger.Start(_log);              
                _log.Info("Called EntitySecurityController Post {0}=", JsonConvert.SerializeObject(entitySecurity)); 
                entitySecurity.EntityId= _iMetadataManager.GetEntityContextByEntityName(entityname);                 
                var retVal = _managerEntitySecurity.Create(TenantCode,entitySecurity);
                retVal.EntityId=string.Empty;
                _securityCacheManager.Clear(TenantCode,UserId,EntityCacheType.Entity);
                stopwatch.StopAndLog("End EntitySecurityController Post");
                return Ok(retVal);
            }
            catch (Exception ex)
            {
                _log.Error(ExceptionFormatter.SerializeToString(ex));
                return StatusCode((int)HttpStatusCode.InternalServerError, ApiConstant.CustomErrorMessage);
            }
        }

        [HttpPut]
        [Route("{entityname}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [ProducesResponseType(404)]
        public IActionResult Put(string entityname,[FromBody] EntitySecurityInfo entitySecurity)
        {
            try
            {   
                if (entitySecurity==null) 
                    return BadRequest("Invalid parameter");  

                var stopwatch = StopwatchLogger.Start(_log);              
                _log.Info("Called EntitySecurityController Post {0}=", JsonConvert.SerializeObject(entitySecurity)); 
                entitySecurity.EntityId= _iMetadataManager.GetEntityContextByEntityName(entityname);                  
                var retVal = _managerEntitySecurity.Update(TenantCode,entitySecurity);
                _securityCacheManager.Clear(TenantCode,UserId,EntityCacheType.Entity);
                retVal.EntityId=string.Empty;
                stopwatch.StopAndLog("End EntitySecurityController Post");
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