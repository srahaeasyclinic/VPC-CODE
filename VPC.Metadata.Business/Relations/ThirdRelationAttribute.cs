using System;

namespace VPC.Metadata.Business.Relations
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public class ThirdRelationAttribute:Attribute
    {
        public ThirdRelationAttribute(string relationName, string relationType, string relationColumnName)
        {
            RelationName = relationName;
            RelationType = relationType;
            RelationColumnName = relationColumnName;
        }

        public string RelationName { get; }
        public string RelationType { get; }
        public string RelationColumnName { get; }
        public string TableName { get { return "RD_RelationData"; } }
    }
}


