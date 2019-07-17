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

namespace VPC.Framework.Business.EntityResourceManager.Contracts {
    internal static class OrderByHelper {
        internal static void AddOrderBy (List<ColumnAndField> columns, QueryContext queryModel, SelectQueryBuilder queryBuilder) {
             //tapash need to change this portion :::: NEED TO ADD DEFAULT SORTING....
            if (string.IsNullOrEmpty (queryModel.OrderBy)) return;
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
                if (orderByColumnData == null) return;
                queryBuilder.AddOrderBy (orderByColumnData.EntityPrefix + "." + orderByColumnData.ColumnName, orderEnum);
            }
        }
    }
}

//Enum.TryParse("Active", out StatusEnum myStatus);