using System;
using System.Collections.Generic;
using System.Text;
using VPC.Framework.Business.DynamicQueryManager.Core.Enums;

namespace VPC.Framework.Business.DynamicQueryManager.Core.Clauses
{
    public struct WhereClause
    {
        private string m_FieldName;
        private Comparison m_ComparisonOperator;
        private object m_Value;

        private string t_relations;

private string w_group;


        internal struct SubClause
        {
            public LogicOperator LogicOperator;
            public Comparison ComparisonOperator;
            public object Value;
            public string CompareParam;
            public SubClause(LogicOperator logic, Comparison compareOperator, object compareValue, string compareParam)
            {
                LogicOperator = logic;
                ComparisonOperator = compareOperator;
                Value = compareValue;
                CompareParam = compareParam;
            }
        }
        internal List<SubClause> SubClauses;    // Array of SubClause


        public string FieldName
        {
            get { return m_FieldName; }
            set { m_FieldName = value; }
        }
public string TableRelations
        {
            get { return t_relations; }
            set { t_relations = value; }
        }

        public Comparison ComparisonOperator
        {
            get { return m_ComparisonOperator; }
            set { m_ComparisonOperator = value; }
        }

        public object Value
        {
            get { return m_Value; }
            set { m_Value = value; }
        }

        public string WhereGroup
        {
            get { return w_group; }
            set { w_group = value; }
        }

        public WhereClause(string field, Comparison firstCompareOperator, object firstCompareValue, string tableRelations = "", string braceName="")
        {
            m_FieldName = field;
            m_ComparisonOperator = firstCompareOperator;
            m_Value = firstCompareValue;
            SubClauses = new List<SubClause>();
            t_relations = tableRelations;
            w_group = braceName;
        }
    

        public void AddClause(LogicOperator logic, Comparison compareOperator, object compareValue, string compareParam)
        {
            SubClause NewSubClause = new SubClause(logic, compareOperator, compareValue, compareParam);
            SubClauses.Add(NewSubClause);
        }
    }
}
