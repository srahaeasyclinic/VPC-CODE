using System;
using System.Collections.Generic;
using System.Linq;
using VPC.Cache;
using VPC.Entities.EntityCore.Metadata;
using VPC.Framework.Business.Menu.API;
using VPC.Framework.Business.MetadataManager.Contracts;

namespace VPC.Framework.Business.Menu.Contracts
{
    public interface IMenuManager
    {
        List<MenuItem> GetMenu(Guid tenantId, string groupName, int pageIndex, int pageSize);
        MenuItem GetMenuById(Guid tenantId, Guid id);
        Guid CreateMenu(MenuItem menuModel, Guid userId, Guid tenantId);
        void UpdateMenu(Guid tenantId, Guid menuId, MenuItem menuModel);
        void DeleteMenu(Guid tenantId, Guid menuId);
        bool ClearMenuCache(Guid tenantId);

        List<MenuItem> GetMenuBytenant(Guid tenantId);

        void InitilizeParentMenu(Guid rootTenantCode, Guid initilizedTenantCode);

        void InitilizeParentMenuFromAPI(Guid rootTenantCode, Guid initilizedTenantCode,Guid UserId);
    }

    public sealed class MenuManager : IMenuManager
    {
        private readonly IMenuAdmin _admin;
        private readonly IMenuReview _review;
        private readonly IMenuCacheManager _IMenuCacheManager;


        public MenuManager()
        {
            _admin = new MenuAdmin();
            _review = new MenuReview();
            _IMenuCacheManager = new MenuCacheManager();

        }

        List<MenuItem> IMenuManager.GetMenu(Guid tenantId, string groupName, int pageIndex, int pageSize)
        {
            var entityName = "";
            IMetadataManager _IMetadataManager = new VPC.Framework.Business.MetadataManager.Contracts.MetadataManager();
            List<MenuItem> allmenus = new List<MenuItem>();
            allmenus = _review.GetMenu(tenantId, groupName, pageIndex, pageSize).OrderBy(x => x.Name).ToList();
            foreach (var menuItem in allmenus)
            {
                if (menuItem.ReferenceEntityId != null && menuItem.ReferenceEntityId != "")
                {
                    if (menuItem != null && menuItem.MenuTypeId == 1)
                    {
                        entityName = _IMetadataManager.GetEntityNameByEntityContext(menuItem.ReferenceEntityId, false);
                    }
                    else if (menuItem != null && menuItem.MenuTypeId == 2)
                    {
                        entityName = _IMetadataManager.GetEntityNameByEntityContext(menuItem.ReferenceEntityId, true);
                    }
                    menuItem.ReferenceEntityId = entityName;
                }

            }
            return allmenus;
        }

        List<MenuItem> IMenuManager.GetMenuBytenant(Guid tenantId)
        {
            var cacheKey = _IMenuCacheManager.GetCacheKey(tenantId);
            var entityName = "";

            if (VPCCache.GetInstance().Contains<List<MenuItem>>(cacheKey))
            {
                return VPCCache.GetInstance().Get<List<MenuItem>>(cacheKey);
            }

            IMetadataManager _IMetadataManager = new VPC.Framework.Business.MetadataManager.Contracts.MetadataManager();

            List<MenuItem> allmenus = new List<MenuItem>();

            allmenus = _review.GetMenuBytenant(tenantId).OrderBy(x => x.Name).ToList();

            foreach (var menuItem in allmenus)
            {
                if (menuItem.ReferenceEntityId != null && menuItem.ReferenceEntityId != "")
                {
                    if (menuItem != null && menuItem.MenuTypeId == 1)
                    {
                        entityName = _IMetadataManager.GetEntityNameByEntityContext(menuItem.ReferenceEntityId, false);
                    }
                    else if (menuItem != null && menuItem.MenuTypeId == 2)
                    {
                        entityName = _IMetadataManager.GetEntityNameByEntityContext(menuItem.ReferenceEntityId, true);
                    }
                    menuItem.ReferenceEntityId = entityName;
                }

            }
            VPCCache.GetInstance().Set(cacheKey, allmenus);

            return allmenus;
        }

        MenuItem IMenuManager.GetMenuById(Guid tenantId, Guid id)
        {
            return _review.GetMenuById(tenantId, id);
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

        void IMenuManager.DeleteMenu(Guid tenantId, Guid menuId)
        {
            _admin.DeleteMenu(tenantId, menuId);
        }

        #region MenuCache
        bool IMenuManager.ClearMenuCache(Guid tenantId)
        {
            var cacheKey = _IMenuCacheManager.GetCacheKey(tenantId);
            _IMenuCacheManager.ClearCashe<MenuItem>(cacheKey);

            return true;
        }

        // bool HasMenucache(Guid tenantId, string groupName, int pageIndex, int pageSize)
        // {
        //     var cacheKey = _IMenuCacheManager.GetCacheKey(tenantId, pageIndex, pageSize, groupName);

        //     return VPCCache.GetInstance().Contains<List<MenuItem>>(cacheKey);
        // }

        // List<MenuItem> IMenuManager.GetMenuFromcache(Guid tenantId, string groupName, int pageIndex, int pageSize)
        // {
        //     var cacheKey = _IMenuCacheManager.GetCacheKey(tenantId, pageIndex, pageSize, groupName);

        //     return VPCCache.GetInstance().Get<List<MenuItem>>(cacheKey);

        // }



        // this is for temporary, later the logic will be change 
        void IMenuManager.InitilizeParentMenu(Guid rootTenantCode, Guid initilizedTenantCode)
        {
            InitilizeParentMenu(rootTenantCode, initilizedTenantCode);

        }

        void IMenuManager.InitilizeParentMenuFromAPI(Guid rootTenantCode, Guid initilizedTenantCode,Guid UserId)
        {
                _review.ApplicationMenuinitilization(rootTenantCode, initilizedTenantCode, UserId, 10015);

                _review.Menuinitilization(rootTenantCode, initilizedTenantCode);

                InitilizeParentMenu(rootTenantCode, initilizedTenantCode);
           

        }

        private void InitilizeParentMenu(Guid rootTenantCode, Guid initilizedTenantCode)
        {
            var rootMenu = _review.GetMenuBytenant(rootTenantCode);
            var initmenu = _review.GetMenuBytenant(initilizedTenantCode);

            foreach (var init_item in initmenu.Where(w => w.ParentId != new Guid("00000000-0000-0000-0000-000000000000")).ToList())
            {
                var parentName = rootMenu.Where(w => w.Id == init_item.ParentId).FirstOrDefault();
                if(parentName!=null)
                {
                    init_item.ParentId = initmenu.Where(w => w.Name == parentName.Name && w.Menucode == parentName.Menucode).Select(s => s.Id).FirstOrDefault();

                    _admin.UpdateMenu(initilizedTenantCode, init_item.Id, init_item);
                }
             
            }

        }

        #endregion
    }
}