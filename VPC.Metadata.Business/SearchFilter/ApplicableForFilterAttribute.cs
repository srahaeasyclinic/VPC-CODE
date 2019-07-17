using System;

namespace VPC.Metadata.Business.SearchFilter
{

    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public class ApplicableForFilterAttribute : Attribute
    {
        public ApplicableForFilterAttribute()
        {
           
        }
    }
}
