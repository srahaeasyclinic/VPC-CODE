using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace VPC.Entities.EntityCore.Model.Setting
{
    public enum SettingContextTypeEnum
    {
        [Description("SMS")]
        SMS = 1,

        [Description("Email")]
        EMAIL = 2,
    }
}
