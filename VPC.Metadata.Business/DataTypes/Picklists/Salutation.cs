using System;
using System.Collections.Generic;
using VPC.Metadata.Business.Validator.Schema;
//using VPC.Metadata.Business.Entity.Model;
using VPC.Metadata.Business.DataAnnotations;

namespace VPC.Metadata.Business.DataTypes.Picklists
{
    public class Salutation : GlobalPickList
    {
        public Salutation()
        {
            DataType = DataType.PickList;
            base.ControlType = ControlType.DropDown;
            PickListType = "Title";
        }

        public override void AddValidator(ValidatorBase validator)
        {
           
        }

        public override List<ValidatorBase> GetValidators()
        {
            return null;
        }

        //@todo data will collect form our salutatoin masters..
        //private List<PickListValue> GetStatusEnum()
        //{
        //    var statusEnum = new List<PickListValue>();
        //    var item = new PickListValue();
        //    item.Id = Guid.NewGuid();
        //    item.Name = "Mr.";

        //    var item1 = new PickListValue();
        //    item1.Id = Guid.NewGuid();
        //    item1.Name = "Mrs.";

        //    statusEnum.Add(item);
        //    return statusEnum;
        //}
        //public override List<PickListValue> GetData
        //{
        //    get
        //    {
        //        return GetStatusEnum();
        //    }
        //}
    }
}
