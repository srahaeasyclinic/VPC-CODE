using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using VPC.Entities.EntitySecurity;
using VPC.Entities.Role;
using VPC.Framework.Business.EntitySecurity.Data;
using VPC.Framework.Business.Role.Data;

namespace VPC.Framework.Business.EntitySecurity.APIs
{
 public interface IManagerEntitySecurity
    {   
      EntitySecurityInfo Create(Guid tenantId, EntitySecurityInfo info);
      EntitySecurityInfo Update(Guid tenantId, EntitySecurityInfo info);     
      EntitySecurityInfo GetEntitySecurity(Guid tenantId,string entityId , Guid roleId );

      List<EntitySecurityInfo> GetEntitySecurities(Guid tenantId,string entityId , Guid? roleId  );
      List<EntitySecurityInfo> GetEntitySecuritiesByUserCode(Guid tenantId, Guid userId);
      List<RoleAccessOption> GetAccessLevel();
      List<RoleAccessOption> GetOperationLevel();

      
      List<EntitySecurityInfo> GetFunctionSecurities(Guid tenantId, string entityId,  Guid? roleId );
      List<EntitySecurityInfo> GetFunctionSecuritiesByUserCode(Guid tenantId, Guid userId);

    }
    
    public  class ManagerEntitySecurity : IManagerEntitySecurity
    {
        private readonly IAdminEntitySecurity _adminEntitySecurity = new AdminEntitySecurity();
        private readonly IReviewEntitySecurity _reviewEntitySecurity=new ReviewEntitySecurity();

        EntitySecurityInfo IManagerEntitySecurity.Create(Guid tenantId, EntitySecurityInfo info)
        {
            info.EntitySecurityId=Guid.NewGuid();
            _adminEntitySecurity.Create(tenantId,info);
            return info;
        }

        EntitySecurityInfo IManagerEntitySecurity.Update(Guid tenantId, EntitySecurityInfo info)
        {
           _adminEntitySecurity.Update(tenantId,info);
           return info;
        }
        
        EntitySecurityInfo IManagerEntitySecurity.GetEntitySecurity(Guid tenantId,string entityId ,  Guid roleId )
        {
             return _reviewEntitySecurity.GetEntitySecurity(tenantId, entityId , roleId );
        }

        List<EntitySecurityInfo> IManagerEntitySecurity.GetEntitySecurities(Guid tenantId, string entityId,  Guid? roleId )
        {
            return _reviewEntitySecurity.GetEntitySecurities(tenantId, entityId , roleId );
        }

        
        List<EntitySecurityInfo> IManagerEntitySecurity.GetEntitySecuritiesByUserCode(Guid tenantId, Guid userId)
        {
          return _reviewEntitySecurity.GetEntitySecuritiesByUserCode(tenantId,userId); 
        }


        List<RoleAccessOption> IManagerEntitySecurity.GetAccessLevel()
        {
            var roleAccessOptions = new List<RoleAccessOption>();
            foreach (RoleAccessLevel security in Enum.GetValues(typeof(RoleAccessLevel)))
            {
                MemberInfo memberInfo = security.GetType().GetMember(security.ToString()).First();
                var descriptionAttribute = memberInfo.GetCustomAttribute<System.ComponentModel.DescriptionAttribute>();
                var option = new RoleAccessOption();
                option.Id = (int)security;
                option.Name = (descriptionAttribute != null) ? descriptionAttribute.Description : security.ToString();
                option.AvailableGroups = GetAvailableGroups(memberInfo.GetCustomAttributes<AccessLevelAttribute>());
                roleAccessOptions.Add(option);
            }
            return roleAccessOptions;
        }
        List<RoleAccessOption> IManagerEntitySecurity.GetOperationLevel()
        {
            var roleAccessOptions = new List<RoleAccessOption>();
            foreach (RoleOperations security in Enum.GetValues(typeof(RoleOperations)))
            {
                MemberInfo memberInfo = security.GetType().GetMember(security.ToString()).First();
                var descriptionAttribute = memberInfo.GetCustomAttribute<System.ComponentModel.DescriptionAttribute>();
                var option = new RoleAccessOption();
                option.Id = (int)security;
                option.Name = (descriptionAttribute != null) ? descriptionAttribute.Description : security.ToString();
                option.AvailableGroups = GetAvailableGroups(memberInfo.GetCustomAttributes<AccessLevelAttribute>());
                roleAccessOptions.Add(option);
            }
            return roleAccessOptions;
        }

        private List<Guid> GetAvailableGroups(IEnumerable<AccessLevelAttribute> enumerable)
        {
            var availableGroups = new List<Guid>();
            if (enumerable != null && enumerable.Any())
            {
                foreach (var item in enumerable)
                {
                    availableGroups.Add(new Guid(item.GetAccessLevel()));
                }
            }
            return availableGroups;
        }

        List<EntitySecurityInfo> IManagerEntitySecurity.GetFunctionSecurities(Guid tenantId, string entityId, Guid? roleId)
        {
            return _reviewEntitySecurity.GetFunctionSecurities(tenantId,entityId,roleId); 
        }

        List<EntitySecurityInfo> IManagerEntitySecurity.GetFunctionSecuritiesByUserCode(Guid tenantId, Guid userId)
        {
            return _reviewEntitySecurity.GetFunctionSecuritiesByUserCode(tenantId,userId); 
        }
    }
}