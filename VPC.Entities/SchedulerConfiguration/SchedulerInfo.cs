using System;
using System.Collections.Generic;
using System.ComponentModel;
using VPC.Entities.Common;

namespace VPC.Entities.SchedulerConfiguration
{
    public class SchedulerInfo
    {
        public Guid SchedulerId{get;set;}
        public Guid BatchTypeId{get;set;}
        public int SyncTime{get;set;}
        public RecurrencePattern RecurrenceType{get;set;}
        public SchedulerDailyInfo Daily{get;set;}
        public SchedulerWeeklyInfo Weekly{get;set;}
        public SchedulerMonthlyInfo Monthly{get;set;}
        public SchedulerYearlyInfo Yearly{get;set;}
    }
}
