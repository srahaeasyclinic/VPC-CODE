using System;

namespace VPC.Entities.EntityCore.Model.PickListItem
{
    public class ExtendedValues
    {
        public Guid Id { get; set; }
        public Guid PicklistValueId { get; set; }
        public Guid Country { get; set; }
        public Guid State { get; set; }
        public Guid Currency { get; set; }
        public Guid Language { get; set; }
        public Guid Timezone { get; set; }
        public Guid Municipality { get; set; }
        public string IsoCode { get; set; }
        public string Nationality { get; set; }
        public string DateFormat { get; set; }
        public byte DecimalPrecision { get; set; }
        public byte DecimalVisualization { get; set; }
        public decimal GmtDeviation { get; set; }
        public string SummerTimeStart { get; set; }
        public string WinterTimeStart { get; set; }
        public byte ScopeEntityId { get; set; }
    }
}