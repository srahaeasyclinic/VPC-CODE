using System;
using VPC.Metadata.Business.DataAnnotations;

namespace VPC.Metadata.Business.DataTypes
{
    public class MultiSelectLookup : LookupBase
    {
        public MultiSelectLookup()
        {
            this.DataType = DataType.Lookup;
            this.ControlType = ControlType.Select2;
        }   
        public override string Value { get; set; }     
    }
}
