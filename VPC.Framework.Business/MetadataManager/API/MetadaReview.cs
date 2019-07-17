

using System;
using System.Collections.Generic;
using VPC.Entities.EntityCore.Model.Storage;
using VPC.Framework.Business.MetadataManager.Data;

namespace VPC.Framework.Business.MetadataManager.API
{
    public interface IMetadaReview
    {
        List<LayoutModel> GetLayoutsByEntityName(Guid tenantId, Guid entityContext);
        LayoutModel GetLayoutsDetailsById(Guid tenantId, Guid layoutId);
        LayoutModel GetLayoutByName(Guid tenantId, string templateName, int layoutType);
    }

   internal sealed class MetadaReview : IMetadaReview
    {
       
        List<LayoutModel> IMetadaReview.GetLayoutsByEntityName(Guid tenantId, Guid entityContext)
        {
            var entityDate = new MetadataData();
            return entityDate.GetLayoutsByEntityName(tenantId, entityContext);
        }

        LayoutModel IMetadaReview.GetLayoutsDetailsById(Guid tenantId, Guid layoutId)
        {
            var entityDate = new MetadataData();
            return entityDate.GetLayoutsDetailsById(tenantId, layoutId);
        }

        LayoutModel IMetadaReview.GetLayoutByName(Guid tenantId, string templateName, int layoutType)
        {
            var entityDate = new MetadataData();
            return entityDate.GetLayoutByName(tenantId, templateName, layoutType);
        }
    }
}
