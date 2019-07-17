using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using VPC.Entities.Common;
using VPC.Entities.WorkFlow;
using VPC.Framework.Business.Common;
using VPC.Framework.Business.Data;
using VPC.Framework.Business.Data.SqlClient;

namespace VPC.Framework.Business.WorkFlow.Data
{
    internal sealed  class DataWorkFlowInnerStep : EntityModelData
    {
        internal bool CreateWorkFlowInnerStep(Guid tenantId, WorkFlowInnerStepInfo workFlowInnerStep)
        {
            try
            {  
                var cmd = CreateProcedureCommand("dbo.WorkFlowInnerStep_Create");
                cmd.AppendGuid("@guidTenantId", tenantId);
                cmd.AppendGuid("@guidInnerStepId", workFlowInnerStep.InnerStepId); 
                cmd.AppendGuid("@guidWorkFlowStepId", workFlowInnerStep.WorkFlowStepId); 
                cmd.AppendGuid("@guidWorkFlowId", workFlowInnerStep.WorkFlowId); 
                cmd.AppendGuid("@guidTransitionType", workFlowInnerStep.TransitionType.Id); 
                ExecuteCommand(cmd);
                return true;
            }
            catch (SqlException e)
            {
                throw ReportAndTranslateException(e, "WorkFlow::dbo.WorkFlowInnerStep_Create");
            }
        }

        internal bool CreateWorkFlowInnerSteps(Guid tenantId, List<WorkFlowInnerStepInfo> workFlowInnerSteps)
        {
            try
            {  
                var cmd = CreateProcedureCommand("dbo.WorkFlowInnerStep_Create_Xml");
                cmd.AppendGuid("@guidTenantId", tenantId);
                cmd.AppendXml("@xmlWorkFlowInnerSteps", DataUtility.GetXmlForWorkFlowInnerStepsCreate(workFlowInnerSteps));  
                ExecuteCommand(cmd);
                return true;
            }
            catch (SqlException e)
            {
                throw ReportAndTranslateException(e, "WorkFlow::dbo.WorkFlowInnerStep_Create_Xml");
            }
        }


         internal bool DeleteWorkFlowInnerStep(Guid tenantId, Guid innerStepId)
        {
            try
            {  
                var cmd = CreateProcedureCommand("dbo.WorkFlowInnerStep_Delete");
                cmd.AppendGuid("@guidTenantId", tenantId);
                cmd.AppendGuid("@guidInnerStepId", innerStepId);               
                ExecuteCommand(cmd);
                return true;
            }
            catch (SqlException e)
            {
                throw ReportAndTranslateException(e, "WorkFlow::dbo.WorkFlowInnerStep_Delete");
            }
        }

        internal bool MoveUpDownWorkFlowInnerStep(Guid tenantId,  List<WorkFlowInnerStepInfo> workFlowStepInfo )
        {
            try
            {  
                var cmd = CreateProcedureCommand("dbo.WorkFlowInnerStep_MoveUpDown");
                cmd.AppendGuid("@guidTenantId", tenantId);
                cmd.AppendXml("@xmlWorkFlowInnerSteps", DataUtility.GetXmlForWorkFlowInnerStepsSequence(workFlowStepInfo));                 
                ExecuteCommand(cmd);
                return true;
            }
            catch (SqlException e)
            {
                throw ReportAndTranslateException(e, "WorkFlow::dbo.WorkFlowInnerStep_MoveUpDown");
            }
        }

       internal List<WorkFlowInnerStepInfo> GetWorkFlowInnerStep(Guid tenantId, Guid workFlowId)
        {
            List<WorkFlowInnerStepInfo> lstWorkFlowSteps = new List<WorkFlowInnerStepInfo>();
            try
            {
                var cmd = CreateProcedureCommand("dbo.WorkFlowInnerStep_Get");
                cmd.AppendGuid("@guidTenantId", tenantId);
                cmd.AppendGuid("@guidWorkFlowId", workFlowId);                
                using (SqlDataReader reader = ExecuteCommandAndReturnReader(cmd))
                { 
                        while (reader.Read())
                        {
                            lstWorkFlowSteps.Add(ReadWorkFlowInnerStep(reader));
                        } 
                }
            }
            catch (SqlException e)
            {
                throw ReportAndTranslateException(e, "WorkFlow::WorkFlowInnerStep_Get");
            }
            return lstWorkFlowSteps;
        }

        internal List<WorkFlowInnerStepInfo> GetWorkFlowInnerStepByWorkFlowIds(Guid tenantId, List<Guid> workFlowIds)
        {
            List<WorkFlowInnerStepInfo> lstWorkFlowSteps = new List<WorkFlowInnerStepInfo>();
            try
            {
                var cmd = CreateProcedureCommand("dbo.WorkFlowInnerStep_GetByWorkFlowIds");
                cmd.AppendGuid("@guidTenantId", tenantId);
                cmd.AppendXml("@xmlWorkFlowIds", DataUtility.GetXmlForIds(workFlowIds));                
                using (SqlDataReader reader = ExecuteCommandAndReturnReader(cmd))
                { 
                        while (reader.Read())
                        {
                            lstWorkFlowSteps.Add(ReadWorkFlowInnerStep(reader));
                        } 
                }
            }
            catch (SqlException e)
            {
                throw ReportAndTranslateException(e, "WorkFlow::WorkFlowInnerStep_GetByWorkFlowIds");
            }
            return lstWorkFlowSteps;
        }

        internal List<WorkFlowInnerStepInfo> GetWorkFlowInnerStep_ByStepIds(Guid tenantId, List<Guid> stepIds)
        {
            List<WorkFlowInnerStepInfo> lstWorkFlowSteps = new List<WorkFlowInnerStepInfo>();
            try
            {
                var cmd = CreateProcedureCommand("dbo.WorkFlowInnerStep_GetByStepIds");
                cmd.AppendGuid("@guidTenantId", tenantId);
                cmd.AppendXml("@xmlStepIds", DataUtility.GetXmlForIds(stepIds));                
                using (SqlDataReader reader = ExecuteCommandAndReturnReader(cmd))
                { 
                        while (reader.Read())
                        {
                            lstWorkFlowSteps.Add(ReadWorkFlowInnerStep(reader));
                        } 
                }
            }
            catch (SqlException e)
            {
                throw ReportAndTranslateException(e, "WorkFlow::WorkFlowInnerStep_GetByStepIds");
            }
            return lstWorkFlowSteps;
        }

         internal List<WorkFlowInnerStepInfo> GetWorkFlowInnerStep_ByStepTransactionType(Guid tenantId, Guid transactionType,Guid workFlowId)
        {
            List<WorkFlowInnerStepInfo> lstWorkFlowSteps = new List<WorkFlowInnerStepInfo>();
            try
            {
                var cmd = CreateProcedureCommand("dbo.WorkFlowInnerStep_GetByStepTransactionType");
                cmd.AppendGuid("@guidTenantId", tenantId);
                cmd.AppendGuid("@guidStepTransactionType", transactionType); 
                cmd.AppendGuid("@guidWorkFlowId", workFlowId);               
                using (SqlDataReader reader = ExecuteCommandAndReturnReader(cmd))
                { 
                        while (reader.Read())
                        {
                            lstWorkFlowSteps.Add(ReadWorkFlowInnerStep(reader));
                        } 
                }
            }
            catch (SqlException e)
            {
                throw ReportAndTranslateException(e, "WorkFlow::WorkFlowInnerStep_GetByStepTransactionType");
            }
            return lstWorkFlowSteps;
        }

        private static WorkFlowInnerStepInfo ReadWorkFlowInnerStep(SqlDataReader reader)
        {
            var objWorkFowStep = new WorkFlowInnerStepInfo
            {
                InnerStepId = reader.IsDBNull(0) ? Guid.Empty : reader.GetGuid(0),
                WorkFlowStepId = reader.IsDBNull(1) ? Guid.Empty : reader.GetGuid(1),
                WorkFlowId = reader.IsDBNull(2) ? Guid.Empty : reader.GetGuid(2),
                TransitionType =new ItemName{Id= reader.IsDBNull(3) ? Guid.Empty : reader.GetGuid(3)},
                SequenceNumber = reader.IsDBNull(4) ? 0 : reader.GetByte(4)
            };
            return objWorkFowStep;
        }    
     }
}
