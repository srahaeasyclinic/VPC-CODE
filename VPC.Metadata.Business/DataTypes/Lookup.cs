using System;
using VPC.Metadata.Business.DataAnnotations;
using VPC.Metadata.Business.Validator;

namespace VPC.Metadata.Business.DataTypes
{
    public class Lookup<Type> : LookupBase
    {
        public Lookup()
        {
            this.DataType = DataType.Lookup;
            this.ControlType = ControlType.DropDown;
            

            var requiredValidator = new RequiredValidator();
            this.AddValidator(requiredValidator);
        }

        public override string Value { get;set; }
    }
}
