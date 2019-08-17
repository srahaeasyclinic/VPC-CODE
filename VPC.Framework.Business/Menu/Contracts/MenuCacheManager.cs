using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VPC.Cache;
using VPC.Framework.Business.Menu.API;

namespace VPC.Framework.Business.Menu.Contracts
{
    public interface IMenuCacheManager
    {
        string GetCacheKey(Guid tenantId);
        void ClearCashe<T>(string cacheKey);

    }
    public sealed class MenuCacheManager : IMenuCacheManager
    {
        private readonly IMenuReview _review;

        public MenuCacheManager()
        {
            _review = new MenuReview();

        }

        void IMenuCacheManager.ClearCashe<T>(string cacheKey)
        {
            if (VPCCache.GetInstance().Contains<List<T>>(cacheKey))
            {
                VPCCache.GetInstance().Remove<List<T>>(cacheKey);
            }
        }

        string IMenuCacheManager.GetCacheKey(Guid tenantId)
        {
            var MenuKeyVal = _review.GetMenuBytenant(tenantId).OrderBy(x => x.Name).ToList();

            return String.Format("{0}-{1}", tenantId.ToString(), MenuKeyVal);


        }
    }
}
