using System;

namespace VPC.Entities.WorkFlow
{
    public class OperationWapper
    {
         public string SubTypeName{get;set;}
        public string EntityName{get;set;}   
        public Guid CurrentTransitionType{get;set;}     
        public WorkFlowOperationType OperationType { get; set; }
        public dynamic Data { get; set; }
    }
}
