using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Formatters;
using System;
using System.Buffers;

namespace VPC.WebApi.Utility
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class CustomJsonFormatter : ActionFilterAttribute
    {
        private readonly string formatName = string.Empty;
        public CustomJsonFormatter(string _formatName)
        {
            formatName = _formatName;
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (context == null || context.Result == null || context.Result.GetType() != typeof(Microsoft.AspNetCore.Mvc.OkObjectResult))
            {
                return;
            }

            var settings = JsonSerializerSettingsProvider.CreateSerializerSettings();

            if (formatName == "camel")
            {
                settings.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();
            }
            else
            {
                settings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver();
            }

            var formatter = new JsonOutputFormatter(settings, ArrayPool<Char>.Shared);

            (context.Result as Microsoft.AspNetCore.Mvc.OkObjectResult).Formatters.Add(formatter);
        }
    }
}
