using System;
using System.Collections.Generic;
using VPC.Entities.Common;

namespace VPC.Entities.TenantSubscription
{
    public class TenantSubscriptionInfo
    {
        public Guid TenantSubscriptionId { get; set; }
        public string Name { get; set; } 
        public ItemName Group{get;set;}
        public decimal? RecurringPrice{get;set;}
        public SubscriptionDuration RecurringDuration{get;set;}
        public decimal? SetUpPrice{get;set;}
        public bool Status{get;set;}
    }
}
