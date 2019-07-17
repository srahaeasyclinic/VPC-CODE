using System;
using VPC.Metadata.Business.Validator;
using VPC.Metadata.Business.DataAnnotations;

namespace VPC.Metadata.Business.DataTypes
{
    public class NumericType : NumberBase
    {
        public NumericType()
        {
            this.DataType = DataType.Number;
            this.ControlType = ControlType.TextBox;
            this.IsConfigurable = true;
            //this.DecimalPrecision = 0;

            var requiredValidator1 = new RequiredValidator();
            this.AddValidator(requiredValidator1);

            var requiredValidator2 = new RangeValidator();
            this.AddValidator(requiredValidator2);

            var lengthvalidator = new LengthValidator();
            lengthvalidator.Dblength = 100;
            this.AddValidator(lengthvalidator);
        }
        public override string Value { get; set; }
    }
}
