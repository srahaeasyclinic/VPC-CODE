

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using VPC.Entities.WorkFlow;
using VPC.Framework.Business.WorkFlow.APIs;
namespace VPC.Framework.Business.WorkFlow.Contracts
{
    public interface IManagerWorkFlowProcessTask
    {
        bool CreateWorkFlowProcessTasks(Guid tenantId, List<WorkFlowProcessTaskInfo> workFlowProcessTasks);
        WorkFlowProcessTaskInfo CreateWorkFlowProcessTask(Guid tenantId, WorkFlowProcessTaskInfo workFlowProcessTask);
        List<WorkFlowProcessTaskInfo> GetWorkFlowProcessTask(Guid tenantId, Guid workFlowId);
        List<WorkFlowProcessTaskInfo> GetWorkFlowProcessTask_ByInnerStepId(Guid tenantId, Guid innerStepId);
        List<WorkFlowProcessTaskInfo> GetWorkFlowProcessTask_ByProcessIds(Guid tenantId, List<Guid> processIds);

        List<WorkFlowProcessTaskInfo> GetWorkFlowProcessTaskByWorkFlowIds(Guid tenantId, List<Guid> workFlowIds);
        bool MoveUpDownWorkFlowProcessTask(Guid tenantId, List<WorkFlowProcessTaskInfo> workFlowProcessTasks);
        bool DeleteWorkFlowProcessTask(Guid tenantId, Guid innerStep);
    }

    public class ManagerWorkFlowProcessTask : IManagerWorkFlowProcessTask
    {
        private readonly IAdminWorkFlowProcessTask _adminWorkFlowProcessTask = new AdminWorkFlowProcessTask();
        private readonly IReviewWorkFlowProcessTask _reviewWorkFlowProcessTask = new ReviewWorkFlowProcessTask();

        WorkFlowProcessTaskInfo IManagerWorkFlowProcessTask.CreateWorkFlowProcessTask(Guid tenantId, WorkFlowProcessTaskInfo workFlowProcessTask)
        {
            //  _adminWorkFlowProcessTask.DeleteWorkFlowProcessTask(tenantId,innerStep);
            workFlowProcessTask.WorkFlowProcessTaskId = Guid.NewGuid();
            _adminWorkFlowProcessTask.CreateWorkFlowProcessTask(tenantId, workFlowProcessTask);
            return workFlowProcessTask;
        }

        bool IManagerWorkFlowProcessTask.CreateWorkFlowProcessTasks(Guid tenantId, List<WorkFlowProcessTaskInfo> workFlowProcessTasks)
        {
            return _adminWorkFlowProcessTask.CreateWorkFlowProcessTasks(tenantId, workFlowProcessTasks);
        }
        bool IManagerWorkFlowProcessTask.MoveUpDownWorkFlowProcessTask(Guid tenantId, List<WorkFlowProcessTaskInfo> workFlowProcessTasks)
        {
            return _adminWorkFlowProcessTask.MoveUpDownWorkFlowProcessTask(tenantId, workFlowProcessTasks);
        }

        List<WorkFlowProcessTaskInfo> IManagerWorkFlowProcessTask.GetWorkFlowProcessTask(Guid tenantId, Guid workFlowId)
        {
            return _reviewWorkFlowProcessTask.GetWorkFlowProcessTask(tenantId, workFlowId);
        }

        bool IManagerWorkFlowProcessTask.DeleteWorkFlowProcessTask(Guid tenantId, Guid innerStep)
        {
            return _adminWorkFlowProcessTask.DeleteWorkFlowProcessTask(tenantId, innerStep);
        }

        List<WorkFlowProcessTaskInfo> IManagerWorkFlowProcessTask.GetWorkFlowProcessTask_ByInnerStepId(Guid tenantId, Guid innerStepId)
        {
            return _reviewWorkFlowProcessTask.GetWorkFlowProcessTask_ByInnerStepId(tenantId, innerStepId);
        }

        List<WorkFlowProcessTaskInfo> IManagerWorkFlowProcessTask.GetWorkFlowProcessTask_ByProcessIds(Guid tenantId, List<Guid> processIds)
        {
            return _reviewWorkFlowProcessTask.GetWorkFlowProcessTask_ByProcessIds(tenantId, processIds);
        }

        List<WorkFlowProcessTaskInfo> IManagerWorkFlowProcessTask.GetWorkFlowProcessTaskByWorkFlowIds(Guid tenantId, List<Guid> workFlowIds)
        {
            return _reviewWorkFlowProcessTask.GetWorkFlowProcessTaskByWorkFlowIds(tenantId, workFlowIds);
        }

       
    }
}


