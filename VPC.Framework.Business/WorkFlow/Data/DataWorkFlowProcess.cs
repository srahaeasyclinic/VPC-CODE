using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using VPC.Entities.WorkFlow;
using VPC.Framework.Business.Common;
using VPC.Framework.Business.Data;
using VPC.Framework.Business.Data.SqlClient;
using VPC.Framework.Business.WorkFlow.APIs;

namespace VPC.Framework.Business.WorkFlow.Data
{
    internal  sealed class DataWorkFlowProcess : EntityModelData
    {
         internal bool CreateWorkFlowProcess(Guid tenantId, List<WorkFlowProcessInfo> workFlowSteps)
        {
            try
            {                
                var cmd = CreateProcedureCommand("dbo.WorkFlowProcess_Create");
                cmd.AppendGuid("@guidTenantId", tenantId);
                cmd.AppendXml("@xmlWorkFlowProcess", DataUtility.GetXmlForWorkFlowProcess(workFlowSteps));              
                ExecuteCommand(cmd);
                return true;
            }
            catch (SqlException e)
            {
                throw ReportAndTranslateException(e, "SampleEntity::dbo.WorkFlowInnerStep_Create");
            }
        }

      

        internal List<WorkFlowProcessInfo> GetWorkFlowProcess(Guid tenantId, Guid workFlowId)
        {
            List<WorkFlowProcessInfo> lstWorkFlowSteps = new List<WorkFlowProcessInfo>();
            try
            {
                var cmd = CreateProcedureCommand("dbo.WorkFlowProcess_Get");
                cmd.AppendGuid("@guidTenantId", tenantId);                
                cmd.AppendGuid("@guidWorkFlowId", workFlowId);                
                using (SqlDataReader reader = ExecuteCommandAndReturnReader(cmd))
                { 
                        while (reader.Read())
                        {
                            lstWorkFlowSteps.Add(ReadWorkFlowProcess(reader));
                        } 
                }
            }
            catch (SqlException e)
            {
                throw ReportAndTranslateException(e, "Workflow::WorkFlowProcess_Get");
            }
            return lstWorkFlowSteps;
        }

        internal List<WorkFlowProcessInfo> GetWorkFlowProcessByWorkFlowIds(Guid tenantId, List<Guid> workFlowIds)
        {
            List<WorkFlowProcessInfo> lstWorkFlowSteps = new List<WorkFlowProcessInfo>();
            try
            {
                var cmd = CreateProcedureCommand("dbo.WorkFlowProcess_GetByW_WorkFlowIds");
                cmd.AppendGuid("@guidTenantId", tenantId);                
                cmd.AppendXml("@xmlWorkFlowIds", DataUtility.GetXmlForIds(workFlowIds));                
                using (SqlDataReader reader = ExecuteCommandAndReturnReader(cmd))
                { 
                        while (reader.Read())
                        {
                            lstWorkFlowSteps.Add(ReadWorkFlowProcess(reader));
                        } 
                }
            }
            catch (SqlException e)
            {
                throw ReportAndTranslateException(e, "Workflow::WorkFlowProcess_GetByW_WorkFlowIds");
            }
            return lstWorkFlowSteps;
        }

        internal List<WorkFlowProcessInfo> GetWorkFlowProcessByOperationOrTransitionIds(Guid tenantId, List<Guid> operationOrTransactionIds)
        {
            List<WorkFlowProcessInfo> lstWorkFlowSteps = new List<WorkFlowProcessInfo>();
            try
            {
                var cmd = CreateProcedureCommand("dbo.WorkFlowProcess_GetBy_OperationOrTransitionIds");
                cmd.AppendGuid("@guidTenantId", tenantId);
                cmd.AppendXml("@xmlOperationOrTransitionIds", DataUtility.GetXmlForIds(operationOrTransactionIds));                
                using (SqlDataReader reader = ExecuteCommandAndReturnReader(cmd))
                { 
                        while (reader.Read())
                        {
                            lstWorkFlowSteps.Add(ReadWorkFlowProcess(reader));
                        } 
                }
            }
            catch (SqlException e)
            {
                throw ReportAndTranslateException(e, "Workflow::WorkFlowProcess_GetBy_OperationOrTransitionIds");
            }
            return lstWorkFlowSteps;
        }

        private static WorkFlowProcessInfo ReadWorkFlowProcess(SqlDataReader reader)
        {
            var objWorkFowStep = new WorkFlowProcessInfo
            {
                WorkFlowProcessId = reader.IsDBNull(0) ? Guid.Empty : reader.GetGuid(0),
                WorkFlowId = reader.IsDBNull(1) ? Guid.Empty : reader.GetGuid(1),
                OperationOrTransactionId = reader.IsDBNull(2) ? Guid.Empty : reader.GetGuid(2),
                OperationOrTransactionType = reader.IsDBNull(3) ? (byte) OperationOrTransactionType.Operation : reader.GetByte(3),
                ProcessType = reader.IsDBNull(4) ? 0 : reader.GetByte(4)                         
            };
            return objWorkFowStep;
        }

    }
}
