using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPC.Metadata.Business.Entity.Configuration
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class InversePropertyAttribute : Attribute
    {
        public InversePropertyAttribute(string key)
        {
            Key = key;
        }
        public string Key { get; }
    }
}
