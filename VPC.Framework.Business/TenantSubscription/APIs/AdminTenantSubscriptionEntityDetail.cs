using System;
using System.Collections.Generic;
using VPC.Entities.Common;
using VPC.Entities.TenantSubscription;
using VPC.Framework.Business.Role.Data;

namespace VPC.Framework.Business.TenantSubscription.APIs
{
 public interface IAdminTenantSubscriptionEntityDetail
    {          
        bool Create(Guid tenantId, TenantSubscriptionEntityDetailInfo info);
        bool Create(Guid tenantId, List<TenantSubscriptionEntityDetailInfo> infos);
        bool Update(Guid tenantId, TenantSubscriptionEntityDetailInfo info);
        bool Delete(Guid tenantId, Guid subscriptionEntityDetailId);
        bool DeleteBySubscriptionEntityId(Guid tenantId, Guid subscriptionEntityId);
   
   
    }
    internal  class AdminTenantSubscriptionEntityDetail : IAdminTenantSubscriptionEntityDetail
    {
        private readonly DataTenantSubscriptionEntityDetail _data = new DataTenantSubscriptionEntityDetail();

        bool IAdminTenantSubscriptionEntityDetail.Create(Guid tenantId, TenantSubscriptionEntityDetailInfo info)
        {
           return _data.Create(tenantId,info);
        }

        bool IAdminTenantSubscriptionEntityDetail.Create(Guid tenantId, List<TenantSubscriptionEntityDetailInfo> infos)
        {
           return _data.Create(tenantId,infos); 
        }

        bool IAdminTenantSubscriptionEntityDetail.Delete(Guid tenantId, Guid subscriptionEntityDetailId)
        {
              return _data.Delete(tenantId,subscriptionEntityDetailId);
        }

        bool IAdminTenantSubscriptionEntityDetail.DeleteBySubscriptionEntityId(Guid tenantId, Guid subscriptionEntityId)
        {
              return _data.DeleteBySubscriptionEntityId(tenantId,subscriptionEntityId);
        }

        bool IAdminTenantSubscriptionEntityDetail.Update(Guid tenantId, TenantSubscriptionEntityDetailInfo info)
        {
              return _data.Update(tenantId,info);
        }
    }
}