
using VPC.Metadata.Business.DataAnnotations;
using VPC.Metadata.Business.Validator;

namespace VPC.Metadata.Business.DataTypes
{
    public class XLargeText : TextBase
    {
        public XLargeText()
        {
            DataType = DataType.Text;
            base.ControlType = ControlType.TextBox;
            IsConfigurable = false;

            var requiredValidator1 = new RequiredValidator();
            AddValidator(requiredValidator1);

            var lengthValidator = new LengthValidator();
            lengthValidator.Dblength = 1000;
            lengthValidator.MinDblength = 1;
            AddValidator(lengthValidator);
             var defaultValueValidattor = new DefaultValueValidator (ControlType);
            this.AddValidator (defaultValueValidattor);
        }
        public override string Value { get; set; }
    }
}
