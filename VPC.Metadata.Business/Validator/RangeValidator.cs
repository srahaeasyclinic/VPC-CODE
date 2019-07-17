using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VPC.Metadata.Business.DataAnnotations;
using VPC.Metadata.Business.Validator.Schema;

namespace VPC.Metadata.Business.Validator
{
    public class RangeValidator : ValidatorBase
    {
        public RangeValidator()
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
                return "RangeValidator";
            }
        }


        List<ValidatorOption> Options = new List<ValidatorOption>() {
                    new ValidatorOption(){
                        Name="minLength", ControlType = ControlType.TextBox, Value = 0
                    },
                    new ValidatorOption(){
                        Name="maxLength", ControlType = ControlType.TextBox, Value = 1000
                    }
            };
        public override List<ValidatorOption> GetExtraValidationParameters()
        {
            return Options;
        }

        public virtual bool IsValid(ValidatorBase basvalidator, string inputdata)
        {
            bool IsNumbervalue = int.TryParse(inputdata, out int ignoreMe);

            if (basvalidator.GetType() != typeof(RangeValidator) && !IsNumbervalue) return false;

            return (IsNumbervalue && Convert.ToInt32(inputdata) >= 0 && Convert.ToInt32(inputdata) <= 1000);

        }
    }
}
