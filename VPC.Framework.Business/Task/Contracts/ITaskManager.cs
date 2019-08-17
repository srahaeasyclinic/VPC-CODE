using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using VPC.Entities.EntityCore.Model.Setting;
using VPC.Framework.Business.Common;
using VPC.Framework.Business.SettingsManager.APIs;
using VPC.Framework.Business.Task.Contracts.Methods;
using VPC.Metadata.Business.Entity.Task;
using VPC.Metadata.Business.Entity.Task.Execution;

namespace VPC.Framework.Business.Task.Contracts {
    public interface ITaskManager {
        TaskExecutionMessage ExecuteTaskById (Guid tenantId, Guid entityId, string entityName, Guid userId, string taskName, JObject payload);
        TaskExecutionMessage ExecuteTask (Guid tenantCode, string entityName, Guid userId, string taskName, JObject payload);
    }
    
    public class TaskManager : ITaskManager {
        TaskExecutionMessage ITaskManager.ExecuteTask (Guid tenantCode, string entityName, Guid userId, string taskName, JObject payload) {
            throw new NotImplementedException ();
        }

        //need to create a layer from this portion....
        TaskExecutionMessage ITaskManager.ExecuteTaskById (Guid tenantId, Guid entityId, string entityName, Guid userId, string taskName, JObject payload) {
            var taskEngine = new TaskEngine ();
            var executionPayload = new TaskExecutionPayload {
                Payload = payload,
                TenantId = tenantId,
                Id = entityId,
                EntityName = entityName,
                UserId = userId
            };
            var value = taskEngine.GetValue (taskName, executionPayload);
            return value;
        }
    }
}