using System;
using System.Collections.Generic;
using VPC.Metadata.Business.Validator.Schema;
using VPC.Metadata.Business.DataAnnotations;

namespace VPC.Metadata.Business.DataTypes
{
    public class BooleanType : BooleanBase
    {
        public BooleanType()
        {
            this.DataType = DataType.Bool;
            this.ControlType = ControlType.Checkbox;
            this.IsConfigurable = true;
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
