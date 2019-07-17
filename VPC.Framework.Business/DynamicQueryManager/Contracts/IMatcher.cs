using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using VPC.Entities.EntityCore.Model.Query;
using VPC.Framework.Business.Common;
using VPC.Framework.Business.EntityResourceManager.Contracts;
using VPC.Framework.Business.MetadataManager.Contracts;

namespace VPC.Framework.Business.DynamicQueryManager.Contracts {
    public interface IMatcher {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="className"></param>
        /// <param name="columns">all columns with item files if entity inherited from item</param>
        /// <param name="clientPayload"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        List<GroupedColumns> GetMatchingColumnsForInsertQuery (string className, List<ColumnAndField> columns, JObject clientPayload, MatchingOptions options);
    }
    public class GroupedColumns {
        public string EntityFullName { get; set; }
        public string ClientName { get; set; }
        public List<ColumnAndField> Columns { get; set; }
        public ColumnAndField NeedToUpdateColumn { get; set; }
        public Guid Id { get; set; }
    }
    public class MatchingOptions {
        public Guid PrimaryId { get; set; }
        public Guid TenantId { get; set; }
        public bool AddValueFromPayload { get; set; }
        public string Context { get; set; }
        public Guid UserId { get; set; }
        public dynamic EntitySubType { get; set; }
    }

    public class Matcher : IMatcher {

        public List<GroupedColumns> GetMatchingColumnsForInsertQuery (string className, List<ColumnAndField> columns, JObject clientPayload, MatchingOptions options) {
            var matchingColumns = GetMatchingFields (className, columns, clientPayload, options);
            return SplitTableMatching (columns, matchingColumns);
        }

        private List<ColumnAndField> GetMatchingFields (string className, List<ColumnAndField> columns, JObject clientPayload, MatchingOptions options) {
            IMetadataManager iMetadataManager = new MetadataManager.Contracts.MetadataManager ();
            var tableName = iMetadataManager.GetTableNameByEntityname (className);
            var primaryKey = iMetadataManager.GetPrimaryKeyByEntityname (className);
            var classColumns = MatchingColumnsFromUserQuery (className, columns, clientPayload, options, tableName, primaryKey);

            //add internalid and tenant id from rest table

            var consolidatedChildren =
                from c in classColumns
            group c by new {
                c.EntityFullName,
                c.ClientName
                }
                into gcs
                select new GroupedColumns () {
                EntityFullName = gcs.Key.EntityFullName,
                ClientName = gcs.Key.ClientName,
                Columns = gcs.ToList (),
            };

            //add primary column and tenant columns
            foreach (var item in consolidatedChildren) {
                var tableName1 = item.Columns[0].TableName;
                var primaryKey1 = item.Columns[0].PrimaryKey;
                var tablePrefix = item.Columns[0].EntityPrefix;
                var itemId = (item.EntityFullName.ToLower().Equals(className.ToLower()))?options.PrimaryId:Guid.NewGuid();
                AddBusinessColumn (columns, classColumns, primaryKey1, tableName1, tablePrefix, itemId);
                AddBusinessColumn (columns, classColumns, BusinessConstant.TenantId, tableName1, tablePrefix, options.TenantId);
            }
            //
            var entityIsAnItem = iMetadataManager.EntityIsAnItem (className, false);
            if (!entityIsAnItem) return classColumns;

            var itemFields = ItemHelper.GetItemSelectDetails (Guid.Empty, "not-required", 0);
            var itemMatchingFields = MatchingColumnsFromUserQuery (itemFields[0].EntityFullName, itemFields, clientPayload, options, ItemHelper.ItemTableName, ItemHelper.ItemTablePrimarykey);

            var mergeColumns = classColumns.Concat (itemMatchingFields).ToList ();
            //add rest item if table is item
            AddRestItemColumns (itemFields, mergeColumns, options, clientPayload);


            //Extra business logic
            AddComputedFieldLogic(mergeColumns);
            return mergeColumns;
        }

        private void AddComputedFieldLogic(List<ColumnAndField> columns)
        {
            foreach (var column in columns)
            {
                if (column.DataType.Equals(DataType.Password))
                {
                    
                }
            }


// var starPassword = entityColumns.FirstOrDefault (x => x.FieldName.Equals (BusinessConstant.PasswordReplace));
//             if (starPassword != null) {
//                 var col = starPassword.EntityFullName + "." + starPassword.FieldName;
//                 var passwordValue = resource[col];
//                 if (passwordValue != null) {
//                     var matchingPasswordItem = matchingColumns.SingleOrDefault (x => x.FieldName == starPassword.FieldName && x.ColumnName == starPassword.ColumnName);

//                     if (matchingPasswordItem != null) {
//                         byte[] passwordHash, passwordSalt;
//                         var encriptPassword = new EncriptPasswrod ();
//                         encriptPassword.CreatePasswordHash (passwordValue.ToString (), out passwordHash, out passwordSalt);

//                         var saltPassword = entityColumns.FirstOrDefault (x => x.ColumnName.Equals (BusinessConstant.PasswordSalt));
//                         if (saltPassword != null) {
//                             saltPassword.Value = Convert.ToBase64String (passwordSalt);
//                             matchingColumns.Add (saltPassword);
//                         }
//                         var hashPassword = entityColumns.FirstOrDefault (x => x.ColumnName.Equals (BusinessConstant.PasswordHash));
//                         if (hashPassword != null) {
//                             hashPassword.Value = Convert.ToBase64String (passwordHash);
//                             matchingColumns.Add (hashPassword);
//                         }
//                     }
//                 }
//             }

        }

        private List<GroupedColumns> SplitTableMatching (List<ColumnAndField> allColumns, List<ColumnAndField> matchingColumns) {
            var consolidatedChildren =
                from c in matchingColumns
            group c by new {
                c.EntityFullName,
                c.ClientName
                }
                into gcs
                select new GroupedColumns () {
                EntityFullName = gcs.Key.EntityFullName,
                ClientName = gcs.Key.ClientName,
                Columns = gcs.ToList (),
            };

            var groupedColumnses = consolidatedChildren as IList<GroupedColumns> ?? consolidatedChildren.ToList ();
            if (groupedColumnses.Any () && groupedColumnses.Count () > 1) {
                var list = groupedColumnses.ToList ();
                foreach (var item in list) {
                    var primaryCol = item.Columns.FirstOrDefault (t => t.ColumnName.ToLower ().Equals (t.PrimaryKey.ToLower ()));
                    if (primaryCol != null) {
                        item.Id = primaryCol.Value;
                    }
                    if (string.IsNullOrEmpty (item.ClientName)) continue; {
                        var updateColumn = allColumns.FirstOrDefault (t => t.FieldName.Equals (item.ClientName));
                        if (updateColumn == null || string.IsNullOrEmpty (updateColumn.ColumnName)) continue;
                        item.NeedToUpdateColumn = updateColumn;
                        item.NeedToUpdateColumn.Value = item.Id;
                    }
                }
                return list;
            }
            return null;
        }
        private static List<ColumnAndField> MatchingColumnsFromUserQuery (string className, List<ColumnAndField> columns, JObject clientPayload,
            MatchingOptions options, string tableName, string primaryKey) {
            var matchingColumns = new List<ColumnAndField> ();
            foreach (var col in columns) {
                var columnName = (!string.IsNullOrEmpty (col.ClientName)) ? col.ClientName + "." + col.FieldName : col.FieldName;
                var matching = clientPayload[columnName];
                //need to add broadcast filed to another two fields (ex password to passwordsalt and passwordhash)
                //need to add broadcast filed to another two fields (ex #ENTCAST)
                //simple matching...
                if (matching == null) continue;
                if (options != null && options.AddValueFromPayload) {
                    col.Value = matching.ToObject<dynamic> ();
                }
                matchingColumns.Add (col);


                //need to change password
                //itemName
                //entcast
                //if (matching != null)
                //{
                //    col.Value = col.Value == "#ENTCAST" ? iMetadataManager.GetEntityContextByEntityName(matching.ToObject<dynamic>()) : matching.ToObject<dynamic>();
                //    matchingColumns.Add(col);
                //}
                //else if (col.Value != null && col.TableName.Equals(tableName) && col.EntityFullName.ToLower().Equals(className.ToLower()))
                //{
                //    matchingColumns.Add(col);
                //}

            }
           
            return matchingColumns;
        }

        private void AddRestItemColumns (List<ColumnAndField> whichFields, List<ColumnAndField> whereToAdd, MatchingOptions options, JObject clientPayload) {
            foreach (var item in whichFields) {
                var isAdded = whereToAdd.Where (t =>
                    t.ColumnName.ToLower ().Equals (item.ColumnName.ToLower ()) &&
                    t.TableName.ToLower ().Equals (item.TableName.ToLower ()) &&
                    t.EntityFullName.ToLower ().Equals (item.EntityFullName.ToLower ()) &&
                    t.EntityPrefix.ToLower ().Equals (item.EntityPrefix.ToLower ())
                ).ToList ();

                if (isAdded.Any ()) continue;

                if (item.ColumnName.Equals (BusinessConstant.UpdatedBy)) {
                    item.Value = options.UserId;
                }
                if (item.ColumnName.Equals (BusinessConstant.UpdatedDate)) {
                    item.Value = HelperUtility.GetCurrentUTCDate ();
                }
                if (item.ColumnName.Equals (BusinessConstant.EntityCode)) {
                    item.Value = options.Context;
                }

                if (item.ColumnName.Equals (BusinessConstant.EntitySubtype)) {
                    item.Value = options.EntitySubType;
                }
                if (item.ColumnName.Equals (BusinessConstant.Active)) {
                    item.Value = 1;
                }
                if (item.ColumnName.Equals (BusinessConstant.Name)) {
                    item.Value = "NotImplemented";
                }
                if (item.ColumnName.Equals (ItemHelper.ItemTablePrimarykey)) {
                    item.Value = options.PrimaryId;
                }
                if (item.ColumnName.Equals (BusinessConstant.TenantId)) {
                    item.Value = options.TenantId;
                }
                if (item.ColumnName.Equals (BusinessConstant.AutogeneratedCode)) {
                    item.Value = CodeGenerationHelper.Generate (ItemHelper.ItemClassName, clientPayload);
                }
                whereToAdd.Add (item);
            }
        }
        private static void AddBusinessColumn (List<ColumnAndField> whereToFind, List<ColumnAndField> whereToAdd, string whichColumnIFind, string tableName, string tablePrefix, dynamic whatValueIAdd) {
            var primaryColumn = whereToFind.FirstOrDefault (t => t.ColumnName.ToLower ().Equals (whichColumnIFind.ToLower ()) && t.TableName.ToLower ().Equals (tableName.ToLower ()) && t.EntityPrefix.ToLower ().Equals (tablePrefix.ToLower ()));
            if (primaryColumn == null) return;
            {
                var addedResult = whereToAdd.Where (t =>
                    t.ColumnName.ToLower ().Equals (primaryColumn.ColumnName.ToLower ()) &&
                    t.TableName.ToLower ().Equals (primaryColumn.TableName.ToLower ()) &&
                    t.EntityFullName.ToLower ().Equals (primaryColumn.EntityFullName.ToLower ()) &&
                    t.EntityPrefix.ToLower ().Equals (primaryColumn.EntityPrefix.ToLower ())
                );
                if (addedResult.Any ()) return; //if added
                primaryColumn.Value = whatValueIAdd;
                whereToAdd.Add (primaryColumn);
            }
        }
    }
}