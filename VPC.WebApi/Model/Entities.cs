using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VPC.WebApi.Model
{
    public class Entities
    {
        public string DisplayName { get; set; }
        public string Name { get; set; }
        public string PluralName { get; set; }
        public bool SupportWorkflow { get; set; }
        public string EntityType { get; set; }
        public Configuration Configurations { get; set; }
        public List<string> Subtypes { get; set; }
        public List<FieldModel> Fields { get; set; }
        public List<Relation> Relations { get; set; }
        public List<Operation> Operations { get; set; }
        public List<Tasks> Tasks { get; set; }
        public List<Entities> RelatedEntities { get; set; }
        public List<RowLevelOperations> RowLevelOperations { get; set; }
    }
}
