using System;
using System.Data;
using System.Data.SqlClient;
using VPC.Framework.Business.Data;
using VPC.Entities.SchedulerConfiguration;

namespace VPC.Framework.Business.SchedulerConfiguration.Scheduler.Data
{
    internal sealed class DataScheduler : EntityModelData
    {
         #region Manage
        internal bool Create(Guid tenantId, SchedulerInfo info)
        {
            try
            {
                var cmd = CreateProcedureCommand("dbo.Scheduler_Create");
                cmd.AppendGuid("@guidTenantId", tenantId);
                cmd.AppendGuid("@guidSchedulerId", info.SchedulerId);
                cmd.AppendGuid("@guidBatchTypeId", info.BatchTypeId);
                cmd.AppendInt("@intSyncTime", info.SyncTime);   
                cmd.AppendTinyInt("@intRecurrenceType", (byte)info.RecurrenceType);               
                ExecuteCommand(cmd);
                return true;               
            }
            catch (SqlException e)
            {
                throw ReportAndTranslateException(e, "DataScheduler::Scheduler_Create");
            }
        } 

        internal bool Update(Guid tenantId, SchedulerInfo info)
        {
            try
            {
                var cmd = CreateProcedureCommand("dbo.Scheduler_Update");
                cmd.AppendGuid("@guidTenantId", tenantId);
                cmd.AppendGuid("@guidSchedulerId", info.SchedulerId);
                cmd.AppendGuid("@guidBatchTypeId", info.BatchTypeId);
                cmd.AppendInt("@intSyncTime", info.SyncTime); 
                cmd.AppendTinyInt("@intRecurrenceType", (byte)info.RecurrenceType);              
                ExecuteCommand(cmd);
                return true;               
            }
            catch (SqlException e)
            {
                throw ReportAndTranslateException(e, "DataScheduler::Scheduler_Update");
            }
        }        

     #endregion
  
  

    #region Review
      internal SchedulerInfo GetScheduler(Guid tenantId,Guid batchTypeId )
        {
            SchedulerInfo info =null;
            try
            {
                var cmd = CreateProcedureCommand("dbo.Scheduler_GetBy_BatchId");
                cmd.AppendGuid("@guidTenantId", tenantId);
                cmd.AppendGuid("@guidBatchTypeId", batchTypeId);
                using (SqlDataReader reader = ExecuteCommandAndReturnReader(cmd))
                { 
                        while (reader.Read())
                        {
                            info=ReadInfo(reader);
                        } 
                }
            }
            catch (SqlException e)
            {
                throw ReportAndTranslateException(e, "DataScheduler::Scheduler_GetBy_BatchId");
            }
            return info;
        }

        private static SchedulerInfo ReadInfo(SqlDataReader reader)
        {
            var info = new SchedulerInfo
            {
                SchedulerId = reader.IsDBNull(0) ? Guid.Empty : reader.GetGuid(0),
                BatchTypeId = reader.IsDBNull(1) ? Guid.Empty : reader.GetGuid(1),               
                SyncTime = reader.IsDBNull(2) ? 0 : reader.GetInt32(2)     ,
                RecurrenceType = reader.IsDBNull(3) ? RecurrencePattern.Daily : (RecurrencePattern)reader.GetByte(3)   
            };
            return info;
        }

         #endregion
       
       
    }
}
