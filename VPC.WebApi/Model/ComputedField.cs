using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VPC.WebApi.Model
{
    public class ComputedField
    {
        public string FieldName { get; set; }
        public dynamic Value { get; set; }
        public string MethodName { get; set; }
    }
}
