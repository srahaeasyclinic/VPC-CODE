using System;

namespace VPC.Entities.Common
{

    public class AuditInfo
    {
        public DateTime CreationDate { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime ModificationDate { get; set; }
        public Guid ModifiedBy { get; set; }
        public DateTime DeletionDate { get; set; }
        public Guid DeletedBy { get; set; }
        public DateTime LastUpdated { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsAdded { get; set; }
    }

    public class NullableAuditInfo
    {
        public DateTime? CreationDate { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? ModificationDate { get; set; }
        public Guid? ModifiedBy { get; set; }
        public DateTime? DeletionDate { get; set; }
        public DateTime? LastUpdated { get; set; }
        public bool? IsDeleted { get; set; }
    }

}