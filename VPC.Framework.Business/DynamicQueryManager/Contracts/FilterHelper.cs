using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using VPC.Entities.EntityCore.Metadata.Runtime;
using VPC.Entities.EntityCore.Model.Query;
using VPC.Entities.EntityCore.Model.Storage;
using VPC.Entities.SampleEntity;
using VPC.Framework.Business.Data;
using VPC.Framework.Business.Data.SqlClient;
using VPC.Framework.Business.DynamicQueryManager.Core;
using VPC.Framework.Business.DynamicQueryManager.Core.Enums;
using VPC.Framework.Business.MetadataManager.Contracts;
using VPC.Metadata.Business.Operator.DataAnnotations;
using Comparison = VPC.Framework.Business.DynamicQueryManager.Core.Enums.Comparison;
namespace VPC.Framework.Business.EntityResourceManager.Contracts {
    internal static class FilterHelper {

        internal static void AddSimpleSearch (QueryContext queryModel, SelectQueryBuilder queryBuilder, List<ColumnAndField> allColumns) {
            var level = 1;
            if (queryModel.Filters != null && queryModel.Filters.Any () && queryModel.FreeTextSearch != null && queryModel.FreeTextSearch.Any ()) {
                //@todo need to add table relations..
                foreach (var item in queryModel.FreeTextSearch) {
                    queryBuilder.AddWhereWithGroup (item.FieldName, GetComparisonValue (item.Operator), item.Value, level, "", "freeTextSearch");
                }
                foreach (var item in queryModel.Filters) {
                    queryBuilder.AddWhere (item.FieldName, GetComparisonValue (item.Operator), item.Value, level);
                }
            } else {
                if (queryModel.Filters != null && queryModel.Filters.Any ()) {
                    foreach (var item in queryModel.Filters) {
                        var filterColumn = allColumns.FirstOrDefault (t => t.FieldName.ToLower ().Equals (item.FieldName.ToLower ()));
                        if (filterColumn != null) {
                            item.FieldName = filterColumn.EntityPrefix + "." + filterColumn.ColumnName;
                        }
                        queryBuilder.AddWhere (item.FieldName, GetComparisonValue (item.Operator), item.Value, level);
                    }
                }
                if (queryModel.FreeTextSearch != null && queryModel.FreeTextSearch.Any ()) {
                    foreach (var item in queryModel.FreeTextSearch) {
                        var filterColumn = allColumns.FirstOrDefault (t => t.FieldName.ToLower ().Equals (item.FieldName.ToLower ()));
                        if (filterColumn != null) {
                            item.FieldName = filterColumn.EntityPrefix + "." + filterColumn.ColumnName;
                        }
                        queryBuilder.AddWhere (item.FieldName, GetComparisonValue (item.Operator), item.Value, level);
                        level++;
                    }
                }
            }
        }
        private static Comparison GetComparisonValue (string value) {
            Type myType = typeof (Operators);
            var properties = myType.GetProperties (BindingFlags.Public | BindingFlags.Static);
            var result = Comparison.Equals;
            Enum.TryParse (value, out Comparison myStatus);
            foreach (var item in properties) {
                var res = (dynamic) item.GetValue (null);
                if (res.ToString ().ToLower () != myStatus.ToString ().ToLower ()) continue;
                Object[] attribute = item.GetCustomAttributes (typeof (OperatorSqlMappertAttribute), true);
                if (attribute.Length <= 0) continue;
                OperatorSqlMappertAttribute myAttribute = (OperatorSqlMappertAttribute) attribute[0];
                result = (Comparison) myAttribute.Value;
            }
            return result;
        }

        internal static void MapQueryFilter (Guid tenantId, string entityName, QueryContext queryModel) {
            IMetadataManager entityManager = new MetadataManager.Contracts.MetadataManager ();
            var allColumns = entityManager.GetColumnNameByEntityName (entityName, null);
            if (queryModel.FreeTextSearch != null && queryModel.FreeTextSearch.Any ()) {
                MapData (queryModel.FreeTextSearch, allColumns);
            }
            if (queryModel.Filters != null && queryModel.Filters.Any ()) {
                MapData (queryModel.FreeTextSearch, allColumns);
            }
            var orderByArr = queryModel.OrderBy.Split (',');
            if (orderByArr.Count () > 1) {
                // Sorting orderEnum = Sorting.Ascending;
                // switch (orderByArr[1].Trim ().ToUpper ()) {
                //     case "ASC":
                //         orderEnum = Sorting.Ascending;
                //         break;
                //     case "DESC":
                //         orderEnum = Sorting.Descending;
                //         break;
                // }
                // var orderByColumnData = allColumns.FirstOrDefault (t => t.FieldName.ToLower ().Equals (orderByArr[0].ToLower ()));
                // if (orderByColumnData != null) {
                //     queryModel.OrderBy = orderByColumnData.EntityPrefix + "." + orderByColumnData.ColumnName;
                //     //queryBuilder.AddOrderBy (orderByColumnData.EntityPrefix + "." + orderByColumnData.ColumnName, orderEnum);
                // }
            }
        }

        private static void MapData (List<QueryFilter> filters, List<ColumnAndField> allColumns) {
            foreach (var item in filters) {
                var filterColumn = allColumns.FirstOrDefault (t => t.FieldName.ToLower ().Equals (item.FieldName.ToLower ()));
                if (filterColumn != null) {
                    item.FieldName = filterColumn.EntityPrefix + "." + filterColumn.ColumnName;
                }
            }
        }

    }
}