using System;
using System.Collections.Generic;
using VPC.Entities.EntityCore.Model.Resource;
using VPC.Framework.Business.ResourceManager.Data;

namespace VPC.Framework.Business.ResourceManager.API
{
    interface IResourceReview
    {
        List<Resource> GetResourcesDetails(Guid tenantId, int pageIndex, int pageSize, string orderBy, ref int totalRowCount,string language = null);
        List<Resource> GetResources(Guid tenentId);
        List<Resource> GetResources(Guid tenantId, string language);
        List<Resource> GetResourcesByKeyAndLanguage(Guid tenantId, string key, string language);
        List<Resource> GetResourcesByKey(Guid tenantId, string key);        
        void CopyResources(Guid rootTenantId, Guid toTenantId);

        string GetKeyFromLanguage(Guid tenantId, string language);

        List<Resource> GetDuplicateResourceKey(Guid tenantId, string key, string language);

        DefaultResourcelanguage GetDefaultLanguage(Guid tenantId);

        List<Resource> GetResourcesByMenuId(Guid tenantId, Guid MenuId);
    }
    public class ResourceReview: IResourceReview
    {
        private readonly DataResource _data = new DataResource();

        DefaultResourcelanguage IResourceReview.GetDefaultLanguage(Guid tenantId)
        {
            return _data.GetLanguageByTenant(tenantId);
        }
        List<Resource> IResourceReview.GetResources(Guid tenantId, string language)
        {
            return _data.GetResources(tenantId, language);
        }

        void IResourceReview.CopyResources(Guid rootTenantId, Guid toTenantId)
        {
            _data.CopyResources(rootTenantId, toTenantId);
        }

        List<Resource> IResourceReview.GetResources(Guid tenentId)
        {
            return _data.GetResources(tenentId, null);
        }

        //List<Resource> IResourceReview.GetResourcesDetails(Guid tenantId, int pageIndex, int pageSize, string orderBy, string language)
        //{
        //    return _data.GetResourcesDetails(tenantId, pageIndex, pageSize, orderBy, language);
        //}

        List<Resource> IResourceReview.GetResourcesByKeyAndLanguage(Guid tenantId, string key, string language)
        {
            return _data.GetResourcesByKeyAndLanguage(tenantId, key, language);
        }

        List<Resource> IResourceReview.GetResourcesByKey(Guid tenantId, string key)
        {
            return _data.GetResourcesByKey(tenantId, key);
        }

        List<Resource> IResourceReview.GetResourcesDetails(Guid tenantId, int pageIndex, int pageSize, string orderBy, ref int totalRowCount, string language)
        {
            return _data.GetResourcesDetails(tenantId, pageIndex, pageSize, orderBy, ref totalRowCount, language);
        }

        string IResourceReview.GetKeyFromLanguage(Guid tenantId, string language)
        {
            return _data.GetKeyFromLanguage(tenantId, language);
        }

        List<Resource> IResourceReview.GetDuplicateResourceKey(Guid tenantId, string key, string language)
        {
            return _data.GetDuplicateResourceKey(tenantId, key, language);
        }

        List<Resource> IResourceReview.GetResourcesByMenuId(Guid tenantId, Guid MenuId)
        {
            return _data.GetResourcesByMenuId(tenantId, MenuId);
        }
    }
}