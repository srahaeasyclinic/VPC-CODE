using System;
using System.Collections.Generic;
using VPC.Metadata.Business.DataAnnotations;
using VPC.Metadata.Business.Entity.Configuration;
using VPC.Metadata.Business.SearchFilter;
using VPC.Metadata.Business.Validator;
using VPC.Metadata.Business.Validator.Schema;

namespace VPC.Metadata.Business.DataTypes {

    [TableProperties ("[dbo].[PickListValue]", "[Id]")]
    public class PickList<Type> : PickListBase {
        public PickList () {
            this.DataType = DataType.PickList;
            this.ControlType = ControlType.DropDown;

            var requiredValidator = new RequiredValidator ();
            this.AddValidator (requiredValidator);

            var defaultValueValidattor = new DefaultValueValidator (ControlType);
            this.AddValidator (defaultValueValidattor);
           
        }
        public override string PickListType {
            get;
            set;
        }

        // public override void AddValidator(ValidatorBase validator)
        // {

        // //     var defaultValueValidattor = new DefaultValueValidator();
        // //    this.AddValidator(requiredValidator);

        //     var requiredValidator = new RequiredValidator();
        //     this.AddValidator(requiredValidator);

        //     var defaultValueValidattor = new DefaultValueValidator();
        //    this.AddValidator(requiredValidator);
        // }

        // public override List<ValidatorBase> GetValidators()
        // {
        //     return null;
        // }

        public override string Value { get; set; }

    }

}