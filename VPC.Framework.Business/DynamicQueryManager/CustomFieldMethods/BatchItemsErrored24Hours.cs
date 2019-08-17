using System;
using VPC.Entities.BatchType;
using VPC.Entities.WorkFlow.Engine.Email;
using VPC.Framework.Business.BatchItems.APIs;
using VPC.Metadata.Business.Entity.CustomField.Execution;

namespace VPC.Framework.Business.EntityResourceManager.CustomFieldMethods
{
    public class BatchItemsErrored24Hours : ICustomServerFieldExecution
    {
        private readonly IManagerBatchItem _managerNacthItem=new ManagerBatchItem();
        public CustomFieldExecutionMessage Execute(CustomFieldExecutionPayload payloadObj)
        {
             var tenantId=new Guid("E15516DF-CFAB-4597-97D2-0BE5F8E16734");            
            var queuedCount=_managerNacthItem.BatchItemByStatus(tenantId,payloadObj.Id,BatchItemTypeEnum.Errored24hours);
            var message = new CustomFieldExecutionMessage {
                Message = queuedCount.ToString ()
            };
            return message;
        }
    }
}

