using System;

namespace VPC.Entities.EntityCore.Model.PickListItem
{
    public class PickListValueForLanguage
    {
        public Guid Id { get; set; }
        public Guid PicklistValueId { get; set; }
        public string DateFormat { get; set; }
        public string IsoCode { get; set; }
    }
}