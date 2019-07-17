
using System;
using System.Collections.Generic;
using VPC.Entities.EntityCore.Model.Storage;
using VPC.Framework.Business.Initilize.Data;

namespace VPC.Framework.Business.Initilize.APIs
{
    public interface IInitilizeReview
    {
        Guid getRootTenantCode();
        List<LayoutModel> GetRootTenantLayouts(Guid tenantId);
    }
    public class InitilizeReview : IInitilizeReview
    {
        private readonly InitilizeData _data = new InitilizeData();

        Guid IInitilizeReview.getRootTenantCode()
        {
            return _data.getRootTenantCode();
        }

        List<LayoutModel> IInitilizeReview.GetRootTenantLayouts(Guid tenantId)
        {
            return _data.GetRootTenantLayouts(tenantId);
        }
    }
}