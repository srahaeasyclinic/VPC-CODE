namespace VPC.Entities.EntityCore.Model.Storage
{
    public class ListFilters
    {
        public string Name { get; set; }
        public int Sequence { get; set; }
        public string ControlType { get; set; }
        public dynamic Value { get; set; }
    }
}