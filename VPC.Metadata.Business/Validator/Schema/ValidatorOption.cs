using System;
using System.Collections.Generic;
//comment
//using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VPC.Metadata.Business.DataAnnotations;

namespace VPC.Metadata.Business.Validator.Schema
{

    public class ValidatorOption
    {
        public string Name { get; set; }
        public dynamic Value { get; set; }

        public ControlType ControlType { get; set; }
 
    } 
}
