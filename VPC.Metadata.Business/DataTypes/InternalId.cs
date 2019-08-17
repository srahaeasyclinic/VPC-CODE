using System;
using System.Collections.Generic;
using VPC.Metadata.Business.Validator.Schema;
using VPC.Metadata.Business.DataAnnotations;
using VPC.Metadata.Business.Validator;

namespace VPC.Metadata.Business.DataTypes
{
    public class InternalId : InternalIdBase
    {
        public InternalId()
        {
            this.DataType = DataType.Guid;
            this.ControlType = ControlType.Label;
             var defaultValueValidattor = new DefaultValueValidator (ControlType);
            this.AddValidator (defaultValueValidattor);
        }

        public override void AddValidator(ValidatorBase validator)
        {

        }

        public override List<ValidatorBase> GetValidators()
        {
            return null;
        }
         public override string Value { get; set; }
        
    }
}
