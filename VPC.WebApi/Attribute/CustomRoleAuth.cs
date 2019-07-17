using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using VPC.Entities.EntityCore;
using VPC.Entities.EntitySecurity;
using VPC.Framework.Business.EntitySecurity.APIs;
using VPC.Framework.Business.MetadataManager.Contracts;

namespace  VPC.WebApi.Attribute
{

public class CustomRoleAuth : AuthorizationHandler<CustomRoleAuth>, IAuthorizationRequirement
{  
    
      protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CustomRoleAuth requirement)
        { 
            if(!context.User.Identity.IsAuthenticated)
            {
                return Task.CompletedTask;   
            }

           bool isSuperAdmin=  bool.Parse(context.User.Claims.FirstOrDefault(c => c.Type == "IsSuperAdmin").Value);
           bool isSystemAdmin=  bool.Parse(context.User.Claims.FirstOrDefault(c => c.Type == "IsSystemAdmin").Value);
           if(isSuperAdmin || isSystemAdmin)
           {
               context.Succeed(requirement);
               return Task.CompletedTask; 
           }

            var userId= new Guid(context.User.Claims.FirstOrDefault(c => c.Type == "UserId").Value);
            var tenantId= new Guid(context.User.Claims.FirstOrDefault(c => c.Type == "TenantId").Value);            

            ISecurityCacheManager securityManager = new SecurityCacheManager();
            IMetadataManager iMetadataManager = new VPC.Framework.Business.MetadataManager.Contracts.MetadataManager();
            var entitySecurities= securityManager.SecurityCache(tenantId, userId,false).EntitySecurity;  
            if(entitySecurities.Count==0)         
            {
              return Task.CompletedTask;  
            }
            int[] codes=null;
            var methodType =string.Empty;                        
            var mvcContext = context.Resource as AuthorizationFilterContext;
            var descriptor = mvcContext?.ActionDescriptor as ControllerActionDescriptor;
            if (descriptor != null)
               {
                     
                    methodType = mvcContext.HttpContext.Request.Method;                    
                    var routeValueOfX = (string)mvcContext.HttpContext.GetRouteValue("entityName");
                    if(routeValueOfX==null)
                    {
                      // mvcContext.Result = new JsonResult("Entity name not matching.") { StatusCode = 418 };
                       return Task.CompletedTask; 
                    }
                    var entityId = iMetadataManager.GetEntityContextByEntityName(routeValueOfX);
                    var itsSecurity=(from entitySecurity in entitySecurities where entitySecurity.EntityId==entityId select entitySecurity).ToList();
                    if(itsSecurity.Count>0)
                    {
                        codes=  itsSecurity[0].SecurityCode.ToString().Select(t => int.Parse(t.ToString())).ToArray();
                                                 
                    }                  


               }  

               if(codes==null)    
               {
                  return Task.CompletedTask;  
               }

            if (methodType.ToUpper().Equals("GET"))
            {
                if(codes[0]>1)
                context.Succeed(requirement);              
            }

           else  if (methodType.ToUpper().Equals("POST"))
            {
                 if(codes[3]>1)
                context.Succeed(requirement); 
            }
            else if (methodType.ToUpper().Equals("PUT") || methodType.ToUpper().Equals("PATCH"))
            {
                 if(codes[4]>1)
                context.Succeed(requirement); 
            }
            else if (methodType.ToUpper().Equals("DELETE"))
            {
                if(codes[5]>1)
                context.Succeed(requirement); 
            }
            // if(!context.HasSucceeded)
            //         mvcContext.Result = new JsonResult("Need a custom message") { StatusCode = 418 };
            return Task.CompletedTask;
        }
    }

}