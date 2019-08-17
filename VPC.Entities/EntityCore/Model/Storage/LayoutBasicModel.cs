using System;

namespace VPC.Entities.EntityCore.Model.Storage
{
    public class LayoutBasicModel
    {

 public string TypeId { get; set; }

        public Guid Id { get; set; }
        //public Guid Type { get; set; }
        public string EntityId { get; set; }
        public string EntityName { get; set; }
        public string Name { get; set; }
        public string PluralName { get; set; }
        public string SingularName { get; set; }
        public LayoutType LayoutType { get; set; }
        public string LayoutTypeName { get; set; }
        //public LayoutSubType Subtype { get; set; }
        public string Subtype { get; set; }
        public string SubtypeeName { get; set; }
        public LayoutContext Context { get; set; }
        public string ContextName { get; set; } 
        public Guid ModifiedBy { get; set; }
        public string ModifiedByName { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool DefaultLayout { get; set; }
        public string ShowDefault { get; set; }
        public LayoutFor LayoutFor { get; set; }

        public string VersionName{get;set;}
    }

    public enum LayoutType
    {
        View =1,
        Form =2,
        List=3
    }

    //public enum LayoutSubType
    //{
    //    Temporary = 1,
    //    Permanent = 2
    //}

    public enum LayoutContext
    {
        Add = 1,
        Edit = 2,
        QuickAdd = 3
    }

    public enum LayoutFor
    {
        Metadata = 1,
        Picklist = 2,
    }


}