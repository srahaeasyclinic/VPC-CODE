using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPC.Metadata.Business.Entity.Configuration
{
   
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class AllowAttribute : Attribute
    {
        private bool isAllow;
        public AllowAttribute(bool val)
        {
            isAllow = val;
        }


        public bool Value { get { return isAllow; } }
    }
}
