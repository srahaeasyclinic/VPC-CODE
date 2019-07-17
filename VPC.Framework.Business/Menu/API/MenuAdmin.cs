using System;
using VPC.Entities.EntityCore.Metadata;
using VPC.Framework.Business.Menu.Data;

namespace VPC.Framework.Business.Menu.API
{
    public interface IMenuAdmin
    {
        void CreateMenu(Guid tenantId, MenuItem menuModel);
        void UpdateMenu(Guid tenantId, Guid menuId, MenuItem menuModel);
        void DeleteMenu(Guid tenantId, Guid menuId);
    }

    internal class MenuAdmin : IMenuAdmin
    {
        private readonly MenuData _data = new MenuData();

        void IMenuAdmin.CreateMenu(Guid tenantId, MenuItem menuModel)
        {
            _data.CreateMenu(tenantId, menuModel);
        }

        void IMenuAdmin.UpdateMenu(Guid tenantId, Guid menuId, MenuItem menuModel)
        {
            _data.UpdateMenu(tenantId, menuId, menuModel);
        }

        void IMenuAdmin.DeleteMenu(Guid tenantId, Guid menuId)
        {
            _data.DeleteMenu(tenantId, menuId);
        }
    }
}