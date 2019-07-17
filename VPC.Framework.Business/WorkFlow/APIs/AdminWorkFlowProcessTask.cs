using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Transactions; 
using VPC.Entities.WorkFlow;
using VPC.Framework.Business.WorkFlow.Data;

namespace VPC.Framework.Business.WorkFlow.APIs
{
    public interface IAdminWorkFlowProcessTask
    {
        bool CreateWorkFlowProcessTasks(Guid tenantId, List<WorkFlowProcessTaskInfo> workFlowProcessTasks);
       bool CreateWorkFlowProcessTask(Guid tenantId, WorkFlowProcessTaskInfo workFlowProcessTask);
       bool DeleteWorkFlowProcessTask(Guid tenantId,Guid innerStep); 
       bool MoveUpDownWorkFlowProcessTask(Guid tenantId,  List<WorkFlowProcessTaskInfo> workFlowProcessTasks);
    }
    internal class AdminWorkFlowProcessTask : IAdminWorkFlowProcessTask
    {
        private readonly DataWorkFlowProcessTask _dataWorkFlowProcessTask = new DataWorkFlowProcessTask();

        bool IAdminWorkFlowProcessTask.CreateWorkFlowProcessTask(Guid tenantId, WorkFlowProcessTaskInfo workFlowProcessTask)
        {
          return _dataWorkFlowProcessTask.CreateWorkFlowProcessTask(tenantId,workFlowProcessTask);
        }

        bool IAdminWorkFlowProcessTask.CreateWorkFlowProcessTasks(Guid tenantId, List<WorkFlowProcessTaskInfo> workFlowProcessTasks)
        {
            return _dataWorkFlowProcessTask.CreateWorkFlowProcessTasks(tenantId,workFlowProcessTasks);
        }

        bool IAdminWorkFlowProcessTask.DeleteWorkFlowProcessTask(Guid tenantId, Guid innerStep)
        {
            return _dataWorkFlowProcessTask.DeleteWorkFlowProcessTask(tenantId,innerStep);
        }

        bool IAdminWorkFlowProcessTask.MoveUpDownWorkFlowProcessTask(Guid tenantId, List<WorkFlowProcessTaskInfo> workFlowProcessTasks)
        {
            return _dataWorkFlowProcessTask.MoveUpDownWorkFlowProcessTask(tenantId,workFlowProcessTasks); 
        }
    }
}