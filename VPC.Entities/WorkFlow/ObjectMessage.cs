namespace VPC.Entities.WorkFlow
{
    public class ObjectMessage
    {
        public WorkFlowMessage Code { get; set; }
        public string Description { get; set; }
        public dynamic ObjectData { get; set; }
    }
}
