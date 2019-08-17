using System;
using System.Data.SqlClient;
using VPC.Framework.Business.Data;
using VPC.Framework.Business.Data.SqlClient;

using VPC.Entities.SchedulerConfiguration;

namespace VPC.Framework.Business.SchedulerConfiguration.SchedulerMonthly.Data
{
    internal sealed class DataSchedulerMonthly : EntityModelData
    {
         #region Manage
        internal bool Create(Guid tenantId, SchedulerMonthlyInfo info)
        {
            try
            {
                var cmd = CreateProcedureCommand("dbo.SchedulerMonthly_Create");
                cmd.AppendGuid("@guidTenantId", tenantId);
                cmd.AppendGuid("@guidSchedulerMonthlyId", info.SchedulerMonthlyId);
                cmd.AppendGuid("@guidSchedulerId", info.SchedulerId);
                cmd.AppendInt("@intUnit", info.Unit); 
                if(info.DayValue1.HasValue && info.DayValue1>0)
                   cmd.AppendInt("@intDayValue1", info.DayValue1.Value);  
                if(info.DayValue2.HasValue && info.DayValue2>0)
                   cmd.AppendInt("@intDayValue2", info.DayValue2.Value);      
                if(info.TheValue1.HasValue && info.TheValue1>0)
                   cmd.AppendInt("@intTheValue1", info.TheValue1.Value);    
                if(info.TheValue2.HasValue && info.TheValue2>0)
                   cmd.AppendInt("@intTheValue2", info.TheValue2.Value);  
                if(info.TheValue3.HasValue && info.TheValue3>0)
                   cmd.AppendInt("@intTheValue3", info.TheValue3.Value);        
               
                ExecuteCommand(cmd);
                return true;               
            }
            catch (SqlException e)
            {
                throw ReportAndTranslateException(e, "DataSchedulerMonthly::SchedulerMonthly_Create");
            }
        } 

        internal bool Update(Guid tenantId, SchedulerMonthlyInfo info)
        {
            try
            {
                var cmd = CreateProcedureCommand("dbo.SchedulerMonthly_Update");
                cmd.AppendGuid("@guidTenantId", tenantId);
                cmd.AppendGuid("@guidSchedulerMonthlyId", info.SchedulerMonthlyId);
                cmd.AppendGuid("@guidSchedulerId", info.SchedulerId);
                cmd.AppendInt("@intUnit", info.Unit); 
                if(info.DayValue1.HasValue && info.DayValue1>0)
                   cmd.AppendInt("@intDayValue1", info.DayValue1.Value);  
                if(info.DayValue2.HasValue && info.DayValue2>0)
                   cmd.AppendInt("@intDayValue2", info.DayValue2.Value);      
                if(info.TheValue1.HasValue && info.TheValue1>0)
                   cmd.AppendInt("@intTheValue1", info.TheValue1.Value);    
                if(info.TheValue2.HasValue && info.TheValue2>0)
                   cmd.AppendInt("@intTheValue2", info.TheValue2.Value);  
                if(info.TheValue3.HasValue && info.TheValue3>0)
                   cmd.AppendInt("@intTheValue3", info.TheValue3.Value);                 
                ExecuteCommand(cmd);
                return true;               
            }
            catch (SqlException e)
            {
                throw ReportAndTranslateException(e, "DataSchedulerMonthly::SchedulerMonthly_Update");
            }
        }  

        internal bool Delete(Guid tenantId, Guid schedulerId )
        {
            try
            {
                var cmd = CreateProcedureCommand("dbo.SchedulerMonthly_Delete");
                cmd.AppendGuid("@guidTenantId", tenantId);                
                cmd.AppendGuid("@guidSchedulerId", schedulerId);                       
                ExecuteCommand(cmd);
                return true;               
            }
            catch (SqlException e)
            {
                throw ReportAndTranslateException(e, "DataSchedulerMonthly::SchedulerMonthly_Delete");
            }
        }       

     #endregion
  
  

    #region Review
      internal SchedulerMonthlyInfo GetSchedulerMonthly(Guid tenantId,Guid schedulerId )
        {
            SchedulerMonthlyInfo info =new SchedulerMonthlyInfo();
            try
            {
                var cmd = CreateProcedureCommand("dbo.SchedulerMonthly_GetBy_SchedulerId");
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
                throw ReportAndTranslateException(e, "DataSchedulerMonthly::SchedulerMonthly_GetBy_SchedulerId");
            }
            return info;
        }

        private static SchedulerMonthlyInfo ReadInfo(SqlDataReader reader)
        {
            var info = new SchedulerMonthlyInfo
            {
                SchedulerMonthlyId = reader.IsDBNull(0) ? Guid.Empty : reader.GetGuid(0),
                SchedulerId = reader.IsDBNull(1) ? Guid.Empty : reader.GetGuid(1),               
                Unit = reader.IsDBNull(2) ? 0 : reader.GetInt32(2) ,    
                DayValue1 = reader.IsDBNull(3) ? (int?)null : reader.GetInt32(3)  ,
                DayValue2 = reader.IsDBNull(4) ? (int?)null : reader.GetInt32(4)  ,
                TheValue1 = reader.IsDBNull(5) ? (int?)null : reader.GetInt32(5)  ,
                TheValue2 = reader.IsDBNull(6) ? (int?)null : reader.GetInt32(6)  ,
                TheValue3 = reader.IsDBNull(7) ? (int?)null : reader.GetInt32(7)  ,
            };
            return info;
        }

         #endregion
       
       
    }
}
