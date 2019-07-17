using System;

namespace VPC.Entities.EntityCore.Model.PickListItem
{
    public class PickListValueFavourite
    {
        public short PicklistId { get; set; }
        public Guid PicklistValueId { get; set; }
        public Guid UserId { get; set; }
    }
}