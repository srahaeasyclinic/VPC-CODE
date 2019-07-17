using System;
using VPC.Entities.EntitySecurity;
using VPC.Entities.Role;
using VPC.Framework.Business.EntitySecurity.Data;
using VPC.Framework.Business.Role.Data;

namespace VPC.Framework.Business.EntitySecurity.APIs
{
 public interface IAdminEntitySecurity
    {        
      bool Create(Guid tenantId, EntitySecurityInfo roleInfo);
      bool Update(Guid tenantId, EntitySecurityInfo roleInfo);
    }
    
    internal  class AdminEntitySecurity : IAdminEntitySecurity
    {
        private readonly DataEntitySecurity _dataEntitySecurity = new DataEntitySecurity();

        bool IAdminEntitySecurity.Create(Guid tenantId, EntitySecurityInfo info)
        {
          return  _dataEntitySecurity.Create(tenantId,info);
        }
        bool IAdminEntitySecurity.Update(Guid tenantId, EntitySecurityInfo info)
        {
            return  _dataEntitySecurity.Update(tenantId,info);
        }
    }
}