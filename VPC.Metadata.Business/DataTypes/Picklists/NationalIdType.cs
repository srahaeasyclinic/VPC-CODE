using System;
using System.Collections.Generic;
//using VPC.Metadata.Business.Entity.Model;
using VPC.Metadata.Business.Validator.Schema;
using VPC.Metadata.Business.DataAnnotations;

namespace VPC.Metadata.Business.DataTypes.Picklists
{


    public class NationalIdType : GlobalPickList
    {
        public NationalIdType()
        {
            DataType = DataType.PickList;
            base.ControlType = ControlType.DropDown;
            PickListType = "Nationality";
        }

        public override void AddValidator(ValidatorBase validator)
        {

        }

        public override List<ValidatorBase> GetValidators()
        {
            return null;
        }
        //    private List<PickListValue> GetStatusEnum()
        //    {
        //        var statusEnum = new List<PickListValue>();
        //        var item = new PickListValue();
        //        item.Id = Guid.NewGuid();
        //        item.Name = "NationalIdType1";
        //        statusEnum.Add(item);

        //        var item2 = new PickListValue();
        //        item2.Id = Guid.NewGuid();
        //        item2.Name = "NationalIdType2";
        //        statusEnum.Add(item2);

        //        return statusEnum;
        //    }
        //    public override List<PickListValue> GetData
        //    {
        //        get
        //        {
        //            return GetStatusEnum();
        //        }
        //    }
    }
}
