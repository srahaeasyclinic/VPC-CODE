using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json.Linq;
using VPC.Entities.EntityCore.Metadata.Runtime;
using VPC.Entities.EntityCore.Model.Query;
using VPC.Entities.EntityCore.Model.Storage;
using VPC.Entities.SampleEntity;
using VPC.Framework.Business.Data;
using VPC.Framework.Business.Data.SqlClient;
using VPC.Framework.Business.DynamicQueryManager.APIs;
using VPC.Framework.Business.DynamicQueryManager.Core;
using VPC.Framework.Business.DynamicQueryManager.Core.Enums;
using VPC.Framework.Business.MetadataManager.Contracts;

namespace VPC.Framework.Business.EntityResourceManager.Contracts {
    internal class MatchingHelper {
        private readonly string _tenantId = "[TenantId]";

        public MatchingHelper () {

        }
        private List<ColumnAndField> GetMatchingColumnsWithSequenceForSelectQuery (string[] result, List<ColumnAndField> columns, string tableName, string entityTablePrimaryKey) {
            var maxCount = columns.Count ();
            if (result == null || !result.Any ()) return columns;
            var queryMatchingColumns = GetColumnsByUserQuery (result, columns);
            var primaryColumns = GetPrimaryIdsColumnsByUserQuery (columns, tableName, entityTablePrimaryKey, queryMatchingColumns);
            var necessaryInverseColumns = GetInverseColumnsByUserQuery (columns, tableName, entityTablePrimaryKey, queryMatchingColumns);
            var matchingColumns = queryMatchingColumns.Concat (primaryColumns).Concat (necessaryInverseColumns).ToList ();
            // var pickListOrLookup = matchingColumns.Where (
            //     t => t.DataType.Equals (VPC.Metadata.Business.DataAnnotations.DataType.PickList) || t.DataType.Equals (VPC.Metadata.Business.DataAnnotations.DataType.Lookup)
            // ).ToList ();
            // if (pickListOrLookup.Any ()) {
            //     AddTextColumn (matchingColumns, pickListOrLookup);
            // }
            return matchingColumns;
        }
        public List<ColumnAndField> GetColumnsByUserQuery (string[] result, List<ColumnAndField> columns) {
            if (result == null || !result.Any ()) return columns;
            var matchingColumns = new List<ColumnAndField> ();
            var index = 0;
            foreach (var item in result) {

                // var matchColumn = columns.FirstOrDefault(
                //     t =>
                //     (
                //         (!string.IsNullOrEmpty(t.ClientName)) &&
                //         (t.ClientName + "." + t.FieldName).ToLower().Equals(item.ToLower())
                //     ) ||
                //     t.FieldName.ToLower().Equals(item.ToLower())
                // );
                ColumnAndField matchColumn = null;
                foreach (var col in columns) {
                    if (col.ColumnName.Equals ("[***]")) continue;
                    if (!string.IsNullOrEmpty (col.ClientName)) {
                        if ((col.ClientName + "." + col.FieldName).ToLower ().Equals (item.ToLower ())) {
                            matchColumn = col;
                            break;
                        }
                    } else {
                        if (col.FieldName.ToLower ().Equals (item.ToLower ())) {
                            matchColumn = col;
                            break;
                        }
                    }
                }
                if (matchColumn != null) {
                    matchingColumns.Add (matchColumn);
                    matchColumn.QueryIndex = index;
                    index++;
                    continue;
                }
            }
            return matchingColumns;
        }

        public List<ColumnAndField> GetPrimaryIdsColumnsByUserQuery (List<ColumnAndField> columns, string tableName, string entityTablePrimaryKey, List<ColumnAndField> queryMatchingColumns) {
            var primaryColumns = new List<ColumnAndField> ();
            var allPrimaryColumns = columns.Where (t => t.ColumnName.Equals (t.PrimaryKey)).ToList ();
            if (allPrimaryColumns.Any ()) {
                foreach (var item in allPrimaryColumns) {
                    var isPrefixMatch = queryMatchingColumns.FirstOrDefault (t => t.EntityPrefix.Equals (item.EntityPrefix));
                    if (isPrefixMatch != null) {
                        primaryColumns.Add (item);
                    }
                }
            }
            //  primaryColumns = allPrimaryColumns;
            var itemObj = columns.FirstOrDefault (t => t.TableName.Equals (ItemHelper.ItemTableName) && t.ColumnName == ItemHelper.ItemTablePrimarykey);
            if (itemObj != null) {
                var isAdded = IsAlreadyAdded (itemObj, queryMatchingColumns, primaryColumns);
                if (!isAdded) {
                    primaryColumns.Add (itemObj);
                }
            }
            return primaryColumns;
        }

        private static bool IsAlreadyAdded (ColumnAndField primaryKeyObj, List<ColumnAndField> whereList1, List<ColumnAndField> whereList2) {
            var isNotAddedInPresentList = whereList1.Where (t => t.EntityPrefix.Equals (primaryKeyObj.EntityPrefix) && t.ColumnName.Equals (primaryKeyObj.ColumnName)).ToList ();
            var isNotAddedInPreviousList = whereList2.Where (t => t.EntityPrefix.Equals (primaryKeyObj.EntityPrefix) && t.ColumnName.Equals (primaryKeyObj.ColumnName)).ToList ();
            return (isNotAddedInPresentList.Any () || isNotAddedInPreviousList.Any ());
        }

        public List<ColumnAndField> GetBasicColumnOfPickListOrLookup (Guid tenantId, List<ColumnAndField> pickListOrLookupWhichFoundInQuery) {
            List<ColumnAndField> matchingColumns = new List<ColumnAndField> ();
            IMetadataManager entityManager = new MetadataManager.Contracts.MetadataManager ();
            foreach (var item in pickListOrLookupWhichFoundInQuery) {
                var virtualName = !string.IsNullOrEmpty (item.ClientName) ? item.ClientName + "." + item.FieldName : item.FieldName;
                if (item.DataType.Equals (VPC.Metadata.Business.DataAnnotations.DataType.Lookup)) {

                    //newly added condition need to fix this portion 
                    //reason product :: product version workflow..

                    if(
                        !string.IsNullOrEmpty(item.ReferenceColumnName)
                    ) continue;

                    var isItem = entityManager.EntityIsAnItem (item.TypeOf, false);
                    if (isItem) {
                        var context = entityManager.GetEntityContextByEntityName (item.TypeOf, false);
                        var itemTableColumns = ItemHelper.GetItemSelectDetails (tenantId, context, 0);
                        List<ColumnAndField> basicColumn = new List<ColumnAndField> ();
                        var itemId = itemTableColumns.FirstOrDefault (t => t.ColumnName.Equals (ItemHelper.ItemTablePrimarykey));
                        var itemText = itemTableColumns.FirstOrDefault (t => t.ColumnName.Equals (ItemHelper.ItemTableItemNameField));
                        basicColumn.Add (itemId);
                        basicColumn.Add (itemText);
                        MapBasicFields (matchingColumns, entityManager, item, virtualName, basicColumn);
                    }
                } else {
                    var basicColumn = entityManager.GetBasicColumnNameByEntityName (item.TypeOf, null);
                    if (basicColumn.Any ()) {
                        MapBasicFields (matchingColumns, entityManager, item, virtualName, basicColumn);
                    }
                }
            }
            return matchingColumns;
        }

        private static void MapBasicFields (List<ColumnAndField> matchingColumns, IMetadataManager entityManager, ColumnAndField item, string VirtualName, List<ColumnAndField> basicColumn) {
            var primaryId = entityManager.GetPrimaryKeyByEntityname (item.TypeOf);
            var tableName = basicColumn[0].TableName;
            var primaryIdPrefix = string.Empty;
            foreach (var bs in basicColumn) {
                bs.ClientName = item.FieldName + "_" + bs.EntityFullName;
                var targetPrefix = item.EntityPrefix + "_" + item.FieldName + "_" + item.TypeOf;
                bs.EntityPrefix = targetPrefix;
                if (bs.ColumnName.ToLower ().Equals (primaryId.ToLower ())) {
                    primaryIdPrefix = targetPrefix;
                }
                bs.VirtualField = true;
                bs.VirtualName = VirtualName;
                matchingColumns.Add (bs);
            }
            item.InversePrefixName = primaryIdPrefix;
            item.InverseColumnName = primaryId;
            item.InverseTableName = tableName;
        }

        internal List<ColumnAndField> AddDataToColumns (Guid tenantId, Guid resourceId, string entityName, string tableName, string primaryKey, Dictionary<string, string> payload, List<ColumnAndField> necessaryColumns) {
            IMetadataManager entityManager = new MetadataManager.Contracts.MetadataManager ();
            //added primary key....
            var matchingColumns = new List<ColumnAndField> ();
            var entityPrimaryKeyColumns = necessaryColumns.FirstOrDefault (t => t.ColumnName.Equals (primaryKey) && t.TableName.Equals (tableName));
            if (entityPrimaryKeyColumns != null) {
                entityPrimaryKeyColumns.Value = resourceId;
                matchingColumns.Add (entityPrimaryKeyColumns);
            }

            //added query matching..
            foreach (var col in necessaryColumns) {
                var colname = !string.IsNullOrEmpty (col.ClientName) ? col.ClientName + "." + col.FieldName : col.FieldName;
                var matching = payload.FirstOrDefault (t => t.Key.ToLower ().Equals (colname.ToLower ()));
                if (matching.Key == null) continue;

                if (col.Value == "#ENTCAST") //this is for context to get Entitycontext Id by defaultvalueattributes
                {
                    col.Value = entityManager.GetEntityContextByEntityName (matching.Value);
                } else {
                    col.Value = matching.Value;
                }

                matchingColumns.Add (col);
            }

            //inverser key matching....
            IMetadataManager iMetadataManager = new MetadataManager.Contracts.MetadataManager ();
            var columns = iMetadataManager.GetColumnNameByEntityName (entityName, null);
            var inverseKey = columns.Where (t => !string.IsNullOrEmpty (t.InverseTableName) && !string.IsNullOrEmpty (t.InverseColumnName)).ToList ();
            if (inverseKey != null) {
                foreach (var item in inverseKey) {
                    var isAddedInMatching = matchingColumns.FirstOrDefault (t => t.TableName.Equals (item.InverseTableName) && t.EntityPrefix.Equals (item.InversePrefixName));
                    if (isAddedInMatching != null && !string.IsNullOrEmpty (isAddedInMatching.Value)) {
                        var targetColumn = item.ColumnName.TrimStart ('[').TrimEnd (']');
                        var resultValue = GetValueFromInverseColumns (tenantId, item.TableName, item.EntityPrefix, primaryKey, targetColumn, resourceId.ToString ());

                        if (!string.IsNullOrEmpty (resultValue)) {
                            item.Value = resultValue.ToString ();
                            var updateResult = UpdateValueInInverseTable (tenantId, item.InverseTableName, item.InversePrefixName, item.PrimaryKey, necessaryColumns, resultValue.ToString ());
                        } else {
                            var addedResult = AddValueFromInverseColumns (tenantId, item.InverseTableName, item.InversePrefixName, item.PrimaryKey, necessaryColumns, columns);
                            item.Value = addedResult.ToString ();
                        }
                        matchingColumns.Add (item);
                    }
                }
            }
            List<Entity> intersects = null;
            var entity = iMetadataManager.GetEntitityByName (entityName);
            if (entity != null && entity.DetailEntities != null && entity.DetailEntities.Any ()) {
                intersects = entity.DetailEntities.Where (t => t.Type.ToLower ().Equals ("intersectentity")).ToList ();
                if (intersects != null && intersects.Any ()) {
                    foreach (var item in intersects) {
                        var match = payload.FirstOrDefault (t => t.Key.ToLower ().Equals (item.Name.ToLower ()));
                        if (match.Key != null) {
                            var value = match.Value;
                            if (!string.IsNullOrEmpty (value)) {
                                break;
                            }
                        }
                    }
                    // if (isRequiredToAddIntersect) {
                    //     // var intersectColumns = InsertIntersectEntity (tenantId, userId, entityName, resource, entityColumns, itemId);
                    //     // var intersectQuery = BuildInsertQuery (intersectColumns);
                    //     // admin.SaveResult (tenantId, intersectColumns[0].TableName, intersectQuery);
                    // }
                }
            }

            var forenkeysInColumn = columns.Where (t => !string.IsNullOrEmpty (t.ReferenceTableName) && !string.IsNullOrEmpty (t.ReferenceColumnName)).ToList ();
            List<ColumnAndField> forenkey = new List<ColumnAndField> ();
            if (intersects != null && intersects.Any ()) {
                foreach (var fore in forenkeysInColumn) {
                    var matchingWithForeignKey = intersects.FirstOrDefault (t => t.Name.Equals (fore.EntityFullName));
                    if (matchingWithForeignKey == null) {
                        forenkey.Add (fore);
                    }
                }
            }

            if (forenkey != null) {

                foreach (var item in forenkey) {
                    var isAddedInMatching = matchingColumns.FirstOrDefault (t => t.TableName.Equals (item.ReferenceTableName) && t.ColumnName.Equals (item.ReferenceColumnName));
                    if (isAddedInMatching != null && !string.IsNullOrEmpty (isAddedInMatching.Value.ToString ())) {

                        var targetColumn = item.ColumnName.TrimStart ('[').TrimEnd (']');
                        var resultValue = GetValueFromForeignKeyColumns (tenantId, item.TableName, item.EntityPrefix, primaryKey, targetColumn, resourceId.ToString ());
                        if (!string.IsNullOrEmpty (resultValue)) {
                            item.Value = resultValue.ToString ();
                            var updateResult = UpdateValueInForeignTable (tenantId, item.TableName, item.EntityPrefix, item.PrimaryKey, necessaryColumns, resultValue.ToString ());
                        } else {
                            var addedResult = AddValueFromForeignKeyColumns (tenantId, item.TableName, item.EntityPrefix, item.ColumnName, resourceId, necessaryColumns, columns);
                            item.Value = addedResult.ToString ();
                        }
                        // matchingColumns.Add (item);
                    }
                }
            }

            return matchingColumns;
        }
        public List<ColumnAndField> InsertIntersectEntity (Guid tenantId, Guid userId, string entityName, JObject resource, List<ColumnAndField> entityColumns, Guid relatedId) {
            IMetadataManager iMetadataManager = new MetadataManager.Contracts.MetadataManager ();
            var matchingColumns = new List<ColumnAndField> ();
            foreach (var col in entityColumns) {
                // var columnName = (!string.IsNullOrEmpty (col.ClientName)) ? col.ClientName + "." + col.FieldName : col.FieldName;
                var matching = resource[col.EntityFullName];
                if (matching != null) {

                    if (col.ColumnName.Equals (col.PrimaryKey)) {
                        col.Value = Guid.NewGuid ();
                    } else if (col.ColumnName.Equals (_tenantId)) {
                        col.Value = tenantId;
                    } else {
                        var valueStr = matching.ToObject<string> ();
                        if (!string.IsNullOrEmpty (valueStr)) {
                            string[] relationList = valueStr.Split ('|');
                            if (relationList.Any () && relationList.Count () > 1) {
                                var relationClassName = relationList[0];
                                if (col.ReferenceTableName != null && col.ReferenceTableName.ToLower ().Contains (relationClassName.ToLower ())) {
                                    string[] values = relationList[1].Split (',');
                                    if (values.Any ()) {
                                        col.Value = values[0];
                                    }
                                } else if (col.ReferenceTableName != null && col.ReferenceTableName.ToLower ().Contains (entityName.ToLower ())) {
                                    col.Value = relatedId;
                                }
                            } else {
                                if (col.ReferenceTableName != null && col.ReferenceTableName.ToLower ().Contains (entityName.ToLower ())) {
                                    col.Value = relatedId;
                                }
                            }
                        }
                    }
                    matchingColumns.Add (col);
                }
            }
            return matchingColumns;
        }
        private string AddValueFromInverseColumns (Guid tenantId, string tableName, string entityPrefix, string primaryKey, List<ColumnAndField> matchingColumns, List<ColumnAndField> entityColumns) {
            var queryBuilder = new InsertQueryBuilder ();
            var insertColumns = new Dictionary<string, string> ();
            foreach (var match in matchingColumns) {
                if (match.TableName.Equals (tableName) && match.EntityPrefix.Equals (entityPrefix)) {
                    if (match.Value != null) {
                        insertColumns.Add (match.ColumnName, match.Value.ToString ());
                    }
                }
            }
            var inverseId = Guid.NewGuid ();
            var inverPrimaryKeyColumns = entityColumns.FirstOrDefault (t => t.ColumnName.Equals (t.PrimaryKey) &&
                t.TableName.Equals (tableName) &&
                t.EntityPrefix.Equals (entityPrefix)
            );
            if (inverPrimaryKeyColumns == null) throw new FieldAccessException ("Primary key not found.");
            insertColumns.Add (inverPrimaryKeyColumns.ColumnName, inverseId.ToString ());

            //tenant id.
            var primaryTenantId = entityColumns.FirstOrDefault (x => x.ColumnName.Equals (_tenantId) && x.TableName.Equals (tableName));
            if (primaryTenantId == null) throw new FieldAccessException ("Tenant id not found.");
            insertColumns.Add (primaryTenantId.ColumnName, tenantId.ToString ());

            queryBuilder.InsertIntoTable (tableName, insertColumns, false);
            var insertQuery = queryBuilder.BuildQuery ();

            IQueryAdmin admin = new QueryAdmin ();
            admin.SaveResult (tenantId, "inverse", insertQuery);
            return inverseId.ToString ();
        }

        private string GetValueFromInverseColumns (Guid tenantId, string tableName, string prefix, string primaryKey, string columnsName, string resourceId) {
            var targetValue = string.Empty;
            var selectQueryBuilder = new SelectQueryBuilder ();
            selectQueryBuilder.SelectFromTable (tableName, prefix);
            selectQueryBuilder.AddWhere (primaryKey, Comparison.Equals, resourceId, 1);
            var selectQuery = selectQueryBuilder.BuildQuery ();

            IQueryReview review = new QueryReview ();
            var targetResult = review.GetResult (tenantId, tableName, selectQuery);
            if (targetResult != null && targetResult.Rows.Count > 0) {
                var targetColumn = columnsName.TrimStart ('[').TrimEnd (']');
                if (!targetResult.Columns.Contains (targetColumn)) return targetValue;
                targetValue = targetResult.Rows[0][targetColumn].ToString ();
            }
            return targetValue;
        }

        private string GetValueFromForeignKeyColumns (Guid tenantId, string tableName, string prefix, string primaryKey, string columnsName, string resourceId) {
            var targetValue = string.Empty;
            var selectQueryBuilder = new SelectQueryBuilder ();
            selectQueryBuilder.SelectFromTable (tableName, prefix);
            selectQueryBuilder.AddWhere (columnsName, Comparison.Equals, resourceId, 1);
            var selectQuery = selectQueryBuilder.BuildQuery ();

            IQueryReview review = new QueryReview ();
            var targetResult = review.GetResult (tenantId, tableName, selectQuery);
            if (targetResult != null && targetResult.Rows.Count > 0) {
                var targetColumn = primaryKey.TrimStart ('[').TrimEnd (']');
                if (!targetResult.Columns.Contains (targetColumn)) return targetValue;
                targetValue = targetResult.Rows[0][targetColumn].ToString ();
            }
            return targetValue;
        }

        private bool UpdateValueInInverseTable (Guid tenantId, string tableName, string entityPrefix, string primaryKey, List<ColumnAndField> matchingColumns, string primaryValue) {
            var queryBuilder = new InsertQueryBuilder ();
            var insertColumns = new Dictionary<string, string> ();
            foreach (var match in matchingColumns) {
                if (match.TableName.Equals (tableName) && match.EntityPrefix.Equals (entityPrefix)) {
                    if (match.Value != null) {
                        insertColumns.Add (match.ColumnName, match.Value.ToString ());
                    }
                }
            }

            UpdateQueryBuilder query = new UpdateQueryBuilder ();
            query.AddTable (tableName, insertColumns);
            query.AddWhere (primaryKey, Comparison.Equals, primaryValue.ToString (), 1);
            var updateQuery = query.BuildQuery ();
            IQueryAdmin admin = new QueryAdmin ();
            var status = admin.UpdateResult (tenantId, "inverse", updateQuery);
            return status;
        }

        private bool UpdateValueInForeignTable (Guid tenantId, string tableName, string entityPrefix, string primaryKey, List<ColumnAndField> matchingColumns, string primaryValue) {
            var queryBuilder = new InsertQueryBuilder ();
            var insertColumns = new Dictionary<string, string> ();
            foreach (var match in matchingColumns) {
                if (match.TableName.Equals (tableName) && match.EntityPrefix.Equals (entityPrefix)) {
                    if (match.Value != null) {
                        insertColumns.Add (match.ColumnName, match.Value.ToString ());
                    }
                }
            }
            if (!insertColumns.Any ()) return true;
            UpdateQueryBuilder query = new UpdateQueryBuilder ();
            query.AddTable (tableName, insertColumns);
            query.AddWhere (primaryKey, Comparison.Equals, primaryValue.ToString (), 1);
            var updateQuery = query.BuildQuery ();
            IQueryAdmin admin = new QueryAdmin ();
            var status = admin.UpdateResult (tenantId, "inverse", updateQuery);
            return status;
        }

        private string AddValueFromForeignKeyColumns (Guid tenantId, string tableName, string entityPrefix, string foreignKeyCoumn, Guid foreignKeyColValue, List<ColumnAndField> matchingColumns, List<ColumnAndField> entityColumns) {
            var queryBuilder = new InsertQueryBuilder ();
            var insertColumns = new Dictionary<string, string> ();
            foreach (var match in matchingColumns) {
                if (match.TableName.Equals (tableName) && match.EntityPrefix.Equals (entityPrefix)) {
                    if (match.Value != null) {
                        insertColumns.Add (match.ColumnName, match.Value.ToString ());
                    }
                }
            }
            var inverseId = Guid.NewGuid ();
            var inverPrimaryKeyColumns = entityColumns.FirstOrDefault (t => t.ColumnName.Equals (t.PrimaryKey) &&
                t.TableName.Equals (tableName) &&
                t.EntityPrefix.Equals (entityPrefix)
            );
            if (inverPrimaryKeyColumns == null) throw new FieldAccessException ("Primary key not found.");
            insertColumns.Add (inverPrimaryKeyColumns.ColumnName, inverseId.ToString ());

            //tenant id.
            var primaryTenantId = entityColumns.FirstOrDefault (x => x.ColumnName.Equals (_tenantId) && x.TableName.Equals (tableName));
            if (primaryTenantId == null) throw new FieldAccessException ("Tenant id not found.");
            insertColumns.Add (primaryTenantId.ColumnName, tenantId.ToString ());

            //foreignKey
            var foreignKeyColumns = entityColumns.FirstOrDefault (x => x.ColumnName.Equals (foreignKeyCoumn) && x.TableName.Equals (tableName));
            if (foreignKeyColumns == null) throw new FieldAccessException ("Tenant id not found.");
            insertColumns.Add (foreignKeyColumns.ColumnName, foreignKeyColValue.ToString ());

            queryBuilder.InsertIntoTable (tableName, insertColumns, false);
            var insertQuery = queryBuilder.BuildQuery ();

            IQueryAdmin admin = new QueryAdmin ();
            admin.SaveResult (tenantId, "inverse", insertQuery);
            return inverseId.ToString ();
        }
        internal List<ColumnAndField> GetInverseColumnsByUserQuery (List<ColumnAndField> columns, string tableName, string entityTablePrimaryKey, List<ColumnAndField> queryMatchingColumns) {

            var inversecolumns = new List<ColumnAndField> ();
            var allInverseColumns = columns.Where (t =>
                !string.IsNullOrEmpty (t.InverseColumnName) &&
                !string.IsNullOrEmpty (t.InverseTableName)
            ).ToList ();

            if (allInverseColumns != null) {
                foreach (var item in allInverseColumns) {
                    var isPrefixMatch = queryMatchingColumns.FirstOrDefault (t => t.EntityPrefix.Equals (item.InversePrefixName));
                    if (isPrefixMatch != null) {
                        inversecolumns.Add (item);
                    }
                }
            }
            return inversecolumns;
        }

        internal List<ColumnAndField> GetForeignColumnsByUserQuery (List<ColumnAndField> columns, string tableName, string entityTablePrimaryKey, List<ColumnAndField> queryMatchingColumns) {
            var foreignKeyColumns = new List<ColumnAndField> ();
            var foreignColumns = columns.Where (t =>
                !string.IsNullOrEmpty (t.ReferenceColumnName) &&
                !string.IsNullOrEmpty (t.ReferenceTableName)
            ).ToList ();

            if (foreignColumns != null) {
                foreach (var item in foreignColumns) {
                    var isPrefixMatch = queryMatchingColumns.FirstOrDefault (t => t.EntityPrefix.Equals (item.EntityPrefix));
                    if (isPrefixMatch != null) {
                        foreignKeyColumns.Add (item);
                    }
                }
            }
            return foreignKeyColumns;
        }
    }
}