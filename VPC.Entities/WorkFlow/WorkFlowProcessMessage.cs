namespace VPC.Entities.WorkFlow
{
    public class WorkFlowProcessMessage : WorkFlowProcessMessageExtra
    {
        public bool Success { get; set; }
        public WarningMessage WarningMessage { get; set; }
        public ErrorMessage ErrorMessage { get; set; }
        public ObjectMessage ObjectMessage { get; set; }
        public dynamic Data { get; set; }
    }
}
