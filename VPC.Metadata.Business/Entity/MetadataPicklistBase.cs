using System;
using System.Collections.Generic;
using System.Text;
using VPC.Metadata.Business.DataAnnotations;
using VPC.Metadata.Business.Operator.DataAnnotations;
using VPC.Metadata.Business.Validator.Schema;

namespace VPC.Metadata.Business.Entity
{
    public abstract class MetadataPickListBase : PickListBase
    {
        private List<ValidatorBase> validators = new List<ValidatorBase>();
      

        public override void AddValidator(ValidatorBase validator)
        {
            validators.Add(validator);
        }


        public override List<ValidatorBase> GetValidators()
        {
            return validators;
        }
        public override string Value { get; set; }
        public virtual string APIUrl{get;set;}
    }
}
