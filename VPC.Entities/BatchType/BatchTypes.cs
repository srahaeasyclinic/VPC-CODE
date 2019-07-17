
using System;
using System.ComponentModel;
using VPC.Entities.Common.Functions;
using VPC.Entities.EntityCore;

namespace VPC.Entities.BatchType
{
  public enum BatchTypes
    {       
        [Description("One time")]
        OneTime = 1,

        [Description("Recurrence")]
        Recurrence = 2,

        [Description("Scheduled")]
        Scheduled = 3
        
    }

}
