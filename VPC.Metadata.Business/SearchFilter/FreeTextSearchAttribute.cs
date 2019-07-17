using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPC.Metadata.Business.SearchFilter
{
    public class FreeTextSearchAttribute : SearchBaseAttribute
    {
        public FreeTextSearchAttribute(string name= "FreeTextSearch") : base(name)
        {
        }
    }
}
