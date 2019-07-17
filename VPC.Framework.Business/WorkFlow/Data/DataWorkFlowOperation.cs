using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using VPC.Entities.WorkFlow;
using VPC.Framework.Business.Common;
using VPC.Framework.Business.Data;
using VPC.Framework.Business.Data.SqlClient;

namespace VPC.Framework.Business.WorkFlow.Data
{
    internal  sealed class DataWorkFlowOperation : EntityModelData
    {
        internal bool CreateWorkFlowOperations(Guid tenantId, List<WorkFlowOperationInfo> workFlowOperationInfo)
        {
            try
            {  
                var cmd = CreateProcedureCommand("dbo.WorkFlowOperation_Create");
                cmd.AppendGuid("@guidTenantId", tenantId);
                cmd.AppendXml("@xmlWorkFlowOperations", DataUtility.GetXmlForWorkFlowOperations(workFlowOperationInfo));  
              
                ExecuteCommand(cmd);
                return true;
            }
            catch (SqlException e)
            {
                throw ReportAndTranslateException(e, "WorkFlow::dbo.WorkFlowOperation_Create");
            }
        }

       internal List<WorkFlowOperationInfo> GetWorkFlowOperations(Guid tenantId, Guid workFlowId)
        {
            List<WorkFlowOperationInfo> lstWorkFlowSteps = new List<WorkFlowOperationInfo>();
            try
            {
                var cmd = CreateProcedureCommand("dbo.WorkFlowOperation_Get");
                cmd.AppendGuid("@guidTenantId", tenantId);
                cmd.AppendGuid("@guidWorkFlowId", workFlowId);                
                using (SqlDataReader reader = ExecuteCommandAndReturnReader(cmd))
                { 
                        while (reader.Read())
                        {
                            lstWorkFlowSteps.Add(ReadWorkFlowOperations(reader));
                        } 
                }
            }
            catch (SqlException e)
            {
                throw ReportAndTranslateException(e, "WorkFlow::WorkFlowOperation_Get");
            }
            return lstWorkFlowSteps;
        }

        internal List<WorkFlowOperationInfo> GetWorkFlowOperationsByWorkFlowIds(Guid tenantId, List<Guid> workFlowIds)
        {
            List<WorkFlowOperationInfo> lstWorkFlowSteps = new List<WorkFlowOperationInfo>();
            try
            {
                var cmd = CreateProcedureCommand("dbo.WorkFlowOperation_GetBy_WorkFlowIds");
                cmd.AppendGuid("@guidTenantId", tenantId);
                cmd.AppendXml("@xmlWorkFlowIds", DataUtility.GetXmlForIds(workFlowIds));                
                using (SqlDataReader reader = ExecuteCommandAndReturnReader(cmd))
                { 
                        while (reader.Read())
                        {
                            lstWorkFlowSteps.Add(ReadWorkFlowOperations(reader));
                        } 
                }
            }
            catch (SqlException e)
            {
                throw ReportAndTranslateException(e, "WorkFlow::WorkFlowOperation_GetBy_WorkFlowIds");
            }
            return lstWorkFlowSteps;
        }

        private static WorkFlowOperationInfo ReadWorkFlowOperations(SqlDataReader reader)
        {
            var objWorkFowStep = new WorkFlowOperationInfo
            {
                WorkFlowOperationId = reader.IsDBNull(0) ? Guid.Empty : reader.GetGuid(0),
                OperationType = reader.IsDBNull(1) ? WorkFlowOperationType.Create :(WorkFlowOperationType) reader.GetByte(1),
                WorkFlowId = reader.IsDBNull(2) ? Guid.Empty : reader.GetGuid(2)            
                
            };
            return objWorkFowStep;
        }    
     }
}


        
