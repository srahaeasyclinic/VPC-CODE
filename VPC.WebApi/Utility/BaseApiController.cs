using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using VPC.Entities.EntitySecurity;
using VPC.Framework.Business.EntitySecurity.APIs;


namespace VPC.WebApi.Utility
{
    public class BaseApiController : Controller
    {
        ISecurityCacheManager securityManager = new SecurityCacheManager();


        public Guid TenantCode
        {
            get { return new Guid(HttpContext.User.Claims.FirstOrDefault(c => c.Type == "TenantId").Value); }
        }

        public bool IsSuperAdmin
        {
            get { return bool.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == "IsSuperAdmin").Value); }
        }

        public bool IsSystemAdmin
        {
            get { return bool.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == "IsSystemAdmin").Value); }
        }


        protected Guid UserId
        {
            get { return new Guid(HttpContext.User.Claims.FirstOrDefault(c => c.Type == "UserId").Value); }
        }

        protected string UserName
        {
            get { return HttpContext.User.Claims.FirstOrDefault(c => c.Type == "UserName").Value; }
        }

        protected EntitySecurityCacheInfo SecurityCache
        {
            get
            {
                return securityManager.SecurityCache(TenantCode, UserId, CheckSpecialUser());
            }
        }

        protected bool CheckSpecialUser()
        {
            if (IsSystemAdmin)
                return true;
            if (IsSuperAdmin)
                return true;

            return false;


        }
    }
}