using System;
using System.Collections.Generic;
using System.Text;

namespace VPC.Metadata.Business.Entity.Trigger.Execution
{
    public class TriggerExecutionPayload
    {
        public Dictionary<string, string> PayloadObj { get; set; }
        public dynamic ConditionalValue { get; set; }
    }
}
