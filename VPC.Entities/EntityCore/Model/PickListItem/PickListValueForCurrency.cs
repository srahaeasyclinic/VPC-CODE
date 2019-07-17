using System;

namespace VPC.Entities.EntityCore.Model.PickListItem
{
    public class PickListValueForCurrency
    {
        public Guid Id { get; set; }
        public Guid PicklistValueId { get; set; }
        public byte DecimalPrecision { get; set; }
        public byte DecimalVisualization { get; set; }
        public string IsoCode { get; set; }
    }
}