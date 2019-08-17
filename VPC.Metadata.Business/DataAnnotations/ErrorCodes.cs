using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace VPC.Metadata.Business.Entity.Infrastructure
{
    public static class ErrorType
    {

        public const string
            Validation = "Validation",
            Functional = "Functional",
            Operational = "Operational",
            System = "System",
            Syntax = "Syntax";



    }
    public enum ErrorCodeEnum 
    {
        #region HTTP Status Code Errors

        [Description("Continue")]
        Continue = 100,
        [Description("Switching Protocols")]
        Switching_Protocols = 101,
        [Description("Ok")]
        Ok = 200,
        [Description("Created")]
        Created = 201,
        [Description("Accepted")]
        Accepted = 202,
        [Description("Non-Authoritative Information")]
        Non_Authoritative_Information = 203,       
        [Description("No Content")]
        No_Content = 204,       
        [Description("Reset Content")]
        Reset_Content = 205,
        [Description("Partial Content")]
        Partial_Content = 206,
        [Description("Multiple Choices")]
        Multiple_Choices = 300,
        [Description("Moved Permanently")]
        Moved_Permanently = 301,
        [Description("Found")]
        Found = 302,
        [Description("See Other")]
        See_Other = 303,
        [Description("Not Modified")]
        Not_Modified = 304,
        [Description("Use Proxy")]
        Use_Proxy = 305,
        [Description("Temporary Redirect")]
        Temporary_Redirect  = 307,
        [Description("Bad Request")]
        Bad_Request = 400,
        [Description("Unauthorized")]
        Unauthorized = 401,
        [Description("Payment Required")]
        Payment_Required = 402,
        [Description("Forbidden")]
        Forbidden = 403,
        [Description("Not Found")]
        Not_Found = 404,
        [Description("Method Not Allowed")]
        Method_Not_Allowed = 405,
        [Description("Not Acceptable")]
        Not_Acceptable = 406,
        [Description("Proxy Authentication Required")]
        Proxy_Authentication_Required = 407,
        [Description("Request Timeout")]
        Request_Timeout = 408,
        [Description("Conflict")]
        Conflict = 409,
        [Description("Gone")]
        Gone = 410,
        [Description("Length Required")]
        Length_Required = 411,
        [Description("Precondition Failed")]
        Precondition_Failed = 412,
        [Description("Payload Too Large")]
        Payload_Too_Large = 413,
        [Description("URI Too Long")]
        URI_Too_Long = 414,
        [Description("Unsupported Media Type")]
        Unsupported_Media_Type  = 415,
        [Description("Range Not Satisfiable")]
        Range_Not_Satisfiable = 416,
        [Description("Expectation Failed")]
        Expectation_Failed = 417,
        [Description("Upgrade Required")]
        Upgrade_Required = 426,
        [Description("Internal Server Error ")]
        Internal_Server_Error = 500,
        [Description("Not Implemented")]
        Not_Implemented = 501,
        [Description("Bad Gateway")]
        Bad_Gateway = 502,
        [Description("Service Unavailable")]
        Service_Unavailable  = 503,
        [Description("Gateway Timeout")]
        Gateway_Timeout = 504,
        [Description("HTTP Version Not Supported")]
        HTTP_Version_Not_Supported = 505,

        [Description("Unknown Error")]
        Unknown_Error = 520,

        #endregion

        #region Application Errors

        [Description("Required")]
        Required = 1001,
        [Description("Duplicate data exists")]
        Duplicate_Data = 1002,
        [Description("Max length exceeded")]
        Data_Length = 1003,
        [Description("Invalid credential")]
        Invalid_Credential = 1004,
        [Description("Server error occured")]
        Server_Error = 1005,
        [Description("Field datatype not matched")]
        Invalid_Datatype = 1006,
        [Description("Invalid email")]
        Invalid_Email = 1007,
        [Description("Server Connection Failed")]
        Invalid_Connection = 0,

        #endregion
    }



}
