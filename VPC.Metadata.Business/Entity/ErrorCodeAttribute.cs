using System;
using System.Collections.Generic;
using System.Text;

namespace VPC.Metadata.Business.Entity.Infrastructure
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public class ErrorCodeAttribute : Attribute
    {
        public ErrorCodeAttribute(params string[] values)
        {
            ErrorCodes = values;
        }
        public string Message { get; set; }

         
        public string[] ErrorCodes { get; }
    }

   
}
