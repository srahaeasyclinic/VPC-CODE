using System;
using System.Collections.Generic;
using VPC.Metadata.Business.Validator.Schema;
using VPC.Metadata.Business.Validator;
using VPC.Metadata.Business.DataAnnotations;

namespace VPC.Metadata.Business.DataTypes
{

    public class LocalPickList<Type> : PickListBase
    {
        public LocalPickList()
        { 
            this.DataType = DataType.PickList;
            this.ControlType = ControlType.DropDown;
            this.IsConfigurable = true;
            var requiredValidator1 = new RequiredValidator();
            this.AddValidator(requiredValidator1);
             var defaultValueValidattor = new DefaultValueValidator (ControlType);
            this.AddValidator (defaultValueValidattor);
        }
        public override string PickListType
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
        public override string Value { get; set; }
        
    }

}
