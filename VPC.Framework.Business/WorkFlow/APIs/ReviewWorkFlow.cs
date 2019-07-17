using System;
using System.Collections.Generic;
using System.Linq;
using VPC.Entities.WorkFlow;
using VPC.Framework.Business.WorkFlow.Data;

namespace VPC.Framework.Business.WorkFlow.APIs
{
    public interface IReviewWorkFlow
     {
        WorkFlowInfo GetWorkFlow(Guid tenantId, string entityId,string subTypeCode );  
        List<WorkFlowInfo> GetWorkFlows(Guid tenantId, string entityId); 
        List<WorkFlowInfo> GetWorkFlowsByIds(Guid tenantId,  List<Guid> workFlowIds);
        List<WorkFlowInfo> GetWorkFlowsByEntityIds(Guid tenantId, List<string> entityIds);
     }
    internal class ReviewWorkFlow : IReviewWorkFlow
    {
        private readonly DataWorkFlow _dataWorkFlow = new DataWorkFlow();
        WorkFlowInfo IReviewWorkFlow.GetWorkFlow(Guid tenantId, string entityId, string subTypeCode)
        {
           return _dataWorkFlow.GetWorkFlow(tenantId,entityId,subTypeCode);
        }

        List<WorkFlowInfo> IReviewWorkFlow.GetWorkFlows(Guid tenantId, string entityId)
        {
            return _dataWorkFlow.GetWorkFlows(tenantId,entityId);
        }

        List<WorkFlowInfo> IReviewWorkFlow.GetWorkFlowsByEntityIds(Guid tenantId, List<string> entityIds)
        {
            return _dataWorkFlow.GetWorkFlowsByEntityIds(tenantId,entityIds);
        }

        List<WorkFlowInfo> IReviewWorkFlow.GetWorkFlowsByIds(Guid tenantId,  List<Guid> workFlowIds)
        {
          return _dataWorkFlow.GetWorkFlowsByIds(tenantId, workFlowIds);
        }
    }
}