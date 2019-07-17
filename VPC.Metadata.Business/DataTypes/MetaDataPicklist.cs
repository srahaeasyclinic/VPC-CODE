using System.Collections.Generic;
using System.Data;
using VPC.Metadata.Business.DataAnnotations;
using VPC.Metadata.Business.DataTypes;
using VPC.Metadata.Business.Entity;
using VPC.Metadata.Business.Entity.Configuration;
using VPC.Metadata.Business.Validator;
using VPC.Metadata.Business.Validator.Schema;

namespace VPC.Metadata.Business.DataTypes
{
    public class MetadataPicklist: MetadataPickListBase
    {
        //private string ApiUrl = "/api/metadata";
        public MetadataPicklist()
        {
            this.DataType = DataType.MetaDataPicklist;
            this.ControlType = ControlType.DropDown;
            this.IsConfigurable = true;
            this.APIUrl = "/api/metadata";
            var requiredValidator1 = new RequiredValidator();
            this.AddValidator(requiredValidator1);
        }

        //public override string APIUrl { get => ApiUrl; set => ApiUrl = value; }

        public override string PickListType
        {
            get;
            set;
        }
        public override string Value { get ; set; }
    }
}
