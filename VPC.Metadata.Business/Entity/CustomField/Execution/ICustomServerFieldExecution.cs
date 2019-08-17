

namespace VPC.Metadata.Business.Entity.CustomField.Execution
{
    public interface ICustomServerFieldExecution
    {
        CustomFieldExecutionMessage Execute(CustomFieldExecutionPayload payloadObj);
    }
}