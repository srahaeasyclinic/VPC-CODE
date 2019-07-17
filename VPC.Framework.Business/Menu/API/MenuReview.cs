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

        // List<MenuItem> IMenuReview.GetMenuByGroupName(Guid tenantId, string groupName)
        // {
        //     List<MenuItem> menus = new List<MenuItem>();
        //     //menus = _data.GetMenuByGroupName(tenantId, groupName).OrderBy(x => x.Name).ToList();
          
        //     return menus;
        // }
    }

}