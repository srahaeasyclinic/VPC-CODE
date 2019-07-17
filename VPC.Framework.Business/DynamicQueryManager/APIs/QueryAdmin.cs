using System;
using System.Collections.Generic;
using System.Text;
using VPC.Framework.Business.EntityResourceManager.Data;

namespace VPC.Framework.Business.DynamicQueryManager.APIs
{
   
    public interface IQueryAdmin
    {
        bool SaveResult(Guid tenantId, string entityName, string query);
        bool UpdateResult(Guid tenantId, string entityName, string query);
        bool DeleteResult(Guid tenantId, string resourceName, string query);
        bool ExecuteUpdateQuery(string queryRes);
    }

    public class QueryAdmin : IQueryAdmin
    {
        bool IQueryAdmin.DeleteResult(Guid tenantId, string resourceName, string query)
        {
            var data = new QueryData();
            return data.DeleteResult(tenantId, resourceName, query);
        }

        bool IQueryAdmin.ExecuteUpdateQuery(string queryRes)
        {
           var data = new QueryData();
            return data.ExecuteUpdateQuery(queryRes);
        }

        bool IQueryAdmin.SaveResult(Guid tenantId, string entityName, string query)
        {
            var data = new QueryData();
            return data.SaveResult(tenantId, entityName, query);
        }

        bool IQueryAdmin.UpdateResult(Guid tenantId, string entityName, string query)
        {
            var data = new QueryData();
            return data.UpdateResult(tenantId, entityName, query);
        }
    }
}
