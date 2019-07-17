using System;
using System.Collections.Generic;
using VPC.Metadata.Business.Validator.Schema;

namespace VPC.Metadata.Business.DataAnnotations
{
    public abstract class BinaryBase : DataTypeBase
    {
        private List<ValidatorBase> validators = new List<ValidatorBase>();
        public override DataType DataType { get; set; }

        public override ControlType ControlType
        {
            get; set;
        }

        public override void AddValidator(ValidatorBase validator)
        {
            validators.Add(validator);
        }


        public override List<ValidatorBase> GetValidators()
        {
            return validators;
        }

        public override bool IsConfigurable
        {
            get; set;
        }
        public abstract string Value { get; set; }
     

    }
}
