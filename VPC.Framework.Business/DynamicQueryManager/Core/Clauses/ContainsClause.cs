using System;
using System.Collections.Generic;
using System.Text;

namespace VPC.Framework.Business.DynamicQueryManager.Core.Clauses
{
    public struct ContainsClause
    {
        private List<string> _containsList;
        private string _containsValue;

        public List<string> containsList
        {
            get { return _containsList; }
        }

        public string value
        {
            get { return _containsValue; }
        }

        public ContainsClause(List<string> lists, string value)
        {
            _containsList = lists;
            _containsValue = value;
        }

    }
}
