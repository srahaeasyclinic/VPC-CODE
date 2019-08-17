using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace VPC.Metadata.Business.Entity.Task.Execution {
    public class TaskExecutionPayload {
        public JObject Payload { get; set; }
        public Guid Id { get; set; }
        public Guid TenantId { get; set; }
        public string EntityName { get; set; }
        public Guid UserId { get; set; }
    }
}