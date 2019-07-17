

using System;
using System.Collections.Generic;

namespace VPC.Entities.EntityCore.Model.Storage
{
    public class Entity
    {
       public string DisplayName { get; set; }
        public string Name { get; set; }
        public string PluralName { get; set; }
        public bool SupportWorkflow { get; set; }
        public string Type { get; set; }

        public string RelatedField { get; set; }
        public string RelatedEntity { get; set; }

        public Configuration Configurations { get; set; }
        public List<string> Subtypes { get; set; }
        public List<FieldModel> Fields { get; set; }
        public List<Relation> Relations { get; set; }
        public List<Operation> Operations { get; set; }
       
        public List<Tasks> Tasks { get; set; }

        public List<Entity> RelatedEntities { get; set; }
        public List<Entity> DetailEntities { get; set; }
        public List<RowLevelOperations> RowLevelOperations { get; set; }
        public Entity ActivityEntity { get; set; }

    }
}