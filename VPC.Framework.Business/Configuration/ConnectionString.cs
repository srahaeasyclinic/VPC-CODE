using System;
using System.Collections.Generic;
using VPC.Framework.Business.Exception;

namespace VPC.Framework.Business.Configuration
{ 
    public sealed class ConnectionString
    { 
        public static string GetConnectionString()
        {
        string connectionString = "data source=192.168.0.4\\MSSQLSERVER2017;initial catalog=1-VPC_Dev;integrated security=false;user id=ecom;password=ecom123;connect timeout=60;encrypt=false;trustservercertificate=false;applicationintent=readwrite;multisubnetfailover=false;pooling=false;enlist=false";
        //string connectionString = @"data source=13.71.21.206,6969\ECSQLEXPRESS;initial catalog=1-VPC_Test;integrated security=false;user id=Viewdba;password=4fZa27F5kUvxymtp;connect timeout=60;encrypt=false;trustservercertificate=false;applicationintent=readwrite;multisubnetfailover=false;pooling=false;enlist=false";
        return connectionString;
        }
    }
}