using System;
using VPC.Metadata.Business.Validator;
using VPC.Metadata.Business.DataAnnotations;

namespace VPC.Metadata.Business.DataTypes.CustomField.Server
{
    public class CustomClientInt : CustomClientFieldBase
    {

        public CustomClientInt()
        {
            this.DataType = DataType.CustomClientInt;
            this.ControlType = ControlType.Link;
        }
        public override string Value { get; set; }


    }
}
