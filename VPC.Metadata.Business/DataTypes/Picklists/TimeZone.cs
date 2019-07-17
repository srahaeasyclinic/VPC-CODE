using System;
using System.Collections.Generic;
using System.Text;
using VPC.Metadata.Business.Validator.Schema;
using VPC.Metadata.Business.DataAnnotations;

namespace VPC.Metadata.Business.DataTypes.Picklists
{



    public class TimeZone : GlobalPickList
    {
        public TimeZone()
        {
            DataType = DataType.PickList;
            base.ControlType = ControlType.DropDown;
            PickListType = "TimeZone";
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
