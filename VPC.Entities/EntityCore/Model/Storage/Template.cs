using System.Collections.Generic;

namespace VPC.Entities.EntityCore.Model.Storage
{
    public class Template
    {
         public string DisplayName { get; set; }
        public string Name { get; set; }
        public string PluralName { get; set; }
        public bool SupportWorkflow { get; set; }
        public string EntityType { get; set; }
        public List<FieldModel> Fields { get; set; }
    }
}