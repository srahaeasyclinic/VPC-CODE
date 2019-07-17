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
    internal  sealed class DataWorkFlowProcessTask  : EntityModelData
    {
        internal bool CreateWorkFlowProcessTask(Guid tenantId, WorkFlowProcessTaskInfo workFlowProcessTask)
        {
            try
            {                
                var cmd = CreateProcedureCommand("dbo.WorkFlowProcessTask_Create");
                cmd.AppendGuid("@guidTenantId", tenantId);
                cmd.AppendGuid("@guidWorkFlowProcessTaskId", workFlowProcessTask.WorkFlowProcessTaskId); 
                cmd.AppendGuid("@guidWorkFlowId", workFlowProcessTask.WorkFlowId); 
                cmd.AppendGuid("@guidWorkFlowProcessId", workFlowProcessTask.WorkFlowProcessId); 
                cmd.AppendGuid("@guidProcessCode", workFlowProcessTask.ProcessCode); 
                ExecuteCommand(cmd);
                return true;
            }
            catch (SqlException e)
            {
                throw ReportAndTranslateException(e, "DataWorkFlowProcessTask::dbo.WorkFlowProcessTask_Create");
            }
        }

        internal bool CreateWorkFlowProcessTasks(Guid tenantId, List<WorkFlowProcessTaskInfo> workFlowProcessTasks)
        {
            try
            {                
                var cmd = CreateProcedureCommand("dbo.WorkFlowProcessTask_Create_Xml");
                cmd.AppendGuid("@guidTenantId", tenantId);
                 cmd.AppendXml("@xmlWorkFlowProcessTasks", DataUtility.GetXmlForWorkFlowProcessTask(workFlowProcessTasks));   
                ExecuteCommand(cmd);
                return true;
            }
            catch (SqlException e)
            {
                throw ReportAndTranslateException(e, "DataWorkFlowProcessTask::dbo.WorkFlowProcessTask_Create_Xml");
            }
        }

        internal bool DeleteWorkFlowProcessTask(Guid tenantId,Guid workFlowProcessTaskId)
        {
            try
            {                
                var cmd = CreateProcedureCommand("dbo.WorkFlowProcessTask_Delete");
                cmd.AppendGuid("@guidTenantId", tenantId);
                cmd.AppendGuid("@guidWorkFlowProcessTaskId", workFlowProcessTaskId);                           
                ExecuteCommand(cmd);
                return true;
            }
            catch (SqlException e)
            {
                throw ReportAndTranslateException(e, "DataWorkFlowProcessTask::dbo.WorkFlowProcessTask_Delete");
            }
        }

       internal bool MoveUpDownWorkFlowProcessTask(Guid tenantId,  List<WorkFlowProcessTaskInfo> workFlowProcessTasks)
        {
            try
            {  
                var cmd = CreateProcedureCommand("dbo.WorkFlowProcessTask_MoveUpDown");
                cmd.AppendGuid("@guidTenantId", tenantId);
                cmd.AppendXml("@xmlWorkFlowProcessTasks", DataUtility.GetXmlForWorkFlowProcessTask(workFlowProcessTasks));                 
                ExecuteCommand(cmd);
                return true;
            }
            catch (SqlException e)
            {
                throw ReportAndTranslateException(e, "DataWorkFlowProcessTask::dbo.WorkFlowProcessTask_MoveUpDown");
            }
        }

         internal List<WorkFlowProcessTaskInfo> GetWorkFlowProcessTask(Guid tenantId, Guid workFlowId)
        {
            List<WorkFlowProcessTaskInfo> lstWorkFlowSteps = new List<WorkFlowProcessTaskInfo>();
            try
            {
                var cmd = CreateProcedureCommand("dbo.WorkFlowProcessTask_Get");
                cmd.AppendGuid("@guidTenantId", tenantId);
                cmd.AppendGuid("@guidWorkFlowId", workFlowId);                
                using (SqlDataReader reader = ExecuteCommandAndReturnReader(cmd))
                { 
                        while (reader.Read())
                        {
                            lstWorkFlowSteps.Add(ReadWorkFlowProcessTask(reader));
                        } 
                }
            }
            catch (SqlException e)
            {
                throw ReportAndTranslateException(e, "DataWorkFlowProcessTask::WorkFlowProcessTask_Get");
            }
            return lstWorkFlowSteps;
        }

        internal List<WorkFlowProcessTaskInfo> GetWorkFlowProcessTaskByWorkFlowIds(Guid tenantId, List<Guid> workFlowIds)
        {
            List<WorkFlowProcessTaskInfo> lstWorkFlowSteps = new List<WorkFlowProcessTaskInfo>();
            try
            {
                var cmd = CreateProcedureCommand("dbo.WorkFlowProcessTask_GetBy_WorkFlowIds");
                cmd.AppendGuid("@guidTenantId", tenantId);
                 cmd.AppendXml("@xmlWorkFlowIds", DataUtility.GetXmlForIds(workFlowIds));                  
                using (SqlDataReader reader = ExecuteCommandAndReturnReader(cmd))
                { 
                        while (reader.Read())
                        {
                            lstWorkFlowSteps.Add(ReadWorkFlowProcessTask(reader));
                        } 
                }
            }
            catch (SqlException e)
            {
                throw ReportAndTranslateException(e, "DataWorkFlowProcessTask::WorkFlowProcessTask_GetBy_WorkFlowIds");
            }
            return lstWorkFlowSteps;
        }

        internal List<WorkFlowProcessTaskInfo> GetWorkFlowProcessTask_ByInnerStepId(Guid tenantId, Guid innerStepId)
        {
            List<WorkFlowProcessTaskInfo> lstWorkFlowSteps = new List<WorkFlowProcessTaskInfo>();
            try
            {
                var cmd = CreateProcedureCommand("dbo.WorkFlowProcessTask_Get_ByInnerStepId");
                cmd.AppendGuid("@guidTenantId", tenantId);
                cmd.AppendGuid("@guidInnerStepId", innerStepId);                
                using (SqlDataReader reader = ExecuteCommandAndReturnReader(cmd))
                { 
                        while (reader.Read())
                        {
                            lstWorkFlowSteps.Add(ReadWorkFlowProcessTask(reader));
                        } 
                }
            }
            catch (SqlException e)
            {
                throw ReportAndTranslateException(e, "DataWorkFlowProcessTask::WorkFlowProcessTask_Get_ByInnerStepId");
            }
            return lstWorkFlowSteps;
        }

        internal List<WorkFlowProcessTaskInfo> GetWorkFlowProcessTask_ByProcessIds(Guid tenantId, List<Guid> processIds)
        {
            List<WorkFlowProcessTaskInfo> lstWorkFlowSteps = new List<WorkFlowProcessTaskInfo>();
            try
            {
                var cmd = CreateProcedureCommand("dbo.WorkFlowProcessTask_Get_ByProcessIds");
                cmd.AppendGuid("@guidTenantId", tenantId);
                cmd.AppendXml("@xmlProcessIds", DataUtility.GetXmlForIds(processIds));                
                using (SqlDataReader reader = ExecuteCommandAndReturnReader(cmd))
                { 
                        while (reader.Read())
                        {
                            lstWorkFlowSteps.Add(ReadWorkFlowProcessTask(reader));
                        } 
                }
            }
            catch (SqlException e)
            {
                throw ReportAndTranslateException(e, "DataWorkFlowProcessTask::WorkFlowProcessTask_Get_ByProcessIds");
            }
            return lstWorkFlowSteps;
        }


        private static WorkFlowProcessTaskInfo ReadWorkFlowProcessTask(SqlDataReader reader)
        {
            var objWorkFowStep = new WorkFlowProcessTaskInfo
            {
                WorkFlowProcessTaskId = reader.IsDBNull(0) ? Guid.Empty : reader.GetGuid(0),
                WorkFlowId = reader.IsDBNull(1) ? Guid.Empty : reader.GetGuid(1),
                WorkFlowProcessId = reader.IsDBNull(2) ? Guid.Empty : reader.GetGuid(2),
                ProcessCode = reader.IsDBNull(3) ? Guid.Empty : reader.GetGuid(3),
                SequenceCode = reader.IsDBNull(4) ? 0 : reader.GetByte(4),
                ProcessType = reader.IsDBNull(5) ? 0 : reader.GetByte(5)                        
            };
            return objWorkFowStep;
        }

    }
}
