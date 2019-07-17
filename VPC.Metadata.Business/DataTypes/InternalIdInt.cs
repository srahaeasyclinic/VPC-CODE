using System.Collections.Generic;
using VPC.Metadata.Business.DataAnnotations;
using VPC.Metadata.Business.Validator.Schema;

namespace VPC.Metadata.Business.DataTypes
{
    public class InternalIdInt : InternalIdIntBase
    {
        public InternalIdInt()
        {
            this.DataType = DataType.Number;
            this.ControlType = ControlType.Label;
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