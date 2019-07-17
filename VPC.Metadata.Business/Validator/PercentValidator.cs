using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VPC.Metadata.Business.DataAnnotations;
using VPC.Metadata.Business.Validator.Schema;

namespace VPC.Metadata.Business.Validator
{
    public class PercentValidator : ValidatorBase
    {
        public PercentValidator() 
        {

        }

        List<ValidatorOption> Options = new List<ValidatorOption>() { 
                new ValidatorOption(){ Name="Length", ControlType = ControlType.TextBox}
        };
        public override List<ValidatorOption> GetExtraValidationParameters()
        {
            return Options;
        }
        public override string ValidationName
        {
            get
            {
                return "PercentValidator";
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

            if (basvalidator.GetType() != typeof(PercentValidator)) return false;


            var baseValidator = (PercentValidator)basvalidator;
            return true;

            //  valid = baseValidator.Options.FirstOrDefault(s => s.ControlType == ControlType.Checkbox) != null ? true : false;



        }
    }
}
