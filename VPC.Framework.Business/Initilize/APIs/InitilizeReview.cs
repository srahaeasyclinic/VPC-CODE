
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


        List<LayoutModel> GetAllEntityAndPickListFormLayoutsByTenantId(Guid id);
        Guid GetNewlyCreatedEntityId(Guid rootTenantId, dynamic defaultValue, Guid intialisedTenantId, string entityId);
        Guid GetNewlyCreatedPickListId(Guid rootTenantId, dynamic defaultValue, Guid intialisedTenantId, string picklistId);
    }
    public class InitilizeReview : IInitilizeReview
    {
        private readonly InitilizeData _data = new InitilizeData();

        List<LayoutModel> IInitilizeReview.GetAllEntityAndPickListFormLayoutsByTenantId(Guid id)
        {
            return _data.GetAllEntityAndPickListFormLayoutsByTenantId(id);
        }


        Guid IInitilizeReview.GetNewlyCreatedEntityId(Guid rootTenantId, dynamic defaultValue, Guid intialisedTenantId, string entityId)
        {
             return _data.GetNewlyCreatedEntityId(rootTenantId, defaultValue, intialisedTenantId, entityId);
        }

        Guid IInitilizeReview.GetNewlyCreatedPickListId(Guid rootTenantId, dynamic defaultValue, Guid intialisedTenantId, string picklistId)
        {
            return _data.GetNewlyCreatedPickListId(rootTenantId, defaultValue, intialisedTenantId, picklistId);
        }

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