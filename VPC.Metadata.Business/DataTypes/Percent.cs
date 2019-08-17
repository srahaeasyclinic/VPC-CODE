using System;
using VPC.Metadata.Business.Validator;
using VPC.Metadata.Business.DataAnnotations;

namespace VPC.Metadata.Business.DataTypes
{
    public class Percent : NumberBase
    {
        public Percent()
        {
            this.DataType = DataType.Number;
            this.ControlType = ControlType.TextBox;
            this.IsConfigurable = true;

            var requiredValidator1 = new RequiredValidator();
            this.AddValidator(requiredValidator1);

            var requiredValidator2 = new PercentValidator();
            this.AddValidator(requiredValidator2);
             var defaultValueValidattor = new DefaultValueValidator (ControlType);
            this.AddValidator (defaultValueValidattor);
        }
        public override string Value { get; set; }
    }
}
