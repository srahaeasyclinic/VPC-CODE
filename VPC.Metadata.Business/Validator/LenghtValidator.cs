using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VPC.Metadata.Business.DataAnnotations;
using VPC.Metadata.Business.Validator.Schema;

namespace VPC.Metadata.Business.Validator
{
    public class LengthValidator : ValidatorBase
    {
        public LengthValidator()
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
                return "LengthValidator";
            }
        }

        public virtual int? Dblength { get; set; }

        List<ValidatorOption> Options = new List<ValidatorOption>() {
                new ValidatorOption(){ Name="Length", ControlType = ControlType.TextBox}
        };
        public override List<ValidatorOption> GetExtraValidationParameters()
        {
            return Options;
        }

        public virtual bool IsValid(ValidatorBase basvalidator, string inputdata)
        {
            if (basvalidator.GetType() != typeof(LengthValidator)) return false;

            var baseValidator = (LengthValidator)basvalidator;
            return baseValidator.Dblength != null ? inputdata.Length <= baseValidator.Dblength : true;

            //valid = baseValidator.Options.FirstOrDefault(s=>s.ControlType== ControlType.TextBox)!=null? true : false;



        }
    }
}
