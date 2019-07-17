using System;
using System.Collections.Generic;
//comment
//using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPC.Metadata.Business.Validator.Schema
{

    public abstract class ValidatorBase
    {
        public abstract string ValidationName { get; }
        //public abstract bool Customize { get; }
        public abstract bool Customizable { get; }

        // Added new validation rule for field lenght as per Database's field lenght.
        //public virtual int? Dblength { get; set; }

        public abstract List<ValidatorOption> GetExtraValidationParameters();


        protected ValidatorBase()
        {


        }
 
    } 
}
