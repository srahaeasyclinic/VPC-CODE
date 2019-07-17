using System;
using System.Collections.Generic;
using System.ComponentModel;
using VPC.Entities.Common;

namespace VPC.Entities.SchedulerConfiguration
{
    public class SchedulerMonthlyInfo
    {
         public Guid SchedulerMonthlyId{get;set;}    
        public Guid SchedulerId{get;set;}     
        public int Unit { get; set; }
        public int? DayValue1 { get; set; }
        public int? DayValue2 { get; set; }

        public int? TheValue1 { get; set; }
        public int? TheValue2 { get; set; }
        public int? TheValue3 { get; set; }
    }
}
