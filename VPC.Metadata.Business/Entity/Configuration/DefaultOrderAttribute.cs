using System;

namespace VPC.Metadata.Business.Entity.Configuration
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
    public class DefaultOrderAttribute : Attribute
    {
        public DefaultOrderAttribute()
        {
            
        }
    }
}
