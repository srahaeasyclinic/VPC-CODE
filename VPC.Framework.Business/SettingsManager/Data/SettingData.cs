using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using VPC.Entities.EntityCore.Model.Setting;
using VPC.Framework.Business.Data;

namespace VPC.Framework.Business.SettingsManager.Data
{
    internal sealed class SettingData : EntityModelData
    {
        #region Review
        internal List<Setting> GetSettings(Guid tenantId, Guid? SettingId)
        {
            List<Setting> settingLst = new List<Setting>();
            try
            {
                var cmd = CreateProcedureCommand("dbo.Setting_GetById");
                cmd.AppendGuid("@guidTenantId", tenantId);
                if (SettingId != null && SettingId != Guid.Empty)
                {
                    cmd.AppendGuid("@guidId", SettingId.Value);
                }

                else
                {
                    cmd.AppendGuid("@guidId", Guid.Empty);
                    var dd = Guid.Empty;
                }

                using (SqlDataReader reader = ExecuteCommandAndReturnReader(cmd))
                {
                    while (reader.Read())
                    {
                        settingLst.Add(ReadSetting(reader));
                    }
                }
            }
            catch (SqlException e)
            {
                throw ReportAndTranslateException(e, "Settings::Setting_GetById");
            }
            return settingLst;
        }

        internal Setting GetSettingsBycontext(Guid tenantId, SettingContextTypeEnum contexttype)
        {
            Setting settingLst = new Setting();
            try
            {
                var cmd = CreateProcedureCommand("dbo.Setting_GetByContextType");
                cmd.AppendGuid("@guidTenantId", tenantId);
                cmd.AppendInt("@contexttype", (int)contexttype);

                using (SqlDataReader reader = ExecuteCommandAndReturnReader(cmd))
                {
                    while (reader.Read())
                    {
                        settingLst=ReadSetting(reader);
                    }
                }
            }
            catch (SqlException e)
            {
                throw ReportAndTranslateException(e, "Settings::Setting_GetByContextType");
            }
            return settingLst;
        }

        private static Setting ReadSetting(SqlDataReader reader)
        {
            var setting = new Setting();

            setting.Id = reader.IsDBNull(0) ? Guid.Empty : reader.GetGuid(0);
            setting.Context = reader.IsDBNull(1) ? 0 : (SettingContextTypeEnum)reader.GetInt32(1);
            setting.Content = reader.IsDBNull(2) ? string.Empty : Convert.ToString(reader.GetSqlString(2));
            setting.UpdatedOn = reader.IsDBNull(3) ? DateTime.UtcNow : DateTime.SpecifyKind((DateTime)reader.GetSqlDateTime(3), DateTimeKind.Utc);
            setting.UpdatedBy = reader.IsDBNull(4) ? Guid.Empty : reader.GetGuid(4);

            return setting;
        }

        #endregion

        #region Manage
        internal bool Create(Guid tenantId, Setting settinginfo)
        {
            try
            {
                var cmd = CreateProcedureCommand("dbo.Setting_Create");
                cmd.AppendGuid("@guidTenantId", tenantId);
                cmd.AppendGuid("@guidId", settinginfo.Id);
                cmd.AppendInt("@context", (int)settinginfo.Context);
                cmd.AppendXLargeText("@content", settinginfo.Content);
                cmd.AppendGuid("@guidUpdatedBy", settinginfo.UpdatedBy);
                ExecuteCommand(cmd);
                return true;
            }
            catch (SqlException e)
            {
                throw ReportAndTranslateException(e, "Setting::Setting_Create");
            }
        }
        internal bool Update(Guid tenantId, Setting settinginfo)
        {
            try
            {
                var cmd = CreateProcedureCommand("dbo.Setting_Update");
                cmd.AppendGuid("@guidTenantId", tenantId);
                cmd.AppendGuid("@guidId", settinginfo.Id);
                cmd.AppendInt("@context", (int)settinginfo.Context);
                cmd.AppendXLargeText("@content", settinginfo.Content);
                cmd.AppendGuid("@guidUpdatedBy", settinginfo.UpdatedBy);
                ExecuteCommand(cmd);
                return true;
            }
            catch (SqlException e)
            {
                throw ReportAndTranslateException(e, "Setting::Setting_Update");
            }
        }

        internal bool Delete(Guid tenantId, Guid settingId)
        {
            try
            {
                var cmd = CreateProcedureCommand("dbo.Setting_DeleteById");
                cmd.AppendGuid("@guidTenantId", tenantId);
                cmd.AppendGuid("@guidId", settingId);
                ExecuteCommand(cmd);
                return true;
            }
            catch (SqlException e)
            {
                throw ReportAndTranslateException(e, "Setting::Setting_Delete");
            }
        }
        #endregion

    }
}
