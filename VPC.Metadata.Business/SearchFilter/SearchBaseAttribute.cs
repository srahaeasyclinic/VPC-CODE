using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPC.Metadata.Business.SearchFilter
{   

    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public class SearchBaseAttribute : Attribute
    {
        private string _name;
        public SearchBaseAttribute(string name)
        {
            _name = name;
        }
        public string GetName { get { return  _name; }}
    }

}
