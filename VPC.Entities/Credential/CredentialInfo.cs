using System;
using System.Collections.Generic;
using VPC.Entities.EntitySecurity;

namespace VPC.Entities.Credential
{
    public class CredentialInfo
    {
        public Guid CredentialId { get; set; }
        public Guid ParentId{get;set;}
        public string UserName { get; set; } 
        public string PasswordHash{get;set;}
        public string PasswordSalt{get;set;}
        public bool IsNew{get;set;}
        public bool IsLocked{get;set;}
        public int? InvalidAttemptCount{get;set;}
        public DateTime LockedOn{get;set;}
        public DateTime PasswordChangedOn{get;set;}
		        
    }
}
