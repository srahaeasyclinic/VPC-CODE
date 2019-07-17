using System;
using System.Collections.Generic;
using System.ComponentModel;
using VPC.Entities.Common;

namespace VPC.Entities.SchedulerConfiguration
{
    public class SchedulerDailyInfo
    {
        public Guid SchedulerDailyId{get;set;}
        public Guid SchedulerId{get;set;}
        public int Unit { get; set; }
        public int? Value { get; set; }
    }
}
