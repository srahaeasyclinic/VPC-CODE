using System;
using System.Collections.Generic;
using System.Text;
using VPC.Framework.Business.EntityResourceManager.Data;

namespace VPC.Framework.Business.RelationManager.APIs
{
   
    public interface IRelationQueryAdmin
    {
        bool SaveResult(Guid tenantId, string entityName, string query);
        bool UpdateResult(Guid tenantId, string entityName, string query);
        bool DeleteResult(Guid tenantId, string resourceName, string query);
    }

    public class RelationQueryAdmin : IRelationQueryAdmin
    {
        bool IRelationQueryAdmin.DeleteResult(Guid tenantId, string resourceName, string query)
        {
            var data = new QueryData();
            return data.DeleteResult(tenantId, resourceName, query);
        }

        bool IRelationQueryAdmin.SaveResult(Guid tenantId, string entityName, string query)
        {
            var data = new QueryData();
            return data.SaveResult(tenantId, entityName, query);
        }

        bool IRelationQueryAdmin.UpdateResult(Guid tenantId, string entityName, string query)
        {
            var data = new QueryData();
            return data.UpdateResult(tenantId, entityName, query);
        }
    }
}
