using System;
using System.Collections.Generic;
using VPC.Metadata.Business.Validator.Schema;
using VPC.Metadata.Business.DataAnnotations;

namespace VPC.Metadata.Business.DataTypes
{
    public class ComputedType : ComputedBase
    {
        public ComputedType()
        {
            this.DataType = DataType.Composite;
            this.ControlType = ControlType.Label;
            this.IsConfigurable = false;
        }
        public override bool Value { get; set; }
    }
}
