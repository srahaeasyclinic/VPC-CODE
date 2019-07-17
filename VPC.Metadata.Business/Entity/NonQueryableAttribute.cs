using System;

namespace VPC.Metadata.Business.Entity.Configuration
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public class NonQueryableAttribute : Attribute
    {
        public NonQueryableAttribute()
        {
            
        }
    }
}
