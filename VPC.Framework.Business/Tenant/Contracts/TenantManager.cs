using System;
using System.Collections.Generic;
using System.Text;
using VPC.Entities.Rule;
using VPC.Entities.Tenant;
using VPC.Framework.Business.MetadataManager.Contracts;
using VPC.Framework.Business.Rule;
using VPC.Framework.Business.Rule.APIs;

namespace VPC.Framework.Business.Rule.Contracts
{
    public interface IManageTenant
    {
        List<TenantInfo> GetTenantInfo(Guid tenantId);


    }
    public class TenantManager : IManageTenant
    {
        
        private readonly ITenantInfoReview tenantReview = new TenantReview();


        List<TenantInfo> IManageTenant.GetTenantInfo(Guid tenantId)
        {
            return tenantReview.GetTenantInfo(tenantId);
        }

    }
}
