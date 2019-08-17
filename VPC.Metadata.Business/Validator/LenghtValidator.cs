using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VPC.Metadata.Business.DataAnnotations;
using VPC.Metadata.Business.Validator.Schema;

namespace VPC.Metadata.Business.Validator
{
    public class 
    LengthValidator : ValidatorBase
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
        public virtual int? MinDblength { get; set; }

        List<ValidatorOption> Options = new List<ValidatorOption>() {
                new ValidatorOption(){ Name="MinLength", Value="10", ControlType = ControlType.TextBox}
                //new ValidatorOption(){ Name="MaxLength", ControlType = ControlType.TextBox},
        };
        public override List<ValidatorOption> GetExtraValidationParameters()
        {
            return Options;
        }

        public virtual bool IsValid(ValidatorBase basvalidator, string inputdata)
        {
            if (basvalidator.GetType() != typeof(LengthValidator)) return false;

            var baseValidator = (LengthValidator)basvalidator;
            if(baseValidator.Dblength != null && baseValidator.MinDblength != null && baseValidator.Dblength < baseValidator.MinDblength )
               return false;
            else if (baseValidator.MinDblength != null && inputdata.Length > baseValidator.MinDblength)
                return false;
           else
               return baseValidator.Dblength != null ? inputdata.Length <= baseValidator.Dblength : true;
        }
        public virtual bool IsMinValid(ValidatorBase basvalidator, string inputdata)
        {
            if (basvalidator.GetType() != typeof(LengthValidator)) return false;

            var baseValidator = (LengthValidator)basvalidator;
            return baseValidator.MinDblength != null ? inputdata.Length > baseValidator.MinDblength : true;
        }
        public virtual bool IsLengthValid(ValidatorBase basvalidator, string inputdata)
        {
            if (basvalidator.GetType() != typeof(LengthValidator)) return false;

            var baseValidator = (LengthValidator)basvalidator;
            return baseValidator.Dblength != null ? inputdata.Length <= baseValidator.Dblength : true;

            //valid = baseValidator.Options.FirstOrDefault(s=>s.ControlType== ControlType.TextBox)!=null? true : false;
        }
    }
}
