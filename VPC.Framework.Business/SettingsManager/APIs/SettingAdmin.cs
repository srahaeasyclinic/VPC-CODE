using System;
using VPC.Entities.EntityCore.Model.Setting;
using VPC.Framework.Business.SettingsManager.Data;

namespace VPC.Framework.Business.SettingsManager.APIs
{
    internal interface ISettingAdmin
    {
        bool Create(Guid tenantId, Setting settingsinfo);
        bool Update(Guid tenantId, Setting settingsinfo);
        bool Delete(Guid tenantId, Guid settingId);
    }
    internal class SettingAdmin : ISettingAdmin
    {
        private readonly SettingData _dataSetting = new SettingData();
        bool ISettingAdmin.Create(Guid tenantId, Setting settingsinfo)
        {
            return _dataSetting.Create(tenantId, settingsinfo);
        }

        bool ISettingAdmin.Delete(Guid tenantId, Guid settingId)
        {
            return _dataSetting.Delete(tenantId, settingId);
        }

        bool ISettingAdmin.Update(Guid tenantId, Setting settingsinfo)
        {
            return _dataSetting.Update(tenantId, settingsinfo);
        }
    }
}
