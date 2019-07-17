using VPC.Metadata.Business.DataAnnotations;

namespace VPC.Entities.EntityCore.Model.Query
{
    public class ColumnAndField
    {
        public string EntityFullName { get; set; }
        public string EntityPrefix { get; set; }
        public string FieldName { get; set; }

        public string ClientName{get;set;}
        public string ColumnName { get; set; }
        public string TableName { get; set; }
        public string PrimaryKey { get; set; }

        public string Linker { get; set; }

        public dynamic Value{get;set;}
        public bool IsNotNull{get; set;}
        
        public string ReferenceTableName { get; set; }

        public string ReferenceColumnName { get; set; }
         public string ReferencePrefixName { get; set; }

        public string InverseTableName{get;set;}
        public string InverseColumnName{get;set;}
        public string InversePrefixName{get; set;}

        public bool AllowCaseCadingDelete{get; set;}

        public int QueryIndex{get;set;}

        public DataType DataType{get; set;}

        public string DisplayName{get;set;}

        public bool IsIntersectProperties{get;set;}
        public string IntersectClassName { get; set; }
        public bool DefaultOrder { get; set; }

        public string TypeOf {get; set;}
        public bool VirtualField{get;set;}
        public string VirtualName{get;set;}
        //-----------------------
    }
}