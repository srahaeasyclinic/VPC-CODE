using System;
using VPC.Metadata.Business.Validator;
using VPC.Metadata.Business.DataAnnotations;

namespace VPC.Metadata.Business.DataTypes
{
    public class Name : TextBase
    {
       
        public Name()
        {
            DataType = DataType.Text;
            base.ControlType = ControlType.TextBox;
            IsConfigurable = false;
            var requiredValidator1 = new RequiredValidator();
            AddValidator(requiredValidator1);
            
            var lengthValidator = new LengthValidator();
            lengthValidator.Dblength = 100;
            lengthValidator.MinDblength = 1;
            AddValidator(lengthValidator);
             var defaultValueValidattor = new DefaultValueValidator (ControlType);
            this.AddValidator (defaultValueValidattor);
        }

        public override string Value { get; set; }
    }
}
