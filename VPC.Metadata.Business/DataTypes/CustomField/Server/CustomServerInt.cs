using System;
using VPC.Metadata.Business.Validator;
using VPC.Metadata.Business.DataAnnotations;

namespace VPC.Metadata.Business.DataTypes.CustomField.Server
{
    public class CustomServerInt : CustomServerFieldBase
    {

        public CustomServerInt()
        {
            this.DataType = DataType.CustomServerInt;
            this.ControlType = ControlType.Link;
        }
        public override string Value { get; set; }


    }
}
