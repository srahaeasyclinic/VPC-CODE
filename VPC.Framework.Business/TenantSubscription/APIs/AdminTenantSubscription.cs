using System;
using System.Collections.Generic;
using VPC.Entities.Common;
using VPC.Entities.TenantSubscription;
using VPC.Framework.Business.Role.Data;

namespace VPC.Framework.Business.TenantSubscription.APIs
{
 public interface IAdminTenantSubscription
    {        
       bool Create(Guid tenantId, TenantSubscriptionInfo info);

       bool Update(Guid tenantId, TenantSubscriptionInfo info);

       bool Delete(Guid tenantId, Guid tenantSubscriptionId);

       bool Status(Guid tenantId, Guid tenantSubscriptionId);
    }
    internal  class AdminTenantSubscription : IAdminTenantSubscription
    {
        private readonly DataTenantSubscription _data = new DataTenantSubscription();

        bool IAdminTenantSubscription.Create(Guid tenantId, TenantSubscriptionInfo info)
        {
            return _data.Create(tenantId,info);
        }        

        bool IAdminTenantSubscription.Update(Guid tenantId, TenantSubscriptionInfo info)
        {
            return _data.Update(tenantId,info);
        }

        bool IAdminTenantSubscription.Delete(Guid tenantId, Guid tenantSubscriptionId)
        {
           return _data.Delete(tenantId,tenantSubscriptionId);
        }

        bool IAdminTenantSubscription.Status(Guid tenantId, Guid tenantSubscriptionId)
        {
            return _data.Status(tenantId,tenantSubscriptionId);
        }
    }
}