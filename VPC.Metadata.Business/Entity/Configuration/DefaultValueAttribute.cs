using System;

namespace VPC.Metadata.Business.Entity.Configuration
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public class DefaultValueAttribute : Attribute
    {
        public DefaultValueAttribute(string str="")
        {
            Value = str;
        }

        public string Value { get; } = null;

    }
}
