using System;
using VPC.Metadata.Business.Validator;
using VPC.Metadata.Business.DataAnnotations;

namespace VPC.Metadata.Business.DataTypes
{
    public class StringType : TextBase
    {

        public StringType()
        {
            this.DataType = DataType.Text;
            this.ControlType = ControlType.Label;
            this.IsConfigurable = true;


            var requiredValidator1 = new RequiredValidator();
            AddValidator(requiredValidator1);

            var lengthValidator = new LengthValidator();
            lengthValidator.Dblength = 255;
            AddValidator(lengthValidator);
        }
        public override string Value { get; set; }
    }
}