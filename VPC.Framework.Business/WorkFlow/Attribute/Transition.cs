using VPC.Entities.WorkFlow;

namespace VPC.Framework.Business.WorkFlow.Attribute
{
    [System.AttributeUsage(System.AttributeTargets.Class | System.AttributeTargets.Struct,AllowMultiple = true)  ] 
    class TransitionAttribute : System.Attribute
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Title { get; set; }
        public string Context { get; set; }
        public string Key { get; set; }
        public string Id { get; set; }
        public WorkFlowProcessType ProcessType{get;set;}
    }

}
