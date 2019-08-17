using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using VPC.Entities.EntityCore.Model.Resource;
using VPC.Framework.Business.Initilize;
using VPC.Framework.Business.Initilize.APIs;
using VPC.Framework.Business.Initilize.Contracts;
using VPC.Framework.Business.ResourceManager.Data;
using VPC.Metadata.Business.Entity.Infrastructure; //.Contracts;
using VPC.Entities.EntityCore;

namespace VPC.Framework.Business.ResourceManager.API {
    public interface IResourceAdmin {
        bool Create (Guid tenantId, Resource resource, Guid userId, ref string strMsg);
        bool CreateResources (Guid rootTenantId, Guid currentTenantId, List<Resource> resources, string defaultLanguage);
        bool Update (Guid resourceId, Guid tenantId, Resource resource, Guid userId, ref string strMsg);
        bool Delete (Guid tenantId, Guid resourceId);
        bool DeleteByKey (Guid tenantId, string resourceKey);
        bool ResetResources (Guid tenantId, List<Resource> staticResource, string defaultLanguage = "en-US"); //List<Resource> 
        List<Resource> GetResourcesForRepair (Guid tenantId, string language); //, string defaultLanguage = "en-US"
    }

    public class ResourceAdmin : IResourceAdmin {
        private readonly DataResource dataResource = new DataResource ();
        private readonly IInitilizeReview _review = new InitilizeReview ();
        private readonly InitilizeManager _initManager = new InitilizeManager ();

        bool IResourceAdmin.Create (Guid tenantId, Resource resource, Guid userId, ref string strMsg) {
            return dataResource.Create (tenantId, resource, userId, ref strMsg);
        }

        bool IResourceAdmin.CreateResources (Guid rootTenantId, Guid currentTenantId, List<Resource> resources, string defaultLanguage) {
            return dataResource.CreateResources (rootTenantId, currentTenantId, resources, defaultLanguage);
        }

        bool IResourceAdmin.Delete (Guid tenantId, Guid resourceId) {
            return dataResource.Delete (tenantId, resourceId);
        }

        bool IResourceAdmin.DeleteByKey (Guid tenantId, string resourceKey) {
            return dataResource.DeleteByKey (tenantId, resourceKey);
        }

        bool IResourceAdmin.Update (Guid resourceId, Guid tenantId, Resource resource, Guid userId, ref string strMsg) {
            return dataResource.Update (resourceId, tenantId, resource, userId, ref strMsg);
        }
        // Added by Soma on 26/7/19
        bool IResourceAdmin.ResetResources (Guid tenantId, List<Resource> staticResource, string defaultLanguage = "en-US") //InitilizeResponseMessage List<Resource>
        {
            //List<Resource> resourceList = new List<Resource>();
            var responses = new InitilizeResponseMessage ();
            responses.Info = new List<Informatiom> ();
            if (staticResource.Count > 0)
                dataResource.ResetResources (tenantId, staticResource, defaultLanguage = "en-US"); //"en-US"

            var rootTenantCode = _review.getRootTenantCode ();
            var info = new Informatiom ();
            if (rootTenantCode == Guid.Empty) {
                info.ErrorLevel = 1;
                info.Message = "Root tenant not found.";
                responses.Info.Add (info);
                return false; // resourceList responses; 
            }

            List<TypeInfo> metadataClasses = Assembly
                .GetEntryAssembly ()
                .GetReferencedAssemblies ()
                .Select (Assembly.Load)
                .SelectMany (x => x.DefinedTypes)
                .Where (type =>
                    (typeof (EntityBase).GetTypeInfo ().IsAssignableFrom (type.AsType ()) ||
                    typeof (PicklistBase).GetTypeInfo ().IsAssignableFrom (type.AsType ())) &&
                    !type.IsAbstract).ToList ();
            if (metadataClasses.Count > 0) {
                _initManager.InitilizeResources (rootTenantCode, tenantId, metadataClasses);
            } else {
                _initManager.InitilizeResources (rootTenantCode, tenantId);
            }
            return true;
            //int totCount = 0;
            //resourceList = dataResource.GetResourcesDetails(tenantId, 0, 0, "key", ref totCount, defaultLanguage); //    .GetResources(tenantId);
            //return (resourceList);

        }
        List<Resource> IResourceAdmin.GetResourcesForRepair (Guid tenantId, string defaultLanguage = "en-US") //InitilizeResponseMessage List<Resource> // , string defaultLanguage = "en-US"
        {
            List<Resource> resourceList = new List<Resource> ();
            var responses = new InitilizeResponseMessage ();
            responses.Info = new List<Informatiom> ();

            var rootTenantCode = _review.getRootTenantCode ();
            var info = new Informatiom ();
            if (rootTenantCode == Guid.Empty) {
                info.ErrorLevel = 1;
                info.Message = "Root tenant not found.";
                responses.Info.Add (info);
                return null; // resourceList responses; 
            }

            List<TypeInfo> metadataClasses = Assembly
                .GetEntryAssembly ()
                .GetReferencedAssemblies ()
                .Select (Assembly.Load)
                .SelectMany (x => x.DefinedTypes)
                 .Where (type =>
                    (typeof (EntityBase).GetTypeInfo ().IsAssignableFrom (type.AsType ()) ||
                    typeof (PicklistBase).GetTypeInfo ().IsAssignableFrom (type.AsType ())) &&
                    !type.IsAbstract).ToList ();
            if (metadataClasses.Count > 0) {
                resourceList = _initManager.InitialiseForRepairResources (rootTenantCode, tenantId, metadataClasses);
            } else {
                resourceList = _initManager.InitialiseForRepairResources (rootTenantCode, tenantId);
            }
            return resourceList;
        }
    }
}