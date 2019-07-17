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
    internal  sealed  class DataWorkFlowRole : EntityModelData
    {
    internal bool CreateWorkFlowRole(Guid tenantId, WorkFlowRoleInfo workFlowRoleInfo)
        {
            try
            {  
                var cmd = CreateProcedureCommand("dbo.WorkFlowRole_Create");
                cmd.AppendGuid("@guidTenantId", tenantId);
                cmd.AppendGuid("@guidRoleAssignmetId", workFlowRoleInfo.RoleAssignmetId);   
                cmd.AppendGuid("@guidWorkFlowStepId", workFlowRoleInfo.WorkFlowStepId);      
                cmd.AppendGuid("@guidRoleId", workFlowRoleInfo.RoleId);    
                cmd.AppendGuid("@guidWorkFlowId", workFlowRoleInfo.WorkFlowId);     
                cmd.AppendTinyInt("@intAssignmentOperationType", (byte)workFlowRoleInfo.AssignmentOperationType);  
                ExecuteCommand(cmd);
                return true;
            }
            catch (SqlException e)
            {
                throw ReportAndTranslateException(e, "DataWorkFlowRole::dbo.WorkFlowRole_Create");
            }
        }

        internal bool CreateWorkFlowRoles(Guid tenantId, List<WorkFlowRoleInfo> workFlowRoleInfos)
        {
            try
            {  
                var cmd = CreateProcedureCommand("dbo.WorkFlowRole_Create_Xml");
                cmd.AppendGuid("@guidTenantId", tenantId);
                cmd.AppendXml("@xmlRoleAssignmets", DataUtility.GetXmlForWorkFlowRoles(workFlowRoleInfos));  
              
                ExecuteCommand(cmd);
                return true;
            }
            catch (SqlException e)
            {
                throw ReportAndTranslateException(e, "DataWorkFlowRole::dbo.WorkFlowRole_Create_Xml");
            }
        }

    internal bool DeleteWorkFlowRole(Guid tenantId, Guid workFlowStepId, Guid roleId,Guid workFlowId,WorkFlowRoleType type)
        {
            try
            {  
                var cmd = CreateProcedureCommand("dbo.WorkFlowRole_Delete");
                cmd.AppendGuid("@guidTenantId", tenantId);
                cmd.AppendGuid("@guidWorkFlowStepId", workFlowStepId);
                cmd.AppendGuid("@guidRoleId", roleId);    
                cmd.AppendGuid("@guidWorkFlowId", workFlowId);
                cmd.AppendTinyInt("@intAssignmentOperationType", (byte)type);  
                ExecuteCommand(cmd);
                return true;
            }
            catch (SqlException e)
            {
                throw ReportAndTranslateException(e, "DataWorkFlowRole::dbo.WorkFlowRole_Delete");
            }
        }

    internal List<WorkFlowRoleInfo> GetWorkFlowRole(Guid tenantId, Guid workFlowId)
        {
            List<WorkFlowRoleInfo> lstWorkFlowSteps = new List<WorkFlowRoleInfo>();
            try
            {
                var cmd = CreateProcedureCommand("dbo.WorkFlowRole_Get");
                cmd.AppendGuid("@guidTenantId", tenantId);
                cmd.AppendGuid("@guidWorkFlowId", workFlowId);                
                using (SqlDataReader reader = ExecuteCommandAndReturnReader(cmd))
                { 
                        while (reader.Read())
                        {
                            lstWorkFlowSteps.Add(ReadWorkFlowRole(reader));
                        } 
                }
            }
            catch (SqlException e)
            {
                throw ReportAndTranslateException(e, "DataWorkFlowRole::WorkFlowRole_Get");
            }
            return lstWorkFlowSteps;
        }
    internal List<WorkFlowRoleInfo> GetWorkFlowRolesByWorkFlowIds(Guid tenantId, List<Guid> workFlowIds)
        {
           List<WorkFlowRoleInfo> lstWorkFlowSteps = new List<WorkFlowRoleInfo>();
            try
            {
                var cmd = CreateProcedureCommand("dbo.WorkFlowRole_Get_WorkFlowIds");
                cmd.AppendGuid("@guidTenantId", tenantId);
                cmd.AppendXml("@xmlWorkFlowIds", DataUtility.GetXmlForIds(workFlowIds));                
                using (SqlDataReader reader = ExecuteCommandAndReturnReader(cmd))
                { 
                        while (reader.Read())
                        {
                            lstWorkFlowSteps.Add(ReadWorkFlowRole(reader));
                        } 
                }
            }
            catch (SqlException e)
            {
                throw ReportAndTranslateException(e, "DataWorkFlowRole::WorkFlowRole_Get_WorkFlowIds");
            }
            return lstWorkFlowSteps; 
        }
        
    internal List<WorkFlowRoleInfo> GetWorkFlowRolesByStepIds(Guid tenantId, List<Guid> stepIds)
        {
           List<WorkFlowRoleInfo> lstWorkFlowSteps = new List<WorkFlowRoleInfo>();
            try
            {
                var cmd = CreateProcedureCommand("dbo.WorkFlowRole_Get_StepIds");
                cmd.AppendGuid("@guidTenantId", tenantId);
                cmd.AppendXml("@xmlStepIds", DataUtility.GetXmlForIds(stepIds));                
                using (SqlDataReader reader = ExecuteCommandAndReturnReader(cmd))
                { 
                        while (reader.Read())
                        {
                            lstWorkFlowSteps.Add(ReadWorkFlowRole(reader));
                        } 
                }
            }
            catch (SqlException e)
            {
                throw ReportAndTranslateException(e, "DataWorkFlowRole::WorkFlowRole_Get_StepIds");
            }
            return lstWorkFlowSteps; 
        }
        

    private static WorkFlowRoleInfo ReadWorkFlowRole(SqlDataReader reader)
        {
            var objWorkFowStep = new WorkFlowRoleInfo
            {
                RoleAssignmetId = reader.IsDBNull(0) ? Guid.Empty : reader.GetGuid(0),
                WorkFlowStepId = reader.IsDBNull(1) ? Guid.Empty : reader.GetGuid(1),
                RoleId= reader.IsDBNull(2) ? Guid.Empty : reader.GetGuid(2),
                WorkFlowId = reader.IsDBNull(3) ? Guid.Empty : reader.GetGuid(3),
                AssignmentOperationType = reader.IsDBNull(4) ? 0 : reader.GetByte(4)  ,
                RoleName=reader.IsDBNull(5) ? string.Empty : reader.GetString(5)
                                 
            };
            return objWorkFowStep;
        }
    }
}