using System;
using System.Collections.Generic;
using System.Text;
using VPC.Entities.EntityCore.Model.Storage;
using VPC.Framework.Business.MetadataManager.Data;

namespace VPC.Framework.Business.MetadataManager.API
{
    public interface ILayoutAdmin
    {
        void CreateLayout(Guid tenantId, LayoutModel layoutModel);
        void UpdateLayoutDetails(Guid tenantId, Guid layoutId, LayoutModel templateModel);
        void CreatePicklistLayout(Guid tenantId, LayoutModel layoutModel);
        void SetPicklistLayoutDefault(Guid tenantId, LayoutModel layoutModel);
        void SetListLayoutDefault(Guid tenantId, LayoutModel layoutModel);
        void DeletePicklistLayout(Guid tenantId, Guid layoutId);
        void DeleteListLayout(Guid tenantId, Guid layoutId);
        void UpdatePicklistLayout(Guid tenantId, Guid layoutId, LayoutModel layout);
    }

    public class LayoutAdmin : ILayoutAdmin
    {
        private readonly LayoutData _data = new LayoutData();

        void ILayoutAdmin.CreateLayout(Guid tenantId, LayoutModel layoutModel)
        {
            _data.CreateLayout(tenantId, layoutModel);
        }

        void ILayoutAdmin.UpdateLayoutDetails(Guid tenantId, Guid layoutId, LayoutModel templateModel)
        {
            _data.UpdateLayoutDetails(tenantId, layoutId, templateModel);
        }
        void ILayoutAdmin.CreatePicklistLayout(Guid tenantId, LayoutModel layoutModel)
        {
            _data.CreatePicklistLayout(tenantId, layoutModel);
        }

        void ILayoutAdmin.SetPicklistLayoutDefault(Guid tenantId, LayoutModel layoutModel)
        {
            _data.SetPicklistLayoutDefault(tenantId, layoutModel);
        }

        void ILayoutAdmin.SetListLayoutDefault(Guid tenantId, LayoutModel layoutModel)
        {
            _data.SetListLayoutDefault(tenantId, layoutModel);
        }

        void ILayoutAdmin.DeletePicklistLayout(Guid tenantId, Guid layoutId)
        {
            _data.DeletePicklistLayout(tenantId, layoutId);
        }

        void ILayoutAdmin.DeleteListLayout(Guid tenantId, Guid layoutId)
        {
            _data.DeleteListLayout(tenantId, layoutId);
        }

        public void UpdatePicklistLayout(Guid tenantId, Guid layoutId, LayoutModel layout)
        {
            _data.UpdatePicklistLayout(tenantId, layoutId, layout);
        }
    }
}
