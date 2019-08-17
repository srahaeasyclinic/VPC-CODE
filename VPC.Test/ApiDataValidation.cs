using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using NUnit.Framework;
using Microsoft.AspNetCore.Mvc;
using VPC.WebApi;
using Microsoft.AspNetCore.Http;
using VPC.WebApi.Controllers.EntityController;
using Microsoft.AspNetCore.Mvc.Infrastructure;
namespace VPC.Test
{
    public class MethodsInfo
    {
        public string Controllers { get; set; }
        public string Action { get; set; }
        public string ReturnType { get; set; }
        public List<string> Attributes { get; set; }
    }
    [Category("API data validation")]
    class ApiDataValidation
    {
        List<MethodsInfo> methods;
        [SetUp]
        public void Setup()
        {
             methods = new List<MethodsInfo>();
             Assembly assembly = Assembly.Load("VPC.WebApi");

             var  methodList = assembly.GetTypes()
            .Where(type => typeof(Microsoft.AspNetCore.Mvc.Controller).IsAssignableFrom(type))
            .SelectMany(type => type.GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public))
            .Where(m => !m.GetCustomAttributes(typeof(System.Runtime.CompilerServices.CompilerGeneratedAttribute), true).Any())
            .Select(x => new { Controller = x.DeclaringType.Name, Action = x.Name, ReturnType = x.ReturnType.Name, Attributes = String.Join(",", x.GetCustomAttributes().Select(a => a.GetType().Name.Replace("Attribute", ""))) })
            .OrderBy(x => x.Controller).ThenBy(x => x.Action).ToList();

            foreach (var methodInfo in methodList)
            {
                var attributeName = methodInfo.Attributes.Split(',').ToList();
                methods.Add(new MethodsInfo
                {
                    Controllers = methodInfo.Controller,
                    Action = methodInfo.Action,
                    ReturnType = methodInfo.ReturnType,
                    Attributes = attributeName
                });

            }
        }
        [Test]
        public void EmailValue_Validate_Format_ThrowInvalidEmailFormatException()
        {
            var sb = new StringBuilder();
            foreach (var type in methods)
            {
                foreach (var attributes in type.Attributes)
                {
                    if(attributes == "HttpPut" || attributes == "HttpPost" && attributes != "ValidateJObjectdata")
                    {
                         sb.AppendLine(String.Format("{0}, {1}", type.Controllers, type.Action));
                    }
                }
            }

            Assert.That(sb.ToString(), Is.Empty);
        }

        [Test]
        public void EmailValue_Validate_Format_Success()
        {
            var sb = new StringBuilder();
            foreach (var type in methods)
            {
                foreach (var attributes in type.Attributes)
                {
                    if (attributes == "HttpPut" || attributes == "HttpPost" && attributes == "ValidateJObjectdata")
                    {
                        sb.AppendLine(String.Format("{0}, {1}", type.Controllers, type.Action));
                    }
                }
            }
            Assert.That(sb.ToString(), Is.Not.Empty);
        }
    }
}
