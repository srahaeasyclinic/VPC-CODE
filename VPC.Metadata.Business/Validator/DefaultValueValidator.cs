using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VPC.Metadata.Business.DataAnnotations;
using VPC.Metadata.Business.Validator.Schema;

namespace VPC.Metadata.Business.Validator {
    public class DefaultValueValidator : ValidatorBase {
        private List<ValidatorOption> Options = new List<ValidatorOption> ();
        public DefaultValueValidator (ControlType dropDown) {
            var option = new ValidatorOption () {
                Name = "Default value",
                ControlType = dropDown,
                Value = null
            };
            Options.Add (option);
        }

        public override List<ValidatorOption> GetExtraValidationParameters () {
            return Options;
        }
        public override string ValidationName {
            get {
                return "DefaultValueValidator";
            }
        }

        public override bool Customizable {
            get {
                return true;
            }
        }

        public virtual bool IsValid (ValidatorBase basvalidator, string inputdata) {
            if (basvalidator.GetType () != typeof (DefaultValueValidator)) return false;
            var baseValidator = (DefaultValueValidator) basvalidator;
            return !string.IsNullOrEmpty (inputdata);
        }
    }
}