﻿using System.Collections.Generic;
using VPC.Metadata.Business.Operator.DataAnnotations;
using VPC.Metadata.Business.Validator.Schema;

namespace VPC.Metadata.Business.DataAnnotations
{
    public abstract class RichTextBase: DataTypeBase
    {
        private List<ValidatorBase> validators = new List<ValidatorBase>();
        public override DataType DataType { get; set; }

        public override ControlType ControlType
        {
            get; set;
        }

        public override void AddValidator(ValidatorBase validator)
        {
            validators.Add(validator);
        }


        public override List<ValidatorBase> GetValidators()
        {
            return validators;
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
                    Operators.Equal,
                    Operators.Contains,
                    Operators.EndssWith,
                    Operators.StartsWith
                };
            }
        }

        public abstract string Value { get; set; }
    }
}
