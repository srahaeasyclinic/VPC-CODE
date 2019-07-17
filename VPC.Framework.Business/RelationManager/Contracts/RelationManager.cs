using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json.Linq;
using VPC.Entities.EntityCore;
using VPC.Entities.EntityCore.Model.Query;
using VPC.Entities.EntityCore.Model.Storage;
using VPC.Framework.Business.DynamicQueryManager.Contracts;
using VPC.Framework.Business.DynamicQueryManager.Core;
using VPC.Framework.Business.DynamicQueryManager.Core.Enums;
using VPC.Framework.Business.EntityResourceManager.Contracts;
using VPC.Framework.Business.Initilize.Contracts;
using VPC.Framework.Business.MetadataManager.Contracts;
using VPC.Framework.Business.RelationManager.APIs;
//using VPC.Metadata.Business.Entity.Infrastructure;

namespace VPC.Framework.Business.RelationManager.Contracts {
    public interface IRelationManager {

        //parentEntityName = user
        //parentId = userId
        //relationEntityName = userInRole
        //childEntityName = role
        //childIds = role lists
        bool AddRelations (Guid tenantId, string relationEntityName, string parentEntityName, Guid parentId, string childEntityName, List<Guid> childIds);
        DataTable GetRelations (Guid tenantId, string entityName, Guid id, string relationEntityName, string intersectFieldName, string intersectClassName);
    }

    public sealed class RelationManager : IRelationManager {

        public bool AddRelations (Guid tenantId, string relationEntityName, string parentEntityName, Guid parentId, string childEntityName, List<Guid> childIds) {
           
            IMetadataManager _metadaManager = new VPC.Framework.Business.MetadataManager.Contracts.MetadataManager ();

            var tableName = _metadaManager.GetTableNameByEntityname (relationEntityName);
            var primaryKey = _metadaManager.GetPrimaryKeyByEntityname (relationEntityName);

            var fields = _metadaManager.GetColumnNameByEntityName (relationEntityName, null);

            if (fields.Any ()) {

                

                var parentTableName = _metadaManager.GetTableNameByEntityname (parentEntityName);
                var parentField = fields.FirstOrDefault (t => t.ReferenceTableName.Equals (parentTableName));

                var childTableName = _metadaManager.GetTableNameByEntityname (childEntityName);
                var childField = fields.FirstOrDefault (t => t.ReferenceTableName.Equals (childTableName));

                //delete and create....
                var queryBuilder = new DeleteQueryBuilder ();
                queryBuilder.SelectFromTable (tableName);
                queryBuilder.AddWhere (parentField.ColumnName, Comparison.Equals, parentId.ToString (), 1);
                var deleteQuery = queryBuilder.BuildQuery ();
                //----------------------------------------------------------------------------------------
                IRelationQueryAdmin deleteAdmin = new RelationQueryAdmin ();
                var res = deleteAdmin.DeleteResult (tenantId, relationEntityName, deleteQuery);

                //need to change this logic....
                foreach (var childId in childIds) {
                    var insertQueryBuilder = new InsertQueryBuilder ();
                    var insertColumns = GetNecessaryColumns (tenantId, parentId, fields, parentField);
                    insertColumns.Add (childField.ColumnName, childId.ToString ());
                    insertQueryBuilder.InsertIntoTable (tableName, insertColumns, false);
                    var insertQuery = insertQueryBuilder.BuildQuery ();
                    IRelationQueryAdmin saveAdmin = new RelationQueryAdmin ();
                    saveAdmin.SaveResult (tenantId, relationEntityName, insertQuery);
                }

            }
            return true;
        }

        private static Dictionary<string, string> GetNecessaryColumns (Guid tenantId, Guid parentId, List<ColumnAndField> fields, ColumnAndField parentField) {
            var columns = new Dictionary<string, string> ();
            foreach (var item in fields) {
                if (item.ColumnName.Equals (parentField.ColumnName)) {
                    item.Value = parentId;
                } else if (item.ColumnName.Equals (item.PrimaryKey)) {
                    item.Value = Guid.NewGuid ().ToString ();
                } else if (item.ColumnName.Equals ("[TenantId]")) {
                    item.Value = tenantId.ToString ();
                }
                if (item.Value != null) {
                    var added = columns.Where(t=>t.Key.Equals(item.ColumnName)).Any();
                    if(!added){
                        columns.Add (item.ColumnName, item.Value.ToString ());
                    }
                }
            }
            return columns;
        }

        public DataTable GetRelations (Guid tenantId, string entityName, Guid id, string relationEntityName, string intersectFieldName, string intersectClassName) {
            IMetadataManager _metadaManager = new VPC.Framework.Business.MetadataManager.Contracts.MetadataManager ();

            var tableName = _metadaManager.GetTableNameByEntityname (relationEntityName);
            var primaryKey = _metadaManager.GetPrimaryKeyByEntityname (relationEntityName);

            var fields = _metadaManager.GetColumnNameByEntityName (relationEntityName, null);

            if (fields.Any ()) {

                IRelationQueryAdmin admin = new RelationQueryAdmin ();

                var parentTableName = _metadaManager.GetTableNameByEntityname (entityName);
                var parentField = fields.FirstOrDefault (t => t.ReferenceTableName.Equals (parentTableName));
                if(parentField==null )return null;

                //
                var childField = fields.FirstOrDefault (t => t.FieldName.Equals (intersectFieldName));
                if(childField==null )return null;
                var childTableName = childField.ReferenceTableName;
                var childTablePrimaryKey = childField.ReferenceColumnName;

                var intersectContext = _metadaManager.GetEntityContextByEntityName(intersectClassName, false);
                var itemTableColumns = ItemHelper.GetItemSelectDetails (tenantId, intersectContext, 0);

              
                var selectQueryBuilder = new SelectQueryBuilder ();
                selectQueryBuilder.SelectFromTable (tableName, fields[0].EntityPrefix);

                selectQueryBuilder.AddJoin (JoinType.InnerJoin, childTableName, "_tt", childTablePrimaryKey, Comparison.Equals, tableName, fields[0].EntityPrefix, childField.ColumnName);

                selectQueryBuilder.AddJoin (JoinType.InnerJoin, itemTableColumns[0].TableName, itemTableColumns[0].EntityPrefix, childTablePrimaryKey, Comparison.Equals, tableName, fields[0].EntityPrefix, childField.ColumnName);

                var queryColumns = new List<string> ();
                var toDict = new Dictionary<string, string> ();
                foreach (var item in itemTableColumns) {
                  //  if(!item.IsIntersectProperties)continue;
                   // var columnName = !string.IsNullOrEmpty (item.ClientName) ? item.ClientName + "." + item.FieldName : item.FieldName;
                   if(item.ColumnName.Equals(item.PrimaryKey) || item.ColumnName.Equals("[Name]")){
                        queryColumns.Add (item.EntityPrefix + "." + item.ColumnName);
                        toDict.Add (item.EntityPrefix + "." + item.ColumnName, item.FieldName);
                   }
                }
                selectQueryBuilder.SelectColumns (queryColumns.ToArray ());
                selectQueryBuilder.SelectColumnsAndAliases (toDict);

                selectQueryBuilder.AddWhere (parentField.ColumnName, Comparison.Equals, id.ToString (), 1);
                var selectQuery = selectQueryBuilder.BuildQuery ();

                IRelationReview review = new RelationReview ();
                return review.GetResult (tenantId, tableName, selectQuery);
            }
            return null;
            //return status;
        }
    }
}