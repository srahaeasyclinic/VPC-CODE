using System;
using System.Collections.Generic;
using System.Text;
using VPC.Entities.EntityCore.Model.Resource;
using VPC.Framework.Business.ResourceManager.Data;

namespace VPC.Framework.Business.ResourceManager.API
{
    public interface IResourceAdmin
    {
        bool Create(Guid tenantId, Resource resource, ref string strMsg);
        bool CreateResources(Guid rootTenantId, Guid currentTenantId, List<Resource> resources);
        bool Update(Guid resourceId, Guid tenantId, Resource resource, ref string strMsg);
        bool Delete(Guid tenantId, Guid resourceId);
        bool DeleteByKey(Guid tenantId, string resourceKey);
    }


    public class ResourceAdmin : IResourceAdmin
    {
        private readonly DataResource dataResource = new DataResource();
        

        bool IResourceAdmin.Create(Guid tenantId, Resource resource, ref string strMsg)
        {
            return dataResource.Create(tenantId, resource,  ref strMsg);
        }

        bool IResourceAdmin.CreateResources(Guid rootTenantId, Guid currentTenantId, List<Resource> resources)
        {
            return dataResource.CreateResources(rootTenantId, currentTenantId, resources);
        }

        bool IResourceAdmin.Delete(Guid tenantId, Guid resourceId)
        {
            return dataResource.Delete(tenantId, resourceId);
        }

        bool IResourceAdmin.DeleteByKey(Guid tenantId, string resourceKey)
        {
            return dataResource.DeleteByKey(tenantId, resourceKey);
        }

        bool IResourceAdmin.Update(Guid resourceId,Guid tenantId, Resource resource, ref string strMsg)
        {
            return dataResource.Update(resourceId, tenantId, resource, ref strMsg);
        }
    }
}
