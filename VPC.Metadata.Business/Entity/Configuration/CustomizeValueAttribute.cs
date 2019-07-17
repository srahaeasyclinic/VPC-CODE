using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VPC.Metadata.Business.Entity.Configuration;

namespace VPC.Metadata.Business.Entity.Configuration
{


    
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class CustomizeValueAttribute : Attribute
    {

        public CustomizeValueAttribute()
        {
        }

    }
}
