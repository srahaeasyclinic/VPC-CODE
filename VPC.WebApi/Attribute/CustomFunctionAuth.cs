using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using VPC.Entities.EntityCore;
using VPC.Entities.EntitySecurity;
using VPC.Framework.Business.EntitySecurity.APIs;
using VPC.Framework.Business.MetadataManager.Contracts;

namespace  VPC.WebApi.Attribute
{

//      internal class CustomFunctionAuthHandler : AuthorizationHandler<CustomFunctionAuth>
//     {
//         private readonly ILogger<CustomFunctionAuthHandler> _logger;

//         public CustomFunctionAuthHandler(ILogger<CustomFunctionAuthHandler> logger)
//         {
//             _logger = logger;
//         }

//         Check whether a given MinimumAgeRequirement is satisfied or not for a particular context
//         protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CustomFunctionAuth requirement)
//         {
//             Log as a warning so that it's very clear in sample output which authorization policies 
//             (and requirements/handlers) are in use
//             _logger.LogWarning("Evaluating authorization requirement for age >= {age}", requirement._infoType);

//             Check the user's age
//             var dateOfBirthClaim = context.User.FindFirst(c => c.Type == ClaimTypes.DateOfBirth);
//             if (dateOfBirthClaim != null)
//             {
//                 // If the user has a date of birth claim, check their age
//                 var dateOfBirth = Convert.ToDateTime(dateOfBirthClaim.Value);
//                 var age = DateTime.Now.Year - dateOfBirth.Year;
//                 if (dateOfBirth > DateTime.Now.AddYears(-age))
//                 {
//                     // Adjust age if the user hasn't had a birthday yet this year
//                     age--;
//                 }

//                 // If the user meets the age criterion, mark the authorization requirement succeeded
//                 if (age >= requirement.Age)
//                 {
//                     _logger.LogInformation("Minimum age authorization requirement {age} satisfied", requirement.Age);
//                     context.Succeed(requirement);
//                 }
//                 else
//                 {
//                     _logger.LogInformation("Current user's DateOfBirth claim ({dateOfBirth}) does not satisfy the minimum age authorization requirement {age}",
//                         dateOfBirthClaim.Value,
//                         requirement.Age);
//                 }
//             }
//             else
//             {
//                 _logger.LogInformation("No DateOfBirth claim present");
//             }

//             return Task.CompletedTask;
//         }
//     }


//  internal class CustomFunctionAuthorizeAttribute : AuthorizeAttribute
//     {
//         const string POLICY_PREFIX = "CustomFunctionAuth";

//         public CustomFunctionAuthorizeAttribute(string infoType) => InfoType = infoType;

//         // Get or set the Age property by manipulating the underlying Policy property
//         public string InfoType
//         {
//             get
//             {                
//                 if (string.TryParse(Policy.Substring(POLICY_PREFIX.Length), out var infoType))
//                 {
//                     return infoType;
//                 }
//                 return default("");
//             }
//             set
//             {
//                 Policy = $"{POLICY_PREFIX}{value.ToString()}";
//             }
//         }
//     }


//     internal class CustomFunctionAuthPolicyProvider : IAuthorizationPolicyProvider
//     {
//         const string POLICY_PREFIX = "CustomFunctionAuth";
//         public DefaultAuthorizationPolicyProvider FallbackPolicyProvider { get; }

//         public CustomFunctionAuthPolicyProvider(IOptions<AuthorizationOptions> options)
//         {            
//             FallbackPolicyProvider = new DefaultAuthorizationPolicyProvider(options);
//         }

//         public Task<AuthorizationPolicy> GetDefaultPolicyAsync() => FallbackPolicyProvider.GetDefaultPolicyAsync();
//         public Task<AuthorizationPolicy> GetPolicyAsync(string policyName)
//         {
//             if (policyName.StartsWith(POLICY_PREFIX, StringComparison.OrdinalIgnoreCase) &&
//                 int.TryParse(policyName.Substring(POLICY_PREFIX.Length), out var infoType))
//             {
//                 var policy = new AuthorizationPolicyBuilder();
//                 policy.AddRequirements(new CustomFunctionAuth(infoType));
//                 return Task.FromResult(policy.Build());
//             }
//             return FallbackPolicyProvider.GetPolicyAsync(policyName);
//         }
//     }

//     internal class CustomFunctionAuth : IAuthorizationRequirement
//     {
//         public string _infoType { get; private set; }

//         public CustomFunctionAuth(string infoType) { _infoType = infoType; }
//     }
    
public class CustomFunctionAuth : AuthorizationHandler<CustomFunctionAuth>, IAuthorizationRequirement
{      
      protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CustomFunctionAuth requirement)
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
            var functionSecurities= securityManager.SecurityCache(tenantId, userId,false).FunctionSecurity;  
            if(functionSecurities.Count==0)         
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

                 var controllerTypeInfo = descriptor.ControllerTypeInfo;
                 var headerAttr = controllerTypeInfo.GetCustomAttribute<AddHeaderFunction>();    
                 if(headerAttr==null)       
                 {
                    return Task.CompletedTask;   
                 }
                 var functionContext = headerAttr.GetHeaderFunction();  
                    if(string.IsNullOrEmpty(functionContext))
                    {                      
                       return Task.CompletedTask; 
                    }
                  
                    var itsSecurity=(from entitySecurity in functionSecurities where entitySecurity.FunctionContext== new Guid(functionContext) select entitySecurity).ToList();
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
         
            return Task.CompletedTask;
        }
    }

}