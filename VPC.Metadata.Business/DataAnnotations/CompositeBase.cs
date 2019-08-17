using System;
using System.Collections.Generic;
using VPC.Metadata.Business.Operator.DataAnnotations;
using VPC.Metadata.Business.Validator.Schema;

namespace VPC.Metadata.Business.DataAnnotations
{
    public abstract class CompositeBase : DataTypeBase
    {
        public override DataType DataType { get; set; }

        public override ControlType ControlType
        {
            get; set;
        }

      

        public override bool IsConfigurable
        {
            get; set;
        }

        public virtual List<string> OperatorsAavailable
        {
            get
            {
                return new List<string>
                {
                    Operators.Equal
                };
            }
            set
            {

            }
        }

         public abstract bool Value {get; set;}

  private List<ValidatorBase> validators = new List<ValidatorBase>();
       
        public override void AddValidator(ValidatorBase validator)
        {
            validators.Add(validator);
        }


        public override List<ValidatorBase> GetValidators()
        {
            return validators;
        }
    }
}
