using System;

namespace VPC.Entities.EntityCore.Model.PickListItem
{
    public class PickListValueForState
    {
        public Guid Id { get; set; }
        public Guid PicklistValueId { get; set; }
        public Guid Country { get; set; }
    }
}