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

namespace VPC.Framework.Business.Role.Data
{
    internal sealed class DataTenantSubscription : EntityModelData
    {
         #region Review
      internal List<TenantSubscriptionInfo> TenantSubscriptions(Guid tenantId )
        {
            List<TenantSubscriptionInfo> list = new List<TenantSubscriptionInfo>();
            try
            {
                var cmd = CreateProcedureCommand("dbo.TenantSubscription_GetAll");
                cmd.AppendGuid("@guidTenantId", tenantId);
                using (SqlDataReader reader = ExecuteCommandAndReturnReader(cmd))
                { 
                        while (reader.Read())
                        {
                            list.Add(ReadInfo(reader));
                        } 
                }
            }
            catch (SqlException e)
            {
                throw ReportAndTranslateException(e, "TenantSubscription::TenantSubscription_GetAll");
            }
            return list;
        }

      

        internal TenantSubscriptionInfo TenantSubscription(Guid tenantId,Guid tenantSubscriptionId )
        {
            TenantSubscriptionInfo info = null;
            try
            {
                var cmd = CreateProcedureCommand("dbo.TenantSubscription_Get");
                cmd.AppendGuid("@guidTenantId", tenantId);
                cmd.AppendGuid("@guidTenantSubscriptionId", tenantSubscriptionId);
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
                throw ReportAndTranslateException(e, "TenantSubscription::TenantSubscription_Get");
            }
            return info;
        }
        private static TenantSubscriptionInfo ReadInfo(SqlDataReader reader)
        {
            var role = new TenantSubscriptionInfo
            {
                TenantSubscriptionId = reader.IsDBNull(0) ? Guid.Empty : reader.GetGuid(0),
                Name = reader.IsDBNull(1) ? string.Empty : reader.GetString(1),
                Group =new ItemName{Id= reader.IsDBNull(2) ? Guid.Empty : reader.GetGuid(2),Name=reader.IsDBNull(7) ? string.Empty : reader.GetString(7)},
                RecurringPrice = reader.IsDBNull(3) ? (decimal?)null : reader.GetDecimal(3),
                RecurringDuration = reader.IsDBNull(4) ? SubscriptionDuration.Weekly : (SubscriptionDuration)reader.GetByte(4),   
                SetUpPrice = reader.IsDBNull(5) ? (decimal?)null : reader.GetDecimal(5),
                 Status = reader.IsDBNull(6) ? false : reader.GetBoolean(6),      
            };
            return role;
        }

         #endregion
       
        #region Manage
        internal bool Create(Guid tenantId, TenantSubscriptionInfo info)
        {
            try
            {
                var cmd = CreateProcedureCommand("dbo.TenantSubscription_Create");
                cmd.AppendGuid("@guidTenantId", tenantId);
                cmd.AppendGuid("@guidTenantSubscriptionId", info.TenantSubscriptionId);
                cmd.AppendMediumText("@strName", info.Name);
                cmd.AppendGuid("@guidGroupId", info.Group.Id);               
                ExecuteCommand(cmd);
                return true;               
            }
            catch (SqlException e)
            {
                throw ReportAndTranslateException(e, "TenantSubscription::TenantSubscription_Create");
            }
        } 

        internal bool Update(Guid tenantId, TenantSubscriptionInfo info)
        {
            try
            {
                var cmd = CreateProcedureCommand("dbo.TenantSubscription_Update");
                cmd.AppendGuid("@guidTenantId", tenantId);
                cmd.AppendGuid("@guidTenantSubscriptionId", info.TenantSubscriptionId);
                cmd.AppendMediumText("@strName", info.Name);
                cmd.AppendGuid("@guidGroupId", info.Group.Id);
                  if(info.RecurringPrice.HasValue && info.RecurringPrice.Value>0.0m)
                cmd.AppendDecimal("@dRecurringPrice", info.RecurringPrice.Value);
                cmd.AppendTinyInt("@tinyintDuration", (byte)info.RecurringDuration);
                 if(info.SetUpPrice.HasValue && info.SetUpPrice.Value>0.0m)
                cmd.AppendDecimal("@dSetUpPrice", info.SetUpPrice.Value);              
                ExecuteCommand(cmd);
                return true;               
            }
            catch (SqlException e)
            {
                throw ReportAndTranslateException(e, "TenantSubscription::TenantSubscription_Update");
            }
        }

        internal bool Delete(Guid tenantId, Guid tenantSubscriptionId)
        {
            try
            {
                var cmd = CreateProcedureCommand("dbo.TenantSubscription_Delete");
                cmd.AppendGuid("@guidTenantId", tenantId);
                cmd.AppendGuid("@guidTenantSubscriptionId", tenantSubscriptionId);                    
                ExecuteCommand(cmd);
                return true;               
            }
            catch (SqlException e)
            {
                throw ReportAndTranslateException(e, "TenantSubscription::TenantSubscription_Delete");
            }
        }

        internal bool Status(Guid tenantId, Guid tenantSubscriptionId)
        {
            try
            {
                var cmd = CreateProcedureCommand("dbo.TenantSubscription_Status");
                cmd.AppendGuid("@guidTenantId", tenantId);
                cmd.AppendGuid("@guidTenantSubscriptionId", tenantSubscriptionId);                    
                ExecuteCommand(cmd);
                return true;               
            }
            catch (SqlException e)
            {
                throw ReportAndTranslateException(e, "TenantSubscription::TenantSubscription_Status");
            }
        }

     #endregion
    }
}
