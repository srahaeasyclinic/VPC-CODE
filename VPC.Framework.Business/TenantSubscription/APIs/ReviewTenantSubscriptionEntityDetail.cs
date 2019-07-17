using System;
using System.Collections.Generic;
using VPC.Entities.Common;
using VPC.Entities.TenantSubscription;
using VPC.Framework.Business.Role.Data;

namespace VPC.Framework.Business.TenantSubscription.APIs
{
 public interface IReviewTenantSubscriptionEntityDetail
    {        
       List<TenantSubscriptionEntityDetailInfo> TenantSubscriptionEntityDetails(Guid tenantId,Guid tenantSubscriptionEntityId );      

       TenantSubscriptionEntityDetailInfo TenantSubscriptionEntityDetail(Guid tenantId,Guid tenantSubscriptionEntityDetailId );
      
    }
    internal  class ReviewTenantSubscriptionEntityDetail : IReviewTenantSubscriptionEntityDetail
    {
        private readonly DataTenantSubscriptionEntityDetail _data = new DataTenantSubscriptionEntityDetail();

        TenantSubscriptionEntityDetailInfo IReviewTenantSubscriptionEntityDetail.TenantSubscriptionEntityDetail(Guid tenantId, Guid tenantSubscriptionEntityDetailId)
        {
            return _data.TenantSubscriptionEntityDetail(tenantId,tenantSubscriptionEntityDetailId);
        }

        List<TenantSubscriptionEntityDetailInfo> IReviewTenantSubscriptionEntityDetail.TenantSubscriptionEntityDetails(Guid tenantId, Guid tenantSubscriptionEntityId)
        {
           return _data.TenantSubscriptionEntityDetails(tenantId,tenantSubscriptionEntityId);
        }
    }
}