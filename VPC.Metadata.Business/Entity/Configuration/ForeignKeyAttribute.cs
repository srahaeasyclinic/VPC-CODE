using System;
using System.Collections.Generic;
using System.Text;

namespace VPC.Metadata.Business.Entity.Configuration
{
   
    [AttributeUsage(AttributeTargets.Property)]
    public class ForeignKeyAttribute : Attribute
    {
        private readonly string _foreignKey;
        private readonly string _columneName;
        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <param name="foreignKeyOf">Ex:Entity name of parent table name..</param>
        public ForeignKeyAttribute(string tableName, string columnName)
        {
            _foreignKey = tableName;
            _columneName = columnName;
        }

        public string GetReferenceTableName()
        {
            return _foreignKey;
        }
        public string GetReferenceColumnName()
        {
            return _columneName;
        }
    }
}
