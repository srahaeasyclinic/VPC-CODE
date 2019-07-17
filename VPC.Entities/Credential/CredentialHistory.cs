using System;
using System.Collections.Generic;
using VPC.Entities.EntitySecurity;

namespace VPC.Entities.Credential
{
    public class CredentialHistory
    {   
        public Guid CredentialHistoryId { get; set; }
        public Guid ParentId{get;set;}
        public string UserName { get; set; } 
        public string PasswordHash{get;set;}
        public string PasswordSalt{get;set;}    
        public DateTime CreatedDate{get;set;} 
   
    }
}
