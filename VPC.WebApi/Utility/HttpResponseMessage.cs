using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
//using System.Web.Http.Filters;
//using EightyTwentyApi.Filter;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace VPC.WebApi.Utility
{
    public interface IHttpResponseMessage
    {
        HttpResponseMessage ReturnBadRequest(string result);
        HttpResponseMessage ReturnBadRequest(Object result);
        HttpResponseMessage ReturnUnAuthorized(string result);
        HttpResponseMessage ReturnUnAuthorized(Object result);
        HttpResponseMessage ReturnNotFound(string result);
        HttpResponseMessage ReturnNotFound(Object result);
        HttpResponseMessage ReturnOk(string result);
        HttpResponseMessage ReturnOk(Object result);

        HttpResponseMessage ReturnOkIgnoreNullable(Object result);

        HttpResponseMessage ReturnInternalServerError(string result);
        HttpResponseMessage ReturnInternalServerError(Object result);
        HttpResponseMessage ReturnCreated(string result);
        HttpResponseMessage ReturnCreated(Object result);
        HttpResponseMessage ReturnGone(string result);
        HttpResponseMessage ReturnGone(Object result);

        HttpResponseMessage ReturnConflict(string result);
        HttpResponseMessage ReturnConflict(Object result);
    }

    public class BasicHttpResponseMessage : IHttpResponseMessage
    {
        HttpResponseMessage IHttpResponseMessage.ReturnBadRequest(string result)
        {
            var response = new HttpResponseMessage(HttpStatusCode.BadRequest)
            {
                Content = new ObjectContent<Object>(new { message = result }, new JsonMediaTypeFormatter(), "application/json")
            };
            return response;
        }

        HttpResponseMessage IHttpResponseMessage.ReturnUnAuthorized(string result)
        {
            var response = new HttpResponseMessage(HttpStatusCode.Unauthorized)
            {
                Content = new StringContent(result, Encoding.UTF8, "text/plain")
            };
            return response;
        }

        HttpResponseMessage IHttpResponseMessage.ReturnNotFound(string result)
        {
            var response = new HttpResponseMessage(HttpStatusCode.NotFound)
            {
                Content = new StringContent(result, Encoding.UTF8, "text/plain")
            };
            return response;
        }

        HttpResponseMessage IHttpResponseMessage.ReturnOk(string result)
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(result, Encoding.UTF8, "text/plain")
            };
            return response;
        }

        public HttpResponseMessage ReturnOk(Object result)
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ObjectContent<Object>(result, new JsonMediaTypeFormatter()
                {
                    SerializerSettings = new JsonSerializerSettings
                    {
                        ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    }
                }, "application/json")
            };
            return response;
        }
        public HttpResponseMessage ReturnOkIgnoreNullable(Object result)
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ObjectContent<Object>(result, new JsonMediaTypeFormatter()
                {
                    SerializerSettings = new JsonSerializerSettings
                    {
                        ContractResolver = new CamelCasePropertyNamesContractResolver(),
                        NullValueHandling = NullValueHandling.Ignore,
                        DefaultValueHandling = DefaultValueHandling.Ignore,
                    }
                }, "application/json")
            };
            return response;
        }


        


        HttpResponseMessage IHttpResponseMessage.ReturnInternalServerError(string result)
        {
            var response = new HttpResponseMessage(HttpStatusCode.InternalServerError)
            {
                Content = new ObjectContent<Object>(new { message = result }, new JsonMediaTypeFormatter()
                {
                    SerializerSettings = new JsonSerializerSettings
                    {
                        ContractResolver = new CamelCasePropertyNamesContractResolver()
                    }
                }, "application/json")
            };
            return response;
        }

        public HttpResponseMessage ReturnInternalServerError(Object result)
        {
            var response = new HttpResponseMessage(HttpStatusCode.InternalServerError)
            {
                Content = new ObjectContent<Object>(result, new JsonMediaTypeFormatter()
                {
                    SerializerSettings = new JsonSerializerSettings
                    {
                        ContractResolver = new CamelCasePropertyNamesContractResolver()
                    }
                }, "application/json")
            };
            return response;
        }

        HttpResponseMessage IHttpResponseMessage.ReturnCreated(string result)
        {
            var response = new HttpResponseMessage(HttpStatusCode.Created)
            {
                Content = new StringContent(result, Encoding.UTF8, "text/plain")
            };
            return response;
        }

        public HttpResponseMessage ReturnCreated(Object result)
        {
            var response = new HttpResponseMessage(HttpStatusCode.Created)
            {
                Content = new ObjectContent<Object>(result, new JsonMediaTypeFormatter(), "application/json")
            };
            return response;
        }

        HttpResponseMessage IHttpResponseMessage.ReturnBadRequest(Object result)
        {
            var response = new HttpResponseMessage(HttpStatusCode.BadRequest)
            {
                Content = new ObjectContent<Object>(result, new JsonMediaTypeFormatter(), "application/json")
            };
            return response;
        }

        HttpResponseMessage IHttpResponseMessage.ReturnUnAuthorized(Object result)
        {
            var response = new HttpResponseMessage(HttpStatusCode.Unauthorized)
            {
                Content = new ObjectContent<Object>(result, new JsonMediaTypeFormatter(), "application/json")
            };
            return response;
        }

        HttpResponseMessage IHttpResponseMessage.ReturnNotFound(Object result)
        {
            var response = new HttpResponseMessage(HttpStatusCode.NotFound)
            {
                Content = new ObjectContent<Object>(result, new JsonMediaTypeFormatter(), "application/json")
            };
            return response;
        }



        HttpResponseMessage IHttpResponseMessage.ReturnGone(string result)
        {
            var response = new HttpResponseMessage(HttpStatusCode.Gone)
            {
                Content = new StringContent(result, Encoding.UTF8, "text/plain")
            };
            return response;
        }

        HttpResponseMessage IHttpResponseMessage.ReturnGone(object result)
        {
            var response = new HttpResponseMessage(HttpStatusCode.Gone)
            {
                Content = new ObjectContent<Object>(result, new JsonMediaTypeFormatter(), "application/json")
            };
            return response;
        }

        HttpResponseMessage IHttpResponseMessage.ReturnConflict(string result)
        {
            var response = new HttpResponseMessage(HttpStatusCode.Conflict)
            {
                Content = new ObjectContent<Object>(new { message = result }, new JsonMediaTypeFormatter(), "application/json")
            };
            return response;
        }

        HttpResponseMessage IHttpResponseMessage.ReturnConflict(object result)
        {
            var response = new HttpResponseMessage(HttpStatusCode.Conflict)
            {
                Content = new ObjectContent<Object>(result, new JsonMediaTypeFormatter(), "application/json")
            };
            return response;
        }
    }
}