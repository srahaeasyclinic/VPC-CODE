using System;

namespace VPC.Metadata.Business.Entity.Configuration
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public class ColumnNameAttribute : Attribute
    {
        public ColumnNameAttribute(string value)
        {
            ColumnName = value;
        }

        public string ColumnName { get; } = string.Empty;

    }
}
