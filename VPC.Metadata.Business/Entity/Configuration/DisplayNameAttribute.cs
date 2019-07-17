using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPC.Metadata.Business.Entity.Configuration
{
    public class DisplayNameAttribute : EntityDescriptionAttribute
    {
        public DisplayNameAttribute(string str) : base(str)
        {
        }
    }
}
