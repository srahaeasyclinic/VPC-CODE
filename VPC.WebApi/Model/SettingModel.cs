using System;
using System.ComponentModel;
using VPC.Entities.EntityCore.Model.Setting;

namespace VPC.WebApi.Model
{
    public class SettingModel
    {
        public string Id { get; set; }
        public SettingContextTypeEnum Context { get; set; }

        public string ContextName { get; set; }
        public string Content { get; set; }

        public DateTime? UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }
    }

  


}
