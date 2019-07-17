using System;
using System.Collections.Generic;
using VPC.Entities.Common;
using VPC.Entities.TenantSubscription;
using VPC.Framework.Business.Role.Data;

namespace VPC.Framework.Business.TenantSubscription.APIs
{
 public interface IAdminTenantSubscriptionEntity
    {      
      
        bool Create(Guid tenantId, TenantSubscriptionEntityInfo info);
        bool Create(Guid tenantId, List<TenantSubscriptionEntityInfo> infos);
        bool Update(Guid tenantId, TenantSubscriptionEntityInfo info);
        bool Delete(Guid tenantId, Guid tenantSubscriptionEntityId);
   
    }
    internal  class AdminTenantSubscriptionEntity : IAdminTenantSubscriptionEntity
    {
        private readonly DataTenantSubscriptionEntity _data = new DataTenantSubscriptionEntity();

        bool IAdminTenantSubscriptionEntity.Create(Guid tenantId, TenantSubscriptionEntityInfo info)
        {
            return _data.Create(tenantId,info);
        }

        bool IAdminTenantSubscriptionEntity.Create(Guid tenantId, List<TenantSubscriptionEntityInfo> infos)
        {
               return _data.Create(tenantId,infos);
        }

        bool IAdminTenantSubscriptionEntity.Delete(Guid tenantId, Guid tenantSubscriptionEntityId)
        {
             return _data.Delete(tenantId,tenantSubscriptionEntityId);
        }

        bool IAdminTenantSubscriptionEntity.Update(Guid tenantId, TenantSubscriptionEntityInfo info)
        {
              return _data.Update(tenantId,info);
        }
    }
}