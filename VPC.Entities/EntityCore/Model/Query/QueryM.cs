using System.Collections.Generic;

namespace VPC.Entities.EntityCore.Model.Query
{
   public class QueryM
    {
        public List<string> Fields { get; set; }
        public List<QueryFilter> Filters { get; set; }
        public SortColumn SortField { get; set; }
        public List<DataTableColumnData> DataTableColumnData { get; set; }
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public int MaxResult { get; set; }
    }
}

