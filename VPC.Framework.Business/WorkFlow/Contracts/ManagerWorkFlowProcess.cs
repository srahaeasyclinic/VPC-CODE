

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Transactions; 
using VPC.Entities.WorkFlow;
using VPC.Framework.Business.WorkFlow.APIs;

namespace VPC.Framework.Business.WorkFlow.Contracts
{
public interface IManagerWorkFlowProcess
{
    bool CreateWorkFlowProcess(Guid tenantId, List<WorkFlowProcessInfo> workFlowSteps);      
    List<WorkFlowProcessInfo> GetWorkFlowProcess(Guid tenantId, Guid workFlowId); 
    List<WorkFlowProcessInfo> GetWorkFlowProcessByOperationOrTransitionIds(Guid tenantId, List<Guid> operationOrTransactionIds);  
    List<WorkFlowProcessInfo> GetWorkFlowProcessByWorkFlowIds(Guid tenantId, List<Guid> workFlowIds);  
}

public class ManagerWorkFlowProcess : IManagerWorkFlowProcess
{       
        private readonly IAdminWorkFlowProcess _adminWorkFlowProcess=new AdminWorkFlowProcess();
        private readonly IReviewWorkFlowProcess _reviewWorkFlowProcess=new ReviewWorkFlowProcess();
        
       
        bool IManagerWorkFlowProcess.CreateWorkFlowProcess(Guid tenantId, List<WorkFlowProcessInfo> workFlowSteps)
        {
           return _adminWorkFlowProcess.CreateWorkFlowProcess (tenantId,workFlowSteps);
        }

        List<WorkFlowProcessInfo> IManagerWorkFlowProcess.GetWorkFlowProcess(Guid tenantId, Guid workFlowId)
        {
            return _reviewWorkFlowProcess.GetWorkFlowProcess (tenantId,workFlowId);
        }

        List<WorkFlowProcessInfo> IManagerWorkFlowProcess.GetWorkFlowProcessByOperationOrTransitionIds(Guid tenantId, List<Guid> operationOrTransactionIds)
        {
             return _reviewWorkFlowProcess.GetWorkFlowProcessByOperationOrTransitionIds(tenantId,operationOrTransactionIds); 
        }

        List<WorkFlowProcessInfo> IManagerWorkFlowProcess.GetWorkFlowProcessByWorkFlowIds(Guid tenantId, List<Guid> workFlowIds)
        {
            return _reviewWorkFlowProcess.GetWorkFlowProcessByWorkFlowIds(tenantId,workFlowIds); 
        }
    }
}
  

