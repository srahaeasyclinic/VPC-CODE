using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using VPC.Entities.EntityCore.Model.Setting;
using VPC.Framework.Business.Common;
using VPC.Framework.Business.DynamicQueryManager.Contracts;
using VPC.Framework.Business.SettingsManager.APIs;
using VPC.Metadata.Business.Entity.Task.Execution;

namespace VPC.Framework.Business.Task.Contracts.Methods {

    public class ProductVersionCheckout : ITaskExecution {

        public TaskExecutionMessage Execute (TaskExecutionPayload payloadObj) {
            IEntityQueryManager entityQueryManager = new EntityQueryManager ();
            var draftId = entityQueryManager.GetSpecificIdByQuery (payloadObj.TenantId, "Product", payloadObj.Id, "DraftVersion");
             var executionMessage = new TaskExecutionMessage ();
                

            if(draftId!=null){
                executionMessage.Message = TaskExecutionCode.AlreadyAdded;
                return executionMessage;
            }
            var versionId = entityQueryManager.GetSpecificIdByQuery (payloadObj.TenantId, "Product", payloadObj.Id, "ActiveVersion");
            var cloneId = entityQueryManager.SelectInsert ("ProductVersion", payloadObj.TenantId, versionId, Guid.Empty);
            if (cloneId != Guid.Empty) {
                var updateStatus = entityQueryManager.UpdateSpecificField (payloadObj.TenantId, "Product", payloadObj.Id, "DraftVersion", cloneId.ToString ());
              
                executionMessage.Message = TaskExecutionCode.Redirect;
                executionMessage.Id = cloneId;
                return executionMessage;
            }
            return null;
        }
    }
}