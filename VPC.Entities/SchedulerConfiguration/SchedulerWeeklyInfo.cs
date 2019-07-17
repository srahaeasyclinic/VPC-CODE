using System;
using System.Collections.Generic;
using System.ComponentModel;
using VPC.Entities.Common;

namespace VPC.Entities.SchedulerConfiguration
{
    public class SchedulerWeeklyInfo
    {
        public Guid SchedulerWeeklyId{get;set;}
        public Guid SchedulerId{get;set;}       
        public int? Value { get; set; }
        public bool Monday { get; set; }
        public bool Tuesday { get; set; }
        public bool Wednesday { get; set; }
        public bool Thrusday { get; set; }
        public bool Friday { get; set; }
        public bool Saturday { get; set; }
        public bool Sunday { get; set; }
    }
}
