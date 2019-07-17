using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPC.Metadata.Business.Entity.Configuration
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public class EntityDescriptionAttribute: Attribute
    {
        private string _description;
        public EntityDescriptionAttribute(string str)
        {
            _description = str;
        }

       
        public string Name { get { return _description; } }
    }
}


