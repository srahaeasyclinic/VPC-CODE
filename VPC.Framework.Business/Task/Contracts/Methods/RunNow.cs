using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using VPC.Entities.EntityCore.Model.Setting;
using VPC.Framework.Business.BatchItems.APIs;
using VPC.Framework.Business.Common;
using VPC.Framework.Business.DynamicQueryManager.Contracts;
using VPC.Framework.Business.SettingsManager.APIs;
using VPC.Metadata.Business.Entity.Task.Execution;

namespace VPC.Framework.Business.Task.Contracts.Methods {

    public class RunNow : ITaskExecution {

        public TaskExecutionMessage Execute (TaskExecutionPayload payloadObj) {

            IManagerBatchItem batchItem = new ManagerBatchItem ();
            var status = batchItem.BatchItemUpdateNextRunTime (payloadObj.TenantId, payloadObj.Id);
            var executionMessage = new TaskExecutionMessage ();
            executionMessage.Message = TaskExecutionCode.Success;
            return executionMessage;
        }
    }
}