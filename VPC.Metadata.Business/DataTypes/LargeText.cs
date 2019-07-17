using System;
using VPC.Metadata.Business.Validator;
using VPC.Metadata.Business.DataAnnotations;

namespace VPC.Metadata.Business.DataTypes
{
    public class LargeText : TextBase
    {
        public LargeText()
        {
            DataType = DataType.Text;
            base.ControlType = ControlType.TextArea;
            IsConfigurable = false;           
        }
        public override string Value { get; set; }
    }
}
