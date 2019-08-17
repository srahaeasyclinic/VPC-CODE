

namespace VPC.Metadata.Business.Entity.Task.Execution
{
    public interface ITaskExecution
    {
        TaskExecutionMessage Execute(TaskExecutionPayload payloadObj);
    }
}