using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using VPC.Entities.Common;
using VPC.Entities.Role;
using VPC.Entities.EntityCore.Metadata;
using VPC.Framework.Business.Data;
using VPC.Framework.Business.Data.SqlClient;
using VPC.Entities.TenantSubscription;
using VPC.Entities.SchedulerConfiguration;

namespace VPC.Framework.Business.SchedulerConfiguration.SchedulerWeekly.Data
{
    internal sealed class DataSchedulerWeekly : EntityModelData
    {
        #region Manage
        internal bool Create(Guid tenantId, SchedulerWeeklyInfo info)
        {
            try
            {
                var cmd = CreateProcedureCommand("dbo.SchedulerWeekly_Create");
                cmd.AppendGuid("@guidTenantId", tenantId);
                cmd.AppendGuid("@guidSchedulerMonthlyId", info.SchedulerWeeklyId);
                cmd.AppendGuid("@guidSchedulerId", info.SchedulerId);
                if(info.Value.HasValue && info.Value.Value>0)
                cmd.AppendInt("@intValue", info.Value.Value); 
                cmd.AppendBit("@bMonday", info.Monday);  
                cmd.AppendBit("@bTuesday", info.Tuesday);  
                cmd.AppendBit("@bWednesday", info.Wednesday);  
                cmd.AppendBit("@bThrusday", info.Thrusday);  
                cmd.AppendBit("@bFriday", info.Friday);  
                cmd.AppendBit("@bSaturday", info.Saturday);  
                cmd.AppendBit("@bSunday", info.Sunday);         
               
                ExecuteCommand(cmd);
                return true;               
            }
            catch (SqlException e)
            {
                throw ReportAndTranslateException(e, "DataSchedulerWeekly::SchedulerWeekly_Create");
            }
        } 

        internal bool Update(Guid tenantId, SchedulerWeeklyInfo info)
        {
            try
            {
                var cmd = CreateProcedureCommand("dbo.SchedulerWeekly_Update");
                cmd.AppendGuid("@guidTenantId", tenantId);
                cmd.AppendGuid("@guidSchedulerMonthlyId", info.SchedulerWeeklyId);
                cmd.AppendGuid("@guidSchedulerId", info.SchedulerId);
                if(info.Value.HasValue && info.Value.Value>0)
                cmd.AppendInt("@intValue", info.Value.Value); 
                cmd.AppendBit("@bMonday", info.Monday);  
                cmd.AppendBit("@bTuesday", info.Tuesday);  
                cmd.AppendBit("@bWednesday", info.Wednesday);  
                cmd.AppendBit("@bThrusday", info.Thrusday);  
                cmd.AppendBit("@bFriday", info.Friday);  
                cmd.AppendBit("@bSaturday", info.Saturday);  
                cmd.AppendBit("@bSunday", info.Sunday);                  
                ExecuteCommand(cmd);
                return true;               
            }
            catch (SqlException e)
            {
                throw ReportAndTranslateException(e, "DataSchedulerWeekly::SchedulerWeekly_Update");
            }
        }     

        internal bool Delete(Guid tenantId, Guid schedulerId )
        {
            try
            {
                var cmd = CreateProcedureCommand("dbo.SchedulerWeekly_Delete");
                cmd.AppendGuid("@guidTenantId", tenantId);                
                cmd.AppendGuid("@guidSchedulerId", schedulerId);                       
                ExecuteCommand(cmd);
                return true;               
            }
            catch (SqlException e)
            {
                throw ReportAndTranslateException(e, "DataSchedulerWeekly::SchedulerWeekly_Delete");
            }
        }      

     #endregion
  
  

    #region Review
      internal SchedulerWeeklyInfo GetSchedulerWeekly(Guid tenantId,Guid schedulerId )
        {
            SchedulerWeeklyInfo info =null;
            try
            {
                var cmd = CreateProcedureCommand("dbo.SchedulerWeekly_GetBy_SchedulerId");
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
                throw ReportAndTranslateException(e, "DataSchedulerWeekly::SchedulerWeekly_GetBy_SchedulerId");
            }
            return info;
        }

        private static SchedulerWeeklyInfo ReadInfo(SqlDataReader reader)
        {
            var info = new SchedulerWeeklyInfo
            {
                SchedulerWeeklyId = reader.IsDBNull(0) ? Guid.Empty : reader.GetGuid(0),
                SchedulerId = reader.IsDBNull(1) ? Guid.Empty : reader.GetGuid(1),               
                Value = reader.IsDBNull(2) ? (int?)null : reader.GetInt32(2) ,    
                Monday = reader.IsDBNull(3) ? false : reader.GetBoolean(3)  ,
                Tuesday = reader.IsDBNull(4) ? false : reader.GetBoolean(4)  ,
                Wednesday = reader.IsDBNull(5) ? false : reader.GetBoolean(5)  ,
                Thrusday = reader.IsDBNull(6) ? false : reader.GetBoolean(6)  ,
                Friday = reader.IsDBNull(7) ? false : reader.GetBoolean(7)  ,
                Saturday = reader.IsDBNull(8) ? false : reader.GetBoolean(8)  ,
                Sunday = reader.IsDBNull(9) ? false : reader.GetBoolean(9)  ,
            };
            return info;
        }

         #endregion
       
       
    }
}
