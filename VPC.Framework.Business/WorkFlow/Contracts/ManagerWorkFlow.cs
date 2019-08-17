

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Transactions;
using VPC.Entities.Common;
using VPC.Entities.WorkFlow;
using VPC.Framework.Business.MetadataManager.Contracts;
using VPC.Framework.Business.WorkFlow.APIs;
using VPC.Framework.Business.WorkFlow.Attribute;
using VPC.Framework.Business.EntityResourceManager.Contracts;
using VPC.Framework.Business.DynamicQueryManager.Contracts;

namespace VPC.Framework.Business.WorkFlow.Contracts
{
    public interface IManagerWorkFlow
    {
        bool CreateWorkFlow(Guid tenantId, WorkFlowInfo workflowInfo);
        bool CreateWorkFlows(Guid tenantId, List<WorkFlowInfo> workflowInfos); 
        WorkFlowInfo GetWorkFlowDetail(Guid tenantId, string entityname, string subTypeName);
        List<WorkFlowInfo> GetAllWorkFlowDetails(Guid tenantId, string entityname);
        WorkFlowInfo GetWorkFlow(Guid tenantId, string entityId, string subTypeCode);
        List<WorkFlowInfo> GetWorkFlows(Guid tenantId, string entityId);
       // WorkFlowResultMessage<WorkFlowProcessMessage> ManageOperation(Guid tenantId, OperationWapper operation, WorkFlowProcessProperties properties);
        WorkFlowResultMessage<WorkFlowTransition> ManageTransition(Guid tenantId, TransitionWapper operationWapper,bool isSuperAdmin);
        
        WorkFlowResultMessage<WorkFlowTransition> ManageTransitionFirstStep(Guid tenantId,TransitionWapper operationWapper);
        List<WorkFlowInfo> GetWorkFlowsByIds(Guid tenantId, List<Guid> workFlowIds);
         List<WorkFlowInfo> GetWorkFlowsByEntityIds(Guid tenantId, List<string> entityIds);
         List<WorkFlowInnerStepInfo> GetNextPossibleSteps(Guid tenantId, string entityName,string entitySubType,Guid currentTransitionTypeId);
    }

    public class ManagerWorkFlow : IManagerWorkFlow
    {
        private readonly IAdminWorkFlow _adminWorkFlow = new AdminWorkFlow();
        private readonly IReviewWorkFlow _reviewWorkFlow = new ReviewWorkFlow();
        private readonly IManagerWorkFlowInnerStep _managerWorkFlowInnerStep = new ManagerWorkFlowInnerStep();
        private readonly IManagerWorkFlowProcess _managerWorkFlowProcess = new ManagerWorkFlowProcess();
        private readonly IManagerWorkFlowProcessTask _managerWorkFlowProcessTask = new ManagerWorkFlowProcessTask();
        private readonly IManagerWorkFlowStep _managerWorkFlowStep = new ManagerWorkFlowStep();
        private readonly IManagerWorkFlowOperation _managerWorkFlowOperation = new ManagerWorkFlowOperation();
        private IMetadataManager iMetadataManager = new VPC.Framework.Business.MetadataManager.Contracts.MetadataManager();
        private IManagerWorkFlowTransition _managerTrasition = new ManagerWorkFlowTransition();
        private IManagerWorkFlowRole _managerWorkFlowRole = new ManagerWorkFlowRole();

        private readonly IEntityQueryManager _queryManager = new EntityQueryManager();

        private IWorkFlowProcess _processWorkFlow = new WorkFlowProcess();
        bool IManagerWorkFlow.CreateWorkFlow(Guid tenantId, WorkFlowInfo workflowInfo)
        {
            return _adminWorkFlow.CreateWorkFlow(tenantId, workflowInfo);
        }

        bool IManagerWorkFlow.CreateWorkFlows(Guid tenantId, List<WorkFlowInfo> workflowInfos)
        {
            return _adminWorkFlow.CreateWorkFlows(tenantId, workflowInfos);
        }

        WorkFlowInfo IManagerWorkFlow.GetWorkFlow(Guid tenantId, string entityId, string subTypeCode)
        {
            //var entityId = iMetadataManager.GetEntityContextByEntityName(entityname);
           // var subTypeCode = iMetadataManager.GetSubTypeId(entityname, subTypeName);
            return _reviewWorkFlow.GetWorkFlow(tenantId, entityId, subTypeCode);
        }

        List<WorkFlowInfo> IManagerWorkFlow.GetWorkFlows(Guid tenantId, string entityname)
        {
            var entityId = iMetadataManager.GetEntityContextByEntityName(entityname);
            return _reviewWorkFlow.GetWorkFlows(tenantId, entityId);
        }

         List<WorkFlowInfo> IManagerWorkFlow.GetWorkFlowsByEntityIds(Guid tenantId, List<string> entityIds)
         {            
            return _reviewWorkFlow.GetWorkFlowsByEntityIds(tenantId, entityIds);
         }

        WorkFlowInfo IManagerWorkFlow.GetWorkFlowDetail(Guid tenantId, string entityname, string subTypeName)
        {
            var entityId = iMetadataManager.GetEntityContextByEntityName(entityname);
            var subTypeCode = iMetadataManager.GetSubTypeId(entityname, subTypeName);
            var workFlow = _reviewWorkFlow.GetWorkFlow(tenantId, entityId, subTypeCode);

            if(workFlow != null)
            {
                var workflowOperations = _managerWorkFlowOperation.GetWorkFlowOperations(tenantId, workFlow.WorkFlowId);
                workFlow.Operations = workflowOperations;
                //Get all steps from statci class
                var allSteps = WorkFlowHelper.GetAllSteps(entityId);

                var workFlowSteps = _managerWorkFlowStep.GetWorkFlowSteps(tenantId, workFlow.WorkFlowId);
                var workFlowInnerSteps = _managerWorkFlowInnerStep.GetWorkFlowInnerStep(tenantId, workFlow.WorkFlowId);
                var workFlowProcess = _managerWorkFlowProcess.GetWorkFlowProcess(tenantId, workFlow.WorkFlowId);

                // var workFlowProcessTasks=_managerWorkFlowProcessTask.GetWorkFlowProcessTask(tenantId,workFlow.WorkFlowId); 
                var allWorkFlowSteps = new List<WorkFlowStepInfo>();
                var allStepSavedCount = (from workFlowStep in workFlowSteps orderby workFlowStep.SequenceNumber descending select workFlowStep.SequenceNumber).FirstOrDefault();
                foreach (var step in allSteps)
                {
                    //return saved step
                    var checkStepsSaved = (from workFlowStep in workFlowSteps where workFlowStep.TransitionType.Id == step.Id select workFlowStep).FirstOrDefault();
                    if (checkStepsSaved != null)
                    {
                        checkStepsSaved.TransitionType.Name = step.Key;
                        checkStepsSaved.WorkFlowId = workFlow.WorkFlowId;
                        allWorkFlowSteps.Add(checkStepsSaved);
                    }
                    else
                    {
                        //return not saved step
                        allWorkFlowSteps.Add(new WorkFlowStepInfo
                        {
                            WorkFlowStepId = Guid.Empty,
                            WorkFlowId = workFlow.WorkFlowId,
                            SequenceNumber = ++allStepSavedCount,
                            TransitionType = new ItemName { Id = step.Id, Name = step.Key }
                        });
                    }
                }

                foreach (var workFlowStep in allWorkFlowSteps)
                {
                    var itsInnerSteps = (from workFlowInnerStep in workFlowInnerSteps where
                                                        workFlowInnerStep.WorkFlowStepId == workFlowStep.WorkFlowStepId
                                        select workFlowInnerStep).ToList();

                    foreach (var workFlowInnerStep in itsInnerSteps)
                    {
                        workFlowInnerStep.TransitionType.Name = (from allStep in allSteps where allStep.Id == workFlowInnerStep.TransitionType.Id select allStep.Key).FirstOrDefault();

                        workFlowInnerStep.WorkFlowProcess = (from workFlowPro in workFlowProcess
                                                            where workFlowPro.OperationOrTransactionId == workFlowInnerStep.InnerStepId
                                                            select workFlowPro).ToList();                    

                    }

                    workFlowStep.InnerSteps = itsInnerSteps.OrderBy(p => p.SequenceNumber).ToList();

                }
    
                workFlow.Steps = allWorkFlowSteps.OrderBy(p => p.SequenceNumber).ToList();

                //Chnage sab typesId To name
                workFlow.SubTypeCode=subTypeName;
                workFlow.EntityId=entityname;
            }
            

            return workFlow;
        }



        List<WorkFlowInfo> IManagerWorkFlow.GetAllWorkFlowDetails(Guid tenantId, string entityname)
        {
            var entityId = iMetadataManager.GetEntityContextByEntityName(entityname);            
            var workFlows = _reviewWorkFlow.GetWorkFlows(tenantId, entityId);
            var subTypes = iMetadataManager.GetSubTypesDetails(entityname);
            if(workFlows != null && workFlows.Count>0)
            {

                var allSteps = WorkFlowHelper.GetAllSteps(entityId);
                var workFlowIds=workFlows.Select(p=>p.WorkFlowId).ToList();

                var workFlowSteps = _managerWorkFlowStep.GetWorkFlowStepsByWorkFlowIds(tenantId, workFlowIds);
                var workFlowInnerSteps = _managerWorkFlowInnerStep.GetWorkFlowInnerStepByWorkFlowIds(tenantId, workFlowIds);
                var roles = _managerWorkFlowRole.GetWorkFlowRolesByWorkFlowIds(tenantId, workFlowIds).ToList();
                foreach(var workFlow in workFlows)
                {
                    var thisWorkFlowRoles = (from role in roles where role.WorkFlowId == workFlow.WorkFlowId select role).ToList();
                    var allWorkFlowSteps = new List<WorkFlowStepInfo>();
                    var allStepSavedCount = (from workFlowStep in workFlowSteps where workFlowStep.WorkFlowId==workFlow.WorkFlowId 
                                            orderby workFlowStep.SequenceNumber descending select workFlowStep.SequenceNumber).FirstOrDefault();
                    foreach (var step in allSteps)
                    {
                         
                        //return saved step
                        var checkStepsSaved = (from workFlowStep in workFlowSteps where workFlowStep.WorkFlowId==workFlow.WorkFlowId  &&
                                                                                 workFlowStep.TransitionType.Id == step.Id select workFlowStep).FirstOrDefault();

                        var checkRoles = (from role in thisWorkFlowRoles where role.WorkFlowStepId == checkStepsSaved.WorkFlowStepId select role).ToList();

                        if (checkStepsSaved != null)
                        {
                            checkStepsSaved.TransitionType.Name = step.Key;
                            checkStepsSaved.WorkFlowId = workFlow.WorkFlowId;
                            checkStepsSaved.Roles=checkRoles;                        
                            allWorkFlowSteps.Add(checkStepsSaved);
                        }                       
                    }

                    foreach (var workFlowStep in allWorkFlowSteps)
                    {
                        var itsInnerSteps = (from workFlowInnerStep in workFlowInnerSteps where workFlowInnerStep.WorkFlowId==workFlow.WorkFlowId && 
                                                            workFlowInnerStep.WorkFlowStepId == workFlowStep.WorkFlowStepId
                                            select workFlowInnerStep).ToList(); 
                    
                    foreach (var workFlowInnerStep in itsInnerSteps)
                        {
                            workFlowInnerStep.TransitionType.Name = (from allStep in allSteps where allStep.Id == workFlowInnerStep.TransitionType.Id select allStep.Key).FirstOrDefault();
                        }                       
                        workFlowStep.InnerSteps = itsInnerSteps.OrderBy(p => p.SequenceNumber).ToList();
                    }
  
                   workFlow.Steps = allWorkFlowSteps.OrderBy(p => p.SequenceNumber).ToList();
                   //Chnage sab typesId To name
                   var mapped = subTypes.FirstOrDefault(t => t.Key.Equals(workFlow.SubTypeCode));
                        if (mapped.Key != null)
                        {
                           workFlow.SubTypeCode=mapped.Value;
                        }
                    workFlow.EntityId=entityname;

                }
            }
            return workFlows;
        }

        WorkFlowResultMessage<WorkFlowTransition> IManagerWorkFlow.ManageTransitionFirstStep(Guid tenantId,TransitionWapper operationWapper)
            {
            var resM = new WorkFlowResultMessage<WorkFlowTransition>(); 
            IOperationFlowEngine operationEngine = new OperationFlowEngine();
            var subTypeCode = iMetadataManager.GetSubTypeId(operationWapper.EntityName, operationWapper.SubTypeName);
            var properties = new WorkFlowProcessProperties { EntityName = operationWapper.EntityName, SubTypeCode = subTypeCode, UserId = operationWapper.UserId, IsSuperAdmin = false };
            operationEngine.PreProcess(tenantId, new OperationWapper { OperationType = WorkFlowOperationType.Create, Data = operationWapper.ObjectData }, properties);
            resM=Transition(tenantId,operationWapper);            
            operationEngine.Process(tenantId, new OperationWapper { OperationType = WorkFlowOperationType.Create, Data = operationWapper.ObjectData }, properties);
            operationEngine.FirstOperation(tenantId, properties);
            operationEngine.PostProcess(tenantId, new OperationWapper { OperationType = WorkFlowOperationType.Create, Data = operationWapper.ObjectData }, properties);
            return resM;
            }

        WorkFlowResultMessage<WorkFlowTransition> IManagerWorkFlow.ManageTransition(Guid tenantId,TransitionWapper operationWapper,bool isSuperAdmin)
            {
                var resM = new WorkFlowResultMessage<WorkFlowTransition>(); 
                ITransitionFlowEngine transitionEngine = new TransitionFlowEngine();
                var subTypeCode = iMetadataManager.GetSubTypeId(operationWapper.EntityName, operationWapper.SubTypeName);
                if(!isSuperAdmin)
                {
                    var isValid= CheckRoleWorkFlowSecurity(tenantId,operationWapper,subTypeCode);
                    if(!isValid)
                    {
                        throw new CustomWorkflowException<WorkFlowMessage>(WorkFlowMessage.InValidTransition);                  
                    }
                }
                 
                
                var properties = new WorkFlowProcessProperties { EntityName = operationWapper.EntityName, SubTypeCode = subTypeCode, UserId = operationWapper.UserId, IsSuperAdmin = isSuperAdmin };
                transitionEngine.PreProcess(tenantId, operationWapper);
                resM=Transition(tenantId,operationWapper);            
                transitionEngine.Process(tenantId,operationWapper);            
                transitionEngine.PostProcess(tenantId,operationWapper);
                return resM;
            }

            private bool  CheckRoleWorkFlowSecurity(Guid tenantId,TransitionWapper operationWapper,string subTypeCode)
            {
                var entityId = iMetadataManager.GetEntityContextByEntityName(operationWapper.EntityName);   
                var validTransitionTypes=_managerWorkFlowStep.GetAssignedWorkFlowStepsOfUser(tenantId,operationWapper.UserId,entityId,subTypeCode);
                return  validTransitionTypes.Contains(operationWapper.NextTransitionType);
            }
       private  WorkFlowResultMessage<WorkFlowTransition> Transition(Guid tenantId,TransitionWapper operationWapper)
        {
            var resM = new WorkFlowResultMessage<WorkFlowTransition>();                          
            
            //-------Update  last active step endtime 
            var transactionHistories=_managerTrasition.GetTransitionHistoryByRefId(tenantId,operationWapper.RefId);
            if(transactionHistories.Count>0)
            {
               var currentTrasitionTypes=(from transactionHistorie in transactionHistories where transactionHistorie.TransitionType.Id==operationWapper.CurrentTransitionType 
                                            select transactionHistorie).OrderByDescending(p=>p.StartTime).ToList();
                if(currentTrasitionTypes.Count>0)
                {
                        var transitionLast= new WorkFlowTransition{TransitionHistoryId = currentTrasitionTypes[0].TransitionHistoryId,EndTime = DateTime.UtcNow};                       
                        _managerTrasition.UpdateTransitionHistory(tenantId, transitionLast);
                }

            dynamic jsonObject = new JObject (); 
            jsonObject.CurrentWorkFlowStep = operationWapper.NextTransitionType ;

            var myObj = new JObject();
            myObj.Add(operationWapper.EntityName, jsonObject);

            IEntityResourceManager _iEntityResourceManager = new VPC.Framework.Business.EntityResourceManager.Contracts.EntityResourceManager ();
            var resultId = _iEntityResourceManager.UpdateResultWithoutWorkFlow(tenantId, operationWapper.UserId,operationWapper.RefId,
            operationWapper.EntityName, myObj, operationWapper.SubTypeName);

            //----- Insert into history table
            var entityId = iMetadataManager.GetEntityContextByEntityName(operationWapper.EntityName);
            var subTypeCode = iMetadataManager.GetSubTypeId(operationWapper.EntityName, operationWapper.SubTypeName);
            var workFlow = _reviewWorkFlow.GetWorkFlow(tenantId, entityId, subTypeCode);
            if (workFlow == null)
            {
                throw new CustomWorkflowException<WorkFlowMessage>(WorkFlowMessage.NoOperationActivity);
            }
            var itsCurrentStep = _managerWorkFlowStep.GetWorkFlowSteps(tenantId, workFlow.WorkFlowId).FirstOrDefault(x => x.TransitionType.Id == operationWapper.NextTransitionType );
            var transitionNew= new WorkFlowTransition();
            transitionNew.TransitionHistoryId = Guid.NewGuid();
            transitionNew.StartTime = DateTime.UtcNow;
            transitionNew.WorkFlowStepId=itsCurrentStep.WorkFlowStepId;
            transitionNew.EntityId=entityId;
            transitionNew.RefId=operationWapper.RefId;
            _managerTrasition.CreateTransitionHistory(tenantId, transitionNew);
            resM.Result = transitionNew;

            }
           

            return resM;
        }


        List<WorkFlowInnerStepInfo> IManagerWorkFlow.GetNextPossibleSteps(Guid tenantId, string entityName,string subTypeName,Guid currentTransitionTypeId)
        {            

            var entityId = iMetadataManager.GetEntityContextByEntityName(entityName);
            var subTypeCode = iMetadataManager.GetSubTypeId(entityName, subTypeName);
            var workFlow = _reviewWorkFlow.GetWorkFlow(tenantId, entityId, subTypeCode);
            if (workFlow == null)
            {
                throw new CustomWorkflowException<WorkFlowMessage>(WorkFlowMessage.NoOperationActivity);
            }
            var itsCurrentStep = _managerWorkFlowStep.GetWorkFlowSteps(tenantId, workFlow.WorkFlowId).FirstOrDefault(x => x.TransitionType.Id == currentTransitionTypeId);

            var innerSteps = _managerWorkFlowInnerStep.GetWorkFlowInnerStep(tenantId, workFlow.WorkFlowId).Where(x => x.WorkFlowStepId == itsCurrentStep.WorkFlowStepId).ToList();
            foreach(var innerStep in innerSteps)
            {
                innerStep.TransitionType.Name=WorkFlowHelper.GetKeyByContext(entityId, innerStep.TransitionType.Id);
            }
            return innerSteps;
        }
       
        
        
        List<WorkFlowInfo> IManagerWorkFlow.GetWorkFlowsByIds(Guid tenantId,  List<Guid> workFlowIds)
        {
         return _reviewWorkFlow.GetWorkFlowsByIds(tenantId,workFlowIds);
        }

        

        // List<WorkFlowInfo> IManagerWorkFlow.GetWorkFlows(Guid tenantId, string entityname)
        // {
        //     var entityId= iMetadataManager.GetEntityContextByEntityName(entityname);           
        //     var workFlows= _reviewWorkFlow.GetWorkFlows(tenantId,entityId);
        //     if(workFlows.Count>0)
        //     {
        //         var allSteps = WorkFlowHelper.GetAllSteps(entityId);
        //         var workFlowSteps=_managerWorkFlowStep.GetWorkFlowSteps(tenantId,workFlows.Select(p=>p.WorkFlowId).ToList());
        //         var allWorkFlowSteps=new List<WorkFlowStepInfo>();

        //         foreach(var workFlow in workFlows)
        //         {
        //         var itsWorkFlowSteps=(from workFlowStep in workFlowSteps where workFlowStep.WorkFlowId==workFlow.WorkFlowId select workFlowStep).ToList();
        //         var allStepSavedCount=(from workFlowStep in itsWorkFlowSteps orderby workFlowStep.SequenceNumber descending select workFlowStep.SequenceNumber).FirstOrDefault();
        //         foreach(var step in allSteps)
        //         {
        //             //return saved step
        //             var checkStepsSaved=(from workFlowStep in itsWorkFlowSteps where workFlowStep.TransitionType.Id==step.Id select workFlowStep).FirstOrDefault();
        //             if(checkStepsSaved!=null)
        //             {
        //                 checkStepsSaved.TransitionType.Name=step.Value;
        //                 checkStepsSaved.WorkFlowId = workFlow.WorkFlowId; 
        //                 allWorkFlowSteps.Add(checkStepsSaved);   
        //             }
        //             else
        //             {
        //                 //return not saved step
        //                 allWorkFlowSteps.Add(new WorkFlowStepInfo{
        //                     WorkFlowStepId=Guid.Empty, 
        //                     WorkFlowId=workFlow.WorkFlowId, 
        //                     SequenceNumber=++allStepSavedCount,
        //                     TransitionType=new ItemName{Id=step.Id, Name=step.Value}     
        //                 });
        //             } 
        //             workFlow.Steps=allWorkFlowSteps;
        //          }

        //         }               
        //     }            

        //     return workFlows;
        // }



        // WorkFlowResultMessage<WorkFlowProcessMessage> IManagerWorkFlow.ManageOperation(Guid tenantId, OperationWapper operation, WorkFlowProcessProperties properties)
        // {
        //     var arrayList = new ArrayList { properties, operation.Data };
        //     var resM = new WorkFlowResultMessage<WorkFlowProcessMessage>();

        //     var entityId = iMetadataManager.GetEntityContextByEntityName(properties.EntityName);
        //     var subTypeCode = iMetadataManager.GetSubTypeId(properties.EntityName, properties.SubTypeCode);
        //     var workFlow = _reviewWorkFlow.GetWorkFlow(tenantId, entityId, subTypeCode);
        //     if (workFlow == null)
        //     {
        //         throw new CustomWorkflowException<WorkFlowMessage>(WorkFlowMessage.NoOperationActivity);
        //     }

        //     var workflowOperations = _managerWorkFlowOperation.GetWorkFlowOperations(tenantId, workFlow.WorkFlowId);
        //     var itsOperation = (from workflowOperation in workflowOperations where workflowOperation.OperationType == operation.OperationType select workflowOperation).FirstOrDefault();
        //     if (itsOperation == null)
        //     {
        //         throw new CustomWorkflowException<WorkFlowMessage>(WorkFlowMessage.InValidOperation);
        //     }

        //     //get its all pre/post/process 
        //     var allProcess = _managerWorkFlowProcess.GetWorkFlowProcess(tenantId, workFlow.WorkFlowId);
        //     var itsOperationProcess = (from workFlowPro in allProcess where workFlowPro.OperationOrTransactionId == itsOperation.WorkFlowOperationId select workFlowPro).ToList();

        //     //get its all process task
        //     var allTasks = _managerWorkFlowProcessTask.GetWorkFlowProcessTask(tenantId, workFlow.WorkFlowId);
        //     var itsPreProcessTasks = new List<WorkFlowProcessTaskInfo>();
        //     var itsProcessTasks = new List<WorkFlowProcessTaskInfo>();
        //     var itsPostProcessTasks = new List<WorkFlowProcessTaskInfo>();
        //     foreach (var itsOperationProc in itsOperationProcess)
        //     {
        //         if (itsOperationProc.ProcessType == (int)WorkFlowProcessType.PreProcess)
        //             itsPreProcessTasks = (from allProcessTask in allTasks
        //                                   where itsOperationProc.WorkFlowProcessId == allProcessTask.WorkFlowProcessId
        //                                   orderby allProcessTask.SequenceCode
        //                                   select allProcessTask).ToList();

        //         if (itsOperationProc.ProcessType == (int)WorkFlowProcessType.Process)
        //             itsProcessTasks = (from allProcessTask in allTasks
        //                                where itsOperationProc.WorkFlowProcessId == allProcessTask.WorkFlowProcessId
        //                                orderby allProcessTask.SequenceCode
        //                                select allProcessTask).ToList();

        //         if (itsOperationProc.ProcessType == (int)WorkFlowProcessType.PostProcess)
        //             itsPostProcessTasks = (from allProcessTask in allTasks
        //                                    where itsOperationProc.WorkFlowProcessId == allProcessTask.WorkFlowProcessId
        //                                    orderby allProcessTask.SequenceCode
        //                                    select allProcessTask).ToList();
        //     }

        //     foreach (var itsPreProcessTask in itsPreProcessTasks)
        //     {
        //         WorkFlowProcessMessage processResult = _processWorkFlow.OperationProcess(itsPreProcessTask.ProcessCode, arrayList);
        //         if (!processResult.Success)
        //         {
        //             throw new CustomWorkflowException<WorkFlowMessage>(processResult.ErrorMessage.Code);
        //         }
        //         resM.Result = processResult;
        //     }

        //     foreach (var itsProcessTask in itsProcessTasks)
        //     {
        //         WorkFlowProcessMessage processResult = _processWorkFlow.OperationProcess(itsProcessTask.ProcessCode, arrayList);
        //         if (!processResult.Success)
        //         {
        //             throw new CustomWorkflowException<WorkFlowMessage>(processResult.ErrorMessage.Code);
        //         }
        //         resM.Result = processResult;
        //     }

        //     foreach (var itsPostProcessTask in itsPostProcessTasks)
        //     {
        //         WorkFlowProcessMessage processResult = _processWorkFlow.OperationProcess(itsPostProcessTask.ProcessCode, arrayList);
        //         if (!processResult.Success)
        //         {
        //             throw new CustomWorkflowException<WorkFlowMessage>(processResult.ErrorMessage.Code);
        //         }
        //         resM.Result = processResult;
        //     }

        //     var resultArrayList = new ArrayList { properties };
        //     if (resM.Result != null && resM.Result.Data != null)
        //     {
        //         resultArrayList.Add(resM.Result.Data);
        //     }

        //     return resM;
        // }

        // WorkFlowResultMessage<WorkFlowTransition> IManagerWorkFlow.ManageTransition(Guid tenantId, WorkFlowTransition workFlowTransition, WorkFlowProcessProperties properties)
        // {

        //     var arrayList = new ArrayList
        //                         {
        //                             properties,
        //                             workFlowTransition.Data
        //                         };
        //     //workFlowTransition.Data = arrayList;
        //     var resM = new WorkFlowResultMessage<WorkFlowTransition>();
        //     WorkFlowTransition currentWorkflowItem = _managerTrasition.GetCurrentStepByRefId(tenantId, workFlowTransition.RefId);
        //     if (currentWorkflowItem == null)
        //     {
        //         throw new CustomWorkflowException<WorkFlowMessage>(WorkFlowMessage.InvalidAction);
        //     }

        //     var entityId = iMetadataManager.GetEntityContextByEntityName(properties.EntityName);
        //     var subTypeCode = iMetadataManager.GetSubTypeId(properties.EntityName, properties.SubTypeCode);
        //     var workFlow = _reviewWorkFlow.GetWorkFlow(tenantId, entityId, subTypeCode);
        //     if (workFlow == null)
        //     {
        //         throw new CustomWorkflowException<WorkFlowMessage>(WorkFlowMessage.NoOperationActivity);
        //     }
        //     var itsCurrentStep = _managerWorkFlowStep.GetWorkFlowSteps(tenantId, workFlow.WorkFlowId).FirstOrDefault(x => x.TransitionType.Id == currentWorkflowItem.WorkFlowStepId);

        //     var innerStep = _managerWorkFlowInnerStep.GetWorkFlowInnerStep(tenantId, workFlow.WorkFlowId).FirstOrDefault(x => x.WorkFlowStepId == itsCurrentStep.WorkFlowStepId &&
        //                                                                                                                      x.TransitionType.Id == workFlowTransition.WorkFlowStepId);
        //     if (innerStep == null)
        //     {
        //         throw new CustomWorkflowException<WorkFlowMessage>(WorkFlowMessage.InValidTransition);
        //     }

        //     #region Sequrity check
        //     // ISecurityV3Manager _iSecurityManager=new SecurityV3Manager();
        //     // List<SecurityEntityItem> functions = _iSecurityManager.GetFunctionByUser(properties.TenantId, properties.UserId.Value,properties.IsSuperAdmin);
        //     // var functionIds = (from function in functions where function.ReferenceId == VisitFunctionContext.OverrideWorkflowConfiguration && function.SecurityCode > 1
        //     //                    select function).ToList();

        //     // var overrideTransition = ((functionIds.Count>0) || (properties.IsSuperAdmin));

        //     // //Checking Role permission to  step
        //     // if (!overrideTransition)
        //     // {
        //     //     if (!CheckPermission(workFlowTransition, tenantId, innerStep, properties.RoleIds, properties.IsSuperAdmin))
        //     //     {
        //     //         throw new CustomWorkflowException<WorkFlowMessage>(WorkFlowMessage.NoPermission);
        //     //     }
        //     // }
        //     #endregion

        //     //get its all pre/post/process 
        //     var allProcess = _managerWorkFlowProcess.GetWorkFlowProcess(tenantId, workFlow.WorkFlowId);
        //     var itsOperationProcess = (from workFlowPro in allProcess where workFlowPro.OperationOrTransactionId == innerStep.InnerStepId select workFlowPro).ToList();

        //     //get its all process task
        //     var allTasks = _managerWorkFlowProcessTask.GetWorkFlowProcessTask(tenantId, workFlow.WorkFlowId);
        //     var itsPreProcessTasks = new List<WorkFlowProcessTaskInfo>();
        //     var itsProcessTasks = new List<WorkFlowProcessTaskInfo>();
        //     var itsPostProcessTasks = new List<WorkFlowProcessTaskInfo>();
        //     foreach (var itsOperationProc in itsOperationProcess)
        //     {
        //         if (itsOperationProc.ProcessType == (int)WorkFlowProcessType.PreProcess)
        //             itsPreProcessTasks = (from allProcessTask in allTasks
        //                                   where itsOperationProc.WorkFlowProcessId == allProcessTask.WorkFlowProcessId
        //                                   orderby allProcessTask.SequenceCode
        //                                   select allProcessTask).ToList();

        //         if (itsOperationProc.ProcessType == (int)WorkFlowProcessType.Process)
        //             itsProcessTasks = (from allProcessTask in allTasks
        //                                where itsOperationProc.WorkFlowProcessId == allProcessTask.WorkFlowProcessId
        //                                orderby allProcessTask.SequenceCode
        //                                select allProcessTask).ToList();

        //         if (itsOperationProc.ProcessType == (int)WorkFlowProcessType.PostProcess)
        //             itsPostProcessTasks = (from allProcessTask in allTasks
        //                                    where itsOperationProc.WorkFlowProcessId == allProcessTask.WorkFlowProcessId
        //                                    orderby allProcessTask.SequenceCode
        //                                    select allProcessTask).ToList();
        //     }

        //     foreach (var itsPreProcessTask in itsPreProcessTasks)
        //     {
        //         WorkFlowProcessMessage processResult = _processWorkFlow.OperationProcess(itsPreProcessTask.ProcessCode, arrayList);
        //         if (!processResult.Success)
        //         {
        //             throw new CustomWorkflowException<WorkFlowMessage>(processResult.ErrorMessage.Code);
        //         }
        //     }

        //     foreach (var itsProcessTask in itsProcessTasks)
        //     {
        //         WorkFlowProcessMessage processResult = _processWorkFlow.OperationProcess(itsProcessTask.ProcessCode, arrayList);
        //         if (!processResult.Success)
        //         {
        //             throw new CustomWorkflowException<WorkFlowMessage>(processResult.ErrorMessage.Code);
        //         }
        //     }

        //     foreach (var itsPostProcessTask in itsPostProcessTasks)
        //     {
        //         WorkFlowProcessMessage processResult = _processWorkFlow.OperationProcess(itsPostProcessTask.ProcessCode, arrayList);
        //         if (!processResult.Success)
        //         {
        //             throw new CustomWorkflowException<WorkFlowMessage>(processResult.ErrorMessage.Code);
        //         }
        //     }

        //     //----- Insert into history table
        //     workFlowTransition.TransitionHistoryId = Guid.NewGuid();
        //     workFlowTransition.StartTime = DateTime.UtcNow;
        //     workFlowTransition.EntityCode = entityId;
        //     workFlowTransition.AuditInfo = new AuditDetail { CreatedBy = properties.UserId };
        //     _managerTrasition.CreateTransitionHistory(tenantId, workFlowTransition);
        //     resM.Result = workFlowTransition;
        //     //-------Update  last active step endtime 
        //     workFlowTransition.TransitionHistoryId = currentWorkflowItem.TransitionHistoryId;
        //     workFlowTransition.EndTime = DateTime.UtcNow;
        //     _managerTrasition.UpdateTransitionHistory(tenantId, workFlowTransition);

        //     return resM;
        // }

    }
}


