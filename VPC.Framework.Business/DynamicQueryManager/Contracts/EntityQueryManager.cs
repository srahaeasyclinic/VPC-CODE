using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using Newtonsoft.Json.Linq;
using VPC.Entities.EntityCore.Model.Query;
using VPC.Entities.WorkFlow;
using VPC.Framework.Business.DynamicQueryManager.APIs;
using VPC.Framework.Business.DynamicQueryManager.Core;
using VPC.Framework.Business.DynamicQueryManager.Core.Enums;
using VPC.Framework.Business.EntityResourceManager.Contracts;
using VPC.Framework.Business.MetadataManager.Contracts;
using VPC.Framework.Business.RelationManager.Contracts;
using VPC.Framework.Business.WorkFlow.Attribute;
using VPC.Metadata.Business.Entity.Configuration;
using VPC.Metadata.Business.Entity.Trigger;
using VPC.Metadata.Business.Entity.Trigger.Execution;
using Comparison = VPC.Framework.Business.DynamicQueryManager.Core.Enums.Comparison;

namespace VPC.Framework.Business.DynamicQueryManager.Contracts {
    public interface IEntityQueryManager {
        string BuildQuery (Guid tenantId, string entityName, QueryContext query);
        DataTable GetResult (Guid tenantId, string entityName, QueryContext query);
        DataTable GetResultById (Guid tenantId, string entityName, Guid id, QueryContext query);
        bool UpdateResult (Guid tenantId, Guid userId, string entityName, Guid id, JObject payload, string subType);
        Guid SaveResult (Guid tenantId, string entityName, JObject payload, string subtype, Guid userId);
        bool DeleteResult (Guid tenantId, Guid userId, Guid itemId, string entityName);
        bool ExecuteUpdateQuery (string queryRes);
        dynamic GetSpecificIdByQuery (Guid tenantId, string entityName, dynamic primaryKeyValue, string whichPropery);
        bool UpdateSpecificField (Guid tenantId, string entityName, dynamic primaryKeyValue, string whichPropery, string whatValue);

        Guid SelectInsert (string entityName, Guid tenantId, Guid Id, Guid userId);

        bool GetDuplicateStatus (Guid tenantId, string entityName, Dictionary<string, dynamic> fields, Guid? id);
    }

    public class EntityQueryManager : IEntityQueryManager {
        string IEntityQueryManager.BuildQuery (Guid tenantId, string entityName, QueryContext queryModel) {
            return BuildSelectQuery (tenantId, entityName, queryModel);
        }

        private static void MapSearchFilter (string entityName, List<QueryFilter> filters, List<ColumnAndField> columns) {
            if (filters == null || !filters.Any ()) return;
            foreach (var filter in filters) {
                var filterColumn = columns.FirstOrDefault (t => t.FieldName.ToLower ().Equals (filter.FieldName.ToLower ()) && (t.EntityFullName.Equals (entityName) || t.EntityFullName.Equals (ItemHelper.ItemClassName)));
                if (filterColumn == null) continue;
                filter.FieldName = filterColumn.EntityPrefix + "." + filterColumn.ColumnName;
            }
        }

        private void AddForeignKey (SelectQueryBuilder queryBuilder, List<ColumnAndField> columns, string tableName) {
            var foreinkeyTables = columns.Where (
                t => !string.IsNullOrEmpty (t.ReferenceColumnName) &&
                !string.IsNullOrEmpty (t.ReferenceTableName) &&
                t.ReferenceTableName.Equals (tableName)
            ).ToList ();
            if (!foreinkeyTables.Any ()) return; {
                foreach (var item in foreinkeyTables) {
                    var toTableName = item.TableName;
                    var toAlias = item.EntityPrefix;
                    var toColumnName = item.ColumnName;
                    var matching = columns.FirstOrDefault (t => t.TableName.Equals (item.ReferenceTableName) && t.ColumnName.Equals (item.ReferenceColumnName));
                    if (matching == null) continue;
                    var fromTableName = item.ReferenceTableName;
                    var fromAlias = matching.EntityPrefix;
                    var fromColumnName = item.ReferenceColumnName;
                    queryBuilder.AddJoin (JoinType.LeftJoin, toTableName, toAlias, toColumnName, Comparison.Equals, fromTableName, fromAlias, fromColumnName);
                    AddForeignKey (queryBuilder, columns, item.TableName);
                }
            }
        }
        ///prefixName is optional due to multiple address.!--.
        private void AddInverseKey (SelectQueryBuilder queryBuilder, List<ColumnAndField> columns, string tableName, string prefixName = "") {
            var inverseTables = columns.Where (
                t => !string.IsNullOrEmpty (t.InverseColumnName) &&
                !string.IsNullOrEmpty (t.InverseTableName) &&
                ((!string.IsNullOrEmpty (prefixName) && t.EntityPrefix.Equals (prefixName)) || (string.IsNullOrEmpty (prefixName))) &&
                t.TableName.Equals (tableName)
            ).ToList ();
            if (!inverseTables.Any ()) return; {
                foreach (var item in inverseTables) {
                    var curTable = item;
                    var toTableName = curTable.InverseTableName;
                    var toAlias = curTable.InversePrefixName;
                    var toColumnName = curTable.InverseColumnName;
                    var matching = columns.FirstOrDefault (t => t.TableName.Equals (item.InverseTableName) && t.ColumnName.Equals (item.InverseColumnName) && t.EntityPrefix.Equals (item.InversePrefixName));
                    if (matching == null) continue;
                    var fromTableName = item.TableName; //matching.TableName; //tableName
                    var fromAlias = item.EntityPrefix;
                    var fromColumnName = item.ColumnName;
                    var joinType = item.IsNotNull ? JoinType.InnerJoin : JoinType.LeftJoin;
                    queryBuilder.AddJoin (joinType, toTableName, toAlias, toColumnName, Comparison.Equals, fromTableName, fromAlias, fromColumnName);
                    AddInverseKey (queryBuilder, columns, toTableName, toAlias);
                }
            }
        }

        private string GetDbQuery (List<ColumnAndField> columns, QueryContext queryModel, string entityName, bool pagingRequired = true, bool isMappingRequired = true) {
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
                AddForeignKey (queryBuilder, columns, primaryTable.TableName);
                AddInverseKey (queryBuilder, columns, primaryTable.TableName);
            }
            IMetadataManager entityManager = new MetadataManager.Contracts.MetadataManager ();
            var allColumns = entityManager.GetColumnNameByEntityName (entityName, null);
            // please use ;;;; Filter helper....
            FilterHelper.AddSimpleSearch (queryModel, queryBuilder, allColumns);

            //TAPASH NEED TO CHECK THIS PORTION.....
            var orderByCol = columns.OrderBy (p => p.QueryIndex).ToList ();
            var toDict = new Dictionary<string, string> ();
            var queryColumns = new List<string> ();
            foreach (var item in orderByCol) {
                var colStr = item.EntityPrefix + "." + item.ColumnName;
                var isAdded = queryColumns.Any (t => t.Equals (colStr));
                if (isAdded) continue;

                if (!isMappingRequired) {
                    var columnName1 = !string.IsNullOrEmpty (item.ClientName) ? item.ClientName + "." + item.FieldName : item.FieldName;
                    queryColumns.Add (colStr);
                    toDict.Add (item.EntityPrefix + "." + item.ColumnName, columnName1);
                }

                switch (item.DataType) {
                    case Metadata.Business.DataAnnotations.DataType.PickList:

                        IPicklistManager iIPicklistManager = new PicklistManager ();
                        var isNonCustomizablePicklist = iIPicklistManager.IsNonCustomizablePicklist (item.TypeOf);
                        if (!isNonCustomizablePicklist) {
                            continue;
                        }
                        break;
                    case Metadata.Business.DataAnnotations.DataType.Guid when!item.TableName.ToLower ().Equals (primaryTable.TableName.ToLower ()):
                        continue;
                }

                if (item.VirtualField && item.DataType.Equals (Metadata.Business.DataAnnotations.DataType.Guid)) continue;

                if (item.DataType.Equals (Metadata.Business.DataAnnotations.DataType.Complex)) continue;

                //spliting only text field....
                var isTextFieldMatch = false;
                var arr = item.FieldName.Split ('.');
                if (item.VirtualField) {
                    foreach (var col in arr) {
                        if (!col.ToLower ().Equals ("text") && !col.ToLower ().Equals ("itemname")) continue;
                        isTextFieldMatch = true;
                    }
                    if (!isTextFieldMatch) continue;
                }

                var columnName = isTextFieldMatch?item.VirtualName: !string.IsNullOrEmpty (item.ClientName) ? item.ClientName + "." + item.FieldName : item.FieldName;

                var isAddedKey = queryColumns.Where (t => t.Equals (colStr.ToString ())).ToList ();
                if (isAddedKey.Any ()) continue;
                var isAddedColumn = toDict.Where (t => t.Value.Equals (columnName)).ToList ();
                if ((isAddedKey.Any () || isAddedColumn.Any ())) continue;

                // if (isAddedColumn.Any () && !string.IsNullOrEmpty(item.InversePrefixName)) {
                //     //remove old
                //     toDict.Remove (isAddedColumn[0].Key);
                //     queryColumns.Remove (isAddedColumn[0].Key);
                // }

                queryColumns.Add (colStr);
                toDict.Add (colStr, columnName);
            }
            queryBuilder.SelectColumns (queryColumns.ToArray ());
            queryBuilder.SelectColumnsAndAliases (toDict);
            if (!string.IsNullOrEmpty (queryModel.OrderBy)) {
                OrderByHelper.AddOrderBy (columns, queryModel, queryBuilder);
            }
            if (!pagingRequired || queryModel.PageIndex == 0 || queryModel.PageSize == 0) {
                return queryBuilder.BuildQuery ();
            }
            queryBuilder.AddRowNo (columns[0].EntityPrefix + "." + columns[0].ColumnName, Sorting.Ascending);
            var query = queryBuilder.BuildQuery ();
            var queryStr = queryBuilder.CreatePaging (query, queryModel.PageIndex, queryModel.PageSize, "RowNumber");
            return queryStr;

        }
        //private string BuildInsertQuery (List<ColumnAndField> whatToInsert) {
        //    var tables = whatToInsert
        //        .GroupBy (u => u.EntityPrefix)
        //        .Select (grp => new { key = grp.Key, data = grp.ToList () })
        //        .ToList ();

        //    var queryBuilder = new InsertQueryBuilder ();
        //    foreach (var item in tables) {
        //        var columns = new Dictionary<string, string> ();
        //        foreach (var list in item.data) {
        //            if (list.ColumnName.Equals ("[***]")) continue;
        //            var match = columns.FirstOrDefault (t => t.Key == list.ColumnName);
        //            if (match.Key != null || list.Value == null) continue;
        //            columns.Add (list.ColumnName,
        //                Convert.ToString (list.DataType) == "DateTime" ?
        //                HelperUtility.ConvertDateToUTC (list.Value.ToString ()) :
        //                list.Value.ToString ());
        //        }
        //        queryBuilder.InsertIntoTable (item.data[0].TableName, columns, tables.Count > 1);
        //    }
        //    var query = queryBuilder.BuildQuery ();
        //    return query;
        //}

        //private Comparison GetComparisonValue (string value) {
        //    Type myType = typeof (Operators);
        //    var properties = myType.GetProperties (BindingFlags.Public | BindingFlags.Static);
        //    var result = Comparison.Equals;
        //    Enum.TryParse (value, out Comparison myStatus);
        //    foreach (var item in properties) {
        //        var res = (dynamic) item.GetValue (null);
        //        if (res.ToString ().ToLower () != myStatus.ToString ().ToLower ()) continue;
        //        Object[] attribute = item.GetCustomAttributes (typeof (OperatorSqlMappertAttribute), true);
        //        if (attribute.Length <= 0) continue;
        //        OperatorSqlMappertAttribute myAttribute = (OperatorSqlMappertAttribute) attribute[0];
        //        result = (Comparison) myAttribute.Value;
        //    }
        //    return result;
        //}

        //private string CheckValidationRule (List<ColumnAndField> entityColumns, List<ColumnAndField> columsToInsert) {
        //    var message = string.Empty;
        //    foreach (var item in entityColumns) {
        //        if (!item.IsNotNull) continue;
        //        var found = columsToInsert.FirstOrDefault (x => x.EntityPrefix.Equals (item.EntityPrefix) && x.ColumnName.Equals (item.ColumnName) && x.TableName.Equals (item.TableName));
        //        if (found == null || found.Value != null) continue;
        //        message = item.FieldName + " is required";
        //        break;
        //    }
        //    return message;
        //}
        //private List<ColumnAndField> AddDefaultValueForInsert (Guid tenantId, Guid userId, string entityName, string subtype, Guid itemId, List<ColumnAndField> entityColumns, List<ColumnAndField> matchingColumns, JObject resource) {
        //    IMetadataManager iMetadataManager = new MetadataManager.Contracts.MetadataManager ();
        //    var entityTableName = iMetadataManager.GetTableNameByEntityname (entityName);
        //    var entityTablePrimaryKey = iMetadataManager.GetPrimaryKeyByEntityname (entityName);
        //    var entityContext = iMetadataManager.GetEntityContextByEntityName (entityName, BusinessConstant.IsPickList);

        //    //primary key.
        //    var primaryEntityId = entityColumns.FirstOrDefault (x => x.ColumnName.Equals (entityTablePrimaryKey) && x.TableName.Equals (entityTableName));
        //    if (primaryEntityId == null) throw new FieldAccessException ("Primary key not found.");
        //    primaryEntityId.Value = itemId;
        //    matchingColumns.Add (primaryEntityId);

        //    //tenant id. new logic due to tenant table
        //    var primaryTenantId = entityColumns.FirstOrDefault (x => x.ColumnName.Equals (BusinessConstant.TenantId) && x.TableName.Equals (entityTableName));
        //    if (primaryTenantId != null) {
        //        primaryTenantId.Value = tenantId;
        //        matchingColumns.Add (primaryTenantId);
        //    }

        //    var updatedBy = entityColumns.FirstOrDefault (x => x.ColumnName.Equals (BusinessConstant.UpdatedBy) && x.TableName.Equals (entityTableName));
        //    if (updatedBy != null) {
        //        updatedBy.Value = userId;
        //    }

        //    var updatedDate = entityColumns.FirstOrDefault (x => x.ColumnName.Equals (BusinessConstant.UpdatedDate) && x.TableName.Equals (entityTableName));
        //    if (updatedDate != null) {
        //        updatedDate.Value = HelperUtility.GetCurrentUTCDate ();
        //    }

        //    var starPassword = entityColumns.FirstOrDefault (x => x.FieldName.Equals (BusinessConstant.PasswordReplace));
        //    if (starPassword != null) {
        //        var col = starPassword.EntityFullName + "." + starPassword.FieldName;
        //        var passwordValue = resource[col];
        //        if (passwordValue != null) {
        //            var matchingPasswordItem = matchingColumns.SingleOrDefault (x => x.FieldName == starPassword.FieldName && x.ColumnName == starPassword.ColumnName);

        //            if (matchingPasswordItem != null) {
        //                byte[] passwordHash, passwordSalt;
        //                var encriptPassword = new EncriptPasswrod ();
        //                encriptPassword.CreatePasswordHash (passwordValue.ToString (), out passwordHash, out passwordSalt);

        //                var saltPassword = entityColumns.FirstOrDefault (x => x.ColumnName.Equals (BusinessConstant.PasswordSalt));
        //                if (saltPassword != null) {
        //                    saltPassword.Value = Convert.ToBase64String (passwordSalt);
        //                    matchingColumns.Add (saltPassword);
        //                }
        //                var hashPassword = entityColumns.FirstOrDefault (x => x.ColumnName.Equals (BusinessConstant.PasswordHash));
        //                if (hashPassword != null) {
        //                    hashPassword.Value = Convert.ToBase64String (passwordHash);
        //                    matchingColumns.Add (hashPassword);
        //                }
        //            }
        //        }
        //    }
        //    // primaryEntityId.Value = itemId;
        //    // matchingColumns.Add (primaryEntityId);

        //    foreach (var item in entityColumns) {
        //        var isAdded = matchingColumns.FirstOrDefault (x =>
        //            x.TableName.Equals (item.TableName) &&
        //            x.ClientName.Equals (item.ClientName)
        //        );
        //        if (isAdded == null || item.TableName == entityTableName) continue;
        //        if (item.ReferenceTableName != null && item.ReferenceTableName.Equals (entityTableName)) {
        //            item.Value = itemId;
        //        }
        //        if (item.ColumnName.Equals (BusinessConstant.TenantId)) {
        //            item.Value = tenantId;
        //        }
        //        if (item.ColumnName.Equals (item.PrimaryKey)) {
        //            item.Value = Guid.NewGuid ();
        //            if (!string.IsNullOrEmpty (item.Linker)) {
        //                var findLinker = entityColumns.FirstOrDefault (t => t.ColumnName.Equals (item.Linker));
        //                if (findLinker != null) {
        //                    findLinker.Value = item.Value;
        //                    matchingColumns.Add (findLinker);
        //                }
        //            }
        //        }
        //        if (!item.ColumnName.Equals (BusinessConstant.PasswordReplace)) {
        //            matchingColumns.Add (item);
        //        }
        //    }
        //    //.................................................................................................
        //    var entityIsAnItem = iMetadataManager.EntityIsAnItem (entityName, BusinessConstant.IsPickList);
        //    if (!entityIsAnItem) return matchingColumns;

        //    //JObject resource need to create computed field architechture..
        //    var resourceName = resource["ItemName"];
        //    if (resourceName == null || string.IsNullOrEmpty (resourceName.ToString ())) {
        //        resourceName = entityName;
        //        if (entityName.ToLower () == "user") {
        //            var firstName = resource["FirstName"];
        //            var lastName = resource["LastName"];
        //            resourceName = firstName + " " + lastName;
        //        }
        //    }
        //    string code = CodeGenerationHelper.Generate (entityName, resource);
        //    var itemTableWithValue = ItemHelper.GetItemSelectDetailsWithValue (tenantId, itemId, entityContext, subtype, resourceName.ToString (), true, Guid.Empty, code);
        //    itemTableWithValue.AddRange (matchingColumns);
        //    var inverseList = entityColumns.Where (t => !string.IsNullOrEmpty (t.InverseTableName) && !string.IsNullOrEmpty (t.InverseColumnName)).ToList ();

        //    foreach (var item in inverseList) {
        //        var isRequriedToAdd = matchingColumns.FirstOrDefault (t => t.ColumnName.Equals (item.InverseColumnName) && t.TableName.Equals (item.InverseTableName) && t.EntityPrefix.Equals (item.InversePrefixName));
        //        if (isRequriedToAdd == null || isRequriedToAdd.Value == null) continue;
        //        item.Value = isRequriedToAdd.Value;
        //        itemTableWithValue.Add (item);
        //    }
        //    return itemTableWithValue;
        //}

        private List<ColumnAndField> GetCommonMatchingColumns (Guid tenantId, string entityName, QueryContext queryModel, string[] result) {
            IMetadataManager entityManager = new MetadataManager.Contracts.MetadataManager ();
            var tableName = entityManager.GetTableNameByEntityname (entityName);
            var context = entityManager.GetEntityContextByEntityName (entityName, BusinessConstant.IsPickList);
            if (string.IsNullOrEmpty (tableName)) throw new FieldAccessException ("Entity not found.");
            var entityTablePrimaryKey = entityManager.GetPrimaryKeyByEntityname (entityName);
            List<ColumnAndField> entityWithItemColumns;
            var entityColumns = (result != null && result.Any ()) ? entityManager.GetColumnNameByEntityName (entityName, null) : entityManager.GetBasicColumnNameByEntityName (entityName, null);
            var isItem = entityManager.EntityIsAnItem (entityName, BusinessConstant.IsPickList);
            if (isItem) {
                var itemColumns = ItemHelper.GetItemSelectDetails (tenantId, context, 0);
                entityWithItemColumns = entityColumns.Concat (itemColumns).ToList ();
            } else {
                entityWithItemColumns = entityColumns;
            }

            //--------------------------------------
            var versionControlName = entityManager.GetVersionControlName (entityName);
            if (!string.IsNullOrEmpty (versionControlName)) {
                var versionColumns = entityManager.GetColumnNameByEntityName (versionControlName, null);
                entityWithItemColumns.AddRange (versionColumns);
            }
            //--------------------------------------
            if (entityColumns == null || !entityColumns.Any ()) throw new FieldAccessException ("Entity column is not decorate.");
            if (queryModel != null) {
                if (queryModel.Filters == null) {
                    queryModel.Filters = new List<QueryFilter> ();
                }
                //tapash please use filter helper..
                //filter Search...
                MapSearchFilter (entityName, queryModel.Filters, entityWithItemColumns);
                //text search
                MapSearchFilter (entityName, queryModel.FreeTextSearch, entityWithItemColumns);
            }

            var mergeColumns = GetMatchingColumnsWithSequenceForSelectQuery (result, entityWithItemColumns, tableName, entityTablePrimaryKey);
            return mergeColumns;
        }
        private string BuildSelectQuery (Guid tenantId, string entityName, QueryContext queryModel, bool pagingRequired = true) {
            List<ColumnAndField> orderByColumns = GetColumnsUsingBusinessLogic (tenantId, entityName, queryModel);
            var query = GetDbQuery (orderByColumns, queryModel, entityName, pagingRequired, true);
            return query;
        }

        private List<ColumnAndField> GetColumnsUsingBusinessLogic (Guid tenantId, string entityName, QueryContext queryModel) {
            var result = queryModel?.Fields?.Split (',');
            var matchingColumnsWithSequence = GetCommonMatchingColumns (tenantId, entityName, queryModel, result);
            if (!matchingColumnsWithSequence.Any ()) throw new FieldAccessException ("Column not found");
            var pickListOrLookup = matchingColumnsWithSequence.Where (
                t => t.DataType.Equals (VPC.Metadata.Business.DataAnnotations.DataType.PickList) || t.DataType.Equals (VPC.Metadata.Business.DataAnnotations.DataType.Lookup)
            ).ToList ();
            if (pickListOrLookup.Any ()) {
                var matchingHelper = new MatchingHelper ();
                var basicFieldsOfPickLists = matchingHelper.GetBasicColumnOfPickListOrLookup (tenantId, pickListOrLookup);
                matchingColumnsWithSequence.AddRange (basicFieldsOfPickLists);
            }
            var sequence = matchingColumnsWithSequence.Count ();
            if (result != null && result.Any ()) {
                foreach (var item in matchingColumnsWithSequence) {
                    int index = Array.IndexOf (result, item.FieldName);
                    item.QueryIndex = (index >= 0) ? index : sequence;
                }
            }

            var orderByColumns = matchingColumnsWithSequence.OrderBy (t => t.QueryIndex).ToList ();

            IMetadataManager entityManager = new MetadataManager.Contracts.MetadataManager ();
            var entityColumns = (result != null && result.Any ()) ? entityManager.GetColumnNameByEntityName (entityName, null) : entityManager.GetBasicColumnNameByEntityName (entityName, null);
            AddVersionConrolProperties (entityName, entityColumns, orderByColumns, "activeversion");
            AddVersionConrolProperties (entityName, entityColumns, orderByColumns, "draftversion");
            return orderByColumns;
        }

        private string BuildGetByIdQuery (Guid tenantId, string entityName, QueryContext queryModel, bool pagingRequired = true) {

            // var isApplicableForVersion = false;
            // QueryFilter draftVersion = null;
            // if (queryModel != null && queryModel.Filters.Any ()) {
            //     draftVersion = queryModel.Filters.FirstOrDefault (t => t.FieldName.Equals ("DraftVersion"));
            //     isApplicableForVersion = draftVersion != null;
            // }

            IMetadataManager entityManager = new MetadataManager.Contracts.MetadataManager ();
            var tableName = entityManager.GetTableNameByEntityname (entityName);
            var context = entityManager.GetEntityContextByEntityName (entityName, BusinessConstant.IsPickList);
            if (string.IsNullOrEmpty (tableName)) throw new FieldAccessException ("Entity not found.");
            var entityTablePrimaryKey = entityManager.GetPrimaryKeyByEntityname (entityName);
            var result = queryModel?.Fields?.Split (',');
            var entityColumns = (result != null && result.Any ()) ? entityManager.GetColumnNameByEntityName (entityName, null) : entityManager.GetBasicColumnNameByEntityName (entityName, null);
            if (entityColumns == null || !entityColumns.Any ()) throw new FieldAccessException ("Entity column is not decorate.");

            //versions...
            //  var versionControlName = entityManager.GetVersionControlName (entityName);
            // // var versionColumns = new List<ColumnAndField> ();
            // if (!string.IsNullOrEmpty (versionControlName)) {
            //     // versionColumns = entityManager.GetColumnNameByEntityName (versionControlName, null);
            //     // entityColumns.AddRange (versionColumns);
            // }

            var itemTableColumns = new List<ColumnAndField> ();
            var isItem = entityManager.EntityIsAnItem (entityName, BusinessConstant.IsPickList);
            if (isItem) {
                itemTableColumns = ItemHelper.GetItemSelectDetails (tenantId, context, 0);
            }
            var mergeColumns = GetMatchingColumnsWithSequenceForSelectQuery (result, entityColumns, tableName, entityTablePrimaryKey);
            if (!mergeColumns.Any ()) return "";
            var matchingColumnsWithSequence = itemTableColumns.Concat (mergeColumns).ToList ();

            if (!matchingColumnsWithSequence.Any ()) throw new FieldAccessException ("Column not found");
            var sequence = matchingColumnsWithSequence.Count ();
            if (result != null && result.Any ()) {
                foreach (var item in matchingColumnsWithSequence) {
                    int index = Array.IndexOf (result, item.FieldName);
                    item.QueryIndex = (index >= 0) ? index : sequence;
                }
            }
            var orderByColumns = matchingColumnsWithSequence.OrderBy (t => t.QueryIndex).ToList ();

            //---------------------filter

            //
            if (queryModel != null) {
                if (queryModel.Filters == null) {
                    queryModel.Filters = new List<QueryFilter> ();
                }
                //tapash need to change this method ...filter Search...
                MapSearchFilter (entityName, queryModel.Filters, entityColumns);
                //text search
                MapSearchFilter (entityName, queryModel.FreeTextSearch, entityColumns);
                //-------------------------------------

                // if (!string.IsNullOrEmpty (versionControlName)) {
                //     VersionControlMapper (entityName, entityColumns, versionControlName, versionColumns, orderByColumns, draftVersion);
                // }
            }
            //----------------------------

            AddVersionConrolProperties (entityName, entityColumns, orderByColumns, "activeversion");
            AddVersionConrolProperties (entityName, entityColumns, orderByColumns, "draftversion");
            var query = GetDbQuery (orderByColumns, queryModel, entityName, pagingRequired, false);
            return query;
        }

        private static void AddVersionConrolProperties (string entityName, List<ColumnAndField> entityColumns, List<ColumnAndField> orderByColumns, string properties) {
            IMetadataManager entityManager = new MetadataManager.Contracts.MetadataManager ();
            var versionControlName = entityManager.GetVersionControlName (entityName);
            if (!string.IsNullOrEmpty (versionControlName)) {
                var targetVersion = entityColumns.FirstOrDefault (t => t.FieldName.ToLower ().Equals (properties.ToLower ()));
                if (properties == "activeversion") {
                    SwitchActiveVersionInverseProperty (orderByColumns, versionControlName, targetVersion);
                }

                if (targetVersion != null) {
                    orderByColumns.Add (targetVersion);
                }
            }

        }

        private static void SwitchActiveVersionInverseProperty (List<ColumnAndField> orderByColumns, string versionControlName, ColumnAndField targetVersion) {
            var versionPrimaryField = orderByColumns.FirstOrDefault (t => t.EntityFullName.ToLower ().Equals (versionControlName.ToLower ()) && t.ColumnName.Equals (t.PrimaryKey));
            if (versionPrimaryField != null && targetVersion != null) {
                targetVersion.InversePrefixName = versionPrimaryField.EntityPrefix;
                targetVersion.InverseColumnName = versionPrimaryField.ColumnName;
                targetVersion.InverseTableName = versionPrimaryField.TableName;
                targetVersion.IsNotNull = true;
                foreach (var t in orderByColumns) {
                    if (
                        t.EntityFullName.ToLower ().Equals (versionControlName.ToLower ()) &&
                        (!string.IsNullOrEmpty (t.ReferenceColumnName) && !string.IsNullOrEmpty (t.ReferenceTableName)) &&
                        (t.ReferenceTableName.Equals (targetVersion.TableName))
                    ) {
                        t.ReferenceColumnName = string.Empty;
                        t.ReferenceTableName = string.Empty;
                        t.ReferencePrefixName = string.Empty;
                    }
                }
            }
        }

        DataTable IEntityQueryManager.GetResult (Guid tenantId, string entityName, QueryContext queryModel) {
            var query = BuildSelectQuery (tenantId, entityName, queryModel);
            IQueryReview review = new QueryReview ();
            var result = review.GetResult (tenantId, entityName, query);
            // return result;
            var entityResultMapper = new EntityResultMapper ();
            return entityResultMapper.MapResult (tenantId, entityName, result, queryModel);
        }

        DataTable IEntityQueryManager.GetResultById (Guid tenantId, string entityName, Guid id, QueryContext queryModel) {
            var query = BuildGetByIdQuery (tenantId, entityName, queryModel, false);
            IQueryReview review = new QueryReview ();
            var result = review.GetResult (tenantId, entityName, query);

            //--intersectEntity..........................
            var intersectEntity = GetIntersectEntity (tenantId, entityName, id, queryModel);
            if (intersectEntity.Any ()) {
                foreach (var item in intersectEntity) {
                    if (item.Result != null && item.Result.Rows.Count > 0) {
                        MapDynamicColumn (item.Result.Columns);
                        result.Columns.Add (item.Name, typeof (DataTable));
                        foreach (DataRow row in result.Rows) {
                            row[item.Name] = item.Result;
                        }
                    }
                }
            }

            //  return result;
            var entityResultMapper = new EntityResultMapper ();
            var mappedData = entityResultMapper.GetCustomField (tenantId, entityName, result, queryModel);
            return mappedData;

        }

        //tapash why this two columns are static??
        private static void MapDynamicColumn (DataColumnCollection columns) {
            var targetColumn = new List<string> (new [] { "rowNumber", "totalRow" });
            foreach (var item in targetColumn) {
                if (columns.Contains (item)) {
                    columns.Remove (item);
                }
            }
            foreach (DataColumn col in columns) {
                var colNameArr = col.ColumnName.Split ('_');
                var colName = (colNameArr.Any () && colNameArr.Count () > 1) ? colNameArr[1] : col.ColumnName;
                col.ColumnName = colName;
            }
        }

        private List<EntityQueryManager.IntersectHelperData> GetIntersectEntity (Guid tenantId, string entityName, Guid id, QueryContext queryModel) {
            var intersectionEntities = new List<EntityQueryManager.IntersectHelperData> ();
            IRelationManager iRelationManager = new VPC.Framework.Business.RelationManager.Contracts.RelationManager ();
            IMetadataManager entityManager = new MetadataManager.Contracts.MetadataManager ();
            var tableName = entityManager.GetTableNameByEntityname (entityName);
            if (string.IsNullOrEmpty (tableName)) throw new FieldAccessException ("Entity not found.");
            var intersectColumns = entityManager.GetIntersectColumnNameByEntityName (entityName, null);
            var result = queryModel?.Fields?.Split (',');
            if (result != null && !result.Any ()) return intersectionEntities;
            foreach (var item in result) {
                var matching = intersectColumns.FirstOrDefault (t => t.EntityFullName.ToLower ().Equals (item.ToLower ()));
                if (matching == null) continue;
                var relations = iRelationManager.GetRelations (tenantId, entityName, id, matching.EntityFullName, matching.FieldName, matching.IntersectClassName);
                if (relations == null || relations.Rows.Count <= 0) continue;
                var intHel = new EntityQueryManager.IntersectHelperData {
                    Name = matching.EntityFullName,
                    Result = relations
                };
                intersectionEntities.Add (intHel);
            }
            return intersectionEntities;
        }

        private class IntersectHelperData {
            public string Name { get; set; }
            public DataTable Result { get; set; }
        }
        /////// @todo remove id form column ...
        bool IEntityQueryManager.UpdateResult (Guid tenantId, Guid userId, string resourceName, Guid resourceId, JObject resource, string subType) {

            var targetEntityObj = resource.Children ().FirstOrDefault (t => t.Path.ToLower ().Equals (resourceName.ToLower ()));
            var entityObj = targetEntityObj.First ().ToObject<JObject> ();
            entityObj.Remove ("internalId");
            var updateStatus = UpdateEntity (tenantId, resourceName, resourceId, entityObj, subType);

            foreach (var item in resource.Children ()) {
                var path = item.Path.ToString ();
                if (string.IsNullOrEmpty (path) || path.ToLower ().Equals (resourceName.ToLower ())) continue;
                var childObj = item.First ().ToObject<JObject> ();
                var getSubTypeId = GetSubTypeId (resourceName, path, subType);
                try {
                    var targetId = childObj["internalId"];
                    childObj.Remove ("internalId");
                    if (targetId != null) {
                        Guid childId = Guid.Parse (targetId.ToString ());
                        var childrenStatus = UpdateEntity (tenantId, path, childId, childObj, subType);
                    }
                } catch {

                }
            }

            return true;
        }

        private bool UpdateEntity (Guid tenantId, string resourceName, Guid resourceId, JObject resource, string subType) {
            var children = resource.Children ();

            IMetadataManager iMetadataManager = new MetadataManager.Contracts.MetadataManager ();
            var entityTableName = iMetadataManager.GetTableNameByEntityname (resourceName);
            var entityTablePrimaryKey = iMetadataManager.GetPrimaryKeyByEntityname (resourceName);
            var columns = iMetadataManager.GetColumnNameByEntityName (resourceName, null);
            if (columns == null) throw new FieldAccessException ("Column not found.");
            Dictionary<string, string> payload = ((IDictionary<string, JToken>) (JObject) resource).ToDictionary (pair => pair.Key, pair => (string) pair.Value);
            var matchingColumns = GetMatchingColumnForUpdate (tenantId, resourceId, resourceName, entityTableName, entityTablePrimaryKey, payload, columns);
            if (!matchingColumns.Any ()) throw new FieldAccessException ("Column not matching.");

            //tapash these line blocks are unnecessary..
            var itemNameValue = resource["ItemName"];
            var name = string.Empty;
            var codeValue = resource["Code"];
            var code = string.Empty;

            if (!string.IsNullOrEmpty (itemNameValue?.ToString ())) {
                name = itemNameValue.ToString ();
            }

            if (!string.IsNullOrEmpty (codeValue?.ToString ())) {
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
            RemoveRelation (tenantId, resourceName, resourceId);
            var updateResult = admin.SaveResult (tenantId, resourceName, updateQuery);
            AddRelatoin (tenantId, resourceName, resourceId, payload, columns);

            var triggers = iMetadataManager.GetTriggerProperties (resourceName);
            if (!triggers.Any ()) return true; {
                var singletonTrigger = triggers[0];
                var bodyProp = singletonTrigger.GetBody ();
                // var search = matchingColumns.FirstOrDefault (t => t.EntityFullName.ToLower ().Equals (resourceName.ToLower ()));
                // if (search == null) return true;
                // {
                var payload1 = bodyProp.Select (item => matchingColumns.FirstOrDefault (t => t.FieldName.ToLower ().Equals (item.ToLower ()))).Where (matching => matching != null).ToDictionary<ColumnAndField, string, string> (matching => matching.ColumnName, matching => matching.Value);
                if (!payload1.Any ()) return true;
                var triggerEngine = new TriggerEngine ();
                var triggerExecutionPayload = new TriggerExecutionPayload {
                    PayloadObj = payload1,
                    ConditionalValue = resourceId.ToString ()
                };
                var triggerQuery = triggerEngine.GetQuery (triggers, triggerExecutionPayload);
                if (string.IsNullOrEmpty (triggerQuery)) return true;
                IQueryAdmin admin1 = new QueryAdmin ();
                admin1.SaveResult (tenantId, resourceName, triggerQuery);
                // }
            }
            return true;
        }

        private void RemoveRelation (Guid tenantId, string entityName, Guid resourceId) {
            IMetadataManager iMetadataManager = new MetadataManager.Contracts.MetadataManager ();
            var intersectsColumnsFromEntity = iMetadataManager.GetIntersectColumnNameByEntityName (entityName, null);
            if (intersectsColumnsFromEntity == null || !intersectsColumnsFromEntity.Any ()) return;
            foreach (var item in intersectsColumnsFromEntity) {
                var intersectColumns = iMetadataManager.GetColumnNameByEntityName (item.EntityFullName, null);
                if (!intersectColumns.Any ()) continue;
                var targetcolumns = intersectColumns.FirstOrDefault (t => t.EntityFullName != null && item.EntityFullName != null && t.EntityFullName.ToLower ().Equals (item.EntityFullName.ToLower ()) && t.TypeOf != null && t.TypeOf.ToLower ().Equals (entityName.ToLower ()));
                if (targetcolumns == null) continue;
                var queryBuilder = new DeleteQueryBuilder ();
                queryBuilder.SelectFromTable (targetcolumns.TableName);
                queryBuilder.AddWhere (targetcolumns.ColumnName, Comparison.Equals, resourceId.ToString (), 1);
                var query = queryBuilder.BuildQuery ();
                IQueryAdmin admin = new QueryAdmin ();
                var res = admin.DeleteResult (tenantId, targetcolumns.EntityFullName, query);
            }
        }

        private void AddRelatoin (Guid tenantId, string resourceName, Guid resourceId, Dictionary<string, string> payload, List<ColumnAndField> columns) {
            IRelationManager iRelationManager = new VPC.Framework.Business.RelationManager.Contracts.RelationManager ();
            foreach (var item in payload) {
                if (item.Value == null) continue;
                var relationsObj = item.Value.Split ('|');
                if (!relationsObj.Any () || relationsObj.Count () <= 1) continue;
                var necessaryField = columns.FirstOrDefault (t => t.EntityFullName.Equals (item.Key));
                if (necessaryField == null) continue;
                var values = relationsObj[1].Split (',');
                if (values == null || !values.Any ()) continue;
                var guidList = values.Select (s => Guid.Parse (s)).ToList ();
                iRelationManager.AddRelations (tenantId, item.Key, resourceName, resourceId, relationsObj[0], guidList);
            }
        }

        bool IEntityQueryManager.DeleteResult (Guid tenantId, Guid userId, Guid itemId, string entityName) {
            IDeleteHelper delete = new DeleteHelper ();
            var deleteQuery = delete.BuildDeleteQuery (tenantId, userId, itemId, entityName);
            if (string.IsNullOrEmpty (deleteQuery)) throw new FileNotFoundException (String.Format ("{0} with id {1} not found for deleting", entityName, itemId.ToString ()));
            IQueryAdmin admin = new QueryAdmin ();
            var res = admin.DeleteResult (tenantId, entityName, deleteQuery);
            return res;
        }

        Guid IEntityQueryManager.SaveResult (Guid tenantId, string entityName, JObject resource, string subtype, Guid userId) {

            var staticTenantEntityName = "tenant";

            //save main entity....
            var resultCache = new Dictionary<Guid, string> ();
            var targetEntityObj = resource.Children ().FirstOrDefault (t => t.Path.ToLower ().Equals (entityName.ToLower ()));
            var entityObj = targetEntityObj.First ().ToObject<JObject> ();

            // var test = new Dictionary<string, dynamic> ();
            // test.Add ("DrawingRefNo", "1");
            // test.Add ("RevisionNo", "2");
            // var duplicateChecking = GetDuplicateStatus (entityName, test);

            // return Guid.Empty;

            var entityId = SaveResult (tenantId, userId, entityName, entityObj, subtype);
            var result = entityId;
            resultCache.Add (entityId, entityName);
            if (entityName.ToLower ().Equals (staticTenantEntityName)) {
                tenantId = entityId;
            }

            var newlyCreatedUserId = Guid.Empty;
            foreach (var item in resource.Children ()) {
                var path = item.Path.ToString ();
                if (string.IsNullOrEmpty (path) || path.ToLower ().Equals (entityName.ToLower ())) continue;

                var obj = item.First ().ToObject<JObject> ();
                var getSubTypeId = GetSubTypeId (entityName, path, subtype);

                AddVersionSupportLogic (resultCache, path, obj);

                var id = SaveResult (tenantId, userId, path, obj, getSubTypeId);
                resultCache.Add (id, path);

                // //when senario is different..
                // if (path.ToLower ().Equals (entityName)) {
                //     result = id;
                // }
                if (path.ToLower ().Equals ("user")) {
                    newlyCreatedUserId = id;
                }

                //need to move in user.cs
                //business logic to update user as superadmin.
                if (entityName.ToLower ().Equals (staticTenantEntityName) && path.ToLower ().Equals ("user")) {
                    UpdateQueryBuilder updateQuery = new UpdateQueryBuilder ();
                    var columnWithValue = new Dictionary<string, string> ();
                    columnWithValue.Add ("[SuperAdminId]", newlyCreatedUserId.ToString ());
                    updateQuery.AddTable ("[dbo].[Tenant]", columnWithValue);
                    updateQuery.AddWhere ("[Id]", Comparison.Equals, tenantId.ToString ());
                    var queryRes = updateQuery.BuildQuery ();
                    IQueryAdmin admin = new QueryAdmin ();
                    admin.UpdateResult (tenantId, entityName, queryRes);
                }

            }
            return result;

        }

        private void AddVersionSupportLogic (Dictionary<Guid, string> resultCache, string path, JObject obj) {
            if (!resultCache.Any ()) return;
            IMetadataManager iMetadataManager = new MetadataManager.Contracts.MetadataManager ();
            var entityDetails = iMetadataManager.GetEntitityByName (path.ToLower ());
            if (entityDetails == null || entityDetails.VersionOf == null) return;

            var parentId = resultCache.FirstOrDefault (t => t.Value.ToLower ().Equals (entityDetails.VersionOf.Name.ToLower ())).Key;
            if (parentId != null) {
                var foreignKeys = entityDetails.Fields.Where (t => t.TypeOf.ToLower ().Equals (entityDetails.VersionOf.Name.ToLower ())).ToList ();
                if (foreignKeys.Any ()) {
                    foreach (var item in foreignKeys) {
                        obj.Add (item.Name, parentId.ToString ());
                    }
                }
            }
            var versionField = entityDetails.Fields.FirstOrDefault (t => t.Name.ToLower ().Equals ("versionno"));
            if (versionField != null) {
                obj.Add (versionField.Name, 1);
            }

        }

        private string GetSubTypeId (string entityName, string path, string subtype) {
            if (entityName.ToLower ().Equals (path.ToLower ())) {
                return subtype;
            };
            IMetadataManager iMetadataManager = new MetadataManager.Contracts.MetadataManager ();
            var subTypes = iMetadataManager.GetSubTypes (path);
            if (subTypes.Any ()) {
                return iMetadataManager.GetSubTypeId (path, subTypes[0].Name);
            }
            return string.Empty;
        }

        private Guid SaveResult (Guid tenantId, Guid userId, string entityName, JObject resource, string subtype) {
            IMetadataManager iMetadataManager = new MetadataManager.Contracts.MetadataManager ();
            //  var details = iMetadataManager.GetEntitityByName(entityName);

            //Preprocessor
            //  IOperationFlowEngine operationEngine = new OperationFlowEngine ();
            // var properties = new WorkFlowProcessProperties { EntityName = entityName, SubTypeCode = subtype, UserId = userId, IsSuperAdmin = false };
            // if(details!=null && details.SupportWorkflow)
            //     operationEngine.PreProcess (tenantId, new OperationWapper { OperationType = WorkFlowOperationType.Create, Data = resource }, properties);

            var itemId = Guid.NewGuid ();
            var rootTenantId = tenantId;

            if (entityName == "tenant") {
                tenantId = itemId;
            }

            IInsertHelper insertHelper = new InsertHelper ();
            var insertQuery = insertHelper.BuildInsertQuery (itemId, tenantId, entityName, resource, subtype, userId);

            IQueryAdmin admin = new QueryAdmin ();
            admin.SaveResult (tenantId, entityName, insertQuery);

            //added logic for intersect ... //need to change this logic..         

            var entity = iMetadataManager.GetEntitityByName (entityName);
            if (entity != null && entity.DetailEntities != null && entity.DetailEntities.Any ()) {

                var intersects = entity.DetailEntities.Where (t => t.Type.ToLower ().Equals ("intersectentity")).ToList ();
                if (intersects.Any ()) {
                    var entityColumns = iMetadataManager.GetColumnNameByEntityName (entityName, null);
                    var isRequiredToAddIntersect = (from item in intersects select resource[item.Name] into match where match != null select match.ToObject<string> ()).Any (value => !string.IsNullOrEmpty (value));
                    if (isRequiredToAddIntersect) {
                        Dictionary<string, string> payload = ((IDictionary<string, JToken>) (JObject) resource).ToDictionary (pair => pair.Key, pair => (string) pair.Value);
                        AddRelatoin (tenantId, entityName, itemId, payload, entityColumns);
                    }
                }
            }

            //update tenant intialisation due to exception case..
            if (entityName != "tenant") return itemId;
            var queryStr = SpecificFieldUpdate ("[dbo].[Tenant]", "[Id]", itemId, "[TenantId]", rootTenantId.ToString ());
            queryStr += SpecificFieldUpdate ("[dbo].[Item]", "[Id]", itemId, "[TenantId]", rootTenantId.ToString ());
            var query = TransactionHelper.BuildQuery (queryStr);
            admin.SaveResult (tenantId, "unspecified", query);

            return itemId;
        }

        private string SpecificFieldUpdate (string tableName, string primaryKey, Guid itemId, string whichProperty, string whichValue) {
            UpdateQueryBuilder query = new UpdateQueryBuilder ();
            var columnWithValue = new Dictionary<string, string> { { whichProperty, whichValue } };
            query.AddTable (tableName, columnWithValue);
            query.AddWhere (primaryKey, Comparison.Equals, itemId.ToString (), 1, tableName);
            var queryRes = query.BuildQuery ();
            return queryRes;
        }

        private List<ColumnAndField> GetMatchingColumnForUpdate (Guid tenantId, Guid resourceId, string resourceName, string entityTableName, string entityTablePrimaryKey, Dictionary<string, string> payload, List<ColumnAndField> columns) {
            var userKeys = payload.Select (t => t.Key).ToArray ();
            var matchNew = GetAllMatchingColumnsForEntity (userKeys, columns, entityTableName, entityTablePrimaryKey);
            //testing....
            var matchingHelper = new MatchingHelper ();
            var result = matchingHelper.AddDataToColumns (tenantId, resourceId, resourceName, entityTableName, entityTablePrimaryKey, payload, matchNew);
            return result;
        }

        private static List<ColumnAndField> GetMatchingColumnsWithSequenceForSelectQuery (string[] result, List<ColumnAndField> columns, string tableName, string entityTablePrimaryKey) {
            return GetAllMatchingColumnsForEntity (result, columns, tableName, entityTablePrimaryKey);
        }
        private static List<ColumnAndField> GetAllMatchingColumnsForEntity (string[] result, List<ColumnAndField> columns, string tableName, string entityTablePrimaryKey) {
            var maxCount = columns.Count ();
            if (result == null || !result.Any ()) return columns;
            var matchingHelper = new MatchingHelper ();
            var queryMatchingColumns = matchingHelper.GetColumnsByUserQuery (result, columns);
            if (queryMatchingColumns.Any ()) {
                var primaryColumns = matchingHelper.GetPrimaryIdsColumnsByUserQuery (columns, tableName, entityTablePrimaryKey, queryMatchingColumns);
                var necessaryInverseColumns = matchingHelper.GetInverseColumnsByUserQuery (columns, tableName, entityTablePrimaryKey, queryMatchingColumns);
                var necessaryForeignColumns = matchingHelper.GetForeignColumnsByUserQuery (columns, tableName, entityTablePrimaryKey, queryMatchingColumns);
                //need to add foreign key..
                var matchingColumns = queryMatchingColumns.Concat (primaryColumns).Concat (necessaryInverseColumns).Concat (necessaryForeignColumns).ToList ();
                var parentPrimaryKey = matchingColumns.FirstOrDefault (t => t.TableName.Equals (tableName) && t.PrimaryKey.Equals (entityTablePrimaryKey) && t.ColumnName.Equals (entityTablePrimaryKey));
                if (parentPrimaryKey == null) {
                    var tenantPrimaryObj = columns.FirstOrDefault (t => t.TableName.Equals (tableName) && t.PrimaryKey.Equals (entityTablePrimaryKey) && t.ColumnName.Equals (entityTablePrimaryKey));
                    matchingColumns.Add (tenantPrimaryObj);
                }
                //-- added subtype...
                var subTypeField = matchingColumns.FirstOrDefault (t => t.TableName.Equals (ItemHelper.ItemTableName) && t.FieldName.Equals (ItemHelper.SubTypeField));
                if (subTypeField == null) {
                    var subTypeFieldObj = columns.FirstOrDefault (t => t.TableName.Equals (ItemHelper.ItemTableName) && t.FieldName.Equals (ItemHelper.SubTypeField));
                    if (subTypeFieldObj != null) {
                        matchingColumns.Add (subTypeFieldObj);
                    }
                }
                //--
                return matchingColumns;
            }
            //return only item table..
            return columns.Where (t => t.TableName.Equals (ItemHelper.ItemTableName)).ToList ();
        }
        bool IEntityQueryManager.ExecuteUpdateQuery (string queryRes) {
            IQueryAdmin admin = new QueryAdmin ();
            return admin.ExecuteUpdateQuery (queryRes);
        }

        dynamic IEntityQueryManager.GetSpecificIdByQuery (Guid tenantId, string entityName, dynamic primaryKeyValue, string whichPropery) {
            return GetSpecificIdByQuery (tenantId, entityName, primaryKeyValue, whichPropery);
        }

        private static dynamic GetSpecificIdByQuery (Guid tenantId, string entityName, dynamic primaryKeyValue, string whichPropery) {
            IMetadataManager iMetadataManager = new MetadataManager.Contracts.MetadataManager ();
            var entityTableName = iMetadataManager.GetTableNameByEntityname (entityName);

            var entityColumns = iMetadataManager.GetColumnNameByEntityName (entityName, null);
            var isItem = iMetadataManager.EntityIsAnItem (entityName, BusinessConstant.IsPickList);
            var column = entityColumns.FirstOrDefault (t => t.FieldName.Equals (whichPropery));

            if (isItem && column == null) {
                var context = iMetadataManager.GetEntityContextByEntityName (entityName, BusinessConstant.IsPickList);
                var itemTableColumns = ItemHelper.GetItemSelectDetails (tenantId, context, 0);
                column = itemTableColumns.FirstOrDefault (t => t.FieldName.Equals (whichPropery));
            }

            if (column == null) throw new FieldAccessException ("Column not matching.");
            var selectQueryBuilder = new SelectQueryBuilder ();
            selectQueryBuilder.SelectFromTable (column.TableName, column.EntityPrefix);
            var queryColumns = new List<string> { column.EntityPrefix + "." + column.ColumnName };
            var toDict = new Dictionary<string, string> ();
            var columnName = !string.IsNullOrEmpty (column.ClientName) ? column.ClientName + "." + column.FieldName : column.FieldName;
            toDict.Add (column.EntityPrefix + "." + column.ColumnName, columnName);
            selectQueryBuilder.SelectColumns (queryColumns.ToArray ());
            selectQueryBuilder.SelectColumnsAndAliases (toDict);

            selectQueryBuilder.AddWhere (column.PrimaryKey, Comparison.Equals, primaryKeyValue.ToString (), 1);
            var selectQuery = selectQueryBuilder.BuildQuery ();
            IQueryReview review = new QueryReview ();
            var targetResult = review.GetResult (tenantId, entityTableName, selectQuery);
            if (targetResult != null) {
                return (targetResult.Columns.Contains (whichPropery)) ? targetResult.Rows[0][whichPropery] : null;
            }
            return null;
        }

        bool IEntityQueryManager.UpdateSpecificField (Guid tenantId, string entityName, dynamic primaryKeyValue, string whichPropery, string whatValue) {

            IMetadataManager iMetadataManager = new MetadataManager.Contracts.MetadataManager ();
            var entityColumns = iMetadataManager.GetColumnNameByEntityName (entityName, null);
            var tableName = iMetadataManager.GetTableNameByEntityname (entityName);
            var column = entityColumns.FirstOrDefault (t => t.FieldName.ToLower ().Equals (whichPropery.ToLower ()));
            if (column != null && !string.IsNullOrEmpty (column.ColumnName)) {
                var queryStr = SpecificFieldUpdate (tableName, "[Id]", primaryKeyValue, column.ColumnName, whatValue);
                IQueryAdmin admin = new QueryAdmin ();
                var updateResult = admin.SaveResult (tenantId, entityName, queryStr);
                return updateResult;
            }
            return false;
        }

        Guid IEntityQueryManager.SelectInsert (string entityName, Guid tenantId, Guid Id, Guid userId) {
            IMetadataManager iMetadataManager = new MetadataManager.Contracts.MetadataManager ();
            var entityColumns = iMetadataManager.GetColumnNameByEntityName (entityName, null);

            var itemId = Guid.NewGuid ();

            var tableName = iMetadataManager.GetTableNameByEntityname (entityName);
            var primaryKey = iMetadataManager.GetPrimaryKeyByEntityname (entityName);
            var query = GetColumnsOnlyPrimaryKeyValue (tenantId, entityName, tableName, primaryKey, entityColumns, itemId, Id);

            var isItem = iMetadataManager.EntityIsAnItem (entityName, BusinessConstant.IsPickList);

            if (isItem) {
                var context = iMetadataManager.GetEntityContextByEntityName (entityName, BusinessConstant.IsPickList);
                var itemColumns = ItemHelper.GetItemSelectDetails (tenantId, context, 0);
                query += GetColumnsOnlyPrimaryKeyValue (tenantId, itemColumns[0].EntityFullName, ItemHelper.ItemTableName, ItemHelper.ItemTablePrimarykey, itemColumns, itemId, Id);
                query = TransactionHelper.BuildQuery (query);
            }
            IQueryAdmin admin = new QueryAdmin ();
            admin.SaveResult (tenantId, entityName, query);
            return itemId;

        }

        private static string GetColumnsOnlyPrimaryKeyValue (Guid tenantId, string entityName, string tableName, string primaryKey, List<ColumnAndField> entityColumns, Guid itemId, Guid id) {

            var primaryColumns = new Dictionary<string, string> ();
            foreach (var item in entityColumns) {
                if (item.EntityFullName.ToLower () != entityName.ToLower ()) continue;
                var value = (item.ColumnName.Equals (primaryKey)) ? itemId.ToString () : string.Empty;

                //--need to check this data..
                if (primaryColumns.Any ()) {
                    var addedList = primaryColumns.Where (t => t.Key.Equals (item.ColumnName));
                    if (addedList.Any ()) continue;
                }

                //-- add auto update value 
                if (item.AutoIncrement != null && item.AutoIncrement.Equals (IncrementType.Version)) {
                    var versionNo = GetLastValue (tenantId, entityName, tableName, item, id);
                    value = versionNo.ToString ();
                }

                //-----------------------
                primaryColumns.Add (item.ColumnName, value);
            }
            if (primaryColumns != null && primaryColumns.Any ()) {
                var selectInsertQueryBuilder = new SelectInsertQueryBuilder ();
                selectInsertQueryBuilder.SelectInsertIntoTable (tableName, tableName, primaryColumns);
                selectInsertQueryBuilder.AddWhere (primaryKey, Comparison.Equals, id.ToString (), 1);
                var query = selectInsertQueryBuilder.BuildQuery ();
                return query;
            }
            return string.Empty;
        }

        private static dynamic GetLastValue (Guid tenantId, string entityName, string tableName, ColumnAndField item, Guid id) {
            var value = GetSpecificIdByQuery (tenantId, entityName, id, item.FieldName);
            if (value != null) {
                value = value + 1;
            }
            return value;
        }

        bool IEntityQueryManager.GetDuplicateStatus (Guid tenantId, string entityName, Dictionary<string, dynamic> fields, Guid? id) {
            return GetDuplicateStatus (entityName, fields);
        }

        private static bool GetDuplicateStatus (string entityName, Dictionary<string, dynamic> fields) {
            IMetadataManager entityManager = new MetadataManager.Contracts.MetadataManager ();
            var columns = entityManager.GetColumnNameByEntityName (entityName, null);
            var queryContext = new QueryContext ();
            var fieldArr = fields.Select (t => t.Key).ToArray ();
            queryContext.Fields = string.Join (",", fieldArr);
            //List<ColumnAndField> orderByColumns = GetColumnsUsingBusinessLogic (tenantId, entityName, queryContext);

            queryContext.Filters = new List<QueryFilter> ();

            foreach (var item in fields) {
                var colName = columns.FirstOrDefault (t => t.FieldName.ToLower ().Equals (item.Key.ToLower ()));
                if (colName != null) {
                    var filter = new QueryFilter ();
                    filter.FieldName = colName.ColumnName;
                    filter.Operator = "=";
                    filter.Value = item.Value;
                    queryContext.Filters.Add (filter);
                }
            }

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
            }
            var query = queryBuilder.BuildQuery ();
            return true;
        }
    }
}