using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using VPC.Entities.Common;
using VPC.Entities.Role;
using VPC.Framework.Business.Common;
using VPC.Framework.Business.Data;
using VPC.Framework.Business.Data.SqlClient;

namespace VPC.Framework.Business.Role.Data
{
    internal sealed class DataRole : EntityModelData
    {
        #region Review
        internal List<RoleInfo> Roles(Guid tenantId)
        {
            List<RoleInfo> roleLst = new List<RoleInfo>();

            try
            {
                var cmd = CreateProcedureCommand("dbo.Role_GetAll");
                cmd.AppendGuid("@guidTenantId", tenantId);

                using (SqlDataReader reader = ExecuteCommandAndReturnReader(cmd))
                {
                    while (reader.Read())
                    {
                        roleLst.Add(ReadRole(reader));
                    }
                }
            }
            catch (SqlException e)
            {
                throw ReportAndTranslateException(e, "Role::Role_GetAll");
            }

            return roleLst;
        }

        internal List<RoleInfo> Roles(Guid tenantId, Guid? roleId)
        {
            List<RoleInfo> roleLst = new List<RoleInfo>();

            try
            {
                var cmd = CreateProcedureCommand("dbo.Roles_GetAll_ById");
                cmd.AppendGuid("@guidTenantId", tenantId);

                if (roleId != null && roleId != Guid.Empty)
                    cmd.AppendGuid("@guidRoleId", roleId.Value);

                using (SqlDataReader reader = ExecuteCommandAndReturnReader(cmd))
                {
                    while (reader.Read())
                    {
                        roleLst.Add(ReadRole(reader));
                    }
                }
            }
            catch (SqlException e)
            {
                throw ReportAndTranslateException(e, "Role::Roles_GetAll_ById");
            }

            return roleLst;
        }

        internal RoleInfo Role(Guid tenantId, Guid roleId)
        {
            RoleInfo roleLst = null;

            try
            {
                var cmd = CreateProcedureCommand("dbo.Role_Get");
                cmd.AppendGuid("@guidTenantId", tenantId);
                cmd.AppendGuid("@guidRoleId", roleId);

                using (SqlDataReader reader = ExecuteCommandAndReturnReader(cmd))
                {
                    while (reader.Read())
                    {
                        roleLst = ReadRole(reader);
                    }
                }
            }
            catch (SqlException e)
            {
                throw ReportAndTranslateException(e, "Role::Role_Get");
            }

            return roleLst;
        }

        private static RoleInfo ReadRole(SqlDataReader reader)
        {
            var role = new RoleInfo
            {
                RoleId = reader.IsDBNull(0) ? Guid.Empty : reader.GetGuid(0),
                Name = reader.IsDBNull(1) ? string.Empty : reader.GetString(1),
                RoleType = reader.IsDBNull(2) ? RoleTypeEnum.DetailerAndCarWashe : (RoleTypeEnum)reader.GetByte(2),
            };
            return role;
        }

        #endregion

        #region Manage
        internal bool Create(Guid tenantId, RoleInfo roleInfo)
        {
            try
            {
                var cmd = CreateProcedureCommand("dbo.Role_Create");
                cmd.AppendGuid("@guidTenantId", tenantId);
                cmd.AppendGuid("@guidRoleId", roleInfo.RoleId);
                cmd.AppendMediumText("@strName", roleInfo.Name);
                cmd.AppendGuid("@guidModifiedBy", roleInfo.AuditDetail.ModifiedBy);
                cmd.AppendTinyInt("@byteRoleType", (byte)roleInfo.RoleType);
                ExecuteCommand(cmd);
                return true;
            }
            catch (SqlException e)
            {
                throw ReportAndTranslateException(e, "Role::Role_Create");
            }
        }

        internal bool CreateRoles(Guid tenantId, List<RoleInfo> roleInfos)
        {
            try
            {
                var cmd = CreateProcedureCommand("dbo.Role_Create_Xml");
                cmd.AppendGuid("@guidTenantId", tenantId);
                cmd.AppendXml("@xmlRoles", DataUtility.GetXmlForRoles(roleInfos));
                ExecuteCommand(cmd);
                return true;
            }
            catch (SqlException e)
            {
                throw ReportAndTranslateException(e, "Role::Role_Create_Xml");
            }
        }

        internal bool Update(Guid tenantId, RoleInfo roleInfo)
        {
            try
            {
                var cmd = CreateProcedureCommand("dbo.Role_Update");
                cmd.AppendGuid("@guidTenantId", tenantId);
                cmd.AppendGuid("@guidRoleId", roleInfo.RoleId);
                cmd.AppendMediumText("@strName", roleInfo.Name);
                ExecuteCommand(cmd);
                return true;
            }
            catch (SqlException e)
            {
                throw ReportAndTranslateException(e, "Role::Role_Update");
            }
        }

        internal bool Delete(Guid tenantId, Guid roleId)
        {
            try
            {
                var cmd = CreateProcedureCommand("dbo.Role_Delete");
                cmd.AppendGuid("@guidTenantId", tenantId);
                cmd.AppendGuid("@guidRoleId", roleId);
                ExecuteCommand(cmd);
                return true;
            }
            catch (SqlException e)
            {
                throw ReportAndTranslateException(e, "Role::Role_Delete");
            }
        }

        internal UserDetailInfo GetUserDetails(Guid tenantId, Guid userId)
        {
            var userDetails = new UserDetailInfo();

            try
            {
                SqlProcedureCommand cmd = CreateProcedureCommand("dbo.User_GetBy_Id");
                cmd.AppendGuid("@guidTenantId", tenantId);
                cmd.AppendGuid("@userId", userId);

                using (SqlDataReader reader = ExecuteCommandAndReturnReader(cmd))
                {
                    while (reader.Read())
                    {
                        userDetails.Id = reader.IsDBNull(0) ? Guid.Empty : reader.GetGuid(0);
                        userDetails.Name = reader.IsDBNull(1) ? string.Empty : reader.GetString(1);
                        userDetails.IsSuperadmin = reader.IsDBNull(2) ? false : reader.GetBoolean(2);
                        userDetails.IsSystemAdmin = reader.IsDBNull(3) ? false : reader.GetBoolean(3);
                    }
                }
            }
            catch (SqlException e)
            {
                throw ReportAndTranslateException(e, "LayoutData::GetLayoutsDetailsById");
            }

            return userDetails;
        }

        #endregion
    }
}
