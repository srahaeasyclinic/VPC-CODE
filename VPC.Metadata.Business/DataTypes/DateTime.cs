using System;
using VPC.Metadata.Business.Validator;
using VPC.Metadata.Business.DataAnnotations;

namespace VPC.Metadata.Business.DataTypes
{
    public class DateTime : DateTimeBase
    {
        public DateTime()
        {
            this.DataType = DataType.DateTime;
            this.ControlType = ControlType.Calendar;
            this.IsConfigurable = true;
            
            var requiredValidator1 = new RequiredValidator();
            this.AddValidator(requiredValidator1);

            // var lengthValidator = new RangeValidator();
            // this.AddValidator(lengthValidator);
             var defaultValueValidattor = new DefaultValueValidator (ControlType);
            this.AddValidator (defaultValueValidattor);
        }
        public override string Value { get; set; }
    }
}
