using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using VPC.Entities.Common;
using VPC.Entities.WorkFlow;
using VPC.Framework.Business.Common;
using VPC.Framework.Business.Data;
using VPC.Framework.Business.Data.SqlClient;
namespace VPC.Framework.Business.WorkFlow.Data
{
    internal  sealed  class DataWorkFlowSteps : EntityModelData
    {
       internal bool CreateWorkFlowStep(Guid tenantId, WorkFlowStepInfo workFlowStepInfo)
        {
            try
            {  
                var cmd = CreateProcedureCommand("dbo.WorkFlowStep_Create");
                cmd.AppendGuid("@guidTenantId", tenantId);
                cmd.AppendGuid("@guidWorkFlowStepId", workFlowStepInfo.WorkFlowStepId); 
                cmd.AppendGuid("@guidWorkFlowId", workFlowStepInfo.WorkFlowId); 
                cmd.AppendGuid("@guidTransitionType", workFlowStepInfo.TransitionType.Id); 
                ExecuteCommand(cmd);
                return true;
            }
            catch (SqlException e)
            {
                throw ReportAndTranslateException(e, "WorkFlow::dbo.WorkFlowStep_Create");
            }
        }

         internal bool CreateWorkFlowSteps(Guid tenantId, List<WorkFlowStepInfo> workFlowStepInfos)
        {
            try
            {  
                var cmd = CreateProcedureCommand("dbo.WorkFlowStep_Create_Xml");
                cmd.AppendGuid("@guidTenantId", tenantId);
                cmd.AppendXml("@xmlWorkFlowSteps", DataUtility.GetXmlForWorkFlowStepsCreate(workFlowStepInfos));   
                ExecuteCommand(cmd);
                return true;
            }
            catch (SqlException e)
            {
                throw ReportAndTranslateException(e, "WorkFlow::dbo.WorkFlowStep_Create_Xml");
            }
        }

        internal bool UpdateWorkFlowSteps(Guid tenantId, WorkFlowStepInfo workFlowStepInfo)
        {
            try
            {  
                var cmd = CreateProcedureCommand("dbo.WorkFlowStep_Update");
                cmd.AppendGuid("@guidTenantId", tenantId);
                cmd.AppendGuid("@guidWorkFlowStepId", workFlowStepInfo.WorkFlowStepId); 
                cmd.AppendGuid("@guidWorkFlowId", workFlowStepInfo.WorkFlowId);               
                cmd.AppendBit("@bIsAssigmentMandatory", workFlowStepInfo.IsAssigmentMandatory);  
                if(workFlowStepInfo.AllotedTime.HasValue && workFlowStepInfo.AllotedTime.Value>0)   
                  cmd.AppendInt("@intAllotedTime", workFlowStepInfo.AllotedTime.Value); 
                    if(workFlowStepInfo.CriticalTime.HasValue && workFlowStepInfo.CriticalTime.Value>0)   
                  cmd.AppendInt("@intCriticalTime", workFlowStepInfo.CriticalTime.Value);          
                ExecuteCommand(cmd);
                return true;
            }
            catch (SqlException e)
            {
                throw ReportAndTranslateException(e, "WorkFlow::dbo.WorkFlowStep_Update");
            }
        }

         internal bool DeleteWorkFlowSteps(Guid tenantId, Guid workFlowStepId,Guid workFlowId )
        {
            try
            {  
                var cmd = CreateProcedureCommand("dbo.WorkFlowStep_Delete");
                cmd.AppendGuid("@guidTenantId", tenantId);
                cmd.AppendGuid("@guidWorkFlowStepId", workFlowStepId); 
                cmd.AppendGuid("@guidWorkFlowId", workFlowId);
                ExecuteCommand(cmd);
                return true;
            }
            catch (SqlException e)
            {
                throw ReportAndTranslateException(e, "WorkFlow::dbo.WorkFlowStep_Delete");
            }
        }

        internal bool MoveUpDownWorkFlowSteps(Guid tenantId,  List<WorkFlowStepInfo> workFlowStepInfo )
        {
            try
            {  
                var cmd = CreateProcedureCommand("dbo.WorkFlowStep_MoveUpDown");
                cmd.AppendGuid("@guidTenantId", tenantId);
                cmd.AppendXml("@xmlWorkFlowSteps", DataUtility.GetXmlForWorkFlowStepsSequence(workFlowStepInfo));                 
                ExecuteCommand(cmd);
                return true;
            }
            catch (SqlException e)
            {
                throw ReportAndTranslateException(e, "WorkFlow::dbo.WorkFlowStep_MoveUpDown");
            }
        }

       internal List<WorkFlowStepInfo> GetWorkFlowSteps(Guid tenantId, Guid workFlowId)
        {
            List<WorkFlowStepInfo> lstWorkFlowSteps = new List<WorkFlowStepInfo>();
            try
            {
                var cmd = CreateProcedureCommand("dbo.WorkFlowStep_Get");
                cmd.AppendGuid("@guidTenantId", tenantId);
                cmd.AppendGuid("@guidWorkFlowId", workFlowId);                
                using (SqlDataReader reader = ExecuteCommandAndReturnReader(cmd))
                { 
                        while (reader.Read())
                        {
                            lstWorkFlowSteps.Add(ReadWorkFlowStep(reader));
                        } 
                }
            }
            catch (SqlException e)
            {
                throw ReportAndTranslateException(e, "WorkFlow::WorkFlowStep_Get");
            }
            return lstWorkFlowSteps;
        }

        internal List<WorkFlowStepInfo> GetWorkFlowStepsByWorkFlowIds(Guid tenantId, List<Guid> workFlowIds)
        {
            List<WorkFlowStepInfo> lstWorkFlowSteps = new List<WorkFlowStepInfo>();
            try
            {
                var cmd = CreateProcedureCommand("dbo.WorkFlowSteps_Get_WorkFlowIds");
                cmd.AppendGuid("@guidTenantId", tenantId);
                cmd.AppendXml("@xmlWorkFlowIds", DataUtility.GetXmlForIds(workFlowIds));                
                using (SqlDataReader reader = ExecuteCommandAndReturnReader(cmd))
                { 
                        while (reader.Read())
                        {
                            lstWorkFlowSteps.Add(ReadWorkFlowStep(reader));
                        } 
                }
            }
            catch (SqlException e)
            {
                throw ReportAndTranslateException(e, "WorkFlow::WorkFlowSteps_Get_WorkFlowIds");
            }
            return lstWorkFlowSteps;
        }

        internal List<WorkFlowStepInfo> GetWorkFlowStepsByUserId(Guid tenantId, Guid userId,bool isSuperAdmin)
        {
            List<WorkFlowStepInfo> lstWorkFlowSteps = new List<WorkFlowStepInfo>();
            try
            {
                var cmd = CreateProcedureCommand("dbo.WorkFlowSteps_GetBy_UserCode");
                cmd.AppendGuid("@guidTenantId", tenantId);
                cmd.AppendGuid("@guidUserId", userId);  
                cmd.AppendBit("@bitIsSuperAdmin",isSuperAdmin);
                using (SqlDataReader reader = ExecuteCommandAndReturnReader(cmd))
                { 
                        while (reader.Read())
                        {
                            lstWorkFlowSteps.Add(ReadWorkFlowStep(reader));
                        } 
                }
            }
            catch (SqlException e)
            {
                throw ReportAndTranslateException(e, "WorkFlow::WorkFlowSteps_GetBy_UserCode");
            }
            return lstWorkFlowSteps;
        }


       internal List<Guid> GetAssignedWorkFlowStepsOfUser(Guid tenantId, Guid userId,string entityId,string subTypeCode)
        {
            List<Guid> transitionType = new List<Guid>();
            try
            {
                var cmd = CreateProcedureCommand("dbo.WorkFlowSteps_UserCanAssign");
                cmd.AppendGuid("@guidTenantId", tenantId);
                cmd.AppendGuid("@guidUserId", userId);  
                cmd.AppendXSmallText("@strEntityId", entityId);                
                cmd.AppendSmallText("@strSubTypeCode", subTypeCode);               
                using (SqlDataReader reader = ExecuteCommandAndReturnReader(cmd))
                { 
                        while (reader.Read())
                        {
                            transitionType.Add(reader.GetGuid(0));
                        } 
                }
            }
            catch (SqlException e)
            {
                throw ReportAndTranslateException(e, "WorkFlow::WorkFlowSteps_UserCanAssign");
            }
            return transitionType;
        }


        private static WorkFlowStepInfo ReadWorkFlowStep(SqlDataReader reader)
        {
            var objWorkFowStep = new WorkFlowStepInfo
            {
                WorkFlowStepId = reader.IsDBNull(0) ? Guid.Empty : reader.GetGuid(0),
                WorkFlowId = reader.IsDBNull(1) ? Guid.Empty : reader.GetGuid(1),
                TransitionType = new ItemName{
                    Id=reader.IsDBNull(2) ? Guid.Empty : reader.GetGuid(2),                    
                },
                SequenceNumber = reader.IsDBNull(3) ? 0 : reader.GetByte(3),              
                IsAssigmentMandatory = reader.IsDBNull(4) ? false : reader.GetBoolean(4),
                AllotedTime = reader.IsDBNull(5) ? (int?)null : reader.GetInt32(5),
                CriticalTime = reader.IsDBNull(6) ? (int?)null : reader.GetInt32(6),
            };
            return objWorkFowStep;
        }  

       
 
     }
}
