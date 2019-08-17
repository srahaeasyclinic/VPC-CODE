using System;
using System.Collections.Generic;
//using VPC.Entities.EntityCore.Metadata.Picklist;
// using VPC.Entities.EntityCore.Metadata.Runtime;
// using VPC.Metadata.Business.DataAnnotations;
// using VPC.Metadata.Business.DataTypes;
// using VPC.Metadata.Business.DataTypes.Complex;
// using VPC.Metadata.Business.Entity;
// using VPC.Metadata.Business.Entity.Configuration;
// using VPC.Metadata.Business.Entity.Infrastructure;
// using VPC.Metadata.Business.Operations;
// using VPC.Metadata.Business.SearchFilter;
// using VPC.Metadata.Business.Tasks;

namespace VPC.Entities.EntityCore.Metadata
{
    // [TableProperties("[dbo].[Menu]", "[Id]")]
    // [Operation(Operations.UpdateStatus, Operations.Delete)]
    // [DisplayName("Menu")]
    // [PluralName("Menu")]
    // [Import(false)]
    // [Export(false)]
    public class MenuItem
    {

        // public MenuItem()
        // {
        //     SubGroup = new List<MenuItem>();
        // }
        // [DefaultValue(InfoType.Menu)]
        // public EntityContext EntityContext => new EntityContext(InfoType.Menu);   
        public Guid TenantId { get; set; }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid GroupId { get; set; }
        public string GroupName { get; set; }
        public int MenuTypeId { get; set; }
        public string MenuTypeName { get; set; }
        public string ReferenceEntityId { get; set; }
        public int ActionTypeId { get; set; }
        public string ActionTypeName { get; set; }
        public string WellKnownLink { get; set; }
        public Guid ModifiedBy { get; set; }
        public Guid LayoutId { get; set; }
        public string ModifiedByName { get; set; }
        public DateTime ModifiedDate { get; set; }
        public Guid ParentId { get; set; }
        public int? SortItem { get; set; }
        public string MenuIcon { get; set; }
        public string Menucode { get; set; }

        public Boolean IsMenuGroup { get; set; }

        public int GroupIdSort { get; set; }

        // public List<MenuItem> SubGroup { get; set; }

    }
}