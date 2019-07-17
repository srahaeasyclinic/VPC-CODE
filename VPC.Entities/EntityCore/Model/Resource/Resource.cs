using System; 

namespace VPC.Entities.EntityCore.Model.Resource
{
    public class Resource
    { 
        public Guid Id { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public string Language { get; set; }
        public string LanguageName { get; set; }
    }
}