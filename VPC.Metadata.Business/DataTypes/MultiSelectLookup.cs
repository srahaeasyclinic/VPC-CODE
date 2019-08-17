using System;
using VPC.Metadata.Business.DataAnnotations;
using VPC.Metadata.Business.Validator;

namespace VPC.Metadata.Business.DataTypes
{
    public class MultiSelectLookup : LookupBase
    {
        public MultiSelectLookup()
        {
            this.DataType = DataType.Lookup;
            this.ControlType = ControlType.Select2;
             var defaultValueValidattor = new DefaultValueValidator (ControlType);
            this.AddValidator (defaultValueValidattor);
        }   
        public override string Value { get; set; }     
    }
}
