using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPC.Metadata.Business.Entity.Configuration
{
    public class PluralNameAttribute : EntityDescriptionAttribute
    {
        public PluralNameAttribute(string pluralName) : base(pluralName)
        {

        }
    }
}
