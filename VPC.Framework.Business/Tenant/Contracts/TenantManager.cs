using System;
using System.Collections.Generic;
using System.Text;
using VPC.Entities.Rule;
using VPC.Entities.Tenant;
using VPC.Framework.Business.MetadataManager.Contracts;
using VPC.Framework.Business.Rule;
using VPC.Framework.Business.Rule.APIs;

//namespace VPC.Framework.Business.Rule.Contracts updated by Soma as per request to change from Tanmay
namespace VPC.Framework.Business.Tenant.Contracts
{
    public interface IManageTenant
    {
        TenantLanguageInfo GetTenantLanguageInfo(Guid tenantId);
    }
    public class TenantManager : IManageTenant
    {
        
        private readonly ITenantInfoReview tenantReview = new TenantReview();



        TenantLanguageInfo IManageTenant.GetTenantLanguageInfo(Guid tenantId)
        {
            return tenantReview.GetTenantLanguageInfo(tenantId);
        }

    }
}
