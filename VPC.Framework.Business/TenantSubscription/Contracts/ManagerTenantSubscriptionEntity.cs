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
    public interface IManagerTenantSubscriptionEntity
    {   
        Guid Create(Guid tenantId, TenantSubscriptionEntityInfo info);
        bool Create(Guid tenantId, List<TenantSubscriptionEntityInfo> infos);
        bool Update(Guid tenantId, TenantSubscriptionEntityInfo info);
        bool Delete(Guid tenantId, Guid tenantSubscriptionEntityId);
        List<TenantSubscriptionEntityInfo> TenantSubscriptionEntities(Guid tenantId,Guid tenantSubscriptionId );

         List<TenantSubscriptionEntityInfo> GetSubscriptionsByTenantId(Guid tenantId);

        
        TenantSubscriptionEntityInfo TenantSubscriptionEntity(Guid tenantId,Guid tenantSubscriptionEntityId );
     
    }
    public  class ManagerTenantSubscriptionEntity : IManagerTenantSubscriptionEntity
    {
        private readonly IAdminTenantSubscriptionEntity _adminTenantSubscriptionEntity = new AdminTenantSubscriptionEntity();
        private readonly IReviewTenantSubscriptionEntity _reviewTenantSubscriptionEntity = new ReviewTenantSubscriptionEntity();

        Guid IManagerTenantSubscriptionEntity.Create(Guid tenantId, TenantSubscriptionEntityInfo info)
        {
            info.TenantSubscriptionEntityId=Guid.NewGuid();
            _adminTenantSubscriptionEntity.Create(tenantId,info);
            return info.TenantSubscriptionEntityId;
        }

        
        bool IManagerTenantSubscriptionEntity.Create(Guid tenantId, List<TenantSubscriptionEntityInfo> infos)
        {
         return  _adminTenantSubscriptionEntity.Create(tenantId,infos);
        }

        bool IManagerTenantSubscriptionEntity.Delete(Guid tenantId, Guid tenantSubscriptionEntityId)
        {
           return _adminTenantSubscriptionEntity.Delete(tenantId,tenantSubscriptionEntityId);
        }      

        bool IManagerTenantSubscriptionEntity.Update(Guid tenantId, TenantSubscriptionEntityInfo info)
        {
           return _adminTenantSubscriptionEntity.Update(tenantId,info);
        }

         List<TenantSubscriptionEntityInfo> IManagerTenantSubscriptionEntity.TenantSubscriptionEntities(Guid tenantId, Guid tenantSubscriptionId)
        {
            return _reviewTenantSubscriptionEntity.TenantSubscriptionEntities(tenantId,tenantSubscriptionId);
        }

         List<TenantSubscriptionEntityInfo> IManagerTenantSubscriptionEntity.GetSubscriptionsByTenantId(Guid tenantId)
        {
            return _reviewTenantSubscriptionEntity.GetSubscriptionsByTenantId(tenantId);
        }

        TenantSubscriptionEntityInfo IManagerTenantSubscriptionEntity.TenantSubscriptionEntity(Guid tenantId, Guid tenantSubscriptionEntityId)
        {
           return _reviewTenantSubscriptionEntity.TenantSubscriptionEntity(tenantId,tenantSubscriptionEntityId);
        }

    }
}