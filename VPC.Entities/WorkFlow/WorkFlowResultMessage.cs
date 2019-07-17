
namespace VPC.Entities.WorkFlow
{
    public class WorkFlowResultMessage<T>
    {
        public ErrorMessage Error { get; set; }
        public WarningMessage Warning { get; set; }
        public T Result { get; set; }
    }

   
}
