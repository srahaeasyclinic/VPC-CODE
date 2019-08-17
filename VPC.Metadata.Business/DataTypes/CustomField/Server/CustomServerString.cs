using System;
using VPC.Metadata.Business.Validator;
using VPC.Metadata.Business.DataAnnotations;

namespace VPC.Metadata.Business.DataTypes.CustomField.Server
{
    public class CustomServerString : CustomServerFieldBase
    {

        public CustomServerString()
        {
            this.DataType = DataType.CustomServerString;
            this.ControlType = ControlType.Link;
        }
        public override string Value { get; set; }


    }
}
