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
    internal sealed class DataTenantSubscriptionEntity : EntityModelData
    {
        #region Review
      internal List<TenantSubscriptionEntityInfo> TenantSubscriptionEntities(Guid tenantId,Guid tenantSubscriptionId )
        {
            List<TenantSubscriptionEntityInfo> list = new List<TenantSubscriptionEntityInfo>();
            try
            {
                var cmd = CreateProcedureCommand("dbo.TenantSubscriptionEntity_GetAll");
                cmd.AppendGuid("@guidTenantId", tenantId);
                if(tenantSubscriptionId!=Guid.Empty)
                   cmd.AppendGuid("@guidTenantSubscriptionId", tenantSubscriptionId);
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
                throw ReportAndTranslateException(e, "TenantSubscriptionEntity::TenantSubscriptionEntity_GetAll");
            }
            return list;
        }
     

        internal List<TenantSubscriptionEntityInfo> GetSubscriptionsByTenantId(Guid tenantId)
        {
            List<TenantSubscriptionEntityInfo> list = new List<TenantSubscriptionEntityInfo>();
            try
            {
                var cmd = CreateProcedureCommand("dbo.GetSubscriptionsByTenantId");
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
                throw ReportAndTranslateException(e, "TenantSubscriptionEntity::TenantSubscriptionEntity_GetAll");
            }
            return list;
        }

        internal TenantSubscriptionEntityInfo TenantSubscriptionEntity(Guid tenantId,Guid tenantSubscriptionEntityId )
        {
            TenantSubscriptionEntityInfo info = null;
            try
            {
                var cmd = CreateProcedureCommand("dbo.TenantSubscriptionEntity_Get");
                cmd.AppendGuid("@guidTenantId", tenantId);
                cmd.AppendGuid("@guidTenantSubscriptionEntityId", tenantSubscriptionEntityId);
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
                throw ReportAndTranslateException(e, "TenantSubscriptionEntity::TenantSubscriptionEntity_Get");
            }
            return info;
        }
        private static TenantSubscriptionEntityInfo ReadInfo(SqlDataReader reader)
        {
            var role = new TenantSubscriptionEntityInfo
            {
                TenantSubscriptionEntityId = reader.IsDBNull(0) ? Guid.Empty : reader.GetGuid(0),
                TenantSubscriptionId = reader.IsDBNull(1) ? Guid.Empty : reader.GetGuid(1),
                EntityId = reader.IsDBNull(2) ? string.Empty : reader.GetString(2),                
                LimtNumber = reader.IsDBNull(3) ? (int?)null : reader.GetInt32(3),
                LimitType = reader.IsDBNull(4) ? LimitTypes.TotalCount : (LimitTypes)reader.GetByte(4)
                  
            };
            return role;
        }

         #endregion
       
        #region Manage
        internal bool Create(Guid tenantId, TenantSubscriptionEntityInfo info)
        {
            try
            {
                var cmd = CreateProcedureCommand("dbo.TenantSubscriptionEntity_Create");
                cmd.AppendGuid("@guidTenantId", tenantId);
                cmd.AppendGuid("@guidTenantSubscriptionEntityId", info.TenantSubscriptionEntityId);
                cmd.AppendGuid("@guidTenantSubscriptionId", info.TenantSubscriptionId);
                cmd.AppendXSmallText("@strEntityId", info.EntityId); 
                ExecuteCommand(cmd);
                return true;               
            }
            catch (SqlException e)
            {
                throw ReportAndTranslateException(e, "TenantSubscriptionEntity::TenantSubscriptionEntity_Create");
            }
        } 

        internal bool Create(Guid tenantId, List<TenantSubscriptionEntityInfo> infos)
        {
            try
            {
                var cmd = CreateProcedureCommand("dbo.TenantSubscriptionEntity_Create_Xml");
                cmd.AppendGuid("@guidTenantId", tenantId);
                cmd.AppendXml("@xmlEntities", DataUtility.GetXmlSubscriptionEntities(infos));                
                ExecuteCommand(cmd);
                return true;               
            }
            catch (SqlException e)
            {
                throw ReportAndTranslateException(e, "TenantSubscriptionEntity::TenantSubscriptionEntity_Create_Xml");
            } 
        }

        internal bool Update(Guid tenantId, TenantSubscriptionEntityInfo info)
        {
            try
            {
                var cmd = CreateProcedureCommand("dbo.TenantSubscriptionEntity_Update");
                cmd.AppendGuid("@guidTenantId", tenantId);
                cmd.AppendGuid("@guidTenantSubscriptionEntityId", info.TenantSubscriptionEntityId);
                cmd.AppendGuid("@guidTenantSubscriptionId", info.TenantSubscriptionId);
                cmd.AppendXSmallText("@strEntityId", info.EntityId);
                if(info.LimtNumber.HasValue && info.LimtNumber.Value>0)
                {
                    cmd.AppendInt("@intLimitNumber", info.LimtNumber.Value);
                    cmd.AppendTinyInt("@tinyintLimitType", (byte)info.LimitType);
                }           
                ExecuteCommand(cmd);
                return true;               
            }
            catch (SqlException e)
            {
                throw ReportAndTranslateException(e, "TenantSubscriptionEntity::TenantSubscriptionEntity_Update");
            }
        }

        internal bool Delete(Guid tenantId, Guid tenantSubscriptionEntityId)
        {
            try
            {
                var cmd = CreateProcedureCommand("dbo.TenantSubscriptionEntity_Delete");
                cmd.AppendGuid("@guidTenantId", tenantId);
                cmd.AppendGuid("@guidTenantSubscriptionEntityId", tenantSubscriptionEntityId);                  
                ExecuteCommand(cmd);
                return true;               
            }
            catch (SqlException e)
            {
                throw ReportAndTranslateException(e, "TenantSubscriptionEntity::TenantSubscriptionEntity_Delete");
            }
        }

      

     #endregion
   
   
    }
}
