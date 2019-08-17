using System;
using VPC.Metadata.Business.Validator;
using VPC.Metadata.Business.DataAnnotations;

namespace VPC.Metadata.Business.DataTypes
{
    public class XSmallText : TextBase
    {
        public XSmallText()
        {
            DataType = DataType.Text;
            base.ControlType = ControlType.TextBox;
            IsConfigurable = false;

            var requiredValidator1 = new RequiredValidator();
            AddValidator(requiredValidator1);

            var lengthValidator = new LengthValidator();
            lengthValidator.Dblength = 10; // Set values for field lenght as per Database's field lenght.
            lengthValidator.MinDblength = 1;

            AddValidator(lengthValidator);
             var defaultValueValidattor = new DefaultValueValidator (ControlType);
            this.AddValidator (defaultValueValidattor);
        }
        public override string Value { get; set; }
    }

    
}
