using System.Collections.Generic;

namespace VPC.Entities.EntityCore.Model.Query {
    public class QueryContext {
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        //  public Dictionary<string, dynamic> Filters { get; set; }
        public List<QueryFilter> Filters { get; set; }
        public List<QueryFilter> FreeTextSearch { get; set; }
        public string Fields { get; set; }
        public int MaxResult { get; set; }
        public string OrderBy { get; set; }
        public bool IsMappedData { get; set; }
    }
}