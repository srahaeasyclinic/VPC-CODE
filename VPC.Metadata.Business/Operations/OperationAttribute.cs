using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPC.Metadata.Business.Operations
{

    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public class OperationAttribute : Attribute
    {
        public OperationAttribute(params string[] values)
        {
            Operations = values;
        }

        public string[] Operations { get; }
      
    }
}
