namespace VPC.Entities.EntityCore.Model.Query
{
    public class QueryFilter
    {
        public string FieldName { get; set; }
        public string Operator { get; set; }
        public dynamic Value { get; set; }
    }
}