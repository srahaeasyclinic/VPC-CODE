using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPC.Metadata.Business.Operator.DataAnnotations
{
   

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class OperatorSqlMappertAttribute : Attribute
    {
        private Comparison _comparison;
        public OperatorSqlMappertAttribute(Comparison  comparison)
        {
            _comparison = comparison;
        }


        public Comparison Value { get { return _comparison; } }
    }
}
