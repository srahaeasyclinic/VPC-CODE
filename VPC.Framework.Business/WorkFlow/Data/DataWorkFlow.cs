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
    internal sealed class DataWorkFlow : EntityModelData
    {
         #region Review
        internal WorkFlowInfo GetWorkFlow(Guid tenantId, string entityId,string subTypeCode )
        {
            WorkFlowInfo lstWorkFlow = null;
            try
            {
                var cmd = CreateProcedureCommand("dbo.WorkFlow_Get");
                cmd.AppendGuid("@guidTenantId", tenantId);
                cmd.AppendXSmallText("@strEntityId", entityId);                
                cmd.AppendSmallText("@strSubTypeCode", subTypeCode);
                using (SqlDataReader reader = ExecuteCommandAndReturnReader(cmd))
                { 
                        while (reader.Read())
                        {
                            lstWorkFlow=ReadWorkFlow(reader);
                        } 
                }
            }
            catch (SqlException e)
            {
                throw ReportAndTranslateException(e, "Workflow::WorkFlow_Get");
            }
            return lstWorkFlow;
        }

         internal List<WorkFlowInfo> GetWorkFlows(Guid tenantId, string entityId)
        {
            List<WorkFlowInfo> lstWorkFlows = new List<WorkFlowInfo>();
            try
            {
                var cmd = CreateProcedureCommand("dbo.WorkFlow_All_Get");
                cmd.AppendGuid("@guidTenantId", tenantId);
                cmd.AppendXSmallText("@strEntityId", entityId);
                using (SqlDataReader reader = ExecuteCommandAndReturnReader(cmd))
                { 
                        while (reader.Read())
                        {
                            lstWorkFlows.Add(ReadWorkFlow(reader));
                        } 
                }
            }
            catch (SqlException e)
            {
                throw ReportAndTranslateException(e, "Workflow::WorkFlow_All_Get");
            }
            return lstWorkFlows;
        }

        internal List<WorkFlowInfo> GetWorkFlowsByEntityIds(Guid tenantId, List<string> entityIds)
        {
            List<WorkFlowInfo> lstWorkFlows = new List<WorkFlowInfo>();
            try
            {
                var cmd = CreateProcedureCommand("dbo.WorkFlows_GetBy_EntityIds");
                cmd.AppendGuid("@guidTenantId", tenantId);
                cmd.AppendXml("@xmlEntityIds", DataUtility.GetXmlForIds(entityIds));
                using (SqlDataReader reader = ExecuteCommandAndReturnReader(cmd))
                { 
                        while (reader.Read())
                        {
                            lstWorkFlows.Add(ReadWorkFlow(reader));
                        } 
                }
            }
            catch (SqlException e)
            {
                throw ReportAndTranslateException(e, "Workflow::WorkFlows_GetBy_EntityIds");
            }
            return lstWorkFlows;
        }

        internal List<WorkFlowInfo> GetWorkFlowsByIds(Guid tenantId, List<Guid> workFlowIds)
        {
            List<WorkFlowInfo> lstWorkFlows = new List<WorkFlowInfo>();
            try
            {
                var cmd = CreateProcedureCommand("dbo.WorkFlows_GetBy_Ids");
                cmd.AppendGuid("@guidTenantId", tenantId);
                cmd.AppendXml("@xmlWorkFlowIds", DataUtility.GetXmlForIds(workFlowIds));
                using (SqlDataReader reader = ExecuteCommandAndReturnReader(cmd))
                { 
                        while (reader.Read())
                        {
                            lstWorkFlows.Add(ReadWorkFlow(reader));
                        } 
                }
            }
            catch (SqlException e)
            {
                throw ReportAndTranslateException(e, "Workflow::WorkFlows_GetBy_Ids");
            }
            return lstWorkFlows;
        }


        private static WorkFlowInfo ReadWorkFlow(SqlDataReader reader)
        {
            var objWorkFow = new WorkFlowInfo
            {
                WorkFlowId = reader.IsDBNull(0) ? Guid.Empty : reader.GetGuid(0),
                EntityId = reader.IsDBNull(1) ? string.Empty : reader.GetString(1),
                Status = !reader.IsDBNull(2) && reader.GetBoolean(2),
                SubTypeCode = reader.IsDBNull(3) ? String.Empty : reader.GetString(3),
            };
            return objWorkFow;
        }

         #endregion
        #region Manage

        internal bool CreateWorkFlow(Guid tenantId, WorkFlowInfo workflowInfo)
        {
            try
            {
                var cmd = CreateProcedureCommand("dbo.WorkFlow_Create");
                cmd.AppendGuid("@guidTenantId", tenantId);
                cmd.AppendGuid("@guidWorkFlowId", workflowInfo.WorkFlowId);
                cmd.AppendXSmallText("@strEntityId", workflowInfo.EntityId);
                cmd.AppendBit("@bitStatus", workflowInfo.Status);               
                cmd.AppendSmallText("@strSubTypeCode", workflowInfo.SubTypeCode);
                ExecuteCommand(cmd);
                return true;
               
            }
            catch (SqlException e)
            {
                throw ReportAndTranslateException(e, "Workflow::WorkFlow_Create");
            }
        }  

        internal bool CreateWorkFlows(Guid tenantId, List<WorkFlowInfo> workflowInfos)
        {
            try
            {
                var cmd = CreateProcedureCommand("dbo.WorkFlow_Create_Xml");
                cmd.AppendGuid("@guidTenantId", tenantId);
                cmd.AppendXml("@xmlWorkFlowId", DataUtility.GetXmlForWorkFlows(workflowInfos));
                ExecuteCommand(cmd);
                return true;
               
            }
            catch (SqlException e)
            {
                throw ReportAndTranslateException(e, "Workflow::WorkFlow_Create_Xml");
            }
        }      
     #endregion
    }
}
