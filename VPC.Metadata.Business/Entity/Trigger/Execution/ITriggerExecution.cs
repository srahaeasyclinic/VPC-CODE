using System;
using System.Collections.Generic;
using System.Text;

namespace VPC.Metadata.Business.Entity.Trigger.Execution
{
    public interface ITriggerExecution
    {
        TriggerExecutionMessage ExecuteTrigger(TriggerExecutionPayload payload);
    }
}