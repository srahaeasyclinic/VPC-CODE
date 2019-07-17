using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using VPC.Metadata.Business.DataAnnotations;
using VPC.Metadata.Business.Validator.Schema;

namespace VPC.Metadata.Business.Validator {
    public class RegularExpression : ValidatorBase {
        public override bool Customizable {
            get {
                return true;
            }
        }

        public override string ValidationName {
            get {
                return "RegularExpression";
            }
        }

        // public override Dictionary<string, dynamic> GetExtraValidationParameters()
        // {
        //     return new Dictionary<string, dynamic> {
        //          { "regEx", "^4[0-9]{12}(?:[0-9]{3})?$" }
        //     };
        // }

        List<ValidatorOption> Options = new List<ValidatorOption> () {
            new ValidatorOption () {
            Name = "regEx", ControlType = ControlType.TextBox, Value = "4[0-9]{12}(?:[0-9]{3})?"
            }
           
        };
        public override List<ValidatorOption> GetExtraValidationParameters () {
            return Options;
        }

        public virtual bool IsValid(ValidatorBase basvalidator, string inputdata)
        {

            if (basvalidator.GetType() != typeof(RegularExpression)) return false;


            var baseValidator = (RegularExpression)basvalidator;
            return Regex.IsMatch(inputdata, "^4[0-9]{12}(?:[0-9]{3})?$");

            //  valid = baseValidator.Options.FirstOrDefault(s => s.ControlType == ControlType.Checkbox) != null ? true : false;



        }
    }
}