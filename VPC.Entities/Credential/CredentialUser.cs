using System;
using System.Collections.Generic;
using VPC.Entities.EntitySecurity;

namespace VPC.Entities.Credential
{
    public class UserCredential
    {
        public int? InvalidAttemptCount { get; set; }
        public bool IsLocked{get;set;}
        public DateTime LockedOn { get; set; } 
     
    }
}
