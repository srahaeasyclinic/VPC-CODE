using System;
using System.Collections.Generic;
using VPC.Metadata.Business.Validator.Schema;
//using VPC.Metadata.Business.Entity.Model;
using VPC.Metadata.Business.DataAnnotations;

namespace VPC.Metadata.Business.DataTypes.Picklists
{
    public class Status : LocalPickList
    {
        public Status()
        {
            DataType = DataType.PickList;
            base.ControlType = ControlType.Radio;
            PickListType = "Status";
        }

        public override void AddValidator(ValidatorBase validator)
        {

        }

        //public override List<ValidatorBase> GetValidators()
        //{
        //    return null;
        //}

        //public override List<PickListValue> GetData
        //{
        //    get
        //    {
        //        return PicklistHelper.GetPickListData(typeof(StatusOption));
        //    }
        //}
    }
}
