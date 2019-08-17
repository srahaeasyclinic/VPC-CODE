using System;
using System.Collections.Generic;

namespace VPC.Metadata.Business.Entity.CustomField.Execution
{
    public class CustomFieldExecutionPayload
    {
        public Dictionary<string, string> Payload { get; set; }
        public Guid Id { get; set; }

    }
}
