using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VPC.WebApi.Model
{
    public class Relation
    {
        public string Name { get; set; }
        public string Entity { get; set; }
        public string RelatedField { get; set; }
    }
}
