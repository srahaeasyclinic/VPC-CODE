using VPC.Metadata.Business.DataAnnotations;
using VPC.Metadata.Business.Validator;

namespace VPC.Metadata.Business.DataTypes
{
    public class RichText : RichTextBase
    {
        public RichText()
        {
            DataType = DataType.Text;
            ControlType = VPC.Metadata.Business.DataAnnotations.ControlType.RichText;
            IsConfigurable = true;

            var requiredValidator1 = new RequiredValidator();
            AddValidator(requiredValidator1);

            var lengthValidator = new LengthValidator();
            //lengthValidator.Dblength = 3000;
            AddValidator(lengthValidator);
        }
        public override string Value { get; set; }
    }
}
