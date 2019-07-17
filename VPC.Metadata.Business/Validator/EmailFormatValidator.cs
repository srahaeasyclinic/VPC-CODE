using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using VPC.Metadata.Business.DataAnnotations;
using VPC.Metadata.Business.Validator.Schema;

namespace VPC.Metadata.Business.Validator
{
    public class EmailFormatValidator : ValidatorBase
    {
        public EmailFormatValidator()
        {

        }

        public override bool Customizable
        {
            get
            {
                return true;
            }
        }

        public override string ValidationName
        {
            get
            {
                return "EmailFormatValidator";
            }
        }

        public virtual string RegexFormat { get; set; }

        List<ValidatorOption> Options = new List<ValidatorOption>() {
                new ValidatorOption(){ Name="EmailFormat", ControlType = ControlType.Checkbox,Value=1}
        };
        public override List<ValidatorOption> GetExtraValidationParameters()
        {
            return Options;
        }

        public virtual bool IsValid(ValidatorBase basvalidator, string inputdata)
        {

            if (basvalidator.GetType() != typeof(EmailFormatValidator)) return false;

            // string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
            //          @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
            //          @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";

            var baseValidator = (EmailFormatValidator)basvalidator;
            Regex re = new Regex(baseValidator.RegexFormat);

            return re.IsMatch(inputdata);



        }
    }
}
