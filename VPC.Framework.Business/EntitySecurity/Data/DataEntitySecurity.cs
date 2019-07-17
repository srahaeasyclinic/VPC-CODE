using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using VPC.Entities.EntitySecurity;
using VPC.Entities.Role;
using VPC.Framework.Business.Data;
using VPC.Framework.Business.Data.SqlClient;

namespace VPC.Framework.Business.EntitySecurity.Data
{
    internal sealed class DataEntitySecurity : EntityModelData
    {
         #region Review
      internal EntitySecurityInfo GetEntitySecurity(Guid tenantId,string entityId , Guid roleId  )
        {
            EntitySecurityInfo entitiesLst = null;
            try
            {
                var cmd = CreateProcedureCommand("dbo.EntitySecurity_Get");
                cmd.AppendGuid("@guidTenantId", tenantId);
                cmd.AppendXSmallText("@strEntityId", entityId);
                cmd.AppendGuid("@guidRoleId", roleId);        
                using (SqlDataReader reader = ExecuteCommandAndReturnReader(cmd))
                { 
                        while (reader.Read())
                        {
                            entitiesLst=ReadData(reader);
                        } 
                }
            }
            catch (SqlException e)
            {
                throw ReportAndTranslateException(e, "Entity_Security::EntitySecurity_Get");
            }
            return entitiesLst;
        }

       internal List<EntitySecurityInfo> GetEntitySecurities(Guid tenantId, string entityId,  Guid? roleId )
        {
            List<EntitySecurityInfo> entitiesLst = new List<EntitySecurityInfo>();
            try
            {
                var cmd = CreateProcedureCommand("dbo.EntitySecurity_GetAll_ById");
                cmd.AppendGuid("@guidTenantId", tenantId);
                cmd.AppendXSmallText("@strEntityId", entityId);
                if(roleId !=null && roleId!=Guid.Empty)
                 cmd.AppendGuid("@guidRoleId", roleId.Value);        
                using (SqlDataReader reader = ExecuteCommandAndReturnReader(cmd))
                { 
                        while (reader.Read())
                        {
                            entitiesLst.Add(ReadData(reader));
                        } 
                }
            }
            catch (SqlException e)
            {
                throw ReportAndTranslateException(e, "Entity_Security::EntitySecurity_GetAll_ById");
            }
            return entitiesLst;
        }

        
       internal List<EntitySecurityInfo> GetEntitySecuritiesByUserCode(Guid tenantId, Guid userId)
        {
            List<EntitySecurityInfo> entitiesLst = new List<EntitySecurityInfo>();
            try
            {
                var cmd = CreateProcedureCommand("dbo.EntitySecurity_GetAll_ByUserId");
                cmd.AppendGuid("@guidTenantId", tenantId);
                cmd.AppendGuid("@guidUserId", userId);         
                using (SqlDataReader reader = ExecuteCommandAndReturnReader(cmd))
                { 
                        while (reader.Read())
                        {
                            entitiesLst.Add(ReadData(reader));
                        } 
                }
            }
            catch (SqlException e)
            {
                throw ReportAndTranslateException(e, "Entity_Security::EntitySecurity_GetAll_ById");
            }
            return entitiesLst;
        }




       internal List<EntitySecurityInfo> GetFunctionSecurities(Guid tenantId, string entityId,  Guid? roleId )
        {
            List<EntitySecurityInfo> entitiesLst = new List<EntitySecurityInfo>();
            try
            {
                var cmd = CreateProcedureCommand("dbo.FunctionSecurity_GetAll_ById");
                cmd.AppendGuid("@guidTenantId", tenantId);
                cmd.AppendXSmallText("@strEntityId", entityId);
                if(roleId !=null && roleId!=Guid.Empty)
                 cmd.AppendGuid("@guidRoleId", roleId.Value);        
                using (SqlDataReader reader = ExecuteCommandAndReturnReader(cmd))
                { 
                        while (reader.Read())
                        {
                            entitiesLst.Add(ReadData(reader));
                        } 
                }
            }
            catch (SqlException e)
            {
                throw ReportAndTranslateException(e, "Entity_Security::FunctionSecurity_GetAll_ById");
            }
            return entitiesLst;
        }

        
       internal List<EntitySecurityInfo> GetFunctionSecuritiesByUserCode(Guid tenantId, Guid userId)
        {
            List<EntitySecurityInfo> entitiesLst = new List<EntitySecurityInfo>();
            try
            {
                var cmd = CreateProcedureCommand("dbo.FunctionSecurity_GetAll_ByUserId");
                cmd.AppendGuid("@guidTenantId", tenantId);
                cmd.AppendGuid("@guidUserId", userId);         
                using (SqlDataReader reader = ExecuteCommandAndReturnReader(cmd))
                { 
                        while (reader.Read())
                        {
                            entitiesLst.Add(ReadData(reader));
                        } 
                }
            }
            catch (SqlException e)
            {
                throw ReportAndTranslateException(e, "Entity_Security::FunctionSecurity_GetAll_ByUserId");
            }
            return entitiesLst;
        }





        private static EntitySecurityInfo ReadData(SqlDataReader reader)
        {
            var role = new EntitySecurityInfo
            {
                EntitySecurityId = reader.IsDBNull(0) ? Guid.Empty : reader.GetGuid(0),
                EntityId = reader.IsDBNull(1) ? string.Empty : reader.GetString(1),
                RoleId = reader.IsDBNull(2) ? Guid.Empty : reader.GetGuid(2),
                SecurityCode=reader.IsDBNull(3) ? 0 : reader.GetInt32(3),
                FunctionContext=   reader.IsDBNull(4) ? Guid.Empty : reader.GetGuid(4),      
            };
            return role;
        }

         #endregion
       
        #region Manage
        internal bool Create(Guid tenantId, EntitySecurityInfo entitySecurityInfo)
        {
            try
            {
                var cmd = CreateProcedureCommand("dbo.EntitySecurity_Create");
                cmd.AppendGuid("@guidTenantId", tenantId);
                cmd.AppendGuid("@guidEntitySecurityId", entitySecurityInfo.EntitySecurityId);
                cmd.AppendXSmallText("@strEntityId", entitySecurityInfo.EntityId);
                cmd.AppendGuid("@guidRoleId", entitySecurityInfo.RoleId);
                cmd.AppendInt("@intSecurityCode", entitySecurityInfo.SecurityCode);
                if(entitySecurityInfo.FunctionContext !=Guid.Empty)
                cmd.AppendGuid("@guidFunctionContext", entitySecurityInfo.FunctionContext);
                ExecuteCommand(cmd);
                return true;               
            }
            catch (SqlException e)
            {
                throw ReportAndTranslateException(e, "Entity_Security::EntitySecurity_Create");
            }
        } 

        internal bool Update(Guid tenantId, EntitySecurityInfo entitySecurityInfo)
        {
            try
            {
                var cmd = CreateProcedureCommand("dbo.EntitySecurity_Update");
                cmd.AppendGuid("@guidTenantId", tenantId);
                cmd.AppendGuid("@guidEntitySecurityId", entitySecurityInfo.EntitySecurityId);
                cmd.AppendXSmallText("@strEntityId", entitySecurityInfo.EntityId);
                cmd.AppendGuid("@guidRoleId", entitySecurityInfo.RoleId);
                cmd.AppendInt("@intSecurityCode", entitySecurityInfo.SecurityCode);
                ExecuteCommand(cmd);
                return true;              
            }
            catch (SqlException e)
            {
                throw ReportAndTranslateException(e, "Entity_Security::EntitySecurity_Update");
            }
        }

        

     #endregion
    }
}
