using System;
using VPC.Metadata.Business.Validator;
using VPC.Metadata.Business.DataAnnotations;

namespace VPC.Metadata.Business.DataTypes
{
    public class Byte : BinaryBase
    {

        public Byte()
        {
            this.DataType = DataType.Byte;

            var requiredValidator1 = new RequiredValidator();
            AddValidator(requiredValidator1);

            var lengthValidator = new LengthValidator();
            AddValidator(lengthValidator);
        
        }
        public override string Value { get; set; }


    }
}
