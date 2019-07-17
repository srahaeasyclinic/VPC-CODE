using System;
using System.Collections.Generic;
using VPC.Entities.EntitySecurity;

namespace VPC.Entities.Credential
{
    public class ChangePasswordInfo
    {
        public string TenantCode{get;set;}
        public string UserName { get; set; } 
        public string OldPassword{get;set;}         
        public string NewPassword{get;set;}  
               
    }
}
