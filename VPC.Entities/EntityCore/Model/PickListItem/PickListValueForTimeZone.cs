using System;

namespace VPC.Entities.EntityCore.Model.PickListItem
{
    public class PickListValueForTimeZone
    {
        public Guid Id { get; set; }
        public Guid PicklistValueId { get; set; }
        public decimal GmtDeviation { get; set; }
        public string SummerTimeStart { get; set; }
        public string WinterTimeStart { get; set; }
    }
}