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
    public interface IManagerTenantSubscriptionEntityDetail
    {   
        Guid Create(Guid tenantId, TenantSubscriptionEntityDetailInfo info);
        bool Create(Guid tenantId, List<TenantSubscriptionEntityDetailInfo> infos);
        bool Update(Guid tenantId, TenantSubscriptionEntityDetailInfo info);
        bool Delete(Guid tenantId, Guid subscriptionEntityDetailId);
        bool DeleteBySubscriptionEntityId(Guid tenantId, Guid subscriptionEntityId);
        List<TenantSubscriptionEntityDetailInfo> TenantSubscriptionEntityDetails(Guid tenantId,Guid tenantSubscriptionEntityId );
        TenantSubscriptionEntityDetailInfo TenantSubscriptionEntityDetail(Guid tenantId,Guid tenantSubscriptionEntityDetailId );
     
    }
    public  class ManagerTenantSubscriptionEntityDetail : IManagerTenantSubscriptionEntityDetail
    {
        private readonly IAdminTenantSubscriptionEntityDetail _adminTenantSubscriptionEntityDetail = new AdminTenantSubscriptionEntityDetail();
        private readonly IReviewTenantSubscriptionEntityDetail _reviewTenantSubscriptionEntityDetail = new ReviewTenantSubscriptionEntityDetail();

        Guid IManagerTenantSubscriptionEntityDetail.Create(Guid tenantId, TenantSubscriptionEntityDetailInfo info)
        {
            info.SubscriptionEntityDetailId=Guid.NewGuid();
            _adminTenantSubscriptionEntityDetail.Create(tenantId,info);
            return info.SubscriptionEntityDetailId;
        }

        bool IManagerTenantSubscriptionEntityDetail.Update(Guid tenantId, TenantSubscriptionEntityDetailInfo info)
        {
             return _adminTenantSubscriptionEntityDetail.Update(tenantId,info);
        }

        bool IManagerTenantSubscriptionEntityDetail.Delete(Guid tenantId, Guid subscriptionEntityDetailId)
        {
              return _adminTenantSubscriptionEntityDetail.Delete(tenantId,subscriptionEntityDetailId);
        }

        bool IManagerTenantSubscriptionEntityDetail.DeleteBySubscriptionEntityId(Guid tenantId, Guid subscriptionEntityId)
        {
            return _adminTenantSubscriptionEntityDetail.DeleteBySubscriptionEntityId(tenantId,subscriptionEntityId);
        }

        TenantSubscriptionEntityDetailInfo IManagerTenantSubscriptionEntityDetail.TenantSubscriptionEntityDetail(Guid tenantId, Guid tenantSubscriptionEntityDetailId)
        {
              return _reviewTenantSubscriptionEntityDetail.TenantSubscriptionEntityDetail(tenantId,tenantSubscriptionEntityDetailId);
        }

        List<TenantSubscriptionEntityDetailInfo> IManagerTenantSubscriptionEntityDetail.TenantSubscriptionEntityDetails(Guid tenantId, Guid tenantSubscriptionEntityId)
        {
            return _reviewTenantSubscriptionEntityDetail.TenantSubscriptionEntityDetails(tenantId,tenantSubscriptionEntityId);
        }

        bool IManagerTenantSubscriptionEntityDetail.Create(Guid tenantId, List<TenantSubscriptionEntityDetailInfo> infos)
        {
            return  _adminTenantSubscriptionEntityDetail.Create(tenantId,infos);
        }
    }
}