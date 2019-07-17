using System.Collections.Generic;

namespace VPC.Entities.EntityCore.Model.Storage
{
    public class Validator
    {
        public string Name { get; set; }
        public bool Customizable { get; set; }

        // Added new validation rule for field lenght as per Database's field lenght.
        public int? Dblength{ get; set; }

        public int? UserSetlength{ get; set; }

        public string Pattern{ get; set; }
        public List<ValidatorOptions> Options { get; set; }
    }

    public class ValidatorOptions{
         public string Name { get; set; }
        public dynamic Value { get; set; }

        public string ControlType { get; set; }
 
    }
}