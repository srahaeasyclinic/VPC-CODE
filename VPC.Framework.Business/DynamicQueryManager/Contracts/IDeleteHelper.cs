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
using VPC.Metadata.Business.Entity.Trigger;
using VPC.Metadata.Business.Entity.Trigger.Execution;
using VPC.Metadata.Business.Operator.DataAnnotations;
using Comparison = VPC.Framework.Business.DynamicQueryManager.Core.Enums.Comparison;

namespace VPC.Framework.Business.DynamicQueryManager.Contracts {

    public interface IDeleteHelper {
        dynamic BuildDeleteQuery (Guid tenantId, Guid userId, Guid itemId, string entityName);

    }

    public class DeleteHelper : IDeleteHelper {

        //tested on 
        //batchtype (26th July 2019),

        public dynamic BuildDeleteQuery (Guid tenantId, Guid userId, Guid itemId, string entityName) {
            IMetadataManager entityManager = new MetadataManager.Contracts.MetadataManager ();
            var columns = entityManager.GetColumnNameByEntityName (entityName, null);
            //@todo need to add options based column methods.
            if (!columns.Any()) return string.Empty;

            var query = "";
            var tableName = entityManager.GetTableNameByEntityname(entityName);
            var primaryKey = entityManager.GetPrimaryKeyByEntityname(entityName);
            var selectQueryBuilder = new SelectQueryBuilder ();
            selectQueryBuilder.SelectFromTable (tableName, string.Empty);
            selectQueryBuilder.AddWhere (primaryKey, Comparison.Equals, itemId.ToString(), 1);
            var selectQuery = selectQueryBuilder.BuildQuery ();
            IQueryReview review = new QueryReview ();
            var targetResult = review.GetResult (tenantId, entityName, selectQuery);
            if (targetResult == null || targetResult.Rows.Count == 0) return "";


             //delete entity
            query += GetDeleteQuery (tableName, primaryKey, itemId.ToString());

            //delete item 
            //@todo need to change business constat isPicklist..
            var isItem = entityManager.EntityIsAnItem (entityName, BusinessConstant.IsPickList);
            if(isItem){
                query += GetDeleteQuery (ItemHelper.ItemTableName, ItemHelper.ItemTablePrimarykey, itemId.ToString());
            }

            foreach (var col in columns) {
                if (col.DataType.ToString ().ToLower ().Equals ("picklist") || col.DataType.ToString ().ToLower ().Equals ("lookup")) continue;

                if(!col.EntityFullName.ToLower().Equals(entityName.ToLower())) continue;
                var targetColum = col.ColumnName.TrimStart ('[').TrimEnd (']');
                var targetValue = targetResult.Rows[0][targetColum].ToString ();

                //inverse matching column
                if (!string.IsNullOrEmpty (col.InverseColumnName) && !string.IsNullOrEmpty (col.InverseTableName)) {
                    query += GetDeleteQuery (col.InverseTableName, col.InverseColumnName, targetValue);
                }

                //foreign key matching column
                if (!string.IsNullOrEmpty (col.ReferenceColumnName) && !string.IsNullOrEmpty (col.ReferenceTableName)) {
                    query += GetDeleteQuery (col.InverseTableName, col.InverseColumnName, targetValue);
                }
            }

           
            return TransactionHelper.BuildQuery (query);
        }

        private string GetDeleteQuery (string tableName, string whichColumnForWhereCondition, string value) {
            if (string.IsNullOrEmpty (value)) return string.Empty;
            var queryBuilder = new DeleteQueryBuilder ();
            queryBuilder.SelectFromTable (tableName);
            queryBuilder.AddWhere (whichColumnForWhereCondition, Comparison.Equals, value, 1);
            var query = queryBuilder.BuildQuery ();
            return query;
        }

    }
}