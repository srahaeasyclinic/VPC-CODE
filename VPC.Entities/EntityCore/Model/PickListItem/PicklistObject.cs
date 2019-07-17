using System;
using System.Collections.Generic;

namespace VPC.Entities.EntityCore.Model.PickListItem
{
    //public class Picklist
    //{
    //    public short Id { get; set; }
    //    public string Name { get; set; }
    //    public bool IsStandard { get; set; }
    //    public short EntityId { get; set; }
    //    public bool FixedValueList { get; set; }
    //    public bool CustomizeValue { get; set; }
    //    public bool IsKeyValueType { get; set; }
    //    public bool Active { get; set; }
    //    public bool IsDeleteted { get; set; }
    //    public Guid UpdatedBy { get; set; }
    //    public DateTime UpdatedDate { get; set; }
    //}


    public class PicklistObject
    {
        public short Id { get; set; }
        public string Name { get; set; }

        public string Type { get; set; }

        public bool IsStandard { get; set; }
        public bool FixedValueList { get; set; }
        public bool CustomizeValue { get; set; }
  
        public string DisplayName { get; set; }
        public string PluralName { get; set; }
 
        public List<PicklistValueV1> Values { get; set; }
    }
}