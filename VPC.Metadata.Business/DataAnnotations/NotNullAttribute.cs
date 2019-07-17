using System;

namespace VPC.Metadata.Business.DataAnnotations
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class NotNullAttribute : Attribute
    {
        public NotNullAttribute()
        {
        }
    }
}
