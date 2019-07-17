using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VPC.WebApi.Model
{
    public class Operation
    {
        public string name { get; set; }
        public string type { get; set; }
        public int sequence { get; set; }
        public string group { get; set; }
        public string properties { get; set; }
    }
}
