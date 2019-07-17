using NLog;
using System;
using System.Reflection;
using VPC.Entities.WorkFlow;
using VPC.Framework.Business.WorkFlow.Attribute;
using VPC.Framework.Business.WorkFlow.Contracts;
namespace VPC.Framework.Business.WorkFlow.APIs
{
    public interface IWorkFlowProcess
    {
        // WorkFlowProcessMessage Process(Guid? typeName, Guid context, dynamic obj);
        // WorkFlowProcessMessage CompleteTransition(Guid? typeName, Guid context, dynamic obj);
        // WorkFlowProcessMessage CompleteOperation(Guid? typeName, Guid context, dynamic obj);
         WorkFlowProcessMessage OperationProcess(Guid? typeName, dynamic obj);
         WorkFlowProcessMessage TransitionProcess(Guid? typeName, dynamic data);
    }
    internal sealed  class WorkFlowProcess : IWorkFlowProcess
    {
       private readonly Logger _log = LogManager.GetCurrentClassLogger();

        // WorkFlowProcessMessage IWorkFlowProcess.Process(Guid? typeName, Guid context, dynamic data)
        // {
        //     try
        //     {
        //         var result = new WorkFlowProcessMessage();

        //         if (typeName != null)
        //         {
        //             if (typeName == Guid.Empty)
        //             {
        //                 result.Success = true;
        //                 return result;
        //             }
        //         }
        //         else
        //         {
        //             result.Success = true;
        //             return result;
        //         }

        //         var type = typeof(ITransition);
        //         var myType = Helper.GetTransitionType(typeName);

        //         if (myType != null)
        //         {
        //             var myObject = Activator.CreateInstance(myType);
        //             // Retrieve the method you are looking for
        //             MethodInfo preMethodInfo = type.GetMethod("PreProcess");
        //             //PostProcess
        //             // Invoke the method on the instance we created above

        //             result = (WorkFlowProcessMessage)preMethodInfo.Invoke(myObject, new object[] { data });

        //             if (result.Success)
        //             {
        //                 MethodInfo postMethodInfo = type.GetMethod("PostProcess");
        //                 result = (WorkFlowProcessMessage)postMethodInfo.Invoke(myObject, new object[] { data });
        //                 if (!result.Success)
        //                 {
        //                     if (result.WarningMessage != null)
        //                         throw new CustomWorkflowException<WorkFlowMessage>(result.WarningMessage.Code);
        //                     else  if (result.ErrorMessage != null)
        //                         throw new CustomWorkflowException<WorkFlowMessage>(result.ErrorMessage.Code);
        //                     else if (result.ObjectMessage != null)
        //                         throw new CustomWorkflowException<ObjectMessage>(result.ObjectMessage);
        //                     else
        //                         throw new CustomWorkflowException<WorkFlowMessage>(WorkFlowMessage.None);
        //                 }
        //             }
        //             else
        //             {
        //                 if (result.WarningMessage != null)
        //                     throw new CustomWorkflowException<WorkFlowMessage>(result.WarningMessage.Code);
        //                 else if (result.ErrorMessage != null)
        //                     throw new CustomWorkflowException<WorkFlowMessage>(result.ErrorMessage.Code);
        //                 else if (result.ObjectMessage != null)
        //                     throw new CustomWorkflowException<ObjectMessage>(result.ObjectMessage);
        //                 else
        //                     throw new CustomWorkflowException<WorkFlowMessage>(WorkFlowMessage.None);
        //             }
        //         }
        //         else
        //         {
        //             throw new CustomWorkflowException<WorkFlowMessage>(WorkFlowMessage.ClassNotFound);
        //         }
        //         return result;
        //     }

        //     catch (CustomWorkflowException<WorkFlowMessage>)
        //     {
        //         throw;
        //     }
        //     catch (System.Exception ex)
        //     {
        //         _log.Error("IWorkFlowProcess.Process with ProcessContext {0} having System.Exception message {1}", typeName, ex.Message);
        //         throw;
        //     }

        // }

        // WorkFlowProcessMessage IWorkFlowProcess.CompleteTransition(Guid? typeName, Guid context, dynamic obj)
        // {
        //     try
        //     {
        //         var result = new WorkFlowProcessMessage();

        //         if (typeName != null)
        //         {
        //             if (typeName == Guid.Empty)
        //             {
        //                 result.Success = true;
        //                 return result;
        //             }
        //         }
        //         else
        //         {
        //             result.Success = true;
        //             return result;
        //         }

        //         var type = typeof(ITransition);
        //         var myType = Helper.GetTransitionType(typeName);

        //         if (myType != null)
        //         {
        //             var myObject = Activator.CreateInstance(myType);
        //             // Retrieve the method you are looking for
        //             MethodInfo preMethodInfo = type.GetMethod("OnCompleteProcess");
        //             //PostProcess
        //             // Invoke the method on the instance we created above

        //             result = (WorkFlowProcessMessage)preMethodInfo.Invoke(myObject, new object[] { obj });
        //         }
        //         if (result.Success)
        //         {
        //             return result;
        //         }
        //        _log.Error("Work flow on complete failed! for type: " + typeName);
        //         result.Success = true;
        //         return result;
        //     }

        //     catch (CustomWorkflowException<WorkFlowMessage> ex)
        //     {
        //         _log.Error("IWorkFlowProcess.CompleteTransition with ProcessContext {0} having CustomWorkflowException message {1}", typeName, ex);
        //         var result = new WorkFlowProcessMessage { Success = true };
        //         return result;
        //     }
        //     catch (System.Exception ex)
        //     {
        //       _log.Error("IWorkFlowProcess.CompleteTransition with ProcessContext {0} having System.Exception message {1}", typeName, ex);
        //         var result = new WorkFlowProcessMessage { Success = true };
        //         return result;
        //     }
        // }

        // WorkFlowProcessMessage IWorkFlowProcess.CompleteOperation(Guid? typeName, Guid context, dynamic obj)
        // {
        //     try
        //     {
        //         var result = new WorkFlowProcessMessage();

        //         if (typeName != null)
        //         {
        //             if (typeName == Guid.Empty)
        //             {
        //                 result.Success = true;
        //                 return result;
        //             }
        //         }
        //         else
        //         {
        //             result.Success = true;
        //             return result;
        //         }

        //         var type = typeof(IOperation);
        //         var myType = Helper.GetOperationType(typeName);

        //         if (myType != null)
        //         {
        //             var myObject = Activator.CreateInstance(myType);
        //             // Retrieve the method you are looking for
        //             MethodInfo preMethodInfo = type.GetMethod("OnCompleteProcess");
        //             //PostProcess
        //             // Invoke the method on the instance we created above

        //             result = (WorkFlowProcessMessage)preMethodInfo.Invoke(myObject, new object[] { obj });
        //         }
        //         if (result.Success)
        //         {
        //             return result;
        //         }
        //        _log.Error("Work flow on complete failed! for type: " + typeName);
        //         result.Success = true;
        //         return result;
        //     }

        //     catch (CustomWorkflowException<WorkFlowMessage> ex)
        //     {
        //        _log.Error("IWorkFlowProcess.CompleteOperation with ProcessContext {0} having CustomWorkflowException message {1}", typeName, ex);
        //         var result = new WorkFlowProcessMessage { Success = true };
        //         return result;
        //     }
        //     catch (System.Exception ex)
        //     {
        //        _log.Error("IWorkFlowProcess.CompleteOperation with ProcessContext {0} having System.Exception message {1}", typeName, ex);
        //         var result = new WorkFlowProcessMessage { Success = true };
        //         return result;
        //     }
        // }

         WorkFlowProcessMessage IWorkFlowProcess.TransitionProcess(Guid? typeName, dynamic data)
        {
            try
            {
                WorkFlowProcessMessage result = null;
                var type = typeof(ITransition);

                var myType = WorkFlowHelper.GetTransitionType(typeName.Value);

                if (myType != null)
                {                   
                        var myObject = Activator.CreateInstance(myType);                   
                        MethodInfo executeMethod = type.GetMethod("Execute");
                        result = (WorkFlowProcessMessage)executeMethod.Invoke(myObject, new object[] { data });
                        if (!result.Success)
                        {
                            if (result.WarningMessage != null)
                                throw new CustomWorkflowException<WorkFlowMessage>(result.WarningMessage.Code);
                            if (result.ErrorMessage != null)
                                throw new CustomWorkflowException<WorkFlowMessage>(result.ErrorMessage.Code);
                        }

                }
                return result;
            }
            catch (CustomWorkflowException<WorkFlowMessage>)
            {
                throw;
            }
            
            catch (System.Exception ex)
            {
                if (ex.InnerException!=null)
                {
                    throw ex.InnerException;
                }
               _log.Error("IWorkFlowProcess.OperationProcess with ProcessContext {0} having System.Exception message {1}", typeName, ex.Message);
                throw;
            }

        }


        WorkFlowProcessMessage IWorkFlowProcess.OperationProcess(Guid? typeName, dynamic data)
        {
            try
            {
                WorkFlowProcessMessage result = null;
                var type = typeof(IOperation);

                var myType = WorkFlowHelper.GetOperationType(typeName.Value);

                if (myType != null)
                {                   
                        var myObject = Activator.CreateInstance(myType);                   
                        MethodInfo executeMethod = type.GetMethod("Execute");
                        result = (WorkFlowProcessMessage)executeMethod.Invoke(myObject, new object[] { data });
                        if (!result.Success)
                        {
                            if (result.WarningMessage != null)
                                throw new CustomWorkflowException<WorkFlowMessage>(result.WarningMessage.Code);
                            if (result.ErrorMessage != null)
                                throw new CustomWorkflowException<WorkFlowMessage>(result.ErrorMessage.Code);
                        }

                }
                return result;
            }
            catch (CustomWorkflowException<WorkFlowMessage>)
            {
                throw;
            }
            
            catch (System.Exception ex)
            {
                if (ex.InnerException!=null)
                {
                    throw ex.InnerException;
                }
               _log.Error("IWorkFlowProcess.OperationProcess with ProcessContext {0} having System.Exception message {1}", typeName, ex.Message);
                throw;
            }

        }

    }
}
