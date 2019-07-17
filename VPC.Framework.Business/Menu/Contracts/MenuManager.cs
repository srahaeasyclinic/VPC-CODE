using System;
using System.Collections.Generic;
using System.Linq;
using VPC.Entities.EntityCore.Metadata;
using VPC.Framework.Business.Menu.API;

namespace VPC.Framework.Business.Menu.Contracts
{
    public interface IMenuManager
    {
        List<MenuItem> GetMenu(Guid tenantId, string groupName, int pageIndex, int pageSize);
        MenuItem GetMenuById(Guid tenantId, Guid id);        
        Guid CreateMenu(MenuItem menuModel, Guid userId, Guid tenantId);
        void UpdateMenu(Guid tenantId, Guid menuId, MenuItem menuModel);
        void DeleteMenu(Guid tenantId, Guid menuId);
    }

    public sealed class MenuManager : IMenuManager
    {
        private readonly IMenuAdmin _admin;
        private readonly IMenuReview _review;

        public MenuManager()
        {
            _admin = new MenuAdmin();
            _review = new MenuReview();
        }

        List<MenuItem> IMenuManager.GetMenu(Guid tenantId, string groupName, int pageIndex, int pageSize)
        {
            List<MenuItem> menus = new List<MenuItem>();
            menus = _review.GetMenu(tenantId, groupName, pageIndex, pageSize).OrderBy(x => x.Name).ToList();           
            return menus;
        }

        MenuItem IMenuManager.GetMenuById (Guid tenantId, Guid id) {
            return _review.GetMenuById (tenantId, id);
        }

        Guid IMenuManager.CreateMenu(MenuItem menuModel, Guid userId, Guid tenantId)
        {
            menuModel.Id = Guid.NewGuid();
            menuModel.ModifiedBy = userId;
            _admin.CreateMenu(tenantId, menuModel);
            return menuModel.Id;
        }

        void IMenuManager.UpdateMenu(Guid tenantId, Guid menuId, MenuItem menuModel)
        {
            _admin.UpdateMenu(tenantId, menuId, menuModel);
        }

        void IMenuManager.DeleteMenu (Guid tenantId, Guid menuId) {
            _admin.DeleteMenu (tenantId, menuId);
        }
    }
}