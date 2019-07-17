using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VPC.WebApi.AttributesHandler
{
    public class ValidateJObjectdata : TypeFilterAttribute
    {
        public ValidateJObjectdata() : base(typeof(ValidateEntityJsonObjData))
        {

        }
    }

    public class ValidatePicklist: TypeFilterAttribute
    {
        public ValidatePicklist() : base(typeof(ValidatePickList))
        {

        }
    }
}
