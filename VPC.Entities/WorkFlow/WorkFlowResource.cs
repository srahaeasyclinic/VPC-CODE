using System;

namespace VPC.Entities.WorkFlow
{
    public class WorkFlowResource
    {
        public Guid Id { get; set; }
        public string Value { get; set; }
        public string Key { get; set; }
        public string Status{get;set;}

        public string Description{get;set;}
        public bool Enabled { get; set; }
        public Guid? ProcessorCode { get; set; }
        public string TransitionLabelKey { get; set; }
        public WorkFlowProcessType ProcessType{get;set;}               
    }
}
