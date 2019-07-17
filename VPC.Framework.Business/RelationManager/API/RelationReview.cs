using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using VPC.Entities.EntityCore.Model.Query;
using VPC.Entities.EntityCore.Model.Storage;
using VPC.Framework.Business.EntityResourceManager.Data;
using VPC.Framework.Business.MetadataManager.Data;

namespace VPC.Framework.Business.RelationManager.APIs
{
    public interface IRelationReview
    {
        DataTable GetResult(Guid tenantId, string entityName, string query);
    }

    public class RelationReview : IRelationReview
    {
        public DataTable GetResult(Guid tenantId, string entityName, string query)
        {
            var data = new QueryData();
            return data.GetResources(tenantId, entityName, query);
        }
    }
}
