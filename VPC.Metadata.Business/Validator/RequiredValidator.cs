using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VPC.Metadata.Business.DataAnnotations;
using VPC.Metadata.Business.Validator.Schema;

namespace VPC.Metadata.Business.Validator
{
    public class RequiredValidator : ValidatorBase
    {
        public RequiredValidator()
        {

        }

        List<ValidatorOption> Options = new List<ValidatorOption>() {
                new ValidatorOption(){ Name="Required", ControlType = ControlType.Checkbox,Value=false}
        };
        public override List<ValidatorOption> GetExtraValidationParameters()
        {
            return Options;
        }
        public override string ValidationName
        {
            get
            {
                return "RequiredValidator";
            }
        }

        public override bool Customizable
        {
            get
            {
                return true;
            }
        }

        public virtual bool IsValid(ValidatorBase basvalidator, string inputdata)
        {

            if (basvalidator.GetType() != typeof(RequiredValidator)) return false;


            var baseValidator = (RequiredValidator)basvalidator;
            return !string.IsNullOrEmpty(inputdata);

            //  valid = baseValidator.Options.FirstOrDefault(s => s.ControlType == ControlType.Checkbox) != null ? true : false;



        }
    }
}
