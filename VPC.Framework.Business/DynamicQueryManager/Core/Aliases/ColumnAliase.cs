using System;
using System.Collections.Generic;
using System.Text;
using VPC.Framework.Business.DynamicQueryManager.Core.Enums;

namespace VPC.Framework.Business.DynamicQueryManager.Core.Aliases
{
    public struct ColumnAliase
    {
        public string Column;
        public string Alias;
        public ColumnAliase(string column, string alias)
        {
            Column = column;
            Alias = alias;
        }
    }
}
