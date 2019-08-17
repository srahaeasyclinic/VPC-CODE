using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using VPC.Cache;
using VPC.Entities.EntityCore.Model.Resource;
using VPC.Framework.Business.ResourceManager.API;
using VPC.Framework.Business.Tenant.Contracts;

namespace VPC.Framework.Business.ResourceManager.Contracts
{

    public interface IResourceManager
    {
        List<Resource> GetResources(Guid tenentId);
        List<Resource> GetResources(Guid tenantId, string language);
        List<Resource> GetResourcesByKeyAndLanguage(Guid tenantId, string key, string language);
        List<Resource> GetResourcesByKey(Guid tenantId, string key);
        List<Resource> GetResourcesDetails(string language, Guid tenantId, int pageIndex, int pageSize, string orderBy, ref int totalRowCount);
        void CopyResources(Guid rootTenantId, Guid toTenantId);
        List<Resource> GetDuplicateResourceKey(Guid tenantId, string key, string language);

        bool Create(Guid tenantId, Resource resource, Guid userId, ref string strMsg);
        bool CreateResources(Guid rootTenantId, Guid currentTenantId, List<Resource> resources);

        bool Update(Guid resourceId, Guid tenantId, Resource resource, Guid userId, ref string strMsg);
        bool Delete(Guid tenantId, dynamic resourceId);
        bool DeleteByKey(Guid tenantId, string resourceKey);

        bool ClearResourceCache(Guid tenantId);
         bool ResetResources(Guid currentTenantId, List<Resource> staticRresources); //List<Resource>
        List<Resource> GetResourcesForRepair(Guid currentTenantId);
        DefaultResourcelanguage GetDefaultLanguageByTenant(Guid currentTenantId);

        List<Resource> GetResourcesByMenuId(Guid tenantId, Guid MenuId);
    }
    public sealed class ResourceManager : IResourceManager
    {
        private readonly IResourceReview _resourceReview;
        private readonly IResourceAdmin _resourceAdmin;
        private readonly IResourceCacheManager _resourceCacheManager;
        private readonly IManageTenant _tanantManager;

        public ResourceManager()
        {
            _resourceReview = new ResourceReview();
            _resourceAdmin = new ResourceAdmin();
            _resourceCacheManager = new ResourceCacheManager();
            _tanantManager = new TenantManager();



        }
        DefaultResourcelanguage IResourceManager.GetDefaultLanguageByTenant(Guid currentTenantId)
        {
            return _resourceReview.GetDefaultLanguage(currentTenantId);
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

            if (language == null)
                language = _tanantManager.GetTenantLanguageInfo(tenantId).Key;

            var lstResources = _resourceReview.GetResources(tenantId, language);
            VPCCache.GetInstance().Set(cacheKey, lstResources);

            return lstResources;
        }

        bool IResourceManager.Create(Guid tenantId, Resource resource, Guid userId, ref string strMsg)
        {
            ClearAllResourceCache(tenantId);
            resource.Id = Guid.NewGuid();
            return _resourceAdmin.Create(tenantId, resource, userId, ref strMsg);
        }


        bool IResourceManager.Update(Guid resourceId, Guid tenantId, Resource resource, Guid userId, ref string strMsg)
        {
            ClearAllResourceCache(tenantId);
            return _resourceAdmin.Update(resourceId, tenantId, resource, userId, ref strMsg);
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

        List<Resource> IResourceManager.GetResourcesByKey(Guid tenantId, string key)
        {
            return _resourceReview.GetResourcesByKey(tenantId, key);
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
            //Guid longResourceVal;

            //if (Guid.TryParse(resourceId, out longResourceVal))
            //{
            //    isDeleted = _resourceAdmin.Delete(tenantId, longResourceVal);
            //}
            //else
            //{
            //    isDeleted = _resourceAdmin.DeleteByKey(tenantId, resourceId);
            //}

            isDeleted = _resourceAdmin.Delete(tenantId, resourceId);

            return isDeleted;

        }

        bool IResourceManager.CreateResources(Guid rootTenantId, Guid currentTenantId, List<Resource> resources)
        {
            ClearAllResourceCache(rootTenantId);
            string defaultLanguage = GetTanantDefaultLanguage(rootTenantId);
            return _resourceAdmin.CreateResources(rootTenantId, currentTenantId, resources, defaultLanguage);
        }
        //Aded by Soma on 29th July 2019

        bool IResourceManager.ResetResources(Guid currentTenantId, List<Resource> staticRresources) //List<Resource>
        {
            //ClearAllResourceCache(rootTenantId);
            string defaultLanguage = GetTanantDefaultLanguage(currentTenantId);
            return _resourceAdmin.ResetResources(currentTenantId, staticRresources, defaultLanguage);
        }

        bool IResourceManager.ClearResourceCache(Guid tenantId)
        {
            ClearAllResourceCache(tenantId);
            return true;
        }

        string GetTanantDefaultLanguage(Guid TenantId)
        {
            string tanantDefaultLanguage = string.Empty;
            var tenantInfo = _tanantManager.GetTenantLanguageInfo(TenantId);
            if (tenantInfo != null)
            {
                tanantDefaultLanguage = tenantInfo.Key;
            }

            return tanantDefaultLanguage;
        }

        bool IResourceManager.DeleteByKey(Guid tenantId, string resourceKey)
        {
            ClearAllResourceCache(tenantId);
            bool isDeleted = false;
            isDeleted = _resourceAdmin.DeleteByKey(tenantId, resourceKey);

            return isDeleted;
        }
        //Added by Soma on 31/07/2019
        List<Resource> IResourceManager.GetResourcesForRepair(Guid currentTenantId)
        {
            string language = GetTanantDefaultLanguage(currentTenantId);
            return _resourceAdmin.GetResourcesForRepair(currentTenantId, language);
        }

        List<Resource> IResourceManager.GetResourcesByMenuId(Guid tenantId, Guid MenuId)
        {
            return _resourceReview.GetResourcesByMenuId(tenantId, MenuId);
        }
    }

}