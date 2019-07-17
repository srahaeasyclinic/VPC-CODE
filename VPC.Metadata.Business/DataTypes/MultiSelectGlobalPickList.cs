using System;
using System.Collections.Generic;
using VPC.Metadata.Business.Validator.Schema;
using VPC.Metadata.Business.Validator;
using VPC.Metadata.Business.DataAnnotations;

namespace VPC.Metadata.Business.DataTypes
{
    public class MultiSelectGlobalPickList : PickListBase
    {
        public MultiSelectGlobalPickList()
        {
            this.DataType = DataType.Custom;
            this.ControlType = ControlType.Select2;
           
            var requiredValidator1 = new RequiredValidator();
            this.AddValidator(requiredValidator1);
        }

        public override string PickListType
        {
            get;
            set;
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
