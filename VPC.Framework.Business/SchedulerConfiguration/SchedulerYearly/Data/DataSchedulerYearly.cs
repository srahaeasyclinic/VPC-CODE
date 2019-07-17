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

namespace VPC.Framework.Business.SchedulerConfiguration.SchedulerYearly.Data
{
    internal sealed class DataSchedulerYearly : EntityModelData
    {
        #region Manage
        internal bool Create(Guid tenantId, SchedulerYearlyInfo info)
        {
            try
            {
                var cmd = CreateProcedureCommand("dbo.SchedulerYearly_Create");
                cmd.AppendGuid("@guidTenantId", tenantId);
                cmd.AppendGuid("@guidSchedulerYearlyId", info.SchedulerYearlyId);
                cmd.AppendGuid("@guidSchedulerId", info.SchedulerId);
                if(info.RecurrenceValue.HasValue && info.RecurrenceValue>0)
                cmd.AppendInt("@intRecurrenceValue", info.RecurrenceValue.Value); 
                cmd.AppendInt("@intUnit", info.Unit); 
                if(info.OnValue1.HasValue && info.OnValue1>0)
                cmd.AppendInt("@intOnValue1", info.OnValue1.Value);  
               if(info.OnValue2.HasValue && info.OnValue2>0)
                cmd.AppendInt("@intOnValue2", info.OnValue2.Value); 
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
                throw ReportAndTranslateException(e, "DataSchedulerYearly::SchedulerYearly_Create");
            }
        } 

        internal bool Update(Guid tenantId, SchedulerYearlyInfo info)
        {
            try
            {
                var cmd = CreateProcedureCommand("dbo.SchedulerYearly_Update");
                cmd.AppendGuid("@guidTenantId", tenantId);
                cmd.AppendGuid("@guidSchedulerYearlyId", info.SchedulerYearlyId);
                cmd.AppendGuid("@guidSchedulerId", info.SchedulerId);
                if(info.RecurrenceValue.HasValue && info.RecurrenceValue>0)
                cmd.AppendInt("@intRecurrenceValue", info.RecurrenceValue.Value); 
                cmd.AppendInt("@intUnit", info.Unit); 
                if(info.OnValue1.HasValue && info.OnValue1>0)
                cmd.AppendInt("@intOnValue1", info.OnValue1.Value);  
               if(info.OnValue2.HasValue && info.OnValue2>0)
                cmd.AppendInt("@intOnValue2", info.OnValue2.Value); 
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
                throw ReportAndTranslateException(e, "DataSchedulerYearly::SchedulerYearly_Update");
            }
        }    

        internal bool Delete(Guid tenantId, Guid schedulerId )
        {
            try
            {
                var cmd = CreateProcedureCommand("dbo.SchedulerYearly_Delete");
                cmd.AppendGuid("@guidTenantId", tenantId);                
                cmd.AppendGuid("@guidSchedulerId", schedulerId);                       
                ExecuteCommand(cmd);
                return true;               
            }
            catch (SqlException e)
            {
                throw ReportAndTranslateException(e, "DataSchedulerYearly::SchedulerYearly_Delete");
            }
        }     

     #endregion
  
  

    #region Review
      internal SchedulerYearlyInfo GetSchedulerYearly(Guid tenantId,Guid schedulerId )
        {
            SchedulerYearlyInfo info =null;
            try
            {
                var cmd = CreateProcedureCommand("dbo.SchedulerYearly_GetBy_SchedulerId");
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
                throw ReportAndTranslateException(e, "DataSchedulerYearly::SchedulerYearly_GetBy_SchedulerId");
            }
            return info;
        }

        private static SchedulerYearlyInfo ReadInfo(SqlDataReader reader)
        {
            var info = new SchedulerYearlyInfo
            {
                SchedulerYearlyId = reader.IsDBNull(0) ? Guid.Empty : reader.GetGuid(0),
                SchedulerId = reader.IsDBNull(1) ? Guid.Empty : reader.GetGuid(1),               
                RecurrenceValue = reader.IsDBNull(2) ? 0 : reader.GetInt32(2) ,    
                Unit = reader.IsDBNull(3) ? 0 : reader.GetInt32(3)  ,
                OnValue1 = reader.IsDBNull(4) ? (int?)null : reader.GetInt32(4)  ,
                OnValue2 = reader.IsDBNull(5) ?  (int?)null  : reader.GetInt32(5)  ,
                TheValue1 = reader.IsDBNull(6) ?  (int?)null  : reader.GetInt32(6)  ,
                TheValue2 = reader.IsDBNull(7) ?  (int?)null  : reader.GetInt32(7)  ,
                TheValue3 = reader.IsDBNull(8) ?  (int?)null  : reader.GetInt32(8)  ,
              
            };
            return info;
        }

         #endregion
       
       
    }
}
