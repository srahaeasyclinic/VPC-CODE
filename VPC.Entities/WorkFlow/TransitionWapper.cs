using System;

namespace VPC.Entities.WorkFlow
{
    public class TransitionWapper
    {
        public Guid StepId { get; set; }
        public Guid RefId { get; set; }
        public string SubTypeName{get;set;}
        public string EntityName{get;set;}
        public Guid CurrentTransitionType{get;set;} 
        public Guid NextTransitionType{get;set;}

        public Guid UserId{get;set;}
        public dynamic ObjectData { get; set; }
    }
}
