using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json.Linq;
using VPC.Entities.Common;
using VPC.Entities.EntityCore.Model.PickListItem;
using VPC.Entities.EntityCore.Model.Query;
using VPC.Entities.EntityCore.Model.Storage;
using VPC.Framework.Business.Common;
using VPC.Framework.Business.DynamicQueryManager.Contracts;
using VPC.Framework.Business.EntityResourceManager.Contracts;
using VPC.Framework.Business.Exception;
using VPC.Framework.Business.MetadataManager.API;
using VPC.Metadata.Business.DataAnnotations;
using VPC.Metadata.Business.DataTypes;
using VPC.Metadata.Business.Entity;
using VPC.Metadata.Business.Entity.Configuration;
using VPC.Metadata.Business.Entity.Infrastructure;
using VPC.Metadata.Business.SearchFilter;

namespace VPC.Framework.Business.MetadataManager.Contracts {
    public static class MetadataHelper {
        public static QueryContext GetQueryContext (string pickListName, int pageIndex = 1, int pageSize = 10, string filters = "") {
            var resultQuery = new QueryContext ();
            resultQuery.PageIndex = pageIndex;
            resultQuery.PageSize = pageSize;
            if (string.IsNullOrWhiteSpace (filters)) return resultQuery;
            var result = filters.Split (',');
            if (!result.Any ()) return resultQuery;
            resultQuery.Filters = new List<QueryFilter> ();

            IMetadataManager entityManager = new VPC.Framework.Business.MetadataManager.Contracts.MetadataManager ();
            var entityColumns = entityManager.GetColumnNameByEntityName (pickListName, null);
            foreach (var filter in result) {
                var keyValue = filter.Split ('=');
                if (!keyValue.Any ()) continue;
               // var match = entityColumns.FirstOrDefault (t => t.FieldName.ToLower ().Equals (keyValue[0].ToLower ()));
                var match = entityColumns.FirstOrDefault (t => t.ColumnName.Equals (t.PrimaryKey) && t.EntityFullName.ToLower().Equals(pickListName.ToLower()));
                if (match != null) {
                    var myContextFilter = new QueryFilter ();
                    myContextFilter.FieldName = match.EntityPrefix + "." + match.ColumnName;
                    myContextFilter.Value = keyValue[1];
                    myContextFilter.Operator = "=";
                    resultQuery.Filters.Add (myContextFilter);
                }
            }
            return resultQuery;
        }
    }
}