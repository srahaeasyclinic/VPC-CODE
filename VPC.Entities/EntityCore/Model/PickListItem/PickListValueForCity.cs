using System;

namespace VPC.Entities.EntityCore.Model.PickListItem
{
    public class PickListValueForCity
    {
        public Guid Id { get; set; }
        public Guid PicklistValueId { get; set; }
        public Guid Country { get; set; }
        public Guid State { get; set; }
        public Guid Municipality { get; set; }
    }
}