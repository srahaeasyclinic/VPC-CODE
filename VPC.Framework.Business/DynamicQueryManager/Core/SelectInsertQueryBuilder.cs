using System;
using System.Collections.Generic;
using System.Text;
using VPC.Framework.Business.DynamicQueryManager.Core.Clauses;
using VPC.Framework.Business.DynamicQueryManager.Core.Enums;

namespace VPC.Framework.Business.DynamicQueryManager.Core {
    public class SelectInsertQueryBuilder : IQueryBuilder {

        protected WhereStatement _whereStatement = new WhereStatement ();
        protected List<string> _selectedTables = new List<string> (); // array of string
        protected List<string> _selectedColumns = new List<string> (); // array of string
        protected ContainsClause _contains; // array of string

        //  protected Dictionary<string, Dictionary<string, string>> _tables = new Dictionary<string, Dictionary<string, string>>();
        protected Dictionary<Guid, Dictionary<string, Dictionary<string, string>>> _tables = new Dictionary<Guid, Dictionary<string, Dictionary<string, string>>> ();

        protected string _tableName;
        protected Dictionary<string, string> _columns = new Dictionary<string, string> ();

        public void AddContains (List<string> lists, string vlaue, Comparison comparison) {
            _contains = new ContainsClause (lists, vlaue);
        }

        public void Refresh () {
            _selectedTables.Clear ();
        }

        protected string _table1;
        protected string _table2;
        protected string _uniqueId;
        public void SelectInsertIntoTable (string whereTolnsert, string whereFromCopy, Dictionary<string, string> columns) {
            _table2 = whereTolnsert;
            _table1 = whereFromCopy;
            _columns = columns;
        }

        public void AddWhere (WhereClause clause) { AddWhere (clause, 1); }
        public void AddWhere (WhereClause clause, int level) {
            _whereStatement.Add (clause, level);
        }
        public WhereClause AddWhere (string field, Comparison @operator, object compareValue) {
            return AddWhere (field, @operator, compareValue, 1);
        }
        public WhereClause AddWhere (Enum field, Comparison @operator, object compareValue) {
            return AddWhere (field.ToString (), @operator, compareValue, 1);
        }
        public WhereClause AddWhere (string field, Comparison @operator, object compareValue, int level) {
            WhereClause NewWhereClause = new WhereClause (field, @operator, compareValue);
            _whereStatement.Add (NewWhereClause, level);
            return NewWhereClause;
        }
        public string BuildQuery () {
            return SelectInsertQuery (_table2, _table1, _columns);
        }

        private string SelectInsertQuery (string table2, string table1, Dictionary<string, string> columns) {
            string Query = "INSERT INTO " + table2;
            var keys = "";
            var values = "";
            foreach (KeyValuePair<string, string> pair in columns) {
                keys += pair.Key.ToString () + ',';
                values += (string.IsNullOrEmpty (pair.Value)) ? pair.Key.ToString () + ',' : "'"+pair.Value.ToString ()+"'" + ',';
            }
            keys = keys.TrimEnd(','); // Trim de last comma inserted by foreach loop
            values = values.TrimEnd(','); // Trim de last comma inserted by foreach loop
            Query += "(" + keys + ") ";
            Query += "SELECT " + values;
            Query += " FROM " + table1;
            // if (column.Count > 0)
            // {
            //     var keys = "";
            //     var values = "";
            //     foreach (KeyValuePair<string, string> pair in column)
            //     {
            //         keys += pair.Key.ToString() + ',';
            //         values += "'" + pair.Value.ToString() + "'" + ',';
            //     }
            //     keys = keys.TrimEnd(',');
            //     values = values.TrimEnd(',');
            //     Query += "(" + keys + ") " + " VALUES (" + values + ") ";
            // }

             if (_whereStatement.ClauseLevels > 0)
            {
                Query += " WHERE " + _whereStatement.BuildWhereStatement(_contains);
            }

            return Query + " ";
        }
    }
}