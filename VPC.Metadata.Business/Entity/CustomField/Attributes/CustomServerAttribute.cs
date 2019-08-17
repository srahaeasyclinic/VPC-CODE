using System;
using System.Collections.Generic;
using System.Linq;
namespace VPC.Metadata.Business.Entity.CustomField.Attributes {
    [AttributeUsage (AttributeTargets.All, AllowMultiple = true)]
    public class CustomServerAttribute : Attribute {

        private CustomServerContext _value { get; set; }
        public CustomServerAttribute (CustomServerContext value) {
            _value = value;
        }
        public CustomServerContext GetValue () {
            return _value;
        }
    }

}