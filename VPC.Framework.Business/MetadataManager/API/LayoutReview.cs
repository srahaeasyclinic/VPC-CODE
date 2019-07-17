using System;
using System.Collections.Generic; 
using VPC.Entities.EntityCore.Model.Storage;
using VPC.Framework.Business.MetadataManager.Data;
using VPC.Entities.Role;

namespace VPC.Framework.Business.MetadataManager.API
{
    public interface ILayoutReview
    {
        List<LayoutModel> GetLayoutsByEntityName(Guid tenantId, string entityId);
        LayoutModel GetLayoutsDetailsById(Guid tenantId, Guid id);
        //LayoutModel GetLayoutsDetailsById(Guid tenantId, string entityId, int type, string subtype, int context);
        LayoutModel GetLayoutsDetail(Guid tenantId, string entityContext, int layoutType, string subType, int context);

        List<LayoutModel> GetLayoutsByPicklistId(Guid tenantId, string picklistId);
        LayoutModel GetPicklistLayoutDetailsById(Guid tenantId, Guid id);
        LayoutModel GetDefaultPicklistLayout(Guid tenantId, string entityName, LayoutType layoutType, int context);
        List<LayoutModel> GetLayoutsByEntityName(Guid tenantId, string entityContext, int type, bool isPicklist);
        Guid GetTenantId( string entityContext, string code);
        List<RoleInfo> GetUserRoles(Guid userId);
    }

    public class LayoutReview : ILayoutReview
    {
        private readonly LayoutData _data = new LayoutData();

        List<LayoutModel> ILayoutReview.GetLayoutsByEntityName(Guid tenantId, string entityId)
        {
            return _data.GetLayoutsByEntityName(tenantId, entityId);
        }

        List<LayoutModel> ILayoutReview.GetLayoutsByPicklistId(Guid tenantId, string picklistId)
        {
            return _data.GetLayoutsByPicklistId(tenantId, picklistId);
        }

        LayoutModel ILayoutReview.GetPicklistLayoutDetailsById(Guid tenantId, Guid id)
        {
            return _data.GetPicklistLayoutDetailsById(tenantId, id);
        }

        LayoutModel ILayoutReview.GetLayoutsDetailsById(Guid tenantId, Guid id)
        {
            return _data.GetLayoutsDetailsById(tenantId, id);
        }

        //LayoutModel ILayoutReview.GetLayoutsDetailsById(Guid tenantId, string entityId, int type, string subtype, int context)
        //{
        //    return _data.GetLayoutsDetailsById(tenantId, entityId, type, subtype, context);
        //}

        LayoutModel ILayoutReview.GetLayoutsDetail(Guid tenantId, string entityContext, int layoutType, string subType, int context)
        {
            return _data.GetLayoutsDetail(tenantId, entityContext, layoutType, subType, context);
        }

        LayoutModel ILayoutReview.GetDefaultPicklistLayout(Guid tenantId, string entityName, LayoutType layoutType, int context)
        {
            return _data.GetDefaultPicklistLayout(tenantId, entityName, layoutType, context);
        }

        List<LayoutModel> ILayoutReview.GetLayoutsByEntityName(Guid tenantId, string entityContext, int type, bool isPicklist)
        {
            return _data.GetLayoutsByEntityName(tenantId, entityContext, type, isPicklist);
        }

        Guid ILayoutReview.GetTenantId( string entityContext, string code)
        {
            return _data.GetTenantId( entityContext, code);
        }

        List<RoleInfo> ILayoutReview.GetUserRoles(Guid userId)
        {
            return _data.GetUserRoles(userId);
        }
    }
}
