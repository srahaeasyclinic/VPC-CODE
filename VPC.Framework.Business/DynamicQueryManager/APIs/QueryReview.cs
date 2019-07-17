using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using VPC.Entities.EntityCore.Model.Query;
using VPC.Entities.EntityCore.Model.Storage;
using VPC.Framework.Business.EntityResourceManager.Data;
using VPC.Framework.Business.MetadataManager.Data;

namespace VPC.Framework.Business.DynamicQueryManager.APIs
{
    public interface IQueryReview
    {
        DataTable GetResult(Guid tenantId, string entityName, string query);
    }

    public class QueryReview : IQueryReview
    {
        public DataTable GetResult(Guid tenantId, string entityName, string query)
        {
            if(string.IsNullOrEmpty(query))return null;
            var data = new QueryData();
            return data.GetResources(tenantId, entityName, query);
        }
    }
}
