using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Transactions;
using VPC.Entities.Common;
using VPC.Entities.Role;
using VPC.Entities.WorkFlow;
using VPC.Framework.Business.Common;
using VPC.Framework.Business.MetadataManager.Contracts;
using VPC.Framework.Business.Role.APIs;
using VPC.Framework.Business.WorkFlow.APIs;
using VPC.Framework.Business.WorkFlow.Attribute;

namespace VPC.Framework.Business.WorkFlow.Contracts
{
    public interface IManagerWorkFlowSecurity
    {
        object GetWorkFlowSecurity(Guid tenantId, string entityname);
        List<WorkFlowInfo> GetWorkFlowsByUserCode(Guid tenantId, Guid userId,bool isSuperAdmin);
        bool InitializeRootTenantWorkFlow(Guid tenantId);
        bool InitializeRootTenantWorkFlow(Guid tenantId,string entityId);
        bool InitializeTenantWorkFlow(Guid rootTenantId,Guid newTenantId,List<string> entityIds);
        
    }
 

    public class ManagerWorkFlowSecurity : IManagerWorkFlowSecurity
    {
        private readonly IManagerWorkFlow _managerWorkFlow = new ManagerWorkFlow();
        private readonly IManagerWorkFlowStep _managerWorkFlowStep = new ManagerWorkFlowStep();
        private IMetadataManager iMetadataManager = new VPC.Framework.Business.MetadataManager.Contracts.MetadataManager();
        private readonly IManagerRole _managerRole = new ManagerRole();
        private IManagerWorkFlowRole _managerWorkFlowRole = new ManagerWorkFlowRole();
        private IManagerWorkFlowInnerStep _managerInnerStep = new ManagerWorkFlowInnerStep();
        private IManagerWorkFlowOperation _managerOperation = new ManagerWorkFlowOperation();
        private IManagerWorkFlowProcess _managerWorkFlowProcess = new ManagerWorkFlowProcess();
        private IManagerWorkFlowProcessTask _managerWorkFlowProcessTask = new ManagerWorkFlowProcessTask();      
     


        object IManagerWorkFlowSecurity.GetWorkFlowSecurity(Guid tenantId, string entityname)
        {

            var subTypes = iMetadataManager.GetSubTypesDetails(entityname);
            var entityId = iMetadataManager.GetEntityContextByEntityName(entityname);
            var workFlowItems = _managerWorkFlow.GetWorkFlows(tenantId, entityname);
            var allRoles = _managerRole.Roles(tenantId);
            var mainObj = new { roleList = allRoles, workFlows = new List<object>() };
            var allSteps = WorkFlowHelper.GetAllSteps(entityId);
            var workFlowSteps = _managerWorkFlowStep.GetWorkFlowStepsByWorkFlowIds(tenantId, workFlowItems.Select(p => p.WorkFlowId).ToList());
            var roles = _managerWorkFlowRole.GetWorkFlowRolesByWorkFlowIds(tenantId, workFlowItems.Select(p => p.WorkFlowId).ToList());
            foreach (var workFlowItem in workFlowItems)
            {
                var subTypeName = subTypes.FirstOrDefault(t => t.Key.Equals(workFlowItem.SubTypeCode));
                var thisWorkFlowRoles = (from role in roles where role.WorkFlowId == workFlowItem.WorkFlowId select role).ToList();
                var workFLowObj = new
                {
                    EntityId = entityname,
                    SubTypeCode = subTypeName.Value,
                    WorkFlowId = workFlowItem.WorkFlowId,
                    Steps = new List<object>()
                };

                var thisWorkFLowSteps = (from workFlowStep in workFlowSteps where workFlowStep.WorkFlowId == workFlowItem.WorkFlowId select workFlowStep).ToList();
                foreach (var step in allSteps)
                {
                    var filteredStep = (from workFlowStep in thisWorkFLowSteps where workFlowStep.TransitionType.Id == step.Id select workFlowStep).FirstOrDefault();
                    if (filteredStep != null)
                    {
                        var stepObj = new
                        {
                            filteredStep.WorkFlowStepId,
                            filteredStep.WorkFlowId,
                            Name = step.Value,
                            filteredStep.IsAssigmentMandatory,
                            filteredStep.AllotedTime,
                            filteredStep.CriticalTime,
                            ActivatedList = new List<object>(),
                            AccessedList = new List<object>(),
                            AssignedList = new List<object>()
                        };


                        var checkRoles = (from role in thisWorkFlowRoles where role.WorkFlowStepId == filteredStep.WorkFlowStepId select role).ToList();
                        if (checkRoles.Count > 0)
                        {
                            foreach (var checkRole in checkRoles)
                            {
                                var rolObj = new
                                {
                                    checkRole.RoleAssignmetId,
                                    checkRole.RoleId,
                                    checkRole.WorkFlowId,
                                    checkRole.WorkFlowStepId,
                                    name = checkRole.RoleName,
                                    checkRole.AssignmentOperationType
                                };
                                if (checkRole.AssignmentOperationType == (int)WorkFlowRoleType.ActivatedRoles)
                                    stepObj.ActivatedList.Add(rolObj);
                                if (checkRole.AssignmentOperationType == (int)WorkFlowRoleType.AccessedRoles)
                                    stepObj.AccessedList.Add(rolObj);
                                if (checkRole.AssignmentOperationType == (int)WorkFlowRoleType.AssignedRoles)
                                    stepObj.AssignedList.Add(rolObj);
                            }
                        }
                        workFLowObj.Steps.Add(stepObj);
                    }
                }
                mainObj.workFlows.Add(workFLowObj);
            }
            return mainObj;
        }
        

        bool IManagerWorkFlowSecurity.InitializeTenantWorkFlow(Guid rootTenantId,Guid newTenantId,List<string> entityIds)
        {
            List<RoleInfo> newRoles=new List<RoleInfo>();
            var allRoles = _managerRole.Roles(rootTenantId);
            foreach(var allRole in allRoles)
            {
                var itsRole=new RoleInfo() ;
                DataUtility.CopyPropertiesTo(allRole, itsRole);
                itsRole.RoleId=Guid.NewGuid();
                newRoles.Add(itsRole);
            }
            //Init roles
            _managerRole.CreateRoles(newTenantId,newRoles);

            var workFlows=_managerWorkFlow.GetWorkFlowsByEntityIds(rootTenantId,entityIds);
            if(workFlows.Count>0)
            {
                //Get root tenant 
                var workFlowIds=workFlows.Select(p=>p.WorkFlowId).ToList();
                var workFlowSteps=_managerWorkFlowStep.GetWorkFlowStepsByWorkFlowIds(rootTenantId,workFlowIds);
                var workFlowInnerSteps=_managerInnerStep.GetWorkFlowInnerStepByWorkFlowIds(rootTenantId,workFlowIds);
                var workFlowOperations=_managerOperation.GetWorkFlowOperationsByWorkFlowIds(rootTenantId,workFlowIds);
                var workFlowProcess=_managerWorkFlowProcess.GetWorkFlowProcessByWorkFlowIds(rootTenantId,workFlowIds);
                var workFlowProcessTasks=_managerWorkFlowProcessTask.GetWorkFlowProcessTaskByWorkFlowIds(rootTenantId,workFlowIds);
                var workFlowRoles=_managerWorkFlowRole.GetWorkFlowRolesByWorkFlowIds(rootTenantId,workFlowIds);

                List<WorkFlowInfo> newWorkFlows=new List<WorkFlowInfo>();
                List<WorkFlowStepInfo> newWorkFlowSteps=new List<WorkFlowStepInfo>();
                List<WorkFlowInnerStepInfo> newInnerSteps=new List<WorkFlowInnerStepInfo>();
                List<WorkFlowOperationInfo> newOperations=new List<WorkFlowOperationInfo>();
                List<WorkFlowProcessInfo> newProcess=new List<WorkFlowProcessInfo>();
                List<WorkFlowProcessTaskInfo> newProcessTasks=new List<WorkFlowProcessTaskInfo>();
                List<WorkFlowRoleInfo> newWorkFlowRoles=new List<WorkFlowRoleInfo>();

                foreach(var workFlow in workFlows)
                {
                    var itsWorkFlow = new WorkFlowInfo();                   
                    DataUtility.CopyPropertiesTo(workFlow, itsWorkFlow);
                    itsWorkFlow.WorkFlowId=Guid.NewGuid();

                    //Filter steps
                    var filterWorkFlowSteps=(from workFlowStep in workFlowSteps where workFlowStep.WorkFlowId==workFlow.WorkFlowId select workFlowStep).ToList();
                    foreach(var filterWorkFlowStep in filterWorkFlowSteps)
                    {
                        var itsStep=new WorkFlowStepInfo();
                        DataUtility.CopyPropertiesTo(filterWorkFlowStep, itsStep);                        
                        itsStep.WorkFlowId=itsWorkFlow.WorkFlowId;
                        itsStep.WorkFlowStepId=Guid.NewGuid();

                        //Filter Inner steps
                        var filterInnerSteps=(from innerStep in workFlowInnerSteps where innerStep.WorkFlowStepId==filterWorkFlowStep.WorkFlowStepId 
                                                                                        &&  innerStep.WorkFlowId==workFlow.WorkFlowId select innerStep ).ToList();

                        foreach(var filterInnerStep in filterInnerSteps)
                        {
                            var itsInnerStep= new WorkFlowInnerStepInfo(); ;
                            DataUtility.CopyPropertiesTo(filterInnerStep, itsInnerStep);  
                            itsInnerStep.InnerStepId=Guid.NewGuid();
                            itsInnerStep.WorkFlowId=itsWorkFlow.WorkFlowId;
                            itsInnerStep.WorkFlowStepId=itsStep.WorkFlowStepId;
                            //Filter Process
                           var filterProcess=(from workFlowProcs in workFlowProcess where workFlowProcs.OperationOrTransactionId==filterInnerStep.InnerStepId 
                                                                                         &&  workFlowProcs.WorkFlowId==workFlow.WorkFlowId  select workFlowProcs ).ToList();

                            foreach(var filterProces in filterProcess)
                            {
                                var itsProcess=new WorkFlowProcessInfo(); 
                                DataUtility.CopyPropertiesTo(filterProces, itsProcess);
                                
                                itsProcess.WorkFlowProcessId=Guid.NewGuid();
                                itsProcess.WorkFlowId=itsWorkFlow.WorkFlowId;
                                itsProcess.OperationOrTransactionId=itsInnerStep.InnerStepId;

                                //Filter process tasks
                               var filterProcessTasks=(from processTask in workFlowProcessTasks where processTask.WorkFlowProcessId==filterProces.WorkFlowProcessId 
                                                                                   && processTask.WorkFlowId==workFlow.WorkFlowId select processTask ) .ToList();

                                    foreach(var filterProcessTask in filterProcessTasks)
                                    {
                                        var itsTasks=new WorkFlowProcessTaskInfo() ;
                                        DataUtility.CopyPropertiesTo(filterProcessTask, itsTasks);
                                        itsTasks.WorkFlowProcessTaskId=Guid.NewGuid();
                                        itsTasks.WorkFlowId=itsWorkFlow.WorkFlowId;
                                        itsTasks.WorkFlowProcessId=itsProcess.WorkFlowProcessId;
                                    newProcessTasks.Add(itsTasks);    
                                    }

                            newProcess.Add(itsProcess);
                            }

                        newInnerSteps.Add(itsInnerStep);
                        }                      

                        //Filter roles

                        var filterRoles=(from workFlowRole in workFlowRoles where workFlowRole.WorkFlowStepId==filterWorkFlowStep.WorkFlowStepId 
                                                                                        &&  workFlowRole.WorkFlowId==workFlow.WorkFlowId select workFlowRole ).ToList();

                            foreach(var filterRole in filterRoles)
                            {
                                var roleId=Guid.Empty;
                                var rootTenantRoleFilter=(from allRole in allRoles where allRole.RoleId==filterRole.RoleId select allRole).ToList();
                                if(rootTenantRoleFilter.Count>0)
                                {
                                   roleId=(from newRole in newRoles where newRole.Name==rootTenantRoleFilter[0].Name select newRole.RoleId).FirstOrDefault();
                                   
                                }
                                var itsRole=new WorkFlowRoleInfo() ;
                                 DataUtility.CopyPropertiesTo(filterRole, itsRole);
                                itsRole.RoleAssignmetId=Guid.NewGuid();
                                itsRole.WorkFlowId=itsWorkFlow.WorkFlowId;
                                itsRole.WorkFlowStepId=itsStep.WorkFlowStepId;
                                itsRole.RoleId=roleId;

                            newWorkFlowRoles.Add(itsRole);    
                            }

                    newWorkFlowSteps.Add(itsStep);
                    }

                      //Filter Operation*****************************************
                      var filterOperations=(from workFlowOperation in workFlowOperations where workFlowOperation.WorkFlowId==workFlow.WorkFlowId select workFlowOperation ).ToList();
                      foreach(var filterOperation in filterOperations)
                      {
                          var itsOperation=new WorkFlowOperationInfo() ;
                          DataUtility.CopyPropertiesTo(filterOperation, itsOperation);
                          itsOperation.WorkFlowOperationId=Guid.NewGuid();
                          itsOperation.WorkFlowId=itsWorkFlow.WorkFlowId;

                          //filter process
                          var filterProcess=(from workFlowProcs in workFlowProcess where workFlowProcs.OperationOrTransactionId==filterOperation.WorkFlowOperationId 
                                                                                         &&  workFlowProcs.WorkFlowId==workFlow.WorkFlowId  select workFlowProcs ).ToList();

                        foreach(var filterProces in filterProcess)
                            {                             

                                var itsProcess=new WorkFlowProcessInfo(); 
                                DataUtility.CopyPropertiesTo(filterProces, itsProcess);

                                itsProcess.WorkFlowProcessId=Guid.NewGuid();
                                itsProcess.WorkFlowId=itsWorkFlow.WorkFlowId;
                                itsProcess.OperationOrTransactionId=itsOperation.WorkFlowOperationId;

                                //Filter process tasks
                               var filterProcessTasks=(from processTask in workFlowProcessTasks where processTask.WorkFlowProcessId==filterProces.WorkFlowProcessId 
                                                                                   && processTask.WorkFlowId==workFlow.WorkFlowId select processTask ) .ToList();

                                    foreach(var filterProcessTask in filterProcessTasks)
                                    {
                                         var itsTasks=new WorkFlowProcessTaskInfo() ;
                                        DataUtility.CopyPropertiesTo(filterProcessTask, itsTasks);
                                        itsTasks.WorkFlowProcessTaskId=Guid.NewGuid();
                                        itsTasks.WorkFlowId=itsWorkFlow.WorkFlowId;
                                        itsTasks.WorkFlowProcessId=itsProcess.WorkFlowProcessId;
                                    newProcessTasks.Add(itsTasks);    
                                    }

                            newProcess.Add(itsProcess);
                            }


                      newOperations.Add(itsOperation);
                      }
                       

                newWorkFlows.Add(itsWorkFlow);
                }
                
                //Init workflow
                if(newWorkFlows.Count>0)
                   _managerWorkFlow.CreateWorkFlows(newTenantId,newWorkFlows);
                if(newOperations.Count>0)
                _managerOperation.CreateWorkFlowOperations(newTenantId,newOperations);
                if(newWorkFlowSteps.Count>0)
                _managerWorkFlowStep.CreateWorkFlowSteps(newTenantId,newWorkFlowSteps);
                if(newInnerSteps.Count>0)
                _managerInnerStep.CreateWorkFlowInnerSteps(newTenantId,newInnerSteps);
                if(newProcess.Count>0)
                _managerWorkFlowProcess.CreateWorkFlowProcess(newTenantId,newProcess);
                if(newProcessTasks.Count>0)
                _managerWorkFlowProcessTask.CreateWorkFlowProcessTasks(newTenantId,newProcessTasks);
                if(newWorkFlowRoles.Count>0)
                _managerWorkFlowRole.CreateWorkFlowRoles(newTenantId,newWorkFlowRoles);
                
            
            }           

            return true;

        }

        List<WorkFlowInfo> IManagerWorkFlowSecurity.GetWorkFlowsByUserCode(Guid tenantId, Guid userId,bool isSuperAdmin)
        {
            var workFlowSteps = _managerWorkFlowStep.GetWorkFlowStepsByUserId(tenantId, userId,isSuperAdmin);
            var workFlows = _managerWorkFlow.GetWorkFlowsByIds(tenantId, workFlowSteps.Select(p => p.WorkFlowId).Distinct().ToList());
            var workFlowOperations = _managerOperation.GetWorkFlowOperationsByWorkFlowIds(tenantId, workFlows.Select(p => p.WorkFlowId).ToList());

            var workFlowInnerSteps = _managerInnerStep.GetWorkFlowInnerStep_ByStepIds(tenantId, workFlowSteps.Select(p => p.WorkFlowStepId).ToList());
            var operationOrTransactionIds = new List<Guid>();
            operationOrTransactionIds.AddRange(workFlowOperations.Select(p => p.WorkFlowOperationId).ToList());
            operationOrTransactionIds.AddRange(workFlowInnerSteps.Select(p => p.InnerStepId).ToList());
            var workFlowProcess = _managerWorkFlowProcess.GetWorkFlowProcessByOperationOrTransitionIds(tenantId, operationOrTransactionIds);
            var processTasks = _managerWorkFlowProcessTask.GetWorkFlowProcessTask_ByProcessIds(tenantId, workFlowProcess.Select(proc => proc.WorkFlowProcessId).ToList());
            var workFlowRoles = _managerWorkFlowRole.GetWorkFlowRolesByStepIds(tenantId, workFlowSteps.Select(p => p.WorkFlowStepId).ToList());
            foreach (var workFlow in workFlows)
            {
                //get its operations
                workFlow.Operations = (from workFlowOperation in workFlowOperations where workFlowOperation.WorkFlowId == workFlow.WorkFlowId select workFlowOperation).ToList();
                //set process to operation
                foreach (var operation in workFlow.Operations)
                {
                    operation.WorkFlowProcess = (from workFlowPro in workFlowProcess where workFlowPro.OperationOrTransactionId == operation.WorkFlowOperationId 
                                                                                                                && workFlowPro.WorkFlowId==workFlow.WorkFlowId  select workFlowPro).ToList();
                    foreach (var process in operation.WorkFlowProcess)
                    {
                        process.WorkFlowProcessTasks = new List<WorkFlowProcessTaskInfo>();
                        if (process.ProcessType == (int)WorkFlowProcessType.PreProcess)
                            process.WorkFlowProcessTasks.AddRange((from allProcessTask in processTasks
                                                                   where process.WorkFlowProcessId == allProcessTask.WorkFlowProcessId && process.WorkFlowId==workFlow.WorkFlowId 
                                                                   orderby allProcessTask.SequenceCode
                                                                   select allProcessTask).ToList());

                        if (process.ProcessType == (int)WorkFlowProcessType.Process)
                            process.WorkFlowProcessTasks.AddRange((from allProcessTask in processTasks
                                                                   where process.WorkFlowProcessId == allProcessTask.WorkFlowProcessId  && process.WorkFlowId==workFlow.WorkFlowId 
                                                                   orderby allProcessTask.SequenceCode
                                                                   select allProcessTask).ToList());

                        if (process.ProcessType == (int)WorkFlowProcessType.PostProcess)
                            process.WorkFlowProcessTasks.AddRange((from allProcessTask in processTasks
                                                                   where process.WorkFlowProcessId == allProcessTask.WorkFlowProcessId  && process.WorkFlowId==workFlow.WorkFlowId 
                                                                   orderby allProcessTask.SequenceCode
                                                                   select allProcessTask).ToList());
                    }
                }

                //get its steps
                workFlow.Steps = new List<WorkFlowStepInfo>();
                var allSteps = WorkFlowHelper.GetAllSteps(workFlow.EntityId);
                var itsWorkFlowSteps = (from workFlowStep in workFlowSteps where workFlowStep.WorkFlowId == workFlow.WorkFlowId select workFlowStep).ToList();

                foreach (var itsWorkFlowStep in itsWorkFlowSteps)
                {
                    //filter its role
                    itsWorkFlowStep.Roles = (from workFlowRole in workFlowRoles where workFlowRole.WorkFlowStepId == itsWorkFlowStep.WorkFlowStepId 
                                                                                                         && workFlowRole.WorkFlowId==workFlow.WorkFlowId select workFlowRole).ToList();
                    var filteredStep = (from allStep in allSteps where itsWorkFlowStep.TransitionType.Id == allStep.Id select allStep).FirstOrDefault();
                    itsWorkFlowStep.TransitionType.Name = filteredStep.Value;
                    //filter its inner steps
                    var itsInnerSteps = (from workFlowInnerStep in workFlowInnerSteps where workFlowInnerStep.WorkFlowStepId == itsWorkFlowStep.WorkFlowStepId 
                                                                                                         && workFlowInnerStep.WorkFlowId==workFlow.WorkFlowId select workFlowInnerStep).ToList();
                    if (itsInnerSteps.Count > 0)
                    {
                        //filter Inner step process
                        foreach (var itsInnerStep in itsInnerSteps)
                        {
                            itsInnerStep.WorkFlowProcess = (from workFlowPro in workFlowProcess where workFlowPro.OperationOrTransactionId == itsInnerStep.InnerStepId 
                            &&  workFlowPro.WorkFlowId==workFlow.WorkFlowId select workFlowPro).ToList();

                            foreach (var process in itsInnerStep.WorkFlowProcess)
                            {
                                process.WorkFlowProcessTasks = new List<WorkFlowProcessTaskInfo>();
                                if (process.ProcessType == (int)WorkFlowProcessType.PreProcess)
                                    process.WorkFlowProcessTasks.AddRange((from allProcessTask in processTasks
                                                                           where process.WorkFlowProcessId == allProcessTask.WorkFlowProcessId &&  process.WorkFlowId==workFlow.WorkFlowId
                                                                           orderby allProcessTask.SequenceCode
                                                                           select allProcessTask).ToList());

                                if (process.ProcessType == (int)WorkFlowProcessType.Process)
                                    process.WorkFlowProcessTasks.AddRange((from allProcessTask in processTasks
                                                                           where process.WorkFlowProcessId == allProcessTask.WorkFlowProcessId &&  process.WorkFlowId==workFlow.WorkFlowId
                                                                           orderby allProcessTask.SequenceCode
                                                                           select allProcessTask).ToList());

                                if (process.ProcessType == (int)WorkFlowProcessType.PostProcess)
                                    process.WorkFlowProcessTasks.AddRange((from allProcessTask in processTasks
                                                                           where process.WorkFlowProcessId == allProcessTask.WorkFlowProcessId &&  process.WorkFlowId==workFlow.WorkFlowId
                                                                           orderby allProcessTask.SequenceCode
                                                                           select allProcessTask).ToList());
                            }

                        }
                        itsWorkFlowStep.InnerSteps = new List<WorkFlowInnerStepInfo>(itsInnerSteps);
                    }
                    workFlow.Steps.Add(itsWorkFlowStep);
                }
            }
            foreach(var workFlow in workFlows)
            {
                 workFlow.EntityId = iMetadataManager.GetEntityNameByEntityContext(workFlow.EntityId);
                 var subTypes=iMetadataManager.GetSubTypesDetails(workFlow.EntityId);
                 foreach(var subType in subTypes)
                 {
                     if(subType.Key==workFlow.SubTypeCode)
                     workFlow.SubTypeCode=subType.Value;
                 }
                

            }
            return workFlows;
        }
       

        bool IManagerWorkFlowSecurity.InitializeRootTenantWorkFlow(Guid tenantId)
        {

            var allEntities = iMetadataManager.GetEntities(false);
            // Filtwer all  Workflow supported entity
            var workFlowEntities = (from allEntity in allEntities where allEntity.SupportWorkflow  select allEntity).ToList();
            foreach (var workFlowEntity in workFlowEntities)
            {
                var entityContextAll = iMetadataManager.GetEntitityByName(workFlowEntity.Name);
                var entityContext = iMetadataManager.GetEntityContextByEntityName(workFlowEntity.Name);
                foreach (var subtype in workFlowEntity.Subtypes)
                {
                    var workflowInfo = new WorkFlowInfo
                    {
                        WorkFlowId = Guid.NewGuid(),
                        EntityId = entityContext,
                        Status = true,
                        SubTypeCode = iMetadataManager.GetSubTypeId(workFlowEntity.Name, subtype)
                    };
                    _managerWorkFlow.CreateWorkFlow(tenantId, workflowInfo);

                    //all steps from static classes (Transition) 
                    List<WorkFlowResource> allSteps = WorkFlowHelper.GetAllSteps(entityContext);
                    var count = 0;
                    foreach (var allStep in allSteps)
                    {
                        var workFlowStep = new WorkFlowStepInfo
                        {
                            WorkFlowId = workflowInfo.WorkFlowId,
                            TransitionType = new ItemName { Id = allStep.Id },
                            SequenceNumber = count++
                        };
                        workFlowStep.WorkFlowStepId = _managerWorkFlowStep.CreateWorkFlowStep(tenantId, workFlowStep);
                    }

                    // (Operations)
                    List<WorkFlowOperationInfo> workFlowOperations = new List<WorkFlowOperationInfo>();
                    foreach (var entityCon in entityContextAll.Operations)
                    {
                        if (entityCon.Name == "Create")
                        {
                            workFlowOperations.Add(new WorkFlowOperationInfo
                            {
                                WorkFlowOperationId = Guid.NewGuid(),
                                OperationType = WorkFlowOperationType.Create,
                                WorkFlowId = workflowInfo.WorkFlowId
                            });
                        }
                        else if (entityCon.Name == "Update")
                        {
                            workFlowOperations.Add(new WorkFlowOperationInfo
                            {
                                WorkFlowOperationId = Guid.NewGuid(),
                                OperationType = WorkFlowOperationType.Update,
                                WorkFlowId = workflowInfo.WorkFlowId
                            });
                        }
                        else if (entityCon.Name == "Delete")
                        {
                            workFlowOperations.Add(new WorkFlowOperationInfo
                            {
                                WorkFlowOperationId = Guid.NewGuid(),
                                OperationType = WorkFlowOperationType.Delete,
                                WorkFlowId = workflowInfo.WorkFlowId
                            });
                        }
                        else if (entityCon.Name == "UpdateStatus")
                        {
                            workFlowOperations.Add(new WorkFlowOperationInfo
                            {
                                WorkFlowOperationId = Guid.NewGuid(),
                                OperationType = WorkFlowOperationType.UpdateStatus,
                                WorkFlowId = workflowInfo.WorkFlowId
                            });
                        }
                    }
                    _managerOperation.CreateWorkFlowOperations(tenantId, workFlowOperations);
                    count = 0;
                    List<WorkFlowProcessInfo> workFlowProcesses = new List<WorkFlowProcessInfo>();
                    foreach (var workFlowOperation in workFlowOperations)
                    {
                        workFlowProcesses.Add(new WorkFlowProcessInfo
                        {
                            WorkFlowProcessId = Guid.NewGuid(),
                            WorkFlowId = workflowInfo.WorkFlowId,
                            OperationOrTransactionId = workFlowOperation.WorkFlowOperationId,
                            OperationOrTransactionType = (int)OperationOrTransactionType.Operation,
                            ProcessType = (int)WorkFlowProcessType.PreProcess
                        });

                        workFlowProcesses.Add(new WorkFlowProcessInfo
                        {
                            WorkFlowProcessId = Guid.NewGuid(),
                            WorkFlowId = workflowInfo.WorkFlowId,
                            OperationOrTransactionId = workFlowOperation.WorkFlowOperationId,
                            OperationOrTransactionType = (int)OperationOrTransactionType.Operation,
                            ProcessType = (int)WorkFlowProcessType.Process
                        });
                        workFlowProcesses.Add(new WorkFlowProcessInfo
                        {
                            WorkFlowProcessId = Guid.NewGuid(),
                            WorkFlowId = workflowInfo.WorkFlowId,
                            OperationOrTransactionId = workFlowOperation.WorkFlowOperationId,
                            OperationOrTransactionType = (int)OperationOrTransactionType.Operation,
                            ProcessType = (int)WorkFlowProcessType.PostProcess
                        });
                    }
                    _managerWorkFlowProcess.CreateWorkFlowProcess(tenantId, workFlowProcesses);
                }

            }

            return true;

        }

         bool IManagerWorkFlowSecurity.InitializeRootTenantWorkFlow(Guid tenantId,string entityId)
        {

            var allEntities = iMetadataManager.GetEntities(false);
            // Filtwer all  Workflow supported entity
            var workFlowEntities = (from allEntity in allEntities where allEntity.SupportWorkflow && allEntity.Name==entityId  select allEntity).ToList();
            foreach (var workFlowEntity in workFlowEntities)
            {
                var entityContextAll = iMetadataManager.GetEntitityByName(workFlowEntity.Name);
                var entityContext = iMetadataManager.GetEntityContextByEntityName(workFlowEntity.Name);
                foreach (var subtype in workFlowEntity.Subtypes)
                {
                    var workflowInfo = new WorkFlowInfo
                    {
                        WorkFlowId = Guid.NewGuid(),
                        EntityId = entityContext,
                        Status = true,
                        SubTypeCode = iMetadataManager.GetSubTypeId(workFlowEntity.Name, subtype)
                    };
                    _managerWorkFlow.CreateWorkFlow(tenantId, workflowInfo);

                    //all steps from static classes (Transition) 
                    List<WorkFlowResource> allSteps = WorkFlowHelper.GetAllSteps(entityContext);
                    var count = 0;
                    foreach (var allStep in allSteps)
                    {
                        var workFlowStep = new WorkFlowStepInfo
                        {
                            WorkFlowId = workflowInfo.WorkFlowId,
                            TransitionType = new ItemName { Id = allStep.Id },
                            SequenceNumber = count++
                        };
                        workFlowStep.WorkFlowStepId = _managerWorkFlowStep.CreateWorkFlowStep(tenantId, workFlowStep);
                    }

                    // (Operations)
                    List<WorkFlowOperationInfo> workFlowOperations = new List<WorkFlowOperationInfo>();
                    foreach (var entityCon in entityContextAll.Operations)
                    {
                        if (entityCon.Name == "Create")
                        {
                            workFlowOperations.Add(new WorkFlowOperationInfo
                            {
                                WorkFlowOperationId = Guid.NewGuid(),
                                OperationType = WorkFlowOperationType.Create,
                                WorkFlowId = workflowInfo.WorkFlowId
                            });
                        }
                        else if (entityCon.Name == "Update")
                        {
                            workFlowOperations.Add(new WorkFlowOperationInfo
                            {
                                WorkFlowOperationId = Guid.NewGuid(),
                                OperationType = WorkFlowOperationType.Update,
                                WorkFlowId = workflowInfo.WorkFlowId
                            });
                        }
                        else if (entityCon.Name == "Delete")
                        {
                            workFlowOperations.Add(new WorkFlowOperationInfo
                            {
                                WorkFlowOperationId = Guid.NewGuid(),
                                OperationType = WorkFlowOperationType.Delete,
                                WorkFlowId = workflowInfo.WorkFlowId
                            });
                        }
                        else if (entityCon.Name == "UpdateStatus")
                        {
                            workFlowOperations.Add(new WorkFlowOperationInfo
                            {
                                WorkFlowOperationId = Guid.NewGuid(),
                                OperationType = WorkFlowOperationType.UpdateStatus,
                                WorkFlowId = workflowInfo.WorkFlowId
                            });
                        }
                    }
                    _managerOperation.CreateWorkFlowOperations(tenantId, workFlowOperations);
                    count = 0;
                    List<WorkFlowProcessInfo> workFlowProcesses = new List<WorkFlowProcessInfo>();
                    foreach (var workFlowOperation in workFlowOperations)
                    {
                        workFlowProcesses.Add(new WorkFlowProcessInfo
                        {
                            WorkFlowProcessId = Guid.NewGuid(),
                            WorkFlowId = workflowInfo.WorkFlowId,
                            OperationOrTransactionId = workFlowOperation.WorkFlowOperationId,
                            OperationOrTransactionType = (int)OperationOrTransactionType.Operation,
                            ProcessType = (int)WorkFlowProcessType.PreProcess
                        });

                        workFlowProcesses.Add(new WorkFlowProcessInfo
                        {
                            WorkFlowProcessId = Guid.NewGuid(),
                            WorkFlowId = workflowInfo.WorkFlowId,
                            OperationOrTransactionId = workFlowOperation.WorkFlowOperationId,
                            OperationOrTransactionType = (int)OperationOrTransactionType.Operation,
                            ProcessType = (int)WorkFlowProcessType.Process
                        });
                        workFlowProcesses.Add(new WorkFlowProcessInfo
                        {
                            WorkFlowProcessId = Guid.NewGuid(),
                            WorkFlowId = workflowInfo.WorkFlowId,
                            OperationOrTransactionId = workFlowOperation.WorkFlowOperationId,
                            OperationOrTransactionType = (int)OperationOrTransactionType.Operation,
                            ProcessType = (int)WorkFlowProcessType.PostProcess
                        });
                    }
                    _managerWorkFlowProcess.CreateWorkFlowProcess(tenantId, workFlowProcesses);
                }

            }

            return true;

        }

        // List<object> IManagerWorkFlowSecurity.GetWorkFlowSecurity(Guid tenantId, string entityname)
        // {
        //     var mainObj=new List<object>();
        //     var entityId= iMetadataManager.GetEntityContextByEntityName(entityname);            
        //     var workFlowItems= _managerWorkFlow.GetWorkFlows(tenantId,entityname);           

        //     var allSteps = WorkFlowHelper.GetAllSteps(entityId); 
        //     var workFlowSteps=_managerWorkFlowStep.GetWorkFlowSteps(tenantId,workFlowItems.Select(p=>p.WorkFlowId).ToList());
        //     var roles=_managerWorkFlowRole.GetWorkFlowRoles(tenantId,workFlowItems.Select(p=>p.WorkFlowId).ToList());  
        //     foreach(var workFlowItem in workFlowItems)
        //     {
        //         var thisWorkFlowRoles=(from role in roles where role.WorkFlowId==workFlowItem.WorkFlowId select role).ToList();

        //         var workFLowObj=new {
        //                 EntityId=workFlowItem.EntityId,
        //                 SubTypeCode=workFlowItem.SubTypeCode,
        //                 WorkFlowId=workFlowItem.WorkFlowId,
        //                 Steps=new List<object>(),
        //                 RoleList=new List<object>()

        //                 // ActivatedList=new List<object>(),
        //                 // AccessedList=new List<object>(),
        //                 // AssignedList=new List<object>()
        //             };

        //        // workFlowItem.Steps=new List<WorkFlowStepInfo>();
        //         var thisWorkFLowSteps=(from workFlowStep in workFlowSteps where workFlowStep.WorkFlowId==workFlowItem.WorkFlowId select workFlowStep).ToList();                    
        //         foreach(var step in allSteps)
        //             {                        
        //                 var checkStepsSaved=(from workFlowStep in thisWorkFLowSteps where workFlowStep.TransitionType.Id==step.Id select workFlowStep).FirstOrDefault();
        //                 if(checkStepsSaved!=null)
        //                 {
        //                     workFLowObj.Steps.Add(new {
        //                         WorkFlowStepId=checkStepsSaved.WorkFlowStepId,
        //                         Name=step.Value                                
        //                     });  
        //                     var checkRoles=(from role in roles where role.WorkFlowStepId==checkStepsSaved.WorkFlowStepId select role).ToList();
        //                     if(checkRoles.Count>0)
        //                     {
        //                         foreach(var checkRole in checkRoles)
        //                         {                                
        //                                 var rolObj=new{
        //                                     checkRole.RoleAssignmetId,
        //                                     checkRole.RoleId,
        //                                     checkRole.WorkFlowId,
        //                                     checkRole.WorkFlowStepId,
        //                                     name=step.Value,
        //                                     checkRole.AssignmentOperationType
        //                                 };
        //                                 workFLowObj.RoleList.Add(rolObj);

        //                                 // if(checkRole.AssignmentOperationType==(int)WorkFlowRoleType.ActivatedRoles)
        //                                 // workFLowObj.ActivatedList.Add(rolObj);
        //                                 // if(checkRole.AssignmentOperationType==(int)WorkFlowRoleType.AccessedRoles)
        //                                 // workFLowObj.AccessedList.Add(rolObj);
        //                                 // if(checkRole.AssignmentOperationType==(int)WorkFlowRoleType.AssignedRoles)
        //                                 // workFLowObj.AssignedList.Add(rolObj);
        //                         }
        //                         //workFLowObj.Roles.Add);
        //                     }

        //                 }                              
        //             }

        //             mainObj.Add(workFLowObj); 
        //     }

        //     return mainObj;
        // }


    }
}


