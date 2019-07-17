using System;
using System.Collections.Generic;
using VPC.Metadata.Business.Validator.Schema;

namespace VPC.Metadata.Business.DataAnnotations
{
    public abstract class DataTypeBase
    {
        public DataTypeBase()
        {

        }

    
        public abstract DataType DataType { get; set; }
        public abstract ControlType ControlType { get; set; }
        public abstract bool IsConfigurable { get; set; }

        public virtual void AddValidator(ValidatorBase validator)
        {

        }

        public virtual List<ValidatorBase> GetValidators()
        {
            return null;
        }

        
    }
}
