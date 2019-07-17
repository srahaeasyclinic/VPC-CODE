using System;
using System.Collections.Generic;
using System.Text;
using VPC.Framework.Business.DynamicQueryManager.Core.Clauses;
using VPC.Framework.Business.DynamicQueryManager.Core.Enums;

namespace VPC.Framework.Business.DynamicQueryManager.Core
{
    public class InsertQueryBuilder : IQueryBuilder
    {

        protected WhereStatement _whereStatement = new WhereStatement();
        protected List<string> _selectedTables = new List<string>();    // array of string
        protected List<string> _selectedColumns = new List<string>();   // array of string
        protected ContainsClause _contains;     // array of string


      //  protected Dictionary<string, Dictionary<string, string>> _tables = new Dictionary<string, Dictionary<string, string>>();
      protected Dictionary<Guid, Dictionary<string, Dictionary<string, string>>> _tables = new Dictionary<Guid, Dictionary<string, Dictionary<string, string>>>();

        protected string _tableName;
        protected Dictionary<string, string> _columns = new Dictionary<string, string>();

        public void AddContains(List<string> lists, string vlaue, Comparison comparison)
        {
            _contains = new ContainsClause(lists, vlaue);
        }

        public void Refresh()
        {
            _selectedTables.Clear();
        }


        public void InsertIntoTable(string table, Dictionary<string, string> column, bool transactionRequired = true)
        {
            if (transactionRequired)
            {
                
                var dictionary = new Dictionary<string, Dictionary<string, string>>();
                dictionary.Add(table, column);
                _tables.Add(Guid.NewGuid(), dictionary);
            }
            else
            {
                _tableName = table;
                _columns = column;
            }

        }
        //public void SelectColumns(params string[] columns)
        //{
        //    _selectedColumns.Clear();
        //    foreach (string column in columns)
        //    {
        //        _selectedColumns.Add(column);
        //    }
        //}


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
        public string BuildQuery()
        {
            if (_tables.Count > 1)
            {
                return TransactionQuery();
            }

            return InsertQuery(_tableName, _columns);
        }


        private string TransactionQuery()
        {

            string Query = "";
            foreach (var table in _tables)
            {
                var value = table.Value;
                foreach (var item in value)
                {
                    Query += InsertQuery(item.Key, item.Value);
                }
              //  Query += InsertQuery(table.Key, table.Value);
            }
            return TransactionHelper.BuildQuery(Query);

            // Query += "BEGIN BEGIN TRY ";
            // Query += "BEGIN TRANSACTION ";
            // foreach (var table in _tables)
            // {
            //     var value = table.Value;
            //     foreach (var item in value)
            //     {
            //         Query += InsertQuery(item.Key, item.Value);
            //     }
            //   //  Query += InsertQuery(table.Key, table.Value);
            // }
            // Query += "COMMIT TRANSACTION ";
            // Query += "END TRY ";
            // Query += "BEGIN CATCH ";
            // Query += "ROLLBACK TRANSACTION ";
            // Query += "declare @strErrorMessage nvarchar(200),";
            // Query += "@intErrorNumber int,";
            // Query += "@intErrorSeverity int,";
            // Query += "@intErrorState int,";
            // Query += "@intErrorLine int,";
            // Query += "@strErrorProcedure nvarchar(200)";
            // Query += "SELECT @strErrorMessage = ERROR_MESSAGE(),";
            // Query += "@intErrorNumber = ERROR_NUMBER(),";
            // Query += "@intErrorSeverity = ERROR_SEVERITY(),";
            // Query += "@intErrorState = ERROR_STATE(),";
            // Query += "@intErrorLine = ERROR_LINE(),";
            // Query += "@strErrorProcedure = ISNULL(ERROR_PROCEDURE(), '-');";
            // Query += "RAISERROR(@strErrorMessage, @intErrorSeverity, 1, @intErrorNumber, @intErrorSeverity, @intErrorState, @strErrorProcedure, @intErrorLine);";
            // Query += "END CATCH ";
            // Query += "END ";
            //return Query;
        }

        private string InsertQuery(string table, Dictionary<string, string> column)
        {
            string Query = "INSERT INTO " + table;

            if (column.Count > 0)
            {
                var keys = "";
                var values = "";
                foreach (KeyValuePair<string, string> pair in column)
                {
                    keys += pair.Key.ToString() + ',';
                    values += "'" + pair.Value.ToString() + "'" + ',';
                }
                keys = keys.TrimEnd(',');
                values = values.TrimEnd(',');
                Query += "(" + keys + ") " + " VALUES (" + values + ") ";
            }
            return Query + " ";
        }
    }
}
