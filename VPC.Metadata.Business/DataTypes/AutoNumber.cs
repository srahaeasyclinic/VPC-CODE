using System;
using System.Text;
using VPC.Metadata.Business.Validator;
using VPC.Metadata.Business.DataAnnotations;

namespace VPC.Metadata.Business.DataTypes
{
    public class AutoNumber : AutoNumberBase
    {
        public AutoNumber()
        {
            this.DataType = DataType.Custom;
            this.ControlType = ControlType.Label;
            this.AutoNumbers = RandomNumber();

            var requiredValidator1 = new RequiredValidator();
            this.AddValidator(requiredValidator1);

            var requiredValidator2 = new RangeValidator();
            this.AddValidator(requiredValidator2);
        }

        public string AutoNumbers { get; set; }

        public string RandomNumber()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(RandomString(3, true));
            builder.Append(RandomNumber(1000, 9999));
            builder.Append(RandomString(3, false));
            return builder.ToString();
        }

        public int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }

        public string RandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }        
    }
}
