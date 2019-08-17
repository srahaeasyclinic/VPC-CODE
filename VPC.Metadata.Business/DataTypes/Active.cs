using System;
using System.Collections.Generic;
using VPC.Metadata.Business.Validator.Schema;
using VPC.Metadata.Business.DataAnnotations;
using VPC.Metadata.Business.Validator;

namespace VPC.Metadata.Business.DataTypes
{
    public class Active : BooleanBase
    {
        public Active()
        {
            this.DataType = DataType.Bool;
            this.ControlType = ControlType.Checkbox;
            this.IsConfigurable = true;
            var defaultValueValidattor = new DefaultValueValidator(ControlType.DropDown);
            this.AddValidator(defaultValueValidattor);
        }



        public override void AddValidator(ValidatorBase validator)
        {

        }

        public override List<ValidatorBase> GetValidators()
        {
            return null;
        }
        public override bool Value { get; set; }
    }
}
