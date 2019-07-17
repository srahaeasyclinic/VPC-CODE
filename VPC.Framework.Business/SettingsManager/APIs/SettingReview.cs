using System;
using System.Collections.Generic;
using System.Linq;
using VPC.Entities.EntityCore.Model.Setting;
using VPC.Framework.Business.SettingsManager.Data;

namespace VPC.Framework.Business.SettingsManager.APIs
{
    internal interface ISettingReview
    {
        List<Setting> GetSettings(Guid tenantId);
        Setting GetSettings(Guid tenantId, Guid settingId);

        Setting GetSettingsByContext(Guid tenantId, SettingContextTypeEnum contexttype);
    }
    internal class SettingReview : ISettingReview
    {
        private readonly SettingData _dataSetting = new SettingData();
        List<Setting> ISettingReview.GetSettings(Guid tenantId)
        {
            return _dataSetting.GetSettings(tenantId, null);
        }

        Setting ISettingReview.GetSettings(Guid tenantId, Guid settingId)
        {
            return _dataSetting.GetSettings(tenantId, settingId).FirstOrDefault();
        }

        Setting ISettingReview.GetSettingsByContext(Guid tenantId, SettingContextTypeEnum contexttype)
        {
            return _dataSetting.GetSettingsBycontext(tenantId, contexttype);
        }
    }
}
