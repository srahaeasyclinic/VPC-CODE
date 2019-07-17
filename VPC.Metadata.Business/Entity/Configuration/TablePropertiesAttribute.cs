using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPC.Metadata.Business.Entity.Configuration
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class TablePropertiesAttribute : Attribute
    {
        public TablePropertiesAttribute(string tableName, string primaryKey = "")
        {
            TableName = tableName;
            PrimaryKey = primaryKey;
        }
        public string TableName { get; }
        public string PrimaryKey { get; }
    }
}
