using System;

namespace VPC.Entities.EntityCore.Model.PickListItem
{
    public class PickListValueForCountry
    {
        public Guid Id { get; set; }
        public Guid PicklistValueId { get; set; }
        public Guid Currency { get; set; }
        public Guid Language { get; set; }
        public Guid Timezone { get; set; }
        public string IsoCode { get; set; }
        public string Nationality { get; set; }
    }
}