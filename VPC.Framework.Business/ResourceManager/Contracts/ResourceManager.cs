using System;
using System.Collections.Generic;
using VPC.Cache;
using VPC.Entities.EntityCore.Model.Resource;
using VPC.Framework.Business.ResourceManager.API;

namespace VPC.Framework.Business.ResourceManager.Contracts
{

    public interface IResourceManager
    {
        List<Resource> GetResources(Guid tenentId);
        List<Resource> GetResources(Guid tenantId, string language);
        List<Resource> GetResourcesByKeyAndLanguage(Guid tenantId, string key, string language);
        List<Resource> GetResourcesDetails(string language, Guid tenantId, int pageIndex, int pageSize, string orderBy, ref int totalRowCount);
        void CopyResources(Guid rootTenantId, Guid toTenantId);
        List<Resource> GetDuplicateResourceKey(Guid tenantId, string key, string language);

        bool Create(Guid tenantId, Resource resource, ref string strMsg);
        bool CreateResources(Guid rootTenantId, Guid currentTenantId, List<Resource> resources);

        bool Update(Guid resourceId,Guid tenantId, Resource resource, ref string strMsg);
        bool Delete(Guid tenantId, dynamic resourceId);


        bool ClearResourceCache(Guid tenantId);
    }
    public sealed class ResourceManager : IResourceManager
    {
        private readonly IResourceReview _resourceReview;
        private readonly IResourceAdmin _resourceAdmin;
        private readonly IResourceCacheManager _resourceCacheManager;

        public ResourceManager()
        {
            _resourceReview = new ResourceReview();
            _resourceAdmin = new ResourceAdmin();
            _resourceCacheManager = new ResourceCacheManager();
            
        }

        List<Resource> IResourceManager.GetResources(Guid tenantId)
        {
            //string  language = "en-US";
            //  return _review.GetResources(tenantId, language);
            return _resourceReview.GetResources(tenantId, null);
        }

        void IResourceManager.CopyResources(Guid rootTenantId, Guid toTenantId)
        {
            _resourceReview.CopyResources(rootTenantId, toTenantId);
        }


        List<Resource> IResourceManager.GetResources(Guid tenantId, string language)
        {
            var cacheKey = _resourceCacheManager.GetCacheKey(tenantId, language); 

            // if (VPCCache.GetInstance().Contains<List<Resource>>(cacheKey))
            // {
            //     return VPCCache.GetInstance().Get<List<Resource>>(cacheKey);
            // }

            var lstResources = _resourceReview.GetResources(tenantId, language);
            VPCCache.GetInstance().Set(cacheKey, lstResources);

            return lstResources;
        }

        bool IResourceManager.Create(Guid tenantId, Resource resource, ref string strMsg)
        {
            ClearAllResourceCache(tenantId);
            resource.Id = Guid.NewGuid();
            return _resourceAdmin.Create(tenantId, resource, ref strMsg);
        }


        bool IResourceManager.Update(Guid resourceId, Guid tenantId, Resource resource, ref string strMsg)
        {
            ClearAllResourceCache(tenantId);
            return _resourceAdmin.Update(resourceId, tenantId, resource, ref strMsg);
        }

        //bool IResourceManager.Delete(Guid tenantId, long resourceId)
        //{
        //    ClearAllResourceCache(tenantId);
        //    return _resourceAdmin.Delete(tenantId, resourceId);
        //}
       

        List<Resource> IResourceManager.GetResourcesByKeyAndLanguage(Guid tenantId, string key, string language)
        {
            return _resourceReview.GetResourcesByKeyAndLanguage(tenantId, key, language); 
        }

        List<Resource> IResourceManager.GetResourcesDetails(string language, Guid tenantId, int pageIndex, int pageSize, string orderBy, ref int totalRowCount)
        {  
            return _resourceReview.GetResourcesDetails(tenantId, pageIndex, pageSize, orderBy, ref totalRowCount, language);
        }

        void ClearAllResourceCache(Guid tenantId)
        {
            var cacheKey = _resourceCacheManager.GetCacheKey(tenantId);
            _resourceCacheManager.ClearCashe<Resource>(cacheKey);

            //var cacheKeyGetALL = _resourceCacheManager.GetCacheKey(tenantId) + "_ALL";
            //_resourceCacheManager.ClearCashe<Resource>(cacheKeyGetALL);           

            //var cacheKeyTotalRowCount = _resourceCacheManager.GetCacheKey(tenantId) + "_" + "totalRows";
            //_resourceCacheManager.ClearCashe<Resource>(cacheKeyTotalRowCount);
        }

        List<Resource> IResourceManager.GetDuplicateResourceKey(Guid tenantId, string key, string language)
        {
            return _resourceReview.GetDuplicateResourceKey(tenantId, key, language);
        }

        bool IResourceManager.Delete(Guid tenantId, dynamic resourceId)
        {
            ClearAllResourceCache(tenantId);
            bool isDeleted = false;
            Guid longResourceVal;

            if (Guid.TryParse(resourceId, out longResourceVal))
            {
                isDeleted = _resourceAdmin.Delete(tenantId, longResourceVal);
            }
            else
            {
                isDeleted = _resourceAdmin.DeleteByKey(tenantId, resourceId);
            }

            return isDeleted;
           
        }

        bool IResourceManager.CreateResources(Guid rootTenantId, Guid currentTenantId, List<Resource> resources)
        {
            return _resourceAdmin.CreateResources(rootTenantId, currentTenantId, resources);
        }

        bool IResourceManager.ClearResourceCache(Guid tenantId)
        {
            ClearAllResourceCache(tenantId);
            return true;
        }
    }
}