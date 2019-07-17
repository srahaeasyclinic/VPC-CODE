using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using VPC.Entities.EntityCore.Model.Query;
using VPC.Entities.EntityCore.Model.Storage;
using VPC.Framework.Business.MetadataManager.Contracts;

namespace VPC.WebApi.Utility {
    public static class ApiHelper {

        public static List<QueryFilter> GetQueryFilter (string data, Guid tenantId, bool isSystemAdmin, string entityName) {
            var queryFilters = new List<QueryFilter> ();

            if (isSystemAdmin && entityName.ToLower ().Equals ("tenant")) {

            } else {
                var tenantFilte = new QueryFilter ();
                tenantFilte.FieldName = "TenantId";
                tenantFilte.Operator = "Equal";
                tenantFilte.Value = tenantId.ToString ();
                queryFilters.Add (tenantFilte);
            }

            if (string.IsNullOrEmpty (data)) return queryFilters;
            string[] filters = data.Split ('|');
            if (filters != null && filters.Any ()) {
                for (int i = 0; i < filters.Length; i++) {
                    string[] filterdata = filters[i].Split (',');
                    if (filterdata != null && filterdata.Any () && filterdata.Count () == 2) {
                        var filedName = filterdata[0];
                        var isAdded = queryFilters.Where (t => t.FieldName.Equals (filedName)).Any ();
                      //  var operatorStr = (isAdded) ? "Equal" : "OR";
                        var operatorStr = "Equal";
                        var queryFilter = new QueryFilter ();
                        queryFilter.FieldName = filedName;
                        queryFilter.Operator = operatorStr;
                        queryFilter.Value = filterdata[1];
                        queryFilters.Add (queryFilter);
                    }
                }
            }

            return queryFilters;
        }

        public static void MapDynamicColumn (DataColumnCollection columns) {
            var targetColumn = new List<string> (new string[] { "rowNumber", "item_InternalId", "totalRow" });
            foreach (var item in targetColumn) {
                if (columns.Contains (item)) {
                    columns.Remove (item);
                }
            }
            foreach (DataColumn col in columns) {
                col.ColumnName = col.ColumnName.Replace (@"_", ".");
            }
        }

    }

}