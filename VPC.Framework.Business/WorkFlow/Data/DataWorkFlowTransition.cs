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
    internal   sealed class DataWorkFlowTransition  : EntityModelData
    {
       #region Admin

      internal bool CreateTransitionHistory(Guid tenantId, WorkFlowTransition trasition)
        {
            try
            {
                var cmd = CreateProcedureCommand("dbo.WorkFlowTransition_Create");
                cmd.AppendGuid("@guidTenantId", tenantId);
                cmd.AppendGuid("@guidTransitionHistoryId", trasition.TransitionHistoryId);
                cmd.AppendGuid("@guidRefId", trasition.RefId);
                cmd.AppendXSmallText("@strEntityCode", trasition.EntityId);                
                cmd.AppendGuid("@guidWorkFlowStepId", trasition.WorkFlowStepId);
                cmd.AppendDateTime("@dtStartTime", trasition.StartTime);              
                if(trasition.AssignedUserId!=Guid.Empty)
                cmd.AppendGuid("@guidAssignedUserId", trasition.AssignedUserId); 
                if(trasition.AuditInfo!=null)              
                if(trasition.AuditInfo.CreatedBy!=Guid.Empty)
                   cmd.AppendGuid("@guidCreatedBy", trasition.AuditInfo.CreatedBy);
                ExecuteCommand(cmd);
                return true;
               
            }
            catch (SqlException e)
            {
                throw ReportAndTranslateException(e, "DataWorkFlowTransition::WorkFlowTransition_Create");
            }
        }

        internal bool UpdateTransitionHistory(Guid tenantId, WorkFlowTransition trasition)
        {
            try
            {
                var cmd = CreateProcedureCommand("dbo.WorkFlowTransition_Update");
                cmd.AppendGuid("@guidTenantId", tenantId);
                cmd.AppendGuid("@guidTransitionHistoryId", trasition.TransitionHistoryId);               
                cmd.AppendDateTime("@dtEndTime", trasition.EndTime.Value);                
                ExecuteCommand(cmd);
                return true;
               
            }
            catch (SqlException e)
            {
                throw ReportAndTranslateException(e, "DataWorkFlowTransition::WorkFlowTransition_Update");
            }
        }

        #endregion

        #region Review
        internal List<WorkFlowTransition> GetTransitionHistoryByRefId(Guid tenantId, Guid refId )
        {
            List<WorkFlowTransition> lstWorkFlow = new List<WorkFlowTransition>();
            try
            {
                var cmd = CreateProcedureCommand("dbo.WorkFlowTransition_GetByRefId");
                cmd.AppendGuid("@guidTenantId", tenantId);
                cmd.AppendGuid("@guidRefId", refId);               
               
                using (SqlDataReader reader = ExecuteCommandAndReturnReader(cmd))
                { 
                        while (reader.Read())
                        {
                            lstWorkFlow.Add(ReadTransition(reader));
                        } 
                }
            }
            catch (SqlException e)
            {
                throw ReportAndTranslateException(e, "GetCurrentStepByRefId::WorkFlowTransition_GetByRefId");
            }
            return lstWorkFlow;
        }

        private static WorkFlowTransition ReadTransition(SqlDataReader reader)
        {
            var objWorkFow = new WorkFlowTransition
            {
                TransitionHistoryId = reader.IsDBNull(0) ? Guid.Empty : reader.GetGuid(0),
                RefId = reader.IsDBNull(1) ? Guid.Empty : reader.GetGuid(1),
                EntityId = reader.IsDBNull(2) ? string.Empty : reader.GetString(2),
                WorkFlowStepId = reader.IsDBNull(3) ? Guid.Empty : reader.GetGuid(3),                
                StartTime = reader.IsDBNull(4) ? DateTime.MinValue : reader.GetDateTime(4),
                EndTime = reader.IsDBNull(5) ? (DateTime?)null : reader.GetDateTime(5),
                AssignedUserId = reader.IsDBNull(6) ? Guid.Empty : reader.GetGuid(6), 
                AuditInfo = new AuditDetail
                {                    
                    CreatedBy = reader.IsDBNull(7) ? Guid.Empty : reader.GetGuid(7)
                },
                TransitionType =new ItemName{ Id= reader.IsDBNull(8) ? Guid.Empty : reader.GetGuid(8)   }            
            };
            return objWorkFow;
        }

         #endregion
     }
}
