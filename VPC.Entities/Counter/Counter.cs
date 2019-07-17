using System;
using System.ComponentModel;
using VPC.Entities.Common;
using VPC.Entities.Common.Functions;
using VPC.Entities.EntityCore;

namespace VPC.Entities.Counter
{
    public class CounterInfo
    {
        public string Text { get; set; }
        public Guid CounterId { get; set; }
        public string Description { get; set; }
        public int? CounterN { get; set; }
        public int? CounterO { get; set; }
        public int? CounterP { get; set; }
        public int? ResetCounterN { get; set; }
        public int? ResetCounterO { get; set; }
        public int? ResetCounterP { get; set; }
        public DateTime UpdatedOn { get; set; }
        public Guid UpdatedBy { get; set; }
        public string EntityName { get; set; }


    }

}