﻿using System;
using System.Collections.Generic;
using System.Text;
using VPC.Entities.EntityCore.Metadata.Picklist;
using VPC.Entities.Rule;
using VPC.Entities.Tenant;
using VPC.Framework.Business.MetadataManager.Contracts;
using VPC.Framework.Business.Rule.Data;
using VPC.Metadata.Business.DataTypes;

namespace VPC.Framework.Business.Rule.APIs
{

    public interface ITenantInfoReview
    {
        TenantLanguageInfo GetTenantLanguageInfo(Guid tenantId);   
    }    
    internal class TenantReview : ITenantInfoReview
    {
        private readonly DataTenant dataTenant = new DataTenant();
        TenantLanguageInfo ITenantInfoReview.GetTenantLanguageInfo(Guid tenantId)
        {
            //return dataTenant.GetTenantInfo(tenantId);
            return dataTenant.GetTenantLanguageInfo(tenantId);
        }
    }
}
