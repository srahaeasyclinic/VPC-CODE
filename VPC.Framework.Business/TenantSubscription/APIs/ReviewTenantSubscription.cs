using System;
using System.Collections.Generic;
using VPC.Entities.Common;
using VPC.Entities.TenantSubscription;
using VPC.Framework.Business.Role.Data;

namespace VPC.Framework.Business.TenantSubscription.APIs
{
 public interface IReviewTenantSubscription
    {        
       List<TenantSubscriptionInfo> TenantSubscriptions(Guid tenantId );
       TenantSubscriptionInfo TenantSubscription(Guid tenantId,Guid tenantSubscriptionId );
    }
    internal  class ReviewTenantSubscription : IReviewTenantSubscription
    {
        private readonly DataTenantSubscription _data = new DataTenantSubscription();

        TenantSubscriptionInfo IReviewTenantSubscription.TenantSubscription(Guid tenantId, Guid tenantSubscriptionId)
        {
            return _data.TenantSubscription(tenantId,tenantSubscriptionId);
        }

        List<TenantSubscriptionInfo> IReviewTenantSubscription.TenantSubscriptions(Guid tenantId)
        {
            return _data.TenantSubscriptions(tenantId);
        }
    }
}