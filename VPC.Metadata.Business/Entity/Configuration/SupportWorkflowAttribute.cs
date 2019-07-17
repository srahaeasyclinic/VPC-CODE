using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VPC.Metadata.Business.Entity.Configuration;

namespace VPC.Metadata.Business.Entity.Configuration
{
    public class SupportWorkflowAttribute : AllowAttribute
    {
        public SupportWorkflowAttribute(bool val) : base(val)
        {
        }
    }
}
