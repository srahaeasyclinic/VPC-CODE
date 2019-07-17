using System;
using System.Collections.Generic;
using VPC.Entities.Role;
using VPC.Framework.Business.Role.Data;

namespace VPC.Framework.Business.Role.APIs
{
    public interface IAdminRole
    {
        bool Create(Guid tenantId, RoleInfo roleInfo);
        bool CreateRoles(Guid tenantId, List<RoleInfo> roleInfos);
        bool Update(Guid tenantId, RoleInfo roleInfo);
        bool Delete(Guid tenantId, Guid roleId);
    }

    internal class AdminRole : IAdminRole
    {
        private readonly DataRole _dataRole = new DataRole();

        bool IAdminRole.Create(Guid tenantId, RoleInfo roleInfo)
        {
            return _dataRole.Create(tenantId, roleInfo);
        }

        bool IAdminRole.CreateRoles(Guid tenantId, List<RoleInfo> roleInfos)
        {
            return _dataRole.CreateRoles(tenantId, roleInfos);
        }

        bool IAdminRole.Delete(Guid tenantId, Guid roleId)
        {
            return _dataRole.Delete(tenantId, roleId);
        }

        bool IAdminRole.Update(Guid tenantId, RoleInfo roleInfo)
        {
            return _dataRole.Update(tenantId, roleInfo);
        }
    }
}