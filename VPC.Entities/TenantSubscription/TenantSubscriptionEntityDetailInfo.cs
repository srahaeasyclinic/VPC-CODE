using System;
using System.Collections.Generic;
using VPC.Entities.Common;

namespace VPC.Entities.TenantSubscription
{
    public class TenantSubscriptionEntityDetailInfo
    {
        public Guid SubscriptionEntityDetailId { get; set; }
        public string Name{get;set;}
        public Guid SubscriptionEntityId { get; set; }  
        public Guid Context{get;set;}    
        public decimal? RecurringPrice{get;set;}
        public SubscriptionDuration RecurringDuration{get;set;}
        public decimal? OneTimePrice{get;set;}
        public SubscriptionDuration OneTimeDuration{get;set;}
    }
}
