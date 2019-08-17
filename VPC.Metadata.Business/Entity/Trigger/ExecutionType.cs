using System;
using System.Collections.Generic;
using System.Text;
using VPC.Metadata.Business.DataAnnotations;
using VPC.Metadata.Business.Operator.DataAnnotations;
using VPC.Metadata.Business.Validator.Schema;

namespace VPC.Metadata.Business.Entity.Trigger
{
    public enum ExecutionType
    {
        Create,
        Update,
        Delete
    }
}
