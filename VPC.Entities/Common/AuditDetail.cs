
namespace VPC.Entities.Common
{
public class AuditDetail : AuditInfo
    {
        public string CreatedByName { get; set; }
        public string CreatedByCode { get; set; }
        public string ModifiedByName { get; set; }
        public string ModifiedByCode { get; set; }
        public string DeletedByName { get; set; }
        public string DeletedByCode { get; set; }
    }
}