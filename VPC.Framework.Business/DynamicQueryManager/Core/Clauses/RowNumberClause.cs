using System;
using System.Collections.Generic;
using System.Text;
using VPC.Framework.Business.DynamicQueryManager.Core.Enums;

namespace VPC.Framework.Business.DynamicQueryManager.Core.Clauses
{
    public struct RowNumberClause
    {
       
        public string FieldName;
        public Sorting SortOrder;
        public RowNumberClause(string field, Sorting order)
        {
            FieldName = field;
            SortOrder = order;
        }
    }
}
