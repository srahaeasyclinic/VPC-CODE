using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VPC.WebApi.Model
{
    public class Validator
    {
        public string Name { get; set; }
        public bool Customize { get; set; }
        public Dictionary<string, dynamic> Items { get; set; }
    }
}
