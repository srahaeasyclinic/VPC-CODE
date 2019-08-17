using System;
using System.Collections.Generic;
using VPC.Entities.EntityCore.Metadata;
using VPC.Framework.Business.Menu.Data;

namespace VPC.Framework.Business.Menu.API
{
    public interface IMenuReview
    {
        List<MenuItem> GetMenu(Guid tenantId, string groupName, int pageIndex, int pageSize);
        MenuItem GetMenuById(Guid tenantId, Guid id);

        List<MenuItem> GetMenuBytenant(Guid tenantId);

        void Menuinitilization(Guid rootTenantid, Guid InitTenantid);
        void ApplicationMenuinitilization(Guid rootTenantid, Guid InitTenantid, Guid userid,short PicklistId);
    }

    internal class MenuReview : IMenuReview
    {
        private readonly MenuData _data = new MenuData();

        List<MenuItem> IMenuReview.GetMenu(Guid tenantId, string groupName, int pageIndex, int pageSize)
        {
            return _data.GetMenu(tenantId, groupName, pageIndex, pageSize);
        }

        MenuItem IMenuReview.GetMenuById(Guid tenantId, Guid id)
        {
            return _data.GetMenuById(tenantId, id);
        }

        List<MenuItem> IMenuReview.GetMenuBytenant(Guid tenantId)
        {
            return _data.GetMenuByTenant(tenantId);
        }


        void IMenuReview.Menuinitilization(Guid rootTenantid, Guid InitTenantid)
        {
            _data.InitializeMenu(rootTenantid, InitTenantid);
        }
        void IMenuReview.ApplicationMenuinitilization(Guid rootTenantid, Guid InitTenantid, Guid userid, short PicklistId)
        {
            _data.InitializeApplicationMenu(rootTenantid, InitTenantid, userid, PicklistId);
        }
        // List<MenuItem> IMenuReview.GetMenuByGroupName(Guid tenantId, string groupName)
        // {
        //     List<MenuItem> menus = new List<MenuItem>();
        //     //menus = _data.GetMenuByGroupName(tenantId, groupName).OrderBy(x => x.Name).ToList();

        //     return menus;
        // }
    }

}