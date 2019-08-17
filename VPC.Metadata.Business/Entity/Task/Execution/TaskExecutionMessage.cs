using System;

namespace VPC.Metadata.Business.Entity.Task.Execution {
    public class TaskExecutionMessage {
        public Guid Id { get; set; }
        public TaskExecutionCode Message { get; set; }
    }

    public enum TaskExecutionCode{
        Success = 200,
        Redirect = 308,

        AlreadyAdded = 409
    }
}