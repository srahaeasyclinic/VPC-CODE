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
    public interface IReviewWorkFlowProcessTask
    {
       List<WorkFlowProcessTaskInfo> GetWorkFlowProcessTask(Guid tenantId, Guid workFlowProcessId); 
       List<WorkFlowProcessTaskInfo> GetWorkFlowProcessTask_ByInnerStepId(Guid tenantId, Guid innerStepId);      
       List<WorkFlowProcessTaskInfo> GetWorkFlowProcessTask_ByProcessIds(Guid tenantId, List<Guid> processIds);
       List<WorkFlowProcessTaskInfo> GetWorkFlowProcessTaskByWorkFlowIds(Guid tenantId, List<Guid> workFlowIds); 
    }
    internal class ReviewWorkFlowProcessTask : IReviewWorkFlowProcessTask
    {
        private readonly DataWorkFlowProcessTask _dataWorkFlowProcessTask = new DataWorkFlowProcessTask();

        List<WorkFlowProcessTaskInfo> IReviewWorkFlowProcessTask.GetWorkFlowProcessTask(Guid tenantId, Guid workFlowId)
        {
            return _dataWorkFlowProcessTask.GetWorkFlowProcessTask(tenantId,workFlowId);
        }

        List<WorkFlowProcessTaskInfo> IReviewWorkFlowProcessTask.GetWorkFlowProcessTask_ByInnerStepId(Guid tenantId, Guid innerStepId)
        {
         return _dataWorkFlowProcessTask.GetWorkFlowProcessTask_ByInnerStepId(tenantId,innerStepId);
        }

        List<WorkFlowProcessTaskInfo> IReviewWorkFlowProcessTask.GetWorkFlowProcessTask_ByProcessIds(Guid tenantId, List<Guid> processIds)
        {
            return _dataWorkFlowProcessTask.GetWorkFlowProcessTask_ByProcessIds(tenantId,processIds);
        }

        List<WorkFlowProcessTaskInfo> IReviewWorkFlowProcessTask.GetWorkFlowProcessTaskByWorkFlowIds(Guid tenantId, List<Guid> workFlowIds)
        {
          return _dataWorkFlowProcessTask.GetWorkFlowProcessTaskByWorkFlowIds(tenantId,workFlowIds);
        }
    }
}