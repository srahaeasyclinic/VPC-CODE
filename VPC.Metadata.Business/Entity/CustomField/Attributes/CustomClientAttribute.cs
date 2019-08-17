using System;

namespace VPC.Metadata.Business.Entity.CustomField.Attributes {
    [AttributeUsage (AttributeTargets.All, AllowMultiple = true)]
    public class CustomClientAttribute : Attribute {

        private CustomClientContext _value { get; set; }
        public CustomClientAttribute (CustomClientContext value) {
            _value = value;
        }
        public CustomClientContext GetValue () {
            return _value;
        }
    }

}