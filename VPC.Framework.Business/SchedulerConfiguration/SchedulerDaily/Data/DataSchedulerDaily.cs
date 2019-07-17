using System;
using System.Data.SqlClient;
using VPC.Framework.Business.Data;
using VPC.Entities.SchedulerConfiguration;

namespace VPC.Framework.Business.SchedulerConfiguration.SchedulerDaily.Data
{
    internal sealed class DataSchedulerDaily : EntityModelData
    {
         #region Manage
        internal bool Create(Guid tenantId, SchedulerDailyInfo info)
        {
            try
            {
                var cmd = CreateProcedureCommand("dbo.SchedulerDaily_Create");
                cmd.AppendGuid("@guidTenantId", tenantId);
                cmd.AppendGuid("@guidSchedulerDailyId", info.SchedulerDailyId);
                cmd.AppendGuid("@guidSchedulerId", info.SchedulerId);
                cmd.AppendInt("@intUnit", info.Unit); 
                if(info.Value.HasValue && info.Value>0)
                cmd.AppendInt("@intValue", info.Value.Value);                 
                ExecuteCommand(cmd);
                return true;               
            }
            catch (SqlException e)
            {
                throw ReportAndTranslateException(e, "DataSchedulerDaily::SchedulerDaily_Create");
            }
        } 

        internal bool Update(Guid tenantId, SchedulerDailyInfo info)
        {
            try
            {
                var cmd = CreateProcedureCommand("dbo.SchedulerDaily_Update");
                cmd.AppendGuid("@guidTenantId", tenantId);
                cmd.AppendGuid("@guidSchedulerDailyId", info.SchedulerDailyId);
                cmd.AppendGuid("@guidSchedulerId", info.SchedulerId);
                cmd.AppendInt("@intUnit", info.Unit); 
                if(info.Value.HasValue && info.Value>0)
                cmd.AppendInt("@intValue", info.Value.Value);               
                ExecuteCommand(cmd);
                return true;               
            }
            catch (SqlException e)
            {
                throw ReportAndTranslateException(e, "DataSchedulerDaily::SchedulerDaily_Update");
            }
        }   

        internal bool Delete(Guid tenantId, Guid schedulerId )
        {
            try
            {
                var cmd = CreateProcedureCommand("dbo.SchedulerDaily_Delete");
                cmd.AppendGuid("@guidTenantId", tenantId);                
                cmd.AppendGuid("@guidSchedulerId", schedulerId);                       
                ExecuteCommand(cmd);
                return true;               
            }
            catch (SqlException e)
            {
                throw ReportAndTranslateException(e, "DataSchedulerDaily::SchedulerDaily_Delete");
            }
        }      

     #endregion
  
  

    #region Review
      internal SchedulerDailyInfo GetSchedulerDaily(Guid tenantId,Guid schedulerId )
        {
            SchedulerDailyInfo info =null;
            try
            {
                var cmd = CreateProcedureCommand("dbo.SchedulerDaily_GetBySchedulerId");
                cmd.AppendGuid("@guidTenantId", tenantId);
                cmd.AppendGuid("@guidSchedulerId", schedulerId);
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
                throw ReportAndTranslateException(e, "DataSchedulerDaily::SchedulerDaily_GetBySchedulerId");
            }
            return info;
        }

        private static SchedulerDailyInfo ReadInfo(SqlDataReader reader)
        {
            var info = new SchedulerDailyInfo
            {
                SchedulerDailyId = reader.IsDBNull(0) ? Guid.Empty : reader.GetGuid(0),
                SchedulerId = reader.IsDBNull(1) ? Guid.Empty : reader.GetGuid(1),               
                Unit = reader.IsDBNull(2) ? 0 : reader.GetInt32(2) ,    
                Value = reader.IsDBNull(3) ? (int?)null : reader.GetInt32(3)  
            };
            return info;
        }

        #endregion
       
       
    }
}
