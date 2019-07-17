using System;
using System.Collections.Generic;
using VPC.Metadata.Business.Validator.Schema;
using VPC.Metadata.Business.Operator.DataAnnotations;

namespace VPC.Metadata.Business.DataAnnotations
{
    public class ComplexBase : DataTypeBase
    {
        public override ControlType ControlType
        {
            get; set;
        }

        public override DataType DataType
        {
            get
            {
                return DataType.Complex;
            }

            set
            {
                
            }
        }

        public override bool IsConfigurable
        {
            get; set;
        }

        public override void AddValidator(ValidatorBase validator)
        {
            
        }

        public override List<ValidatorBase> GetValidators()
        {
            return null;
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
        }
    }
}
