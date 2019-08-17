
using System;
using System.Collections.Generic;

namespace VPC.Entities.BatchType
{
   public enum BatchItemTypeEnum
    {
        Queued = 1,
        Processed24hours = 2,
        Errored = 3,
        Errored24hours = 4,

    }

}
