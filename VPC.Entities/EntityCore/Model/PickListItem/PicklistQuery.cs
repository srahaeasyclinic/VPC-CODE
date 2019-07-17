using System.Collections.Generic;
using VPC.Entities.Common;
using VPC.Entities.EntityCore.Model.Query;

namespace VPC.Entities.EntityCore.Model.PickListItem
{
    public class PicklistQuery
    {
        public PagingParameters PagingParameters { get; set; }
        public string SearchText { get; set; } 
        public bool IsDeleted { get; set; } 
    }
}