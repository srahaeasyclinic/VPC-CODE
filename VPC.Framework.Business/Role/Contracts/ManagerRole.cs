using System;
using System.Collections.Generic;
using VPC.Entities.Common;
using VPC.Entities.Role;
using VPC.Framework.Business.Common;

namespace VPC.Framework.Business.Role.APIs
{
    public interface IManagerRole
    {
        bool Create(Guid tenantId, RoleInfo roleInfo);
        bool CreateRoles(Guid tenantId, List<RoleInfo> roleInfos);
        bool Update(Guid tenantId, RoleInfo roleInfo);
        bool Delete(Guid tenantId, Guid roleId);
        List<RoleInfo> Roles(Guid tenantId);
        RoleInfo Role(Guid tenantId, Guid roleId);
        List<RoleInfo> Roles(Guid tenantId, Guid? roleId);
        UserDetailInfo GetUserDetails(Guid tenantId, Guid userId);
    }
    public class ManagerRole : IManagerRole
    {
        private readonly IAdminRole _adminRole = new AdminRole();
        private readonly IReviewRole _reviewRole = new ReviewRole();

        bool IManagerRole.Create(Guid tenantId, RoleInfo roleInfo)
        {
            roleInfo.RoleId = Guid.NewGuid();
            return _adminRole.Create(tenantId, roleInfo);
        }

        bool IManagerRole.CreateRoles(Guid tenantId, List<RoleInfo> roleInfos)
        {
            return _adminRole.CreateRoles(tenantId, roleInfos);
        }

        bool IManagerRole.Update(Guid tenantId, RoleInfo roleInfo)
        {
            return _adminRole.Update(tenantId, roleInfo);
        }

        bool IManagerRole.Delete(Guid tenantId, Guid roleId)
        {
            return _adminRole.Delete(tenantId, roleId);
        }

        RoleInfo IManagerRole.Role(Guid tenantId, Guid roleId)
        {
            return _reviewRole.Role(tenantId, roleId);
        }

        List<RoleInfo> IManagerRole.Roles(Guid tenantId)
        {
            var roles = _reviewRole.Roles(tenantId);

            foreach (var role in roles)
            {
                role.RoleTypeName = DataUtility.GetEnumDescription((RoleTypeEnum)role.RoleType);
            }

            return roles;
        }

        List<RoleInfo> IManagerRole.Roles(Guid tenantId, Guid? roleId)
        {
            var roles = _reviewRole.Roles(tenantId, roleId);

            foreach (var role in roles)
            {
                role.RoleTypeName = DataUtility.GetEnumDescription((RoleTypeEnum)role.RoleType);
            }

            return roles;
        }

        UserDetailInfo IManagerRole.GetUserDetails(Guid tenantId, Guid userId)
        {
            return _reviewRole.GetUserDetails(tenantId, userId);
        }
    }
}