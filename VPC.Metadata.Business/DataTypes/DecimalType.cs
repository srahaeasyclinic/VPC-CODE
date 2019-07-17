using System;
using VPC.Metadata.Business.Validator;
using VPC.Metadata.Business.DataAnnotations;

namespace VPC.Metadata.Business.DataTypes
{
    public class DecimalType : NumberBase
    {
        public DecimalType()
        {
            this.DataType = DataType.Number;
            this.ControlType = ControlType.TextBox;
            this.IsConfigurable = true;
            this.DecimalPrecision = 2;

            var requiredValidator1 = new RequiredValidator();
            this.AddValidator(requiredValidator1);

            var requiredValidator2 = new RangeValidator();
            this.AddValidator(requiredValidator2);
        }
        public override string Value { get; set; }
    }
}
