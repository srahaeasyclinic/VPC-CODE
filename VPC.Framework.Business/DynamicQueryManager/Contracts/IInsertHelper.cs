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
using VPC.Framework.Business.Common;
using VPC.Framework.Business.DynamicQueryManager.APIs;
using VPC.Framework.Business.DynamicQueryManager.Core;
using VPC.Framework.Business.DynamicQueryManager.Core.Clauses;
using VPC.Framework.Business.DynamicQueryManager.Core.Enums;
using VPC.Framework.Business.EntityResourceManager.Contracts;
using VPC.Framework.Business.EntityResourceManager.Data;
using VPC.Framework.Business.MetadataManager.Contracts;
using VPC.Framework.Business.RelationManager.Contracts;
using VPC.Metadata.Business.DataTypes;
using VPC.Metadata.Business.Entity.Infrastructure;
using VPC.Metadata.Business.Operator.DataAnnotations;
using Comparison = VPC.Framework.Business.DynamicQueryManager.Core.Enums.Comparison;

namespace VPC.Framework.Business.DynamicQueryManager.Contracts {

    public interface IInsertHelper {
        string BuildInsertQuery (Guid itemId, Guid tenantId, string entityName, JObject payload, string subtype, Guid userId);
        // List<GroupedColumns> MatchTest (Guid tenantId, string entityName, JObject payload, string subtype, Guid userId);
    }

    public class InsertHelper : IInsertHelper {
        string IInsertHelper.BuildInsertQuery (Guid itemId, Guid tenantId, string entityName, JObject payload, string subtype, Guid userId) {
            var matchingColumns = GetMatchingColumns (itemId, tenantId, entityName, payload, subtype, userId);
            var insertQuery = BuildInsertQueryV1 (matchingColumns);
            return insertQuery;
        }

        // List<GroupedColumns> IEntityQueryManagerV1.MatchTest (Guid tenantId, string entityName, JObject payload, string subtype, Guid userId)
        // {
        //     return GetMatchingColumns(tenantId, entityName, payload, subtype, userId);
        // }

        private List<GroupedColumns> GetMatchingColumns (Guid primaryId, Guid tenantId, string entityName, JObject payload, string subtype, Guid userId) {
            IMetadataManager iMetadataManager = new MetadataManager.Contracts.MetadataManager ();
            var entityColumns = iMetadataManager.GetColumnNameByEntityName (entityName, null);
            var context = iMetadataManager.GetEntityContextByEntityName (entityName);
            if (entityColumns == null) throw new FieldAccessException ("Column not found.");
            var matchingOptions = new MatchingOptions {
                PrimaryId = primaryId,
                TenantId = tenantId,
                AddValueFromPayload = true,
                Context = context,
                UserId = userId,
                EntitySubType = subtype,
            };
            IMatcher iMatcher = new Matcher ();
            return iMatcher.GetMatchingColumnsForInsertQuery (entityName, entityColumns, payload, matchingOptions);
        }

        private string BuildInsertQueryV1 (List<GroupedColumns> matchingColumns) {
            //create lower tables...
            Dictionary<string, string> executedTables = new Dictionary<string, string> ();
            var query = "";
            var tablesWhoHasForeignKeyReference = matchingColumns.Where (t => t.NeedToUpdateColumn != null).ToList ();
            if (tablesWhoHasForeignKeyReference.Any ()) {
                foreach (var table in tablesWhoHasForeignKeyReference) {
                    query += GetQueryStr (table);
                    StoreExecutedTables (executedTables, table);
                }
            }

            //create item table..
            var itemTable = matchingColumns.FirstOrDefault (t => t.EntityFullName.ToLower ().Equals (ItemHelper.ItemClassName.ToLower ()));
            if (itemTable != null) {
                query += GetQueryStr (itemTable);
                StoreExecutedTables (executedTables, itemTable);
            }

            //create other tables..
            foreach (var item in matchingColumns) {
                var clientName = (string.IsNullOrEmpty (item.ClientName)) ? item.EntityFullName : item.ClientName;
                var isExecuted = executedTables.Where (t => t.Key.Equals (clientName)).ToList ();
                if (isExecuted.Any ()) continue;
                query += GetQueryStr (item);
            }

            // add relateions
            foreach (var table in tablesWhoHasForeignKeyReference) {
                if (table.NeedToUpdateColumn == null) continue;
                var columnWithValue = new Dictionary<string, string> ();
                UpdateQueryBuilder updateQuery = new UpdateQueryBuilder ();
                columnWithValue.Add (table.NeedToUpdateColumn.ColumnName, table.Id.ToString ());
                updateQuery.AddTable (table.NeedToUpdateColumn.TableName, columnWithValue);
                var targetRow = matchingColumns.FirstOrDefault (t => t.EntityFullName.ToLower ().Equals (table.NeedToUpdateColumn.EntityFullName.ToLower ()));
                if (targetRow == null) continue;
                updateQuery.AddWhere (table.NeedToUpdateColumn.PrimaryKey, Comparison.Equals, targetRow.Id.ToString (), 1);
                query += updateQuery.BuildQuery ();
            }
            return TransactionHelper.BuildQuery (query);
        }

        private static void StoreExecutedTables (Dictionary<string, string> executedTables, GroupedColumns table) {
            var clientName = (string.IsNullOrEmpty (table.ClientName)) ? table.EntityFullName : table.ClientName;
            executedTables.Add (clientName, table.EntityFullName);
        }

        private string GetQueryStr (GroupedColumns table) {
            var queryBuilder = new InsertQueryBuilder ();
            var insertedColumns = GetInsertedColumns (table.Columns);
            queryBuilder.InsertIntoTable (table.Columns[0].TableName, insertedColumns, false);
            var query = queryBuilder.BuildQuery ();
            return query;
        }

        private Dictionary<string, string> GetInsertedColumns (List<ColumnAndField> columns) {
            var matchedColumns = new Dictionary<string, string> ();
            foreach (var list in columns) {
                var match = matchedColumns.FirstOrDefault (t => t.Key == list.ColumnName);
                if (match.Key != null || list.Value == null) continue;
                if (list.DataType.Equals(Metadata.Business.DataAnnotations.DataType.Password)) {
                    var password = new Password();
                    password.DigestPassword(list.Value.ToString ());
                    matchedColumns.Add ("[" + list.DataType + "Hash]", Convert.ToBase64String (password.PasswordHash));
                    matchedColumns.Add ("[" + list.DataType + "Salt]", Convert.ToBase64String (password.PasswordSalt));
                } else {
                    string value = list.DataType.ToString ().ToLower ().Equals ("datetime") ? HelperUtility.ConvertDateToUTC (list.Value.ToString ()) : list.Value.ToString ();
                    if (string.IsNullOrEmpty (value)) continue;
                    matchedColumns.Add (list.ColumnName, list.Value.ToString ());
                }
            }
            return matchedColumns;
        }

        //col.Value = col.Value == "#ENTCAST" ? iMetadataManager.GetEntityContextByEntityName(matching.ToObject<dynamic>()) : matching.ToObject<dynamic>();
    }
}