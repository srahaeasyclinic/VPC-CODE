using System;
using System.Collections.Generic;
using System.ComponentModel;
using VPC.Entities.Common;

namespace VPC.Entities.TenantSubscription
{
    public enum SubscriptionDuration
    {
        [Description("Weekly")]
         Weekly = 1,

         [Description("Monthly")]
         Monthly = 2,

        [Description("Yearly")]
         Yearly = 3,
       
    }
}
