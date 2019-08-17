using System;
using System.Collections.Generic;
using VPC.Metadata.Business.Validator.Schema;
using VPC.Metadata.Business.DataAnnotations;
using VPC.Metadata.Business.Validator;

namespace VPC.Metadata.Business.DataTypes
{
    public class BooleanType : BooleanBase
    {  private List<ValidatorBase> validators = new List<ValidatorBase> ();
        public BooleanType()
        {
            this.DataType = DataType.Bool;
            this.ControlType = ControlType.Checkbox;
            this.IsConfigurable = true;
             var defaultValueValidattor = new DefaultValueValidator (ControlType.Checkbox );
            this.AddValidator (defaultValueValidattor);
        }

        
        public override void AddValidator(ValidatorBase validator)
        {
             validators.Add (validator);
        }

        public override List<ValidatorBase> GetValidators()
        {
            return validators;
        }
        public override bool Value { get; set; }
    }
}
