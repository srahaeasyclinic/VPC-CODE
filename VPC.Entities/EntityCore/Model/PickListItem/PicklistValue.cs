using System;

namespace VPC.Entities.EntityCore.Model.PickListItem
{
    public class PicklistValue
    {
        public Values Values { get; set; }

        public ExtendedValues ExtendedValues { get; set; }
    }

    public class PicklistValueV1
    {
        public Guid InternalId { get; set; }
        public short PicklistId { get; set; }
        public string Key { get; set; }
        public string Text { get; set; }
        public bool Active { get; set; }
        public bool Flagged { get; set; }
        public bool IsDeleteted { get; set; }
        public Guid UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }

        public ExtendedValues ExtendedValues { get; set; }
    }
}