using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Net;
using System.Reflection;
using Newtonsoft.Json.Linq;
using VPC.Entities.EntityCore.Model.Query;
using VPC.Entities.SampleEntity;
using VPC.Framework.Business.DynamicQueryManager.APIs;
using VPC.Framework.Business.DynamicQueryManager.Core;
using VPC.Framework.Business.DynamicQueryManager.Core.Clauses;
using VPC.Framework.Business.DynamicQueryManager.Core.Enums;
using VPC.Framework.Business.EntityResourceManager.Contracts;
using VPC.Framework.Business.EntityResourceManager.Data;
using VPC.Framework.Business.MetadataManager.Contracts;
using VPC.Metadata.Business.Entity.Infrastructure;
using VPC.Metadata.Business.Operator.DataAnnotations;
using Comparison = VPC.Framework.Business.DynamicQueryManager.Core.Enums.Comparison;

namespace VPC.Framework.Business.DynamicQueryManager.Contracts {
    public interface IQueryManager {
        string BuildQuery (Guid tenantId, string entityName, QueryContext queryModel, bool isPickList = false);
        DataTable GetResult (Guid tenantId, string entityName, QueryContext queryModel, bool IsPickList);
        Guid SaveResult (Guid tenantId, Guid userId, string resourceName, JObject resource, string subtype, bool isPickList);
        bool UpdateResult (Guid tenantId, Guid resourceId, string resourceName, JObject resource, string subType, bool isPickList);
        bool DeleteResult (Guid tenantId, string resourceName, dynamic resourceId, bool isPickList);
        DataTable GetResultById (Guid tenantId, string entityName, dynamic id, QueryContext queryModel, bool isPickList);
    }

    public class QueryManager : IQueryManager {

        private readonly string _tenantId = "[TenantId]";
        private readonly string _updatedBy = "[UpdatedBy]";
        private readonly string _updatedDate = "[UpdatedDate]";
        private readonly string _pickListId = "[PickListId]";
        //private readonly string _active = "[Active]";
        //private readonly string _isDeleted = "[IsDeletetd]";
        //private readonly string _flagged = "[Flagged]";
        //private readonly string _code = "[Code]";
        //private readonly string _name = "[Name]";

        private string BuildSelectQuery (Guid tenantId, string entityName, QueryContext queryModel, bool isPickList = false, bool pagingRequired = true) {
            IMetadataManager entityManager = new MetadataManager.Contracts.MetadataManager ();
            var tableName = entityManager.GetTableNameByEntityname (entityName);
            var context = entityManager.GetEntityContextByEntityName (entityName, isPickList);
            if (string.IsNullOrEmpty (tableName)) throw new FieldAccessException ("Entity not found.");
            var entityTablePrimaryKey = entityManager.GetPrimaryKeyByEntityname (entityName);


            var result = queryModel?.Fields?.Split (',');

            

            var entityColumns = entityManager.GetColumnNameByEntityName (entityName, null);

            if (result == null || !result.Any ()) {
                var entityResult = entityColumns.Where(
                    t=>t.TableName.Equals(tableName) 
                    && t.IsNotNull
                    && t.EntityFullName.ToLower().Equals(entityName.ToLower())
                    && (!t.ColumnName.Equals(t.PrimaryKey))).ToList().Select(t=>t.FieldName).ToList();
                if(!isPickList){
                    var itemColumns = ItemHelper.GetItemSelectDetails(tenantId, context, 0);
                    entityResult.AddRange(itemColumns.Where(t=>!t.ColumnName.Equals(t.PrimaryKey)).Select(t=>t.FieldName).ToList());
                }
                result = entityResult.ToArray();
            }

            var columns = MapColumnsAsperFielsWithIndex (tenantId, entityName, context, entityColumns, result, isPickList);
            if (columns == null || !columns.Any ()) throw new FieldAccessException ("Entity column is not decorate.");
            if (queryModel != null) {
                if (queryModel.Filters == null) {
                    queryModel.Filters = new List<QueryFilter> ();
                }
                if (isPickList) {
                    var contextColumn = entityColumns.FirstOrDefault (t => t.ColumnName.ToLower ().Equals (_pickListId.ToLower ()) && t.TableName.ToLower ().Equals (tableName.ToLower ()));
                    if (contextColumn != null) {
                        var contextFilter = new QueryFilter {
                        FieldName = contextColumn.EntityPrefix + "." + contextColumn.ColumnName,
                        Value = context
                        };
                        queryModel.Filters.Add (contextFilter);
                    }
                }

                if (queryModel.Filters != null && queryModel.Filters.Any ()) {
                    foreach (var filter in queryModel.Filters) {
                        var filterColumn = columns.FirstOrDefault (t => t.FieldName.ToLower ().Equals (filter.FieldName.ToLower ()));
                        if (filterColumn != null) {
                            filter.FieldName = filterColumn.EntityPrefix + "." + filterColumn.ColumnName;
                        }
                    }
                }

                if (queryModel.FreeTextSearch != null && queryModel.FreeTextSearch.Any ()) {
                    foreach (var textSearch in queryModel.FreeTextSearch) {
                        var filterColumn = columns.FirstOrDefault (t => t.FieldName.ToLower ().Equals (textSearch.FieldName.ToLower ()));
                        if (filterColumn != null) {
                            //textSearch.FieldName = filterColumn.ColumnName;
                            textSearch.FieldName = filterColumn.EntityPrefix + "." + filterColumn.ColumnName;
                        }
                    }
                }
            }
            var matchingColumnsWithSequence = GetMatchingColumnsWithSequenceForSelectQuery (result, columns, tableName, entityTablePrimaryKey, isPickList);
            if (!matchingColumnsWithSequence.Any ()) throw new FieldAccessException ("Column not found");
            var query = GetDbQuery (matchingColumnsWithSequence, queryModel, entityName, pagingRequired);
            return query;
        }
        private void AddForeignKey (SelectQueryBuilder queryBuilder, List<ColumnAndField> columns, string tableName) {
            var foreinkeyTables = columns.Where (
                t => !string.IsNullOrEmpty (t.ReferenceColumnName) &&
                !string.IsNullOrEmpty (t.ReferenceTableName) &&
                t.ReferenceTableName.Equals (tableName)
            ).ToList ();
            if (!foreinkeyTables.Any ()) return; {
                foreach (ColumnAndField item in foreinkeyTables) {
                    var toTableName = item.TableName;
                    var toAlias = item.EntityPrefix;
                    var toColumnName = item.ColumnName;
                    var matching = columns.FirstOrDefault (t => t.TableName.Equals (item.ReferenceTableName) && t.ColumnName.Equals (item.ReferenceColumnName));
                    if (matching != null) {
                        var fromTableName = item.ReferenceTableName;
                        var fromAlias = matching.EntityPrefix;
                        var fromColumnName = item.ReferenceColumnName;
                        queryBuilder.AddJoin (JoinType.InnerJoin, toTableName, toAlias, toColumnName, Comparison.Equals, fromTableName, fromAlias, fromColumnName);
                        AddInverseKey (queryBuilder, columns, item.TableName);
                    }
                }
            }
        }
        private void AddInverseKey (SelectQueryBuilder queryBuilder, List<ColumnAndField> columns, string tableName) {
            var inverseTables = columns.Where (
                t => !string.IsNullOrEmpty (t.InverseColumnName) &&
                !string.IsNullOrEmpty (t.InverseTableName) &&
                t.TableName.Equals (tableName)
            ).ToList ();
            if (!inverseTables.Any ()) return; {
                foreach (ColumnAndField item in inverseTables) {
                    var curTable = item;
                    var toTableName = curTable.InverseTableName;
                    var toAlias = curTable.InversePrefixName;
                    var toColumnName = curTable.InverseColumnName;
                    var matching = columns.FirstOrDefault (t => t.TableName.Equals (item.InverseTableName) && t.ColumnName.Equals (item.InverseColumnName));
                    if (matching != null) {
                        var fromTableName = item.TableName; //matching.TableName; //tableName
                        var fromAlias = item.EntityPrefix;
                        var fromColumnName = item.ColumnName;
                        var joinType = item.IsNotNull?JoinType.InnerJoin : JoinType.LeftJoin;
                        queryBuilder.AddJoin (joinType, toTableName, toAlias, toColumnName, Comparison.Equals, fromTableName, fromAlias, fromColumnName);
                        AddForeignKey (queryBuilder, columns, item.TableName);
                    }
                }
            }
        }

        private string GetDbQuery (List<ColumnAndField> columns, QueryContext queryModel, string entityName, bool pagingRequired = true) {
            var primaryTable = columns.FirstOrDefault (t => t.PrimaryKey.Equals (t.ColumnName) && t.EntityFullName.ToLower ().Equals (entityName.ToLower ()));
            var queryBuilder = new SelectQueryBuilder ();
            if (primaryTable != null) {
                queryBuilder.SelectFromTable (primaryTable.TableName, primaryTable.EntityPrefix);
                var itemTable = columns.FirstOrDefault (t =>
                    t.PrimaryKey.Equals (t.ColumnName) && t.EntityFullName.ToLower ().Equals ("item"));
                if (itemTable != null) {
                    queryBuilder.AddJoin (JoinType.InnerJoin, itemTable.TableName, itemTable.EntityPrefix, itemTable.PrimaryKey, Comparison.Equals,
                        primaryTable.TableName, primaryTable.EntityPrefix, primaryTable.PrimaryKey);
                }
              //  AddForeignKey (queryBuilder, columns, primaryTable.TableName);
                AddInverseKey (queryBuilder, columns, primaryTable.TableName);
            }

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
                        queryBuilder.AddWhere (item.FieldName, GetComparisonValue (item.Operator), item.Value, level);
                    }
                }
                if (queryModel.FreeTextSearch != null && queryModel.FreeTextSearch.Any ()) {
                    foreach (var item in queryModel.FreeTextSearch) {
                        //var prefix = columns.FirstOrDefault (x => x.ColumnName.Equals (item.FieldName));
                        queryBuilder.AddWhere (item.FieldName, GetComparisonValue (item.Operator), item.Value, level);
                        level++;
                    }
                }
            }
            var orderByCol = columns.OrderBy (p => p.QueryIndex).ToList ();
            var toDict = new Dictionary<string, string> ();
            var queryColumns = new List<string> ();
            foreach (var item in orderByCol) {
                var columnName = !string.IsNullOrEmpty (item.ClientName) ? item.ClientName + "." + item.FieldName : item.FieldName;
                queryColumns.Add (item.EntityPrefix + "." + item.ColumnName);
                toDict.Add (item.EntityPrefix + "." + item.ColumnName, columnName);

            }
            queryBuilder.SelectColumns (queryColumns.ToArray ());

            queryBuilder.SelectColumnsAndAliases (toDict);

            if (!string.IsNullOrEmpty (queryModel.OrderBy)) {
                var orderByArr = queryModel.OrderBy.Split (',');
                if (orderByArr.Count () > 1) {
                    Sorting orderEnum = Sorting.Ascending;
                    switch (orderByArr[1].Trim ().ToUpper ()) {
                        case "ASC":
                            orderEnum = Sorting.Ascending;
                            break;
                        case "DESC":
                            orderEnum = Sorting.Descending;
                            break;
                    }
                    var orderByColumnData = columns.FirstOrDefault (t => t.FieldName.ToLower ().Equals (orderByArr[0].ToLower ()));
                    if (orderByColumnData != null) {
                        queryBuilder.AddOrderBy (orderByColumnData.EntityPrefix + "." + orderByColumnData.ColumnName, orderEnum);
                    }
                }
            }

            if (!pagingRequired || queryModel.PageIndex == 0 || queryModel.PageSize == 0) {
                return queryBuilder.BuildQuery ();
            } else {

                queryBuilder.AddRowNo (columns[0].EntityPrefix + "." + columns[0].ColumnName, Sorting.Ascending);
                var query = queryBuilder.BuildQuery ();
                var queryStr = queryBuilder.CreatePaging (query, queryModel.PageIndex, queryModel.PageSize, "RowNumber");
                return queryStr;
            }
        }
        private string BuildInsertQuery (List<ColumnAndField> whatToInsert) {
            var tables = whatToInsert
                .GroupBy (u => u.EntityPrefix)
                .Select (grp => new { key = grp.Key, data = grp.ToList () })
                .ToList ();

            var queryBuilder = new InsertQueryBuilder ();

            foreach (var item in tables) {
                var columns = new Dictionary<string, string> ();
                foreach (var list in item.data) {
                    var match = columns.FirstOrDefault (t => t.Key == list.ColumnName);
                    if (match.Key == null && list.Value != null) {
                        columns.Add (list.ColumnName, list.Value.ToString ());
                    }
                }
                queryBuilder.InsertIntoTable (item.data[0].TableName, columns, tables.Count > 1);
            }
            var query = queryBuilder.BuildQuery ();
            return query;
        }

        private Comparison GetComparisonValue (string value) {
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

        private string CheckValidationRule (List<ColumnAndField> entityColumns, List<ColumnAndField> columsToInsert) {
            var message = string.Empty;
            foreach (var item in entityColumns) {
                if (!item.IsNotNull) continue;
                var found = columsToInsert.FirstOrDefault (x => x.EntityPrefix.Equals (item.EntityPrefix) && x.ColumnName.Equals (item.ColumnName) && x.TableName.Equals (item.TableName));
                if (found == null || found.Value != null) continue;
                message = item.FieldName + " is required";
                break;
            }
            return message;
        }
        private List<ColumnAndField> AddDefaultValueForInsert (Guid tenantId, Guid userId, string entityName, string subtype, Guid itemId, List<ColumnAndField> entityColumns, List<ColumnAndField> matchingColumns, bool isPickList, JObject resource) {

            IMetadataManager iMetadataManager = new MetadataManager.Contracts.MetadataManager ();
            var entityTableName = iMetadataManager.GetTableNameByEntityname (entityName);

            var entityTablePrimaryKey = iMetadataManager.GetPrimaryKeyByEntityname (entityName);
            var entityContext = iMetadataManager.GetEntityContextByEntityName (entityName, isPickList);

            //primary key.
            var primaryEntityId = entityColumns.FirstOrDefault (x => x.ColumnName.Equals (entityTablePrimaryKey) && x.TableName.Equals (entityTableName));
            if (primaryEntityId == null) throw new FieldAccessException ("Primary key not found.");
            primaryEntityId.Value = itemId;
            matchingColumns.Add (primaryEntityId);

            //tenant id. new logic due to tenant table
            var primaryTenantId = entityColumns.FirstOrDefault (x => x.ColumnName.Equals (_tenantId) && x.TableName.Equals (entityTableName));
            if (primaryTenantId != null) {
                primaryTenantId.Value = tenantId;
                matchingColumns.Add (primaryTenantId);
            }

            var updatedBy = entityColumns.FirstOrDefault (x => x.ColumnName.Equals (_updatedBy) && x.TableName.Equals (entityTableName));
            if (updatedBy != null) {
                updatedBy.Value = userId;
            }

            var updatedDate = entityColumns.FirstOrDefault (x => x.ColumnName.Equals (_updatedDate) && x.TableName.Equals (entityTableName));
            if (updatedDate != null) {
                updatedDate.Value = DateTime.UtcNow.ToString ("MM/dd/yyyy HH:mm:ss");
            }

            if (isPickList) {
                //updated date.
                // var updatedDate = entityColumns.FirstOrDefault(x => x.ColumnName.Equals(_updatedDate) && x.TableName.Equals(entityTableName));
                // if(updatedDate == null) throw new FieldAccessException ("Updated date declaration not found.");
                // updatedDate.Value = DateTime.UtcNow;
                // matchingColumns.Add(updatedDate);

                // //updated by.
                // var updatedBy = entityColumns.FirstOrDefault(x => x.ColumnName.Equals(_updatedBy) && x.TableName.Equals(entityTableName));
                // if(updatedBy == null) throw new FieldAccessException ("Updated by Declaration not found.");
                // //@To do user id required......
                // updatedBy.Value = userId; 
                // matchingColumns.Add(updatedBy);
            }

            foreach (var item in entityColumns) {
                var clientName = !string.IsNullOrEmpty(item.ClientName)?item.ClientName+"."+item.FieldName:item.FieldName;
           //  && x.FieldName.Equals(item.FieldName)
                var isAdded = matchingColumns.FirstOrDefault (x => 
                    x.TableName.Equals (item.TableName)
                    && x.ClientName.Equals(item.ClientName)
                );
                if (isAdded != null && item.TableName != entityTableName) {
                    if (item.ReferenceTableName != null && item.ReferenceTableName.Equals (entityTableName)) {
                        item.Value = itemId;
                    }
                    if (item.ColumnName.Equals (_tenantId)) {
                        item.Value = tenantId;
                    }
                    if (item.ColumnName.Equals (item.PrimaryKey)) {
                        item.Value = Guid.NewGuid ();
                        if (!string.IsNullOrEmpty (item.Linker)) {
                            var findLinker = entityColumns.FirstOrDefault (t => t.ColumnName.Equals (item.Linker));
                            if (findLinker != null) {
                                findLinker.Value = item.Value;
                                matchingColumns.Add (findLinker);
                            }
                        }
                    }
                    matchingColumns.Add (item);
                }
            }

            //.................................................................................................

            var entityIsAnItem = iMetadataManager.EntityIsAnItem (entityName, isPickList);
            if (!entityIsAnItem) return matchingColumns;

            //JObject resource
            var resourceName = resource["ItemName"];
            if (resourceName == null || string.IsNullOrEmpty (resourceName.ToString ())) {
                resourceName = entityName;
            }
            var code = resource["Code"];
            if (code == null || string.IsNullOrEmpty (code.ToString ())) {
                code = string.Empty;
            }
            var itemTableWithValue = ItemHelper.GetItemSelectDetailsWithValue (tenantId, itemId, entityContext, subtype, resourceName.ToString (), true, Guid.Empty, code.ToString ());
            itemTableWithValue.AddRange (matchingColumns);

            var inverseList = entityColumns.Where (t => !string.IsNullOrEmpty (t.InverseTableName) && !string.IsNullOrEmpty (t.InverseColumnName)).ToList ();

            var test = new List<ColumnAndField>();
            foreach (var item in inverseList) {

                // foreach (var match in matchingColumns)
                // {
                //     var test111 = match;
                //     if(match.ColumnName.Equals(item.InverseColumnName)){
                //         var test1 = match;
                //         if(match.TableName.Equals(item.InverseTableName)){
                //             var test2 = match;
                //              if(match.ClientName.Equals(item.FieldName)){
                //                 var test3 = match;
                //             }
                //         }
                //     }
                // }

                // var newLogic = matchingColumns.FirstOrDefault (t => t.ColumnName.Equals (item.InverseColumnName) && t.TableName.Equals (item.InverseTableName) && t.ClientName.Equals(item.FieldName));
                // if(newLogic!=null){
                //      item.Value = newLogic.Value;
                //     itemTableWithValue.Add(item);
                // }
                var isRequriedToAdd = matchingColumns.FirstOrDefault (t => t.ColumnName.Equals (item.InverseColumnName) && t.TableName.Equals (item.InverseTableName) && t.EntityPrefix.Equals(item.InversePrefixName));
                if (isRequriedToAdd != null && isRequriedToAdd.Value!=null) {
                    item.Value = isRequriedToAdd.Value;
                    itemTableWithValue.Add(item);
                }
            }
            return itemTableWithValue;
        }

        string IQueryManager.BuildQuery (Guid tenantId, string entityName, QueryContext queryModel, bool isPickList) {
            return BuildSelectQuery (tenantId, entityName, queryModel, isPickList);
        }

        DataTable IQueryManager.GetResult (Guid tenantId, string entityName, QueryContext queryModel, bool IsPickList) {
            var query = BuildSelectQuery (tenantId, entityName, queryModel, IsPickList);
            IQueryReview review = new QueryReview ();
            return review.GetResult (tenantId, entityName, query);
        }

        DataTable IQueryManager.GetResultById (Guid tenantId, string entityName, dynamic id, QueryContext queryModel, bool isPickList) {
            var query = BuildSelectQuery (tenantId, entityName, queryModel, isPickList, false);
            IQueryReview review = new QueryReview ();
            return review.GetResult (tenantId, entityName, query);
        }

        /////// @todo remove id form column ...
        bool IQueryManager.UpdateResult (Guid tenantId, Guid resourceId, string resourceName, JObject resource, string subType, bool isPickList) {
            IMetadataManager iMetadataManager = new MetadataManager.Contracts.MetadataManager ();
            var entityTableName = iMetadataManager.GetTableNameByEntityname (resourceName);
            var entityTablePrimaryKey = iMetadataManager.GetPrimaryKeyByEntityname (resourceName);
            var columns = iMetadataManager.GetColumnNameByEntityName (resourceName, null);
            if (columns == null) throw new FieldAccessException ("Column not found.");
            var matchingColumns = GetMatchingColumnForUpdate (tenantId, resourceId, resourceName, entityTableName, entityTablePrimaryKey, resource, columns);
            if (!matchingColumns.Any ()) throw new FieldAccessException ("Column not matching.");
            var itemNameValue = resource["ItemName"];
            var name = string.Empty;
            var codeValue = resource["Code"];
            var code = string.Empty;
            if (itemNameValue != null && !string.IsNullOrEmpty (itemNameValue.ToString ())) {
                name = itemNameValue.ToString ();
            }

            if (codeValue != null && !string.IsNullOrEmpty (codeValue.ToString ())) {
                code = codeValue.ToString ();
            }
            var itemTableWithValue = ItemHelper.GetItemSelectDetailsWithValue (tenantId, resourceId, string.Empty, subType, name.ToString (), true, Guid.Empty, code);
            matchingColumns.AddRange (itemTableWithValue);

            var tables = matchingColumns.GroupBy (o => new { o.EntityPrefix }).Select (o => o.FirstOrDefault (
                x => x.ColumnName.Equals (x.PrimaryKey)
            )).ToList ();

            var queryStr = "";
            foreach (var item in tables) {
                if (item == null) continue;
                var result = matchingColumns.Where (
                    t => (t.TableName.Equals (item.TableName)) &&
                    (t.EntityPrefix.Equals (item.EntityPrefix)) &&
                    (!t.ColumnName.Equals (t.PrimaryKey))
                ).ToList ();
                if (result.Any ()) {
                    UpdateQueryBuilder query = new UpdateQueryBuilder ();
                    var columnWithValue = new Dictionary<string, string> ();
                    foreach (var col in result) {
                        if (col.Value != null && !string.IsNullOrEmpty (col.Value.ToString ())) {
                            columnWithValue.Add (col.ColumnName, col.Value.ToString ());
                        }
                    }
                    //  var columnWithValue = result.ToDictionary<ColumnAndField, string, string> (col => col.ColumnName, col => col.Value.ToString ());
                    query.AddTable (item.TableName, columnWithValue);
                    query.AddWhere (item.PrimaryKey, Comparison.Equals, item.Value.ToString (), 1, item.TableName);
                    var queryRes = query.BuildQuery ();
                    queryStr += queryRes;
                }
            }
            var updateQuery = tables.Count > 1 ? TransactionHelper.BuildQuery (queryStr) : queryStr;
            IQueryAdmin admin = new QueryAdmin ();
            return admin.SaveResult (tenantId, resourceName, updateQuery);
        }
        private string BuildMultipleTableDeleteQuery (Guid tenantId, string entityTableName, string entityPrimaryKey, string value, List<ColumnAndField> tables, List<ColumnAndField> foreignKeys) {
            var selectQueryBuilder = new SelectQueryBuilder ();
            selectQueryBuilder.SelectFromTable (entityTableName, "_us");
            selectQueryBuilder.AddWhere (entityPrimaryKey, Comparison.Equals, value, 1);
            var selectQuery = selectQueryBuilder.BuildQuery ();
            IQueryReview review = new QueryReview ();
            var targetResult = review.GetResult (tenantId, entityTableName, selectQuery);
            if (targetResult == null) return "";
            var queryStr = string.Empty;
            for (var i = tables.Count - 1; i >= 0; --i) {
                var targetTable = tables[i];
                var matchForeignKey = foreignKeys.FirstOrDefault (t => t.TableName.Equals (targetTable.TableName) && t.EntityPrefix.Equals (targetTable.EntityPrefix));
                var columnName = (matchForeignKey != null) ? matchForeignKey.ColumnName : targetTable.PrimaryKey;
                var targetValue = value;
                if (!string.IsNullOrEmpty (targetTable.ReferenceTableName) && !string.IsNullOrEmpty (targetTable.ReferenceColumnName)) {
                    targetValue = string.Empty;
                    var targetColumn = targetTable.ReferenceColumnName.TrimStart ('[').TrimEnd (']');
                    if (targetResult.Columns.Contains (targetColumn)) {
                        targetValue = targetResult.Rows[0][targetColumn].ToString ();
                    }
                }
                if (!string.IsNullOrEmpty (targetValue)) {
                    var queryBuilder = new DeleteQueryBuilder ();
                    queryBuilder.SelectFromTable (targetTable.TableName);
                    queryBuilder.AddWhere (columnName, Comparison.Equals, targetValue, 1);
                    var query = queryBuilder.BuildQuery ();
                    queryStr += query;
                }
            }
            return tables.Count > 1 ? TransactionHelper.BuildQuery (queryStr) : queryStr;
        }

        private string BuildSingleTableDeleteQuery (Guid tenantId, string entityTableName, string entityPrimaryKey, dynamic value) {
            var queryBuilder = new DeleteQueryBuilder ();
            queryBuilder.SelectFromTable (entityTableName);
            queryBuilder.AddWhere (entityPrimaryKey, Comparison.Equals, value, 1);
            return queryBuilder.BuildQuery ();
        }
        bool IQueryManager.DeleteResult (Guid tenantId, string resourceName, dynamic resourceId, bool isPickList) {
            IMetadataManager entityManager = new MetadataManager.Contracts.MetadataManager ();
            var tableName = entityManager.GetTableNameByEntityname (resourceName);
            var primaryKey = entityManager.GetPrimaryKeyByEntityname (resourceName);
            var context = entityManager.GetEntityContextByEntityName (resourceName, isPickList);
            if (string.IsNullOrEmpty (tableName)) throw new FieldAccessException ("Entity not found.");
            var entityColumns = entityManager.GetColumnNameByEntityName (resourceName, null);
            var columns = MapColumnsAsperFielsWithIndex (tenantId, resourceName, context, entityColumns, null, isPickList); // think is it not required
            //    var columns = GetColumnName (tenantId, resourceName, null, isPickList);
            var matchingColumns = columns;
            var tables = matchingColumns.Where (x => ((x.ColumnName.Equals (x.PrimaryKey) && x.EntityFullName == resourceName)) || (x.ColumnName.Equals (x.PrimaryKey) && x.AllowCaseCadingDelete && x.EntityFullName != resourceName)).ToList ().GroupBy (x => new { x.EntityPrefix }).Select (x => x.FirstOrDefault ()).ToList ();
            if (!tables.Any ()) throw new FieldAccessException ("Not allow to delete");
            string query;
            if (tables.Count > 1) {
                tables.OrderByDescending (x => string.IsNullOrEmpty (x.Linker));
                var foreignKeyLinker = columns.Where (t => !string.IsNullOrEmpty (t.ReferenceTableName)).ToList ();
                query = BuildMultipleTableDeleteQuery (tenantId, tableName, primaryKey, resourceId.ToString (), tables, foreignKeyLinker);
            } else {
                query = BuildSingleTableDeleteQuery (tenantId, tableName, primaryKey, resourceId.ToString ());
            }

            IQueryAdmin admin = new QueryAdmin ();
            var res = admin.DeleteResult (tenantId, resourceName, query);
            return res;

        }

        Guid IQueryManager.SaveResult (Guid tenantId, Guid userId, string entityName, JObject resource, string subtype, bool isPickList) {
            IMetadataManager iMetadataManager = new MetadataManager.Contracts.MetadataManager ();
            var subTypeCode = iMetadataManager.GetSubTypeId(entityName, subtype);
            return SaveResult (tenantId, userId, entityName, resource, subTypeCode, isPickList);
        }

        private Guid SaveResult (Guid tenantId, Guid userId, string entityName, JObject resource, string subtype, bool isPickList = false) {
            IMetadataManager iMetadataManager = new MetadataManager.Contracts.MetadataManager ();
            var entityColumns = iMetadataManager.GetColumnNameByEntityName (entityName, null);
            var tableName = iMetadataManager.GetTableNameByEntityname (entityName);
            if (entityColumns == null) throw new FieldAccessException ("Column not found.");
            var matchingColumns = GetMatchingColumnsForInsert (entityName, resource, entityColumns, tableName);
            var itemId = Guid.NewGuid ();
            var itemTableWithValue = AddDefaultValueForInsert (tenantId, userId, entityName, subtype, itemId, entityColumns, matchingColumns, isPickList, resource);
            var validateMessage = CheckValidationRule (entityColumns, itemTableWithValue);
            if (validateMessage != string.Empty) throw new FieldAccessException (validateMessage);
            var insertQuery = BuildInsertQuery (itemTableWithValue);
            IQueryAdmin admin = new QueryAdmin ();
            admin.SaveResult (tenantId, entityName, insertQuery);
            return itemId;
        }
        private static List<ColumnAndField> GetMatchingColumnsForInsert (string entityName, JObject resource, List<ColumnAndField> entityColumns,
            string tableName) {
            var matchingColumns = new List<ColumnAndField> ();
            foreach (var col in entityColumns) {
                //var colname = Char.ToLowerInvariant (col.FieldName[0]) + col.FieldName.Substring (1);

                //var matching = resource[col.FieldName];
                // if (matching == null) continue;
                // col.Value = matching.ToObject<dynamic> ();
                // matchingColumns.Add (col);
                var columnName = (!string.IsNullOrEmpty (col.ClientName)) ? col.ClientName + "." + col.FieldName : col.FieldName;
                var matching = resource[columnName];
                // var matching = resource[col.FieldName];
                if (matching != null) {
                    col.Value = matching.ToObject<dynamic> ();
                    matchingColumns.Add (col);
                } else if (col.Value != null && col.TableName.Equals (tableName) &&
                    col.EntityFullName.ToLower ().Equals (entityName.ToLower ())) {
                    matchingColumns.Add (col);
                }
            }
            if (!matchingColumns.Any ()) throw new FieldAccessException ("Column not matching.");
            return matchingColumns;
        }

        private List<ColumnAndField> MapColumnsAsperFielsWithIndex (Guid tenantId, string entityName, string entityContext, List<ColumnAndField> decoratedColumns, string[] fields, bool isPickList = false) {
            var highestValue = decoratedColumns.Count ();
            var itemTableColumns = (isPickList) ?
                new List<ColumnAndField> () :
                GetDataFromItem (tenantId, entityName, entityContext, isPickList, highestValue);

            if (isPickList || fields == null) return decoratedColumns;

            foreach (var item in decoratedColumns) {
                var colname = !string.IsNullOrEmpty (item.ClientName) ? item.ClientName + "." + item.FieldName : item.FieldName;

                var userFieldsMatching = fields.FirstOrDefault (x =>
                    x.Equals (colname)

                );
                var queryIndex = 0;
                if (userFieldsMatching != null) {
                    var keyIndex = Array.FindIndex (fields, w => w.Equals (colname));
                    queryIndex = keyIndex + 1;
                } else {
                    if (item.ColumnName != item.PrimaryKey) continue;
                    queryIndex = highestValue;
                    highestValue++;
                }
                item.QueryIndex = queryIndex;
                itemTableColumns.Add (item);
            }

            var inverseKey = decoratedColumns.Where (t => !string.IsNullOrEmpty (t.InverseColumnName) && !string.IsNullOrEmpty (t.InverseTableName)).ToList ();
            foreach (var key in inverseKey) {
                var isRequired = itemTableColumns.FirstOrDefault (t => t.TableName.Equals (key.InverseTableName));
                if (isRequired != null) {
                    itemTableColumns.Add (key);
                }
            }
            // return itemTableColumns.OrderBy(p=>p.QueryIndex).ToList();
            return itemTableColumns;
        }

        private List<ColumnAndField> GetDataFromItem (Guid tenantId, string entityName, string entityContext, bool isPicklist, int highestValue) {
            IMetadataManager entityManager = new MetadataManager.Contracts.MetadataManager ();
            var isItem = entityManager.EntityIsAnItem (entityName, isPicklist);

            return (isItem) ?
                ItemHelper.GetItemSelectDetails (tenantId, entityContext, highestValue) : new List<ColumnAndField> ();
        }
        private List<ColumnAndField> GetMatchingColumnForUpdate (Guid tenantId, Guid resourceId, string resourceName, string entityTableName, string entityTablePrimaryKey, JObject resource, List<ColumnAndField> columns)
        {
            var matchingColumns = new List<ColumnAndField>();
            foreach (var col in columns)
            {
                var colname = !string.IsNullOrEmpty(col.ClientName) ? col.ClientName + "." + col.FieldName : col.FieldName;
                var matching = resource[colname];
                if (matching == null) continue;
                col.Value = matching.ToObject<dynamic>();
                matchingColumns.Add(col);
            }

            var entityPrimaryKeyColumns = columns.FirstOrDefault(t => t.ColumnName.Equals(entityTablePrimaryKey) && t.TableName.Equals(entityTableName));
            if (entityPrimaryKeyColumns != null)
            {
                entityPrimaryKeyColumns.Value = resourceId;
                matchingColumns.Add(entityPrimaryKeyColumns);
            }

            //-------------- need to check this prop..
            var foreignKeyLinker = columns.Where(t => !string.IsNullOrEmpty(t.ReferenceTableName)).ToList();
            if (foreignKeyLinker.Any())
            {
                AddForeignKeyMatchingWithValue(tenantId, resourceId, entityTableName, entityTablePrimaryKey, matchingColumns, foreignKeyLinker);
            }
            

            //----------------------------
            var inverseKey = columns.Where(t => !string.IsNullOrEmpty(t.InverseTableName) && !string.IsNullOrEmpty(t.InverseColumnName)).ToList();
            if (inverseKey != null)
            {
                AddInverseKeyMatchingWithValue(tenantId, resourceId, entityTablePrimaryKey, columns, matchingColumns, inverseKey);

            }

            return matchingColumns;

        }

        private void AddInverseKeyMatchingWithValue(Guid tenantId, Guid resourceId, string entityTablePrimaryKey, List<ColumnAndField> columns, List<ColumnAndField> matchingColumns, List<ColumnAndField> inverseKey)
        {
            foreach (var item in inverseKey)
            {
                var isAddedInMatching = matchingColumns.FirstOrDefault(t => t.TableName.Equals(item.InverseTableName));
                if (isAddedInMatching != null)
                {

                    var selectQueryBuilder = new SelectQueryBuilder();
                    selectQueryBuilder.SelectFromTable(item.TableName, item.EntityPrefix);
                    selectQueryBuilder.AddWhere(entityTablePrimaryKey, Comparison.Equals, resourceId.ToString(), 1);
                    var selectQuery = selectQueryBuilder.BuildQuery();
                    IQueryReview review = new QueryReview();
                    var targetResult = review.GetResult(tenantId, item.TableName, selectQuery);
                    if (targetResult == null) continue;

                    var targetColumn = item.ColumnName.TrimStart('[').TrimEnd(']');
                    if (!targetResult.Columns.Contains(targetColumn)) continue;
                    var targetValue = targetResult.Rows[0][targetColumn].ToString();

                    if (string.IsNullOrEmpty(targetValue))
                    {
                        //need to check......

                        var queryBuilder = new InsertQueryBuilder();
                        var insertColumns = new Dictionary<string, string>();
                        foreach (var match in matchingColumns)
                        {
                            if (match.TableName.Equals(item.InverseTableName))
                            {
                                var isAdded = insertColumns.Where(t=>t.Key.Equals(match.ColumnName)).ToList();
                                if(isAdded.Any()) continue;
                                insertColumns.Add(match.ColumnName, match.Value.ToString());
                            }
                        }

                        var inverseId = Guid.NewGuid();
                        var inversePrimaryKeyColumns = columns.FirstOrDefault(t => t.ColumnName.Equals(t.PrimaryKey) && t.TableName.Equals(item.InverseTableName) && t.EntityPrefix.Equals(item.InversePrefixName));
                        if (inversePrimaryKeyColumns == null) throw new FieldAccessException("Primary key not found.");
                        var inverseAddedColumns = insertColumns.Where(t=>t.Key.Equals(inversePrimaryKeyColumns.ColumnName)).ToList();
                        if(!inverseAddedColumns.Any()){
                            insertColumns.Add(inversePrimaryKeyColumns.ColumnName, inverseId.ToString());
                        }
                        //tenant id.
                        var primaryTenantId = columns.FirstOrDefault(x => x.ColumnName.Equals(_tenantId) && x.TableName.Equals(item.InverseTableName));
                        if (primaryTenantId == null) throw new FieldAccessException("Tenant id not found.");
                        insertColumns.Add(primaryTenantId.ColumnName, tenantId.ToString());

                        // var userId = Guid.NewGuid();
                        // var updatedBy = columns.FirstOrDefault (x => x.ColumnName.Equals (_updatedBy) && x.TableName.Equals (item.InverseTableName));
                        // insertColumns.Add (updatedBy.ColumnName, userId.ToString());

                        // var updatedDate = columns.FirstOrDefault (x => x.ColumnName.Equals (_updatedDate) && x.TableName.Equals (item.InverseTableName));
                        // updatedDate.Value = DateTime.UtcNow.ToString ("MM/dd/yyyy HH:mm:ss");

                        queryBuilder.InsertIntoTable(item.InverseTableName, insertColumns, false);
                        var insertQuery = queryBuilder.BuildQuery();

                        IQueryAdmin admin = new QueryAdmin();
                        admin.SaveResult(tenantId, "inverse", insertQuery);
                        targetValue = inverseId.ToString();
                    }
                    item.Value = targetValue.ToString();
                    matchingColumns.Add(item);
                    //add inverse table primary key.
                    var inverserPrimaryTable = columns.FirstOrDefault(t => t.TableName.Equals(item.InverseTableName) && t.ColumnName.Equals(item.PrimaryKey));
                    if (inverserPrimaryTable != null)
                    {
                        var isAdded = matchingColumns.Where(t => t.ColumnName.Equals(inverserPrimaryTable.ColumnName) && t.TableName.Equals(inverserPrimaryTable.TableName)).ToList();
                        if (!isAdded.Any())
                        {
                            inverserPrimaryTable.Value = targetValue.ToString();
                            matchingColumns.Add(inverserPrimaryTable);
                        }
                    }
                }
            }
        }

        private static void AddForeignKeyMatchingWithValue(Guid tenantId, Guid resourceId, string entityTableName, string entityTablePrimaryKey, List<ColumnAndField> matchingColumns, List<ColumnAndField> foreignKeyLinker)
        {
            foreach (var item in foreignKeyLinker)
            {
                if (!item.ReferenceTableName.Equals(entityTableName) ||
                    !item.ReferenceColumnName.Equals(entityTablePrimaryKey)) continue;
                item.Value = resourceId;
                var selectQueryBuilder = new SelectQueryBuilder();
                selectQueryBuilder.SelectFromTable(item.TableName, item.EntityPrefix);
                selectQueryBuilder.AddWhere(item.ColumnName, Comparison.Equals, item.Value.ToString(), 1);
                var selectQuery = selectQueryBuilder.BuildQuery();
                IQueryReview review = new QueryReview();
                var targetResult = review.GetResult(tenantId, item.TableName, selectQuery);
                if (targetResult == null) continue;
                var targetColumn = item.PrimaryKey.TrimStart('[').TrimEnd(']');
                if (!targetResult.Columns.Contains(targetColumn)) continue;
                var targetValue = targetResult.Rows[0][targetColumn].ToString();
                if (string.IsNullOrEmpty(targetValue)) continue;
                var columnAndField = new ColumnAndField();
                columnAndField.TableName = item.TableName;
                columnAndField.EntityFullName = item.EntityFullName;
                columnAndField.EntityPrefix = item.EntityPrefix;
                columnAndField.FieldName = "InternalId"; //need to check this portion...
                columnAndField.ColumnName = item.PrimaryKey;
                columnAndField.TableName = item.TableName;
                columnAndField.PrimaryKey = item.PrimaryKey;
                columnAndField.Value = targetValue;
                matchingColumns.Add(columnAndField);
            }
        }

        private static List<ColumnAndField> GetMatchingColumnsWithSequenceForSelectQuery (string[] result, List<ColumnAndField> columns, string tableName, string entityTablePrimaryKey, bool isPickList) {
          
            if (result == null || !result.Any ()) {return columns;}

            
            var maxCount = columns.Count ();
            var queryMatchingColumns = GetKeyMatchingOnly (result, columns);
            var necessaryColumns = isPickList? GetNecessaryColumnsForPickList (columns, tableName, entityTablePrimaryKey, queryMatchingColumns) : GetNecessaryColumnsForEntity (columns, tableName, entityTablePrimaryKey, queryMatchingColumns);
            var matchingColumns = queryMatchingColumns.Concat (necessaryColumns).ToList ();

            //new display logic as same as sales force..
            var pickListOrLookup = matchingColumns.Where (
                t => t.DataType.Equals (VPC.Metadata.Business.DataAnnotations.DataType.PickList) || t.DataType.Equals (VPC.Metadata.Business.DataAnnotations.DataType.Lookup)
            ).ToList ();
            if (pickListOrLookup.Any ()) {
                AddTextColumn (matchingColumns, pickListOrLookup);
            }
            return matchingColumns;
        }

        private static bool IsAlreadyAdded (ColumnAndField primaryKeyObj, List<ColumnAndField> whereList1, List<ColumnAndField> whereList2) {
            var isNotAddedInPresentList = whereList1.Where (t => t.EntityPrefix.Equals (primaryKeyObj.EntityPrefix) && t.ColumnName.Equals (primaryKeyObj.ColumnName)).ToList ();
            var isNotAddedInPreviousList = whereList2.Where (t => t.EntityPrefix.Equals (primaryKeyObj.EntityPrefix) && t.ColumnName.Equals (primaryKeyObj.ColumnName)).ToList ();
            return (isNotAddedInPresentList.Any () || isNotAddedInPreviousList.Any ());
        }

        private static List<ColumnAndField> GetNecessaryColumnsForPickList (List<ColumnAndField> columns, string tableName, string entityTablePrimaryKey, List<ColumnAndField> queryMatchingColumns) {
            var matchingTables = queryMatchingColumns.GroupBy (o => new { o.EntityPrefix }).Select (o => o.FirstOrDefault ()).Select (t => t.TableName).ToArray ();
            var matchingColumns = new List<ColumnAndField> ();
            var maxCount = columns.Count ();
            foreach (var column in columns) {
                var isItemTable = (column.TableName.Equals (ItemHelper.ItemTableName) && column.ColumnName == ItemHelper.ItemTablePrimarykey);
                var primaryKey = (column.TableName.Equals (tableName) && column.ColumnName == entityTablePrimaryKey);
                var primaryLinker = false;
                if (!string.IsNullOrEmpty (matchingTables.FirstOrDefault (t => t.Equals (column.TableName)))) {
                    var isInverseProperty = ((!string.IsNullOrEmpty (column.InverseColumnName)) && (!string.IsNullOrEmpty (column.InverseTableName)));
                    var isReferenceProperty = ((!string.IsNullOrEmpty (column.ReferenceColumnName)) && (!string.IsNullOrEmpty (column.ReferenceTableName)));
                    primaryLinker = (isInverseProperty || isReferenceProperty);
                }

                if (isItemTable || primaryKey || primaryLinker) {
                    var isNotAddedInPresentList = matchingColumns.Where (t => t.EntityPrefix.Equals (column.EntityPrefix) && t.ColumnName.Equals (column.ColumnName)).ToList ();
                    var isNotAddedInPreviousList = queryMatchingColumns.Where (t => t.EntityPrefix.Equals (column.EntityPrefix) && t.ColumnName.Equals (column.ColumnName)).ToList ();
                    if (!isNotAddedInPresentList.Any () && !isNotAddedInPreviousList.Any ()) {
                        column.QueryIndex = maxCount; //not requried to update value..
                        matchingColumns.Add (column);
                    }
                }
            }
            return matchingColumns;
        }
        private static List<ColumnAndField> GetNecessaryColumnsForEntity (List<ColumnAndField> columns, string tableName, string entityTablePrimaryKey, List<ColumnAndField> queryMatchingColumns) {

           
            
            var matchingTables = queryMatchingColumns.GroupBy (o => new { o.EntityPrefix }).Select (o => o.FirstOrDefault ()).Select (t => t.TableName).ToArray ();
            var matchingColumns = new List<ColumnAndField> ();
            var maxCount = columns.Count ();

            //item
            var itemObj = columns.FirstOrDefault (t => t.TableName.Equals (ItemHelper.ItemTableName) && t.ColumnName == ItemHelper.ItemTablePrimarykey);
            if (itemObj != null) {
                var isAdded = IsAlreadyAdded (itemObj, matchingColumns, queryMatchingColumns);
                if (!isAdded) {
                    itemObj.QueryIndex = maxCount; //not requried to update value..
                    matchingColumns.Add (itemObj);
                }
            }
            var tables = queryMatchingColumns.GroupBy (o => new { o.EntityPrefix });
            foreach (var item in matchingTables) {
                //----------
                //primarykey...
                var primaryKeyObj = columns.FirstOrDefault (t => t.PrimaryKey.Equals (t.ColumnName) && t.TableName.Equals (item));
                if (primaryKeyObj != null) {
                    var isAdded = IsAlreadyAdded (primaryKeyObj, matchingColumns, queryMatchingColumns);
                    if (!isAdded) {
                        primaryKeyObj.QueryIndex = maxCount; //not requried to update value..
                        matchingColumns.Add (primaryKeyObj);
                    }
                }
                //-----
                //inverse

                var inverseObj = columns.FirstOrDefault (t =>
                    !string.IsNullOrEmpty (t.InverseColumnName) &&
                    !string.IsNullOrEmpty (t.InverseTableName) &&
                    t.InverseTableName.Equals (item)
                );
                if (inverseObj != null) {
                    var isAdded = IsAlreadyAdded (inverseObj, matchingColumns, queryMatchingColumns);
                    if (!isAdded) {
                        //--------------- newly added logic for picklist..... \\\\\\\\\\\\\\\\\\\\\

                        inverseObj.QueryIndex = maxCount; //not requried to update value..
                        matchingColumns.Add (inverseObj);

                        // var prefixIsAdded = queryMatchingColumns.FirstOrDefault(t=>t.EntityPrefix.Equals(inverseObj.EntityPrefix));
                        // if(prefixIsAdded!=null){
                        //     inverseObj.QueryIndex = maxCount; //not requried to update value..
                        //     matchingColumns.Add (inverseObj);
                        // }
                    }
                }

                //foreignKey
                var foreignKeyObj = columns.FirstOrDefault (t =>
                    !string.IsNullOrEmpty (t.ReferenceColumnName) &&
                    !string.IsNullOrEmpty (t.ReferenceTableName) &&
                    t.InverseTableName.Equals (item)
                );
                if (foreignKeyObj != null) {
                    var isAdded = IsAlreadyAdded (foreignKeyObj, matchingColumns, queryMatchingColumns);
                    if (!isAdded) {
                        foreignKeyObj.QueryIndex = maxCount; //not requried to update value..
                        matchingColumns.Add (foreignKeyObj);
                    }
                }

                //         var isReferenceProperty = ((!string.IsNullOrEmpty (column.ReferenceColumnName)) && (!string.IsNullOrEmpty (column.ReferenceTableName)));
            }

            // foreach (var column in columns) {
            //     var isItemTable = (column.TableName.Equals (ItemHelper.ItemTableName) && column.ColumnName == ItemHelper.ItemTablePrimarykey);
            //     var primaryKey = (column.TableName.Equals (tableName) && column.ColumnName == entityTablePrimaryKey);
            //     var primaryLinker = false;
            //     if (!string.IsNullOrEmpty (matchingTables.FirstOrDefault (t => t.Equals (column.TableName)))) {
            //         var isInverseProperty = ((!string.IsNullOrEmpty (column.InverseColumnName)) && (!string.IsNullOrEmpty (column.InverseTableName)));
            //         var isReferenceProperty = ((!string.IsNullOrEmpty (column.ReferenceColumnName)) && (!string.IsNullOrEmpty (column.ReferenceTableName)));
            //         primaryLinker = (isInverseProperty || isReferenceProperty);
            //     }

            //     if (isItemTable || primaryKey || primaryLinker) {
            //         var isNotAddedInPresentList = matchingColumns.Where (t => t.EntityPrefix.Equals (column.EntityPrefix) && t.ColumnName.Equals (column.ColumnName)).ToList ();
            //         var isNotAddedInPreviousList = queryMatchingColumns.Where (t => t.EntityPrefix.Equals (column.EntityPrefix) && t.ColumnName.Equals (column.ColumnName)).ToList ();
            //         if (!isNotAddedInPresentList.Any () && !isNotAddedInPreviousList.Any ()) {
            //             column.QueryIndex = maxCount; //not requried to update value..
            //             matchingColumns.Add (column);
            //         }
            //     }
            // }
            return matchingColumns;
        }
        private static List<ColumnAndField> GetNecessaryColumns_old_isWorkingForPicklist (List<ColumnAndField> columns, string tableName, string entityTablePrimaryKey, List<ColumnAndField> queryMatchingColumns) {
            var matchingTables = queryMatchingColumns.GroupBy (o => new { o.EntityPrefix }).Select (o => o.FirstOrDefault ()).Select (t => t.TableName).ToArray ();
            var matchingColumns = new List<ColumnAndField> ();
            var maxCount = columns.Count ();
            foreach (var column in columns) {
                var isItemTable = (column.TableName.Equals (ItemHelper.ItemTableName) && column.ColumnName == ItemHelper.ItemTablePrimarykey);
                var primaryKey = (column.TableName.Equals (tableName) && column.ColumnName == entityTablePrimaryKey);
                var primaryLinker = false;
                if (!string.IsNullOrEmpty (matchingTables.FirstOrDefault (t => t.Equals (column.TableName)))) {
                    var isInverseProperty = ((!string.IsNullOrEmpty (column.InverseColumnName)) && (!string.IsNullOrEmpty (column.InverseTableName)));
                    var isReferenceProperty = ((!string.IsNullOrEmpty (column.ReferenceColumnName)) && (!string.IsNullOrEmpty (column.ReferenceTableName)));
                    primaryLinker = (isInverseProperty || isReferenceProperty);
                }

                if (isItemTable || primaryKey || primaryLinker) {
                    var isNotAddedInPresentList = matchingColumns.Where (t => t.EntityPrefix.Equals (column.EntityPrefix) && t.ColumnName.Equals (column.ColumnName)).ToList ();
                    var isNotAddedInPreviousList = queryMatchingColumns.Where (t => t.EntityPrefix.Equals (column.EntityPrefix) && t.ColumnName.Equals (column.ColumnName)).ToList ();
                    if (!isNotAddedInPresentList.Any () && !isNotAddedInPreviousList.Any ()) {
                        column.QueryIndex = maxCount; //not requried to update value..
                        matchingColumns.Add (column);
                    }
                }
            }
            return matchingColumns;
        }

        private static List<ColumnAndField> GetKeyMatchingOnly (string[] result, List<ColumnAndField> columns) {
            if (result == null || !result.Any ()) return columns;
            var matchingColumns = new List<ColumnAndField> ();
            var index = 0;
            foreach (var item in result) {
                var matchColumn = columns.FirstOrDefault (
                    t =>
                    (
                        (!string.IsNullOrEmpty (t.ClientName)) &&
                        (t.ClientName + "." + t.FieldName).ToLower ().Equals (item.ToLower ())
                    ) ||
                    t.FieldName.ToLower ().Equals (item.ToLower ())
                );

                if (matchColumn != null) {
                    matchingColumns.Add (matchColumn);
                    matchColumn.QueryIndex = index;
                    index++;
                    continue;
                }
                // // last .matching logic
                // var splitQuery = item.Split ('.');
                // if (splitQuery.Any () && splitQuery.Count () > 1) {
                //     // var targetName = "";
                //     // for (var i = splitQuery.Length - 2; i < splitQuery.Length; i++) {
                //     //     targetName += splitQuery[i] + ".";
                //     // }
                //     // targetName = targetName.Substring (0, targetName.Length - 1);
                //     // if (targetName.Equals (column.FieldName)) {
                //     //     fieldMatching = true;
                //     //     break;
                //     // }
                // }
                // index++;
            }
            return matchingColumns;
        }

        private static void AddTextColumn (List<ColumnAndField> matchingColumns, List<ColumnAndField> pickListOrLookup) {
            IMetadataManager entityManager = new MetadataManager.Contracts.MetadataManager ();
            foreach (var item in pickListOrLookup) {
                var splitName = item.FieldName.Split ('.');
                var targetClassName = splitName[splitName.Length - 1];
                var basicColumn = entityManager.GetBasicColumnNameByEntityName (targetClassName, null);
                if (basicColumn.Any ()) {
                    basicColumn[0].FieldName = item.FieldName + "." + basicColumn[0].FieldName;
                    matchingColumns.Add (basicColumn[0]);
                }
            }
        }
    }
}