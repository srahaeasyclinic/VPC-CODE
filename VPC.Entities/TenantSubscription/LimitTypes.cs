using System;
using System.Collections.Generic;
using System.ComponentModel;
using VPC.Entities.Common;

namespace VPC.Entities.TenantSubscription
{
    public enum LimitTypes
    {
        [Description("Total count")]
         TotalCount = 1,

        [Description("Total Unit")]
         TotalUnit = 2
    }
}
