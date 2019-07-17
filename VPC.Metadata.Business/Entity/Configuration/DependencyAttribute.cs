using System;

namespace VPC.Metadata.Business.Entity.Configuration
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public class DependencyAttribute : Attribute
    {


        public DependencyAttribute(params string[] values)
        {
          
        }

    }
}
