using System;
using VPC.Metadata.Business.DataAnnotations;
using VPC.Metadata.Business.Validator;

namespace VPC.Metadata.Business.DataTypes {
    public class Password : TextBase {
        private byte[] _passwordHash;
        private byte[] _passwordSalt;

        public Password () {
            DataType = DataType.Password;
            base.ControlType = ControlType.TextBox;
            IsConfigurable = false;

            var requiredValidator1 = new RequiredValidator ();
            AddValidator (requiredValidator1);

            var lengthValidator = new LengthValidator ();
            lengthValidator.Dblength = 20;
            AddValidator (lengthValidator);
        }

        public override string Value { get; set; }

        public void DigestPassword (string passwordStr) {
            if (passwordStr == null) throw new ArgumentNullException ("password");
            if (string.IsNullOrWhiteSpace (passwordStr)) throw new ArgumentException ("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512 ()) {
                _passwordSalt = hmac.Key;
                _passwordHash = hmac.ComputeHash (System.Text.Encoding.UTF8.GetBytes (passwordStr));
            }
        }
        public byte[] PasswordHash { get { return _passwordHash; } }
        public byte[] PasswordSalt { get { return _passwordSalt; } }
    }
}