using System;
using  System.Runtime.Serialization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace VPC.WebApi.Utility
{
public interface IJsonMessage
{
    JsonResult IgnoreNullableObject (Object result);
    JsonResult ConvertCamelCase (Object result);
}
    public class JsonMessage : IJsonMessage
    {
        public JsonResult IgnoreNullableObject(object result)
        {
            var settings = new JsonSerializerSettings();
            settings.NullValueHandling = NullValueHandling.Ignore;
            settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            var jsonResult = new JsonResult(result, settings);
            jsonResult.ContentType ="application/json";
            return jsonResult;
        }
        public JsonResult ConvertCamelCase(object result)
        {
            var settings = new JsonSerializerSettings();
            settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            var jsonResult = new JsonResult(result, settings);
            jsonResult.ContentType ="application/json";
            return jsonResult;
        }
    }
}

