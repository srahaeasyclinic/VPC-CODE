using System;
using System.Collections.Generic;
//using VPC.Metadata.Business.Entity.Model;
using VPC.Metadata.Business.Operator.DataAnnotations;

namespace VPC.Metadata.Business.DataAnnotations
{
    public abstract class PickListBase : DataTypeBase
    {
        public override DataType DataType { get; set; }

        public override ControlType ControlType
        {
            get; set;
        }

        public override bool IsConfigurable
        {
            get; set;
        }

        public virtual string PickListType { get; set; }
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

        public virtual Dictionary<dynamic, string> GetData
        {
            get;
           
        }

        public abstract string Value {get; set;}
    }
}
