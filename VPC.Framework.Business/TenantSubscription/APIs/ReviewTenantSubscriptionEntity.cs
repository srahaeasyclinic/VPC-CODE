using System;
using System.Collections.Generic;
using VPC.Entities.Common;
using VPC.Entities.TenantSubscription;
using VPC.Framework.Business.Role.Data;

namespace VPC.Framework.Business.TenantSubscription.APIs
{
 public interface IReviewTenantSubscriptionEntity
    { 
      List<TenantSubscriptionEntityInfo> TenantSubscriptionEntities(Guid tenantId,Guid tenantSubscriptionId );



      List<TenantSubscriptionEntityInfo> GetSubscriptionsByTenantId(Guid tenantId );
      TenantSubscriptionEntityInfo TenantSubscriptionEntity(Guid tenantId,Guid tenantSubscriptionEntityId );
            
      
    }
    internal  class ReviewTenantSubscriptionEntity : IReviewTenantSubscriptionEntity
    {
        private readonly DataTenantSubscriptionEntity _data = new DataTenantSubscriptionEntity();

        List<TenantSubscriptionEntityInfo> IReviewTenantSubscriptionEntity.TenantSubscriptionEntities(Guid tenantId, Guid tenantSubscriptionId)
        {
            return _data.TenantSubscriptionEntities(tenantId,tenantSubscriptionId);
        }

List<TenantSubscriptionEntityInfo> IReviewTenantSubscriptionEntity.GetSubscriptionsByTenantId(Guid tenantId)
        {
            return _data.GetSubscriptionsByTenantId(tenantId);
        }
        
        TenantSubscriptionEntityInfo IReviewTenantSubscriptionEntity.TenantSubscriptionEntity(Guid tenantId, Guid tenantSubscriptionEntityId)
        {
            return _data.TenantSubscriptionEntity(tenantId,tenantSubscriptionEntityId);
        }
    }
}