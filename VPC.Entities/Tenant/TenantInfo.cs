using System;
using System.Collections.Generic;
using System.Text;

namespace VPC.Entities.Tenant
{
    public class TenantInfo
    {
        public Guid TenantCode { get; set; }
        public Guid TenantId { get; set; }
        public string OrgNo { get; set; }
        public Guid PickListValue_Id { get; set; }
        public int PickListId { get; set; }
        public string Key { get; set; }
        public string Text { get; set; }
        public Guid LanguageId { get; set; }
        public string DefaultLanguage { get; set; }

    }
}
