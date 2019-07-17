
using System;
using System.ComponentModel;
using VPC.Entities.Common.Functions;
using VPC.Entities.EntityCore;

namespace VPC.Entities.BatchType
{
  
    public class BatchTypeReturnMessage 
    {
        public bool Success { get;set; }
        public bool  NoDataFound { get;set; }
        public string Message{get;set;}
    }

}
