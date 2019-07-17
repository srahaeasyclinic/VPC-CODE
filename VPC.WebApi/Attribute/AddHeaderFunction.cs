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

public class AddHeaderFunction : ResultFilterAttribute
    {
        private readonly string _functionContext;
        public AddHeaderFunction(string functionContext)
        {
            _functionContext = functionContext;        
        }

        public string GetHeaderFunction()
        {
            return _functionContext;
        }

        public override void OnResultExecuting(ResultExecutingContext context)
        {
            context.HttpContext.Response.Headers.Add(  _functionContext, new string[] { string.Empty });
            base.OnResultExecuting(context);
        }
    }
}