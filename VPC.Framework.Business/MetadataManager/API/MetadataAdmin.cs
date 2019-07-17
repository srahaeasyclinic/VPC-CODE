

using System;
using System.Collections.Generic;
using VPC.Entities.EntityCore.Model.Storage;
using VPC.Framework.Business.MetadataManager.Data;

namespace VPC.Framework.Business.MetadataManager.API
{
   
     public interface IMetadataAdmin
    {
        bool SaveLayout(Guid tenantId, LayoutModel layoutmodel);
        bool UpdateLayoutDetails(Guid tenantId, LayoutModel templateModel);
        bool DeleteLayoutDetails(Guid tenantId, Guid LayoutId);
        bool UpdateDefaultLayout(Guid tenantId, Guid layoutId, int layoutType, Guid UserId);
    }
    public class MetadataAdmin : IMetadataAdmin
    {
        readonly MetadataData _dataAdmin = new MetadataData();

        bool IMetadataAdmin.SaveLayout(Guid tenantId, LayoutModel layoutmodel)
        {
           return _dataAdmin.SaveLayout(tenantId, layoutmodel);
        }

        bool IMetadataAdmin.UpdateLayoutDetails(Guid tenantId, LayoutModel templateModel)
        {
            return _dataAdmin.UpdateLayoutDetails(tenantId, templateModel);
        }

        bool IMetadataAdmin.DeleteLayoutDetails(Guid tenantId, Guid LayoutId)
        {
            return _dataAdmin.DeleteLayoutDetails(tenantId, LayoutId);
        }

        bool IMetadataAdmin.UpdateDefaultLayout(Guid tenantId, Guid layoutId, int layoutType, Guid UserId)
        {
            return _dataAdmin.UpdateDefaultLayout(tenantId, layoutId, layoutType, UserId);
        }
    }
}
