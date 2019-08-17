using System;
using VPC.Metadata.Business.Entity.CustomField.Execution;

namespace VPC.Framework.Business.EntityResourceManager.CustomFieldMethods
{
    public class BatchItemsClientQueued : ICustomServerFieldExecution
    {

        public CustomFieldExecutionMessage Execute(CustomFieldExecutionPayload payloadObj)
        {
           Random random = new Random ();
            var no = random.Next (2000);
            var message = new CustomFieldExecutionMessage {
                Message = no.ToString ()
            };
            return message;
        }
    }
}

