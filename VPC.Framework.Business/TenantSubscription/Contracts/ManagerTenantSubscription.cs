using System;
using System.Collections.Generic;
using VPC.Entities.Role;
using VPC.Entities.Common;
using VPC.Entities.EntityCore.Metadata;
using VPC.Framework.Business.Common;
using VPC.Framework.Business.Role.Data;
using VPC.Framework.Business.TenantSubscription.APIs;
using VPC.Entities.TenantSubscription;

namespace VPC.Framework.Business.TenantSubscription.Contracts
{
    public interface IManagerTenantSubscription
    {   
       Guid Create(Guid tenantId, TenantSubscriptionInfo info);

       bool Update(Guid tenantId, TenantSubscriptionInfo info);

       bool Delete(Guid tenantId, Guid tenantSubscriptionId);

       bool Status(Guid tenantId, Guid tenantSubscriptionId);

       List<TenantSubscriptionInfo> TenantSubscriptions(Guid tenantId );
       TenantSubscriptionInfo TenantSubscription(Guid tenantId,Guid tenantSubscriptionId );
     
    }
    public  class ManagerTenantSubscription : IManagerTenantSubscription
    {
        private readonly IAdminTenantSubscription _adminTenantSubscription = new AdminTenantSubscription();
        private readonly IReviewTenantSubscription _reviewTenantSubscription = new ReviewTenantSubscription();

        Guid IManagerTenantSubscription.Create(Guid tenantId, TenantSubscriptionInfo info)
        {
            info.TenantSubscriptionId=Guid.NewGuid();
            _adminTenantSubscription.Create(tenantId,info);
            return info.TenantSubscriptionId;
        }

        bool IManagerTenantSubscription.Update(Guid tenantId, TenantSubscriptionInfo info)
        {
            return _adminTenantSubscription.Update(tenantId,info);
        }

        bool IManagerTenantSubscription.Delete(Guid tenantId, Guid tenantSubscriptionId)
        {
             return _adminTenantSubscription.Delete(tenantId,tenantSubscriptionId);
        }

        bool IManagerTenantSubscription.Status(Guid tenantId, Guid tenantSubscriptionId)
        {
            return _adminTenantSubscription.Status(tenantId,tenantSubscriptionId);
        }

        TenantSubscriptionInfo IManagerTenantSubscription.TenantSubscription(Guid tenantId, Guid tenantSubscriptionId)
        {
          return _reviewTenantSubscription.TenantSubscription(tenantId,tenantSubscriptionId);
        }

        List<TenantSubscriptionInfo> IManagerTenantSubscription.TenantSubscriptions(Guid tenantId)
        {
            return _reviewTenantSubscription.TenantSubscriptions(tenantId);
        }

       
    }
}