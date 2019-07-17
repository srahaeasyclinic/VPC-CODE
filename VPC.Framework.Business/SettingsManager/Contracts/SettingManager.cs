using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using VPC.Entities.EntityCore.Model.Setting;
using VPC.Framework.Business.Common;
using VPC.Framework.Business.SettingsManager.APIs;

namespace VPC.Framework.Business.SettingsManager.Contracts
{
    public interface ISettingManager
    {
        bool CreateSetting(Guid tenantId, Setting settingsinfo);
        bool UpdateSetting(Guid tenantId, Setting settingsinfo);
        bool DeleteSetting(Guid tenantId, Guid settingId);

        List<Setting> GetSettings(Guid tenantId);
        Setting GetSettingsById(Guid tenantId, Guid roleId);
        Setting GetSettingsByContext(Guid tenantId, SettingContextTypeEnum contexttype);
        string GetSenderNameByContext(Guid tenantId, SettingContextTypeEnum contexttype);

    }
    public class SettingManager : ISettingManager
    {
        private readonly ISettingAdmin _IAdminSetting = new SettingAdmin();
        private readonly ISettingReview _IReviewSetting = new SettingReview();
        bool ISettingManager.CreateSetting(Guid tenantId, Setting settingsinfo)
        {
            settingsinfo.Id = Guid.NewGuid();

            return _IAdminSetting.Create(tenantId, settingsinfo);
        }

        bool ISettingManager.DeleteSetting(Guid tenantId, Guid settingId)
        {
            return _IAdminSetting.Delete(tenantId, settingId);
        }

        List<Setting> ISettingManager.GetSettings(Guid tenantId)
        {
            var settings = _IReviewSetting.GetSettings(tenantId);
            foreach (var setting in settings)
            {
                setting.ContextName = DataUtility.GetEnumDescription((SettingContextTypeEnum)setting.Context);
            }
            return settings;
        }
        Setting ISettingManager.GetSettingsById(Guid tenantId, Guid roleId)
        {
            var setting = _IReviewSetting.GetSettings(tenantId, roleId);

            setting.ContextName = DataUtility.GetEnumDescription((SettingContextTypeEnum)setting.Context);

            return setting;
        }

        Setting ISettingManager.GetSettingsByContext(Guid tenantId, SettingContextTypeEnum contexttype)
        {
            var setting = _IReviewSetting.GetSettingsByContext(tenantId, contexttype);

            setting.ContextName = DataUtility.GetEnumDescription((SettingContextTypeEnum)setting.Context);

            return setting;
        }

        bool ISettingManager.UpdateSetting(Guid tenantId, Setting settingsinfo)
        {
            return _IAdminSetting.Update(tenantId, settingsinfo);
        }

        public string GetSenderNameByContext(Guid tenantId, SettingContextTypeEnum contexttype)
        {
            string sendername = string.Empty;
            var conetext = _IReviewSetting.GetSettingsByContext(tenantId, contexttype);
            if (conetext != null)
            {

                var content = JsonConvert.DeserializeObject<dynamic>(conetext.Content);

                if (content != null)
                {
                    switch (contexttype)
                    {
                        case SettingContextTypeEnum.EMAIL:
                            sendername = content.emailSender;
                            break;

                        case SettingContextTypeEnum.SMS:
                            sendername = content.smsSender;
                            break;

                        default:
                            break;
                    }
                }
            }

            return sendername;
        }
    }
}
