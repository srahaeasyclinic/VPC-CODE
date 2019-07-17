using System;
using System.Collections.Generic;
//using VPC.Metadata.Business.Entity.Model;
using VPC.Metadata.Business.Validator.Schema;
using VPC.Metadata.Business.DataAnnotations;

namespace VPC.Metadata.Business.DataTypes.Picklists
{
   
    public class Nationality : GlobalPickList
    {
        public Nationality()
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

        //@todo data will collect form our nationality masters..
        //private List<PickListValue> GetStatusEnum()
        //{
            


        //    var statusEnum = new List<PickListValue>();
        //    var item = new PickListValue();
        //    item.Id = Guid.NewGuid();
        //    item.Name = "Indian";
        //    statusEnum.Add(item);


        //    var item1 = new PickListValue();
        //    item1.Id = Guid.NewGuid();
        //    item1.Name = "Malaysian";

        //    statusEnum.Add(item1);
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
