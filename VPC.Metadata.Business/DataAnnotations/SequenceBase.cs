using System;
using System.Collections.Generic;
using VPC.Metadata.Business.Operator.DataAnnotations;

namespace VPC.Metadata.Business.DataAnnotations
{
    public abstract class SequenceBase : DataTypeBase
    {
        public override DataType DataType { get; set; }

        public override ControlType ControlType
        {
            get;set;
        }

        public override bool IsConfigurable
        {
            get; set;
        }
        public virtual List<string> OperatorsAavailable
        {
            get
            {
                return new List<string>
                {
                    Operators.Equal,
                    Operators.Contains,
                    Operators.EndssWith,
                    Operators.StartsWith
                };
            }           
        }
    }
}
