
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
    public class OperationFlowEngine : IOperationFlowEngine
    {
        IManagerWorkFlowTransition transactionHistory = new ManagerWorkFlowTransition();
        private readonly IManagerWorkFlowInnerStep _managerWorkFlowInnerStep = new ManagerWorkFlowInnerStep();
        private readonly IManagerWorkFlowProcess _managerWorkFlowProcess = new ManagerWorkFlowProcess();
        private readonly IManagerWorkFlowProcessTask _managerWorkFlowProcessTask = new ManagerWorkFlowProcessTask();
        private readonly IManagerWorkFlowStep _managerWorkFlowStep = new ManagerWorkFlowStep();
        private readonly IManagerWorkFlowOperation _managerWorkFlowOperation = new ManagerWorkFlowOperation();
        private IMetadataManager iMetadataManager = new VPC.Framework.Business.MetadataManager.Contracts.MetadataManager();
        private IManagerWorkFlowTransition _managerTrasition = new ManagerWorkFlowTransition();
        private IManagerWorkFlow _managerWorkFlow = new ManagerWorkFlow();
        private IWorkFlowProcess _processWorkFlow = new WorkFlowProcess();
        WorkFlowResultMessage<WorkFlowProcessMessage> IOperationFlowEngine.PreProcess(Guid tenantId, OperationWapper operation, WorkFlowProcessProperties properties)
        {
            var arrayList = new ArrayList { properties, operation.Data, tenantId };
            var resM = new WorkFlowResultMessage<WorkFlowProcessMessage>();


            var itsOperationProcess = WorkFlowCommon(tenantId, properties.EntityName, properties.SubTypeCode, operation.OperationType);
            if (itsOperationProcess.Count > 0)
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
                    WorkFlowProcessMessage processResult = _processWorkFlow.OperationProcess(itsPreProcessTask.ProcessCode, arrayList);
                    if (!processResult.Success)
                    {
                        throw new CustomWorkflowException<WorkFlowMessage>(processResult.ErrorMessage.Code);
                    }
                    resM.Result = processResult;
                }

            }

            var resultArrayList = new ArrayList { properties };
            if (resM.Result != null && resM.Result.Data != null)
            {
                resultArrayList.Add(resM.Result.Data);
            }

            return resM;
        }

        WorkFlowResultMessage<WorkFlowProcessMessage> IOperationFlowEngine.PostProcess(Guid tenantId, OperationWapper operation, WorkFlowProcessProperties properties)
        {
            var arrayList = new ArrayList { properties, operation.Data, tenantId };
            var resM = new WorkFlowResultMessage<WorkFlowProcessMessage>();


            var itsOperationProcess = WorkFlowCommon(tenantId, properties.EntityName, properties.SubTypeCode, operation.OperationType);
            if (itsOperationProcess.Count > 0)
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
                    WorkFlowProcessMessage processResult = _processWorkFlow.OperationProcess(itsPostProcessTask.ProcessCode, arrayList);
                    if (!processResult.Success)
                    {
                        throw new CustomWorkflowException<WorkFlowMessage>(processResult.ErrorMessage.Code);
                    }
                    resM.Result = processResult;
                }
            }


            var resultArrayList = new ArrayList { properties };
            if (resM.Result != null && resM.Result.Data != null)
            {
                resultArrayList.Add(resM.Result.Data);
            }

            return resM;
        }

        WorkFlowResultMessage<WorkFlowProcessMessage> IOperationFlowEngine.Process(Guid tenantId, OperationWapper operation, WorkFlowProcessProperties properties)
        {
            var arrayList = new ArrayList { properties, operation.Data, tenantId };
            var resM = new WorkFlowResultMessage<WorkFlowProcessMessage>();


            var itsOperationProcess = WorkFlowCommon(tenantId, properties.EntityName, properties.SubTypeCode, operation.OperationType);
            if (itsOperationProcess.Count > 0)
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
                    WorkFlowProcessMessage processResult = _processWorkFlow.OperationProcess(itsProcessTask.ProcessCode, arrayList);
                    if (!processResult.Success)
                    {
                        throw new CustomWorkflowException<WorkFlowMessage>(processResult.ErrorMessage.Code);
                    }
                    resM.Result = processResult;
                }
            }

            var resultArrayList = new ArrayList { properties };
            if (resM.Result != null && resM.Result.Data != null)
            {
                resultArrayList.Add(resM.Result.Data);
            }

            return resM;
        }

        void IOperationFlowEngine.FirstOperation(Guid tenantId, WorkFlowProcessProperties properties)
        {

            var entityId = iMetadataManager.GetEntityContextByEntityName(properties.EntityName);
            var workFlow = _managerWorkFlow.GetWorkFlow(tenantId, entityId, properties.SubTypeCode);
            if (workFlow != null)
            {
                var steps = _managerWorkFlowStep.GetWorkFlowSteps(tenantId, workFlow.WorkFlowId).ToList();
                if (steps.Count > 0)
                {
                    transactionHistory.CreateTransitionHistory(tenantId, new WorkFlowTransition
                    {
                        TransitionHistoryId = Guid.NewGuid(),
                        RefId = properties.resultId,
                        EntityId = entityId,
                        WorkFlowStepId = steps[0].WorkFlowStepId,
                        StartTime = DateTime.UtcNow,
                        EndTime = DateTime.UtcNow,
                        AssignedUserId = Guid.Empty,
                        AuditInfo = new Entities.Common.AuditDetail { CreatedBy = properties.UserId }
                    });

                    dynamic jsonObject = new JObject();
                    jsonObject.CurrentWorkFlowStep = steps[0].TransitionType.Id;

                    IEntityResourceManager _iEntityResourceManager = new VPC.Framework.Business.EntityResourceManager.Contracts.EntityResourceManager();
                    _iEntityResourceManager.UpdateSpecificField(tenantId, properties.EntityName, properties.resultId, "CurrentWorkFlowStep", steps[0].TransitionType.Id.ToString());
                    // var resultId = _iEntityResourceManager.UpdateResult (tenantId, properties.UserId,properties.resultId,properties.EntityName, jsonObject, properties.SubTypeCode);
                }
            }
        }


        private List<WorkFlowProcessInfo> WorkFlowCommon(Guid tenantId, string entityName, string subTypeCode, WorkFlowOperationType operationType)
        {
            var entityId = iMetadataManager.GetEntityContextByEntityName(entityName);
            var workFlow = _managerWorkFlow.GetWorkFlow(tenantId, entityId, subTypeCode);
            if (workFlow == null)
            {
                return new List<WorkFlowProcessInfo>();
            }

            var workflowOperations = _managerWorkFlowOperation.GetWorkFlowOperations(tenantId, workFlow.WorkFlowId);
            var itsOperation = (from workflowOperation in workflowOperations where workflowOperation.OperationType == operationType select workflowOperation).FirstOrDefault();
            if (itsOperation == null)
            {
                return new List<WorkFlowProcessInfo>();
            }

            //get its all pre/post/process 
            var allProcess = _managerWorkFlowProcess.GetWorkFlowProcess(tenantId, workFlow.WorkFlowId);
            var itsOperationProcess = (from workFlowPro in allProcess where workFlowPro.OperationOrTransactionId == itsOperation.WorkFlowOperationId select workFlowPro).ToList();
            return itsOperationProcess;
        }


    }
}