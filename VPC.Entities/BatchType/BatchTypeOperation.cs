
using System;
using System.ComponentModel;
using VPC.Entities.Common.Functions;
using VPC.Entities.EntityCore;

namespace VPC.Entities.BatchType
{
  [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class BatchTypeAttribute : Attribute
    {
        public BatchTypeAttribute(string batchName, string context,int batchType )
        {
            BatchName = batchName;
            Context = context;
            BatchType=batchType;
        }
        public string BatchName { get; }
        public string Context { get; }
        public int BatchType{get;}
    }

}
