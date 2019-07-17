using System;
using System.Collections.Generic;
using VPC.Entities.EntitySecurity;

namespace VPC.Entities.Credential
{
    public class LoginInfo
    {
        public string TenantCode{get;set;}
        public string UserName { get; set; } 
        public string Password{get;set;}         
    }
}
