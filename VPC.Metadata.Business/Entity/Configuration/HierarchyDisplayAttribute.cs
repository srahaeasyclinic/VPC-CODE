using System;
using System.Collections.Generic;
using System.Text;

namespace VPC.Metadata.Business.Entity.Configuration
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class HierarchyDisplayAttribute:Attribute
    {
        public HierarchyDisplayAttribute(string str = "")
        {
            Value = str;
        }

        public string Value { get; } = null;
    }
}
