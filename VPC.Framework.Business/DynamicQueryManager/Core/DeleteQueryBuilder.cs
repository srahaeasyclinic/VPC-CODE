using System;
using System.Collections.Generic;
using System.Text;
using VPC.Framework.Business.DynamicQueryManager.Core.Clauses;
using VPC.Framework.Business.DynamicQueryManager.Core.Enums;

namespace VPC.Framework.Business.DynamicQueryManager.Core
{
    public class DeleteQueryBuilder : IQueryBuilder
    {
        protected WhereStatement _whereStatement = new WhereStatement();
        protected List<string> _selectedTables = new List<string>();    // array of string
        protected List<string> _selectedColumns = new List<string>();   // array of string
        protected ContainsClause _contains;     // array of string
        protected Dictionary<string, string> _updateColumns = new Dictionary<string, string>();




        public void AddContains(List<string> lists, string vlaue, Comparison comparison)
        {
            _contains = new ContainsClause(lists, vlaue);
        }
        public void SelectFromTable(string table)
        {
            _selectedTables.Clear();
            _selectedTables.Add(table);
        }
        public void SelectColumns(params string[] columns)
        {
            _selectedColumns.Clear();
            foreach (string column in columns)
            {
                _selectedColumns.Add(column);
            }
        }
        public void AddWhere(WhereClause clause) { AddWhere(clause, 1); }
        public void AddWhere(WhereClause clause, int level)
        {
            _whereStatement.Add(clause, level);
        }
        public WhereClause AddWhere(string field, Comparison @operator, object compareValue)
        {
            return AddWhere(field, @operator, compareValue, 1);
        }
        public WhereClause AddWhere(Enum field, Comparison @operator, object compareValue)
        {
            return AddWhere(field.ToString(), @operator, compareValue, 1);
        }
        public WhereClause AddWhere(string field, Comparison @operator, object compareValue, int level)
        {
            WhereClause NewWhereClause = new WhereClause(field, @operator, compareValue);
            _whereStatement.Add(NewWhereClause, level);
            return NewWhereClause;
        }
        public void SetColumns(Dictionary<string, string> updateColumns)
        {
            _updateColumns.Clear();
            _updateColumns = updateColumns;
        }


        public string BuildQuery()
        {
            string Query = "DELETE ";

            Query += _selectedTables[0] + " ";

            //// set value..
            //if (_updateColumns.Count > 0)
            //{
            //    Query += "SET ";
            //    foreach (KeyValuePair<string, string> pair in _updateColumns)
            //    {
            //        Query += pair.Key.ToString() +" = "+ pair.Value.ToString() + ',';
            //    }
            //    Query = Query.TrimEnd(','); // Trim de last comma inserted by foreach loop
            //    Query += ' ';
            //}


            // Output where statement
            if (_whereStatement.ClauseLevels > 0)
            {
                Query += " WHERE " + _whereStatement.BuildWhereStatement(_contains);
            }


            return Query;
        }



    }
}