

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Transactions; 
using VPC.Entities.WorkFlow;
using VPC.Framework.Business.WorkFlow.APIs;

namespace VPC.Framework.Business.WorkFlow.Contracts
{
public interface IManagerWorkFlowOperation
{
     bool CreateWorkFlowOperations(Guid tenantId, List<WorkFlowOperationInfo> workFlowOperations); 
     List<WorkFlowOperationInfo> GetWorkFlowOperations(Guid tenantId, Guid workFlowId)  ; 
     List<WorkFlowOperationInfo> GetWorkFlowOperationsByWorkFlowIds(Guid tenantId, List<Guid> workFlowIds);    
}

public class ManagerWorkFlowOperation : IManagerWorkFlowOperation
{       
        private readonly IAdminWorkFlowOperation _adminWorkFlowOperation=new AdminWorkFlowOperation();
        private readonly IReviewWorkFlowOperation _reviewWorkFlowOperation=new ReviewWorkFlowOperation();
       
        bool IManagerWorkFlowOperation.CreateWorkFlowOperations(Guid tenantId, List<WorkFlowOperationInfo> workFlowOperations)
        {
          return _adminWorkFlowOperation.CreateWorkFlowOperations(tenantId,workFlowOperations);
        }

        List<WorkFlowOperationInfo> IManagerWorkFlowOperation.GetWorkFlowOperations(Guid tenantId, Guid workFlowId)
        {
           return _reviewWorkFlowOperation.GetWorkFlowOperations(tenantId,workFlowId); 
        }

        List<WorkFlowOperationInfo> IManagerWorkFlowOperation.GetWorkFlowOperationsByWorkFlowIds(Guid tenantId, List<Guid> workFlowIds)
        {
             return _reviewWorkFlowOperation.GetWorkFlowOperationsByWorkFlowIds(tenantId,workFlowIds);   
        }
    }
}
  

