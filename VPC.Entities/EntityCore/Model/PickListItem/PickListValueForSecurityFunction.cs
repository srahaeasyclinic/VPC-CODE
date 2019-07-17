using System;

namespace VPC.Entities.EntityCore.Model.PickListItem
{
    public class PickListValueForSecurityFunction
    {
        public Guid Id { get; set; }
        public Guid PicklistValueId { get; set; }
        public byte ScopeEntityId { get; set; }
    }
}