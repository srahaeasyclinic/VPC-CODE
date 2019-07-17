using System;
using System.Collections.Generic;
using VPC.Entities.Common;

namespace VPC.Entities.TenantSubscription
{
    public class TenantSubscriptionEntityInfo
    {
        public Guid TenantSubscriptionEntityId { get; set; }
        public Guid TenantSubscriptionId { get; set; }      
        public string EntityId{get;set;}
        public int? LimtNumber{get;set;}
        public LimitTypes LimitType{get;set;}   
    }
}
