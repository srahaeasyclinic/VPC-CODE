
using System;
using System.ComponentModel;
using VPC.Entities.Common.Functions;
using VPC.Entities.EntityCore;

namespace VPC.Entities.BatchType
{
  [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class BatchTypeAttribute : Attribute
    {
        public BatchTypeAttribute(int batchType )
        {           
            BatchType=batchType;
        }       
        public int BatchType{get;}
    }

}
