using System;
using System.Collections.Generic;
using System.Linq;
namespace VPC.Metadata.Business.Entity.Configuration
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public class AccessibleLayoutAttribute : Attribute
    {
        private List<int> _values { get; set; }
        public AccessibleLayoutAttribute(params int[] values)
        {
            _values = values.OfType<int>().ToList();
        }
        public List<int> GetValues(){
            return _values;
        }
    }
}
