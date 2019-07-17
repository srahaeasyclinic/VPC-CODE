
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using VPC.Entities.WorkFlow;
using VPC.Framework.Business.EntityResourceManager.Contracts;
using VPC.Framework.Business.MetadataManager.Contracts;
using VPC.Framework.Business.WorkFlow.APIs;
using VPC.Framework.Business.WorkFlow.Contracts;

namespace VPC.Framework.Business.WorkFlow.Attribute
{
    public class TransitionFlowEngine : ITransitionFlowEngine
    {
        IManagerWorkFlowTransition transactionHistory=new ManagerWorkFlowTransition();
        private readonly IManagerWorkFlowInnerStep _managerWorkFlowInnerStep = new ManagerWorkFlowInnerStep();
        private readonly IManagerWorkFlowProcess _managerWorkFlowProcess = new ManagerWorkFlowProcess();
        private readonly IManagerWorkFlowProcessTask _managerWorkFlowProcessTask = new ManagerWorkFlowProcessTask();
        private readonly IManagerWorkFlowStep _managerWorkFlowStep = new ManagerWorkFlowStep();
        private readonly IManagerWorkFlowOperation _managerWorkFlowOperation = new ManagerWorkFlowOperation();
        private IMetadataManager iMetadataManager = new VPC.Framework.Business.MetadataManager.Contracts.MetadataManager();
        private IManagerWorkFlowTransition _managerTrasition = new ManagerWorkFlowTransition();
        private IManagerWorkFlow _managerWorkFlow=new ManagerWorkFlow();
        private IWorkFlowProcess _processWorkFlow = new WorkFlowProcess();
        WorkFlowResultMessage<WorkFlowProcessMessage> ITransitionFlowEngine.PreProcess(Guid tenantId, TransitionWapper transitionWapper)
        {
            var arrayList = new ArrayList { transitionWapper.ObjectData,tenantId };
            var resM = new WorkFlowResultMessage<WorkFlowProcessMessage>();
         
           var itsOperationProcess= WorkFlowCommon(tenantId,transitionWapper.EntityName, transitionWapper.SubTypeName,transitionWapper.StepId);
           if(itsOperationProcess.Count>0)
           {
            //get its all process task
            var allTasks = _managerWorkFlowProcessTask.GetWorkFlowProcessTask(tenantId, itsOperationProcess[0].WorkFlowId);
            var itsPreProcessTasks = new List<WorkFlowProcessTaskInfo>();          
            foreach (var itsOperationProc in itsOperationProcess)
            {
                if (itsOperationProc.ProcessType == (int)WorkFlowProcessType.PreProcess)
                    itsPreProcessTasks = (from allProcessTask in allTasks
                                          where itsOperationProc.WorkFlowProcessId == allProcessTask.WorkFlowProcessId
                                          orderby allProcessTask.SequenceCode
                                          select allProcessTask).ToList();
                
            }

            foreach (var itsPreProcessTask in itsPreProcessTasks)
            {
                WorkFlowProcessMessage processResult = _processWorkFlow.TransitionProcess(itsPreProcessTask.ProcessCode, arrayList);
                if (!processResult.Success)
                {
                    throw new CustomWorkflowException<WorkFlowMessage>(processResult.ErrorMessage.Code);
                }
                resM.Result = processResult;
            }
           
           }            

            var resultArrayList = new ArrayList { transitionWapper };
            if (resM.Result != null && resM.Result.Data != null)
            {
                resultArrayList.Add(resM.Result.Data);
            }

            return resM;
        }

        WorkFlowResultMessage<WorkFlowProcessMessage> ITransitionFlowEngine.PostProcess(Guid tenantId, TransitionWapper transitionWapper)
        {
           var arrayList = new ArrayList { transitionWapper.ObjectData,tenantId };
            var resM = new WorkFlowResultMessage<WorkFlowProcessMessage>();

          var itsOperationProcess= WorkFlowCommon(tenantId,transitionWapper.EntityName, transitionWapper.SubTypeName,transitionWapper.StepId);
           if(itsOperationProcess.Count>0)
           {
            //get its all process task
            var allTasks = _managerWorkFlowProcessTask.GetWorkFlowProcessTask(tenantId, itsOperationProcess[0].WorkFlowId);           
            var itsPostProcessTasks = new List<WorkFlowProcessTaskInfo>();
            foreach (var itsOperationProc in itsOperationProcess)
            {                

                if (itsOperationProc.ProcessType == (int)WorkFlowProcessType.PostProcess)
                    itsPostProcessTasks = (from allProcessTask in allTasks
                                           where itsOperationProc.WorkFlowProcessId == allProcessTask.WorkFlowProcessId
                                           orderby allProcessTask.SequenceCode
                                           select allProcessTask).ToList();
            }

            foreach (var itsPostProcessTask in itsPostProcessTasks)
            {
                WorkFlowProcessMessage processResult = _processWorkFlow.TransitionProcess(itsPostProcessTask.ProcessCode, arrayList);
                if (!processResult.Success)
                {
                    throw new CustomWorkflowException<WorkFlowMessage>(processResult.ErrorMessage.Code);
                }
                resM.Result = processResult;
            }
           }
            

            var resultArrayList = new ArrayList { transitionWapper };
            if (resM.Result != null && resM.Result.Data != null)
            {
                resultArrayList.Add(resM.Result.Data);
            }

            return resM;  
        }

        WorkFlowResultMessage<WorkFlowProcessMessage> ITransitionFlowEngine.Process(Guid tenantId,TransitionWapper transitionWapper)
        {          
             var arrayList = new ArrayList { transitionWapper,tenantId };
            var resM = new WorkFlowResultMessage<WorkFlowProcessMessage>();

         
         var itsOperationProcess= WorkFlowCommon(tenantId,transitionWapper.EntityName, transitionWapper.SubTypeName,transitionWapper.StepId);
           if(itsOperationProcess.Count>0)
           {
            //get its all process task
            var allTasks = _managerWorkFlowProcessTask.GetWorkFlowProcessTask(tenantId, itsOperationProcess[0].WorkFlowId);            
            var itsProcessTasks = new List<WorkFlowProcessTaskInfo>();            
            foreach (var itsOperationProc in itsOperationProcess)
            {               

                if (itsOperationProc.ProcessType == (int)WorkFlowProcessType.Process)
                    itsProcessTasks = (from allProcessTask in allTasks
                                       where itsOperationProc.WorkFlowProcessId == allProcessTask.WorkFlowProcessId
                                       orderby allProcessTask.SequenceCode
                                       select allProcessTask).ToList();

              
            }           

            foreach (var itsProcessTask in itsProcessTasks)
            {
                WorkFlowProcessMessage processResult = _processWorkFlow.TransitionProcess(itsProcessTask.ProcessCode, arrayList);
                if (!processResult.Success)
                {
                    throw new CustomWorkflowException<WorkFlowMessage>(processResult.ErrorMessage.Code);
                }
                resM.Result = processResult;
            }
           }            

            var resultArrayList = new ArrayList { transitionWapper };
            if (resM.Result != null && resM.Result.Data != null)
            {
                resultArrayList.Add(resM.Result.Data);
            }

            return resM;
        }

        private List<WorkFlowProcessInfo> WorkFlowCommon(Guid tenantId,string entityName, string subTypeName,Guid innerStepId)
        {
            var entityId = iMetadataManager.GetEntityContextByEntityName(entityName);
            var subTypeCode = iMetadataManager.GetSubTypeId(entityName, subTypeName);           
            var workFlow = _managerWorkFlow.GetWorkFlow(tenantId, entityId,subTypeCode);
            if (workFlow == null)
            {
                return new List<WorkFlowProcessInfo>();
            }
            //get its all pre/post/process 
            var allProcess = _managerWorkFlowProcess.GetWorkFlowProcess(tenantId, workFlow.WorkFlowId);
            var itsOperationProcess = (from workFlowPro in allProcess where workFlowPro.OperationOrTransactionId == innerStepId select workFlowPro).ToList();
            return itsOperationProcess;
        }

        
    }
}