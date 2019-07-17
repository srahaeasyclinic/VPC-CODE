using System;
using System.Collections.Generic;
using VPC.Metadata.Business.Validator.Schema;
//using VPC.Metadata.Business.Entity.Model;
using VPC.Metadata.Business.DataTypes.Picklists.Local;
using VPC.Metadata.Business.DataAnnotations;

namespace VPC.Metadata.Business.DataTypes.Picklists
{
    public class Category : LocalPickList
    {
        public Category()
        {
            DataType = DataType.PickList;
            base.ControlType = ControlType.DropDown;
            PickListType = "Category";
        }

        public override void AddValidator(ValidatorBase validator)
        {

        }

        public override List<ValidatorBase> GetValidators()
        {
            return null;
        }

        //public override List<PickListValue> GetData
        //{
        //    get
        //    {
        //        return PicklistHelper.GetPickListData(typeof(GenderEnum));
        //    }
        //}
    }
}
