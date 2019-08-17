namespace VPC.Entities.WorkFlow
{
    public class WorkFlowModelAttribute : System.Attribute
    {
        public string Name { get; set; }
        public string Key { get; set; }
        public string Status { get; set; }
        public string Description{get;set;}
        public string Context { get; set; }
        public string TransitionLabelKey { get; set; }
        public string TransitionResourceValue { get; set; }
        public string StatusResourceValue { get; set; }
        
        
    }
}
