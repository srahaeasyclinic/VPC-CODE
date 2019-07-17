using System;
using System.Collections.Generic;
using VPC.Metadata.Business.Validator.Schema;
using VPC.Metadata.Business.Validator;
using VPC.Metadata.Business.DataAnnotations;
using VPC.Metadata.Business.Entity.Configuration;
using VPC.Metadata.Business.SearchFilter;

namespace VPC.Metadata.Business.DataTypes
{
    
    [TableProperties("[dbo].[PickListValue]", "[Id]")]
    public class PickList<Type> : PickListBase
    {
        public PickList()
        { 
            this.DataType = DataType.PickList;
            this.ControlType = ControlType.DropDown;
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




        // [ColumnName("[Id]")]
        // [NotNull]
        // public InternalId Id { get; set; }


        // [ColumnName("[Key]")]
        // [FreeTextSearch]
        // [NotNull]
        // public SmallText Key { get; set; }

        // [ColumnName("[Text]")]
        // [FreeTextSearch]
        // [NotNull]
        // public MediumText Text { get; set; }
        
    }

}
