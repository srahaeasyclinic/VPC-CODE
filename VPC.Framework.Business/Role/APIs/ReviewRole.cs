using System;
using System.Collections.Generic;
using VPC.Entities.Common;
using VPC.Entities.Role;
using VPC.Framework.Business.Role.Data;

namespace VPC.Framework.Business.Role.APIs
{
    public interface IReviewRole
    {
        List<RoleInfo> Roles(Guid tenantId);
        RoleInfo Role(Guid tenantId, Guid roleId);
        List<RoleInfo> Roles(Guid tenantId, Guid? roleId);
        UserDetailInfo GetUserDetails(Guid tenantId, Guid userId);
    }
    internal class ReviewRole : IReviewRole
    {
        private readonly DataRole _dataRole = new DataRole();

        RoleInfo IReviewRole.Role(Guid tenantId, Guid roleId)
        {
            return _dataRole.Role(tenantId, roleId);
        }

        List<RoleInfo> IReviewRole.Roles(Guid tenantId)
        {
            return _dataRole.Roles(tenantId);
        }

        List<RoleInfo> IReviewRole.Roles(Guid tenantId, Guid? roleId)
        {
            return _dataRole.Roles(tenantId, roleId);
        }

        UserDetailInfo IReviewRole.GetUserDetails(Guid tenantId, Guid userId)
        {
            return _dataRole.GetUserDetails(tenantId, userId);
        }
    }
}