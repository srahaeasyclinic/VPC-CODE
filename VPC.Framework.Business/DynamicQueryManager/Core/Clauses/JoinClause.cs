using System;
using System.Collections.Generic;
using System.Text;
using VPC.Framework.Business.DynamicQueryManager.Core.Enums;

namespace VPC.Framework.Business.DynamicQueryManager.Core.Clauses
{
    public struct JoinClause
    {
        public JoinType JoinType;
        public string FromTable;
        public string FromAlias;
        public string FromColumn;
        public Comparison ComparisonOperator;
        public string ToTable;
         public string ToAlias;
        public string ToColumn;
        public JoinClause(JoinType join, string toTableName, string toAlias, string toColumnName, Comparison @operator, string fromTableName, string fromAlias, string fromColumnName)
        {
            JoinType = join;
            FromTable = fromTableName;
            FromColumn = fromColumnName;
            ComparisonOperator = @operator;
            ToTable = toTableName;
            ToColumn = toColumnName;
            FromAlias = fromAlias;
            ToAlias = toAlias;
        }
    }
}
