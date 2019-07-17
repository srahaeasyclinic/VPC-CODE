using System;
using System.Collections.Generic;
using VPC.Entities.EntityCore.Model.PickListItem;

namespace VPC.Entities.EntityCore.Model.PickListItem
{
    public class PickListContext
    {
        public Guid TenantId { get; set; }
        public Guid Id { get; set; }
        public string  Name { get; set; }
        public bool IsStandard { get; set; }
        public List<PickListOption> Values { get; set; }
    }
}