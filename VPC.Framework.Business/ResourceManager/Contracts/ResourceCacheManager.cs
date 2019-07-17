using System;
using System.Collections.Generic;
using System.Text;
using VPC.Cache;
using VPC.Framework.Business.ResourceManager.API;

namespace VPC.Framework.Business.ResourceManager.Contracts
{
    public interface IResourceCacheManager
    {
        string GetCacheKey(Guid tenantId, string language = null);
        void ClearCashe<T>(string cacheKey);

    }


    public sealed class ResourceCacheManager : IResourceCacheManager
    {
        private readonly IResourceReview _resourceReview;        

        public ResourceCacheManager()
        {
            _resourceReview = new ResourceReview();
           
        }

        void IResourceCacheManager.ClearCashe<T>(string cacheKey)
        {
            if (VPCCache.GetInstance().Contains<List<T>>(cacheKey))
            {
                VPCCache.GetInstance().Remove<List<T>>(cacheKey);
            }
        }      

        string IResourceCacheManager.GetCacheKey(Guid tenantId, string language)
        {
            var languageKeyVal = _resourceReview.GetKeyFromLanguage(tenantId,language);

            return String.Format("{0}-{1}", tenantId.ToString(), languageKeyVal);


        }
    }
}
