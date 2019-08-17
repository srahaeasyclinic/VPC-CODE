using System;
using VPC.Metadata.Business.Validator;
using VPC.Metadata.Business.DataAnnotations;

namespace VPC.Metadata.Business.DataTypes.CustomField.Server
{
    public class CustomClientString : CustomClientFieldBase
    {

        public CustomClientString()
        {
            this.DataType = DataType.CustomClientString;
            this.ControlType = ControlType.Link;
        }
        public override string Value { get; set; }


    }
}
