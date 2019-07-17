using System;
using System.Collections.Generic;
using System.Text;
using VPC.Framework.Business.DynamicQueryManager.Core.Clauses;
using VPC.Framework.Business.DynamicQueryManager.Core.Enums;

namespace VPC.Framework.Business.DynamicQueryManager.Core
{
    public class UpdateQueryBuilder : IQueryBuilder
    {

        protected WhereStatement _whereStatement = new WhereStatement();
        protected List<string> _selectedTables = new List<string>();    // array of string
        protected List<string> _selectedColumns = new List<string>();   // array of string
        protected ContainsClause _contains;     // array of string
        protected Dictionary<string, string> _updateColumns = new Dictionary<string, string>();

        protected Dictionary<string, Dictionary<string, string>> _table = new Dictionary<string, Dictionary<string, string>>();


        public void AddContains(List<string> lists, string vlaue, Comparison comparison)
        {
            _contains = new ContainsClause(lists, vlaue);
        }

        public void Refresh()
        {
            _selectedTables.Clear();
        }
        public void InsertIntoTable(string table)
        {
            _selectedTables.Add(table);
        }

        public void AddTable(string table, Dictionary<string, string> column)
        {
            _table.Add(table, column);
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
        public WhereClause AddWhere(string field, Comparison @operator, object compareValue, int level, string relations="")
        {
            WhereClause NewWhereClause = new WhereClause(field, @operator, compareValue, relations);
            _whereStatement.Add(NewWhereClause, level);
            return NewWhereClause;
        }


        public string BuildQuery()
        {
            if (_table.Count > 1)
            {
                return TransactionQuery();
            }
            return SingleQuery();
            //  return InsertQuery(_table[0].Key, _table[0].Value);
        }

        private string SingleQuery()
        {
            string Query = "";
            foreach (var table in _table)
            {
                Query += UpdateQuery(table.Key, table.Value);
            }
            return Query + " ";
        }
        private string TransactionQuery()
        {
            string Query = "";
            foreach (var table in _table)
            {
                string[] data = table.Key.Split('#');
                Query += UpdateQuery(data[1], table.Value);
            }
            return TransactionHelper.BuildQuery(Query);
            // string Query = "BEGIN ";
            // Query += "BEGIN TRY ";
            // Query += "BEGIN TRAN ";
            // foreach (var table in _table)
            // {
            //     string[] data = table.Key.Split('#');
               
            //     Query += UpdateQuery(data[1], table.Value);
            // }
            // Query += "COMMIT TRAN ";
            // Query += "END TRY ";
            // Query += "BEGIN CATCH ";
            // Query += "ROLLBACK TRAN ";
            // Query += "RAISEERROR(SELECT ERROR_MESSAGE(), ERROR_SEVERITY(), ERROR_STATE()) ";
            // Query += "END CATCH ";
            // Query += "END ";
            // return Query;
        }

        private string UpdateQuery(string table, Dictionary<string, string> column)
        {
            var keyValue = string.Empty;
            string Query = "UPDATE " + table;
            Query += " SET ";
            if (column.Count > 0)
            {
                foreach (KeyValuePair<string, string> pair in column)
                {
                    if (!string.IsNullOrEmpty(pair.Value))
                    {
                        keyValue += pair.Key.ToString() + " = " + "'" + pair.Value.ToString() + "'" + ",";
                    }
                }
                keyValue = keyValue.TrimEnd(',');
                Query += keyValue;
            }
            // Output where statement
            if (_whereStatement.ClauseLevels > 0)
            {
                Query += " WHERE " + _whereStatement.BuildWhereStatement(_contains, table);
            }
            return Query + " ";
        }
    }
}
