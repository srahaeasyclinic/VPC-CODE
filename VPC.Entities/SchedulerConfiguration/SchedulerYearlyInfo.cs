using System;
using System.Collections.Generic;
using System.ComponentModel;
using VPC.Entities.Common;

namespace VPC.Entities.SchedulerConfiguration
{
    public class SchedulerYearlyInfo
    {
        public Guid SchedulerYearlyId{get;set;}
        public Guid SchedulerId{get;set;}    
        public int? RecurrenceValue { get; set; }
        public int Unit { get; set; }
        public int? OnValue1 { get; set; }
        public int? OnValue2 { get; set; }
        public int? TheValue1 { get; set; }
        public int? TheValue2 { get; set; }
        public int? TheValue3 { get; set; }
    }
}
