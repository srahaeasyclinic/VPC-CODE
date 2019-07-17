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
using VPC.Framework.Business.Common;

namespace VPC.Framework.Business.Role.Data
{
    internal sealed class DataTenantSubscriptionEntityDetail : EntityModelData
    {
        #region Review
      internal List<TenantSubscriptionEntityDetailInfo> TenantSubscriptionEntityDetails(Guid tenantId,Guid tenantSubscriptionEntityId )
        {
            List<TenantSubscriptionEntityDetailInfo> list = new List<TenantSubscriptionEntityDetailInfo>();
            try
            {
                var cmd = CreateProcedureCommand("dbo.TenantSubscriptionEntityDetail_GetAll");
                cmd.AppendGuid("@guidTenantId", tenantId);
                cmd.AppendGuid("@guidTenantSubscriptionEntityId", tenantSubscriptionEntityId);
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
                throw ReportAndTranslateException(e, "TenantSubscriptionEntityDetail::TenantSubscriptionEntityDetail_GetAll");
            }
            return list;
        }

      

        internal TenantSubscriptionEntityDetailInfo TenantSubscriptionEntityDetail(Guid tenantId,Guid tenantSubscriptionEntityDetailId )
        {
            TenantSubscriptionEntityDetailInfo info = null;
            try
            {
                var cmd = CreateProcedureCommand("dbo.TenantSubscriptionEntityDetail_Get");
                cmd.AppendGuid("@guidTenantId", tenantId);
                cmd.AppendGuid("@guidTenantSubscriptionEntityDetailId", tenantSubscriptionEntityDetailId);
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
                throw ReportAndTranslateException(e, "TenantSubscriptionEntityDetail::TenantSubscriptionEntityDetail_Get");
            }
            return info;
        }
        private static TenantSubscriptionEntityDetailInfo ReadInfo(SqlDataReader reader)
        {
            var role = new TenantSubscriptionEntityDetailInfo
            {
                SubscriptionEntityDetailId = reader.IsDBNull(0) ? Guid.Empty : reader.GetGuid(0),
                SubscriptionEntityId = reader.IsDBNull(1) ? Guid.Empty : reader.GetGuid(1),
                Context = reader.IsDBNull(2) ? Guid.Empty : reader.GetGuid(2),                
                RecurringPrice = reader.IsDBNull(3) ? (decimal?)null : reader.GetDecimal(3),
                RecurringDuration = reader.IsDBNull(4) ? SubscriptionDuration.Weekly : (SubscriptionDuration)reader.GetByte(4),
                OneTimePrice = reader.IsDBNull(5) ? (decimal?)null : reader.GetDecimal(5),
                OneTimeDuration = reader.IsDBNull(6) ? SubscriptionDuration.Weekly : (SubscriptionDuration)reader.GetByte(6)
                  
            };
            return role;
        }

         #endregion
       
        #region Manage
        internal bool Create(Guid tenantId, TenantSubscriptionEntityDetailInfo info)
        {
            try
            {
                var cmd = CreateProcedureCommand("dbo.TenantSubscriptionEntityDetail_Create");
                cmd.AppendGuid("@guidTenantId", tenantId);
                cmd.AppendGuid("@guidSubscriptionEntityDetailId", info.SubscriptionEntityDetailId);
                cmd.AppendGuid("@guidSubscriptionEntityId", info.SubscriptionEntityId);
                cmd.AppendGuid("@guidContext", info.Context);
                if(info.RecurringPrice.HasValue && info.RecurringPrice.Value>0)
                {
                   cmd.AppendDecimal("@dRecurringPrice", info.RecurringPrice.Value);
                   cmd.AppendTinyInt("@tinyintRecurringDuration", (byte)info.RecurringDuration);
                }
                if(info.OneTimePrice.HasValue && info.OneTimePrice.Value>0)
                {
                   cmd.AppendDecimal("@dOneTimePrice", info.OneTimePrice.Value);
                   cmd.AppendTinyInt("@tinyintOneTimeDuration", (byte)info.OneTimeDuration);
                }
                ExecuteCommand(cmd);
                return true;               
            }
            catch (SqlException e)
            {
                throw ReportAndTranslateException(e, "TenantSubscriptionEntityDetail::TenantSubscriptionEntityDetail_Create");
            }
        } 

        internal bool Create(Guid tenantId, List<TenantSubscriptionEntityDetailInfo> infos)
        {
            try
            {
                var cmd = CreateProcedureCommand("dbo.TenantSubscriptionEntityDetail_Create_Xml");
                cmd.AppendGuid("@guidTenantId", tenantId);
                cmd.AppendXml("@xmlSubscriptionEntityDetails", DataUtility.GetXmlSubscriptionEntityDetails(infos));               
                ExecuteCommand(cmd);
                return true;               
            }
            catch (SqlException e)
            {
                throw ReportAndTranslateException(e, "TenantSubscriptionEntityDetail::TenantSubscriptionEntityDetail_Create_Xml");
            }
        }

        internal bool Update(Guid tenantId, TenantSubscriptionEntityDetailInfo info)
        {
            try
            {
               var cmd = CreateProcedureCommand("dbo.TenantSubscriptionEntityDetail_Update");
                cmd.AppendGuid("@guidTenantId", tenantId);
                cmd.AppendGuid("@guidSubscriptionEntityDetailId", info.SubscriptionEntityDetailId);               
                if(info.RecurringPrice.HasValue && info.RecurringPrice.Value>0)
                {
                   cmd.AppendDecimal("@dRecurringPrice", info.RecurringPrice.Value);
                   cmd.AppendTinyInt("@tinyintRecurringDuration", (byte)info.RecurringDuration);
                }
                if(info.OneTimePrice.HasValue && info.OneTimePrice.Value>0)
                {
                   cmd.AppendDecimal("@dOneTimePrice", info.OneTimePrice.Value);
                   cmd.AppendTinyInt("@tinyintOneTimeDuration", (byte)info.OneTimeDuration);
                }
                ExecuteCommand(cmd);
                return true;                
            }
            catch (SqlException e)
            {
                throw ReportAndTranslateException(e, "TenantSubscriptionEntityDetail::TenantSubscriptionEntityDetail_Update");
            }
        }

        internal bool Delete(Guid tenantId, Guid subscriptionEntityDetailId)
        {
            try
            {
                var cmd = CreateProcedureCommand("dbo.TenantSubscriptionEntityDetail_Delete");
                cmd.AppendGuid("@guidTenantId", tenantId);
                cmd.AppendGuid("@guidSubscriptionEntityDetailId", subscriptionEntityDetailId);                 
                ExecuteCommand(cmd);
                return true;               
            }
            catch (SqlException e)
            {
                throw ReportAndTranslateException(e, "TenantSubscriptionEntityDetail::TenantSubscriptionEntityDetail_Delete");
            }
        }

        internal bool DeleteBySubscriptionEntityId(Guid tenantId, Guid subscriptionEntityId)
        {
            try
            {
                var cmd = CreateProcedureCommand("dbo.TenantSubscriptionEntityDetail_DeleteBySubsEntityId");
                cmd.AppendGuid("@guidTenantId", tenantId);
                cmd.AppendGuid("@guidSubscriptionEntityId", subscriptionEntityId);                 
                ExecuteCommand(cmd);
                return true;               
            }
            catch (SqlException e)
            {
                throw ReportAndTranslateException(e, "TenantSubscriptionEntityDetail::TenantSubscriptionEntityDetail_DeleteBySubsEntityId");
            }
        }

      

     #endregion
   
    }
}
