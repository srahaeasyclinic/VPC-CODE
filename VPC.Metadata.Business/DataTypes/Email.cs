using System;
using VPC.Metadata.Business.Validator;
using VPC.Metadata.Business.DataAnnotations;

namespace VPC.Metadata.Business.DataTypes
{
    public class Email : EmailBase
    {
        public Email()
        {
            this.DataType = DataType.Email;
            this.ControlType = ControlType.TextBox;
            this.IsConfigurable = true;

            var requiredValidator1 = new RequiredValidator();
            this.AddValidator(requiredValidator1);

            // var requiredValidator2 = new RangeValidator();
            // this.AddValidator(requiredValidator2);

            var emailformatvalidator = new EmailFormatValidator();
            emailformatvalidator.RegexFormat = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                     @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                     @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";

            this.AddValidator(emailformatvalidator);
        }

        public override string Value { get; set; }

    }
}
