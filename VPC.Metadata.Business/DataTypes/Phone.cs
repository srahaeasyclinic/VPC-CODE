using System;
using System.Collections.Generic;
using VPC.Metadata.Business.Validator;
using VPC.Metadata.Business.Validator.Schema;
using VPC.Metadata.Business.DataAnnotations;

namespace VPC.Metadata.Business.DataTypes
{
    public class Phone : EmailBase
    {
        public Phone()
        {
            this.DataType = DataType.Number;
            this.ControlType = ControlType.TextBox;
            this.IsConfigurable = true;

            var requiredValidator1 = new RequiredValidator();
            this.AddValidator(requiredValidator1);

            //var requiredValidator2 = new RangeValidator();
            //this.AddValidator(requiredValidator2);

            var lengthvalidator = new LengthValidator();
            lengthvalidator.Dblength = 10;
            lengthvalidator.MinDblength = 10;
            this.AddValidator(lengthvalidator);
             var defaultValueValidattor = new DefaultValueValidator (ControlType);
            this.AddValidator (defaultValueValidattor);
        }

        // public override void AddValidator(ValidatorBase validator)
        // {

        // }

        // public override List<ValidatorBase> GetValidators()
        // {
        //     return null;
        // }

        public override string Value { get; set; }

    }
}
