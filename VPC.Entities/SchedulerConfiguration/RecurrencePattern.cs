using System;
using System.Collections.Generic;
using System.ComponentModel;
using VPC.Entities.Common;

namespace VPC.Entities.SchedulerConfiguration
{
    public enum RecurrencePattern
    {
        [Description("Daily")]
        Daily = 1,
        [Description("Weekly")]
        Weekly = 2,
        [Description("Monthly")]
        Monthly = 3,
        [Description("Yearly")]
        Yearly = 4
        
    }
}
