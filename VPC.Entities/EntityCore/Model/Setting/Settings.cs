using System;

namespace VPC.Entities.EntityCore.Model.Setting
{
    public class Setting
    {
        public Guid Id { get; set; }
        public SettingContextTypeEnum Context { get; set; }

        public string ContextName { get; set; }
        public string Content { get; set; }

        public DateTime UpdatedOn { get; set; }
        public Guid UpdatedBy { get; set; }


    }
}
