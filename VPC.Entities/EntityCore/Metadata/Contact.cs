using System;
using System.Collections.Generic;
using VPC.Entities.EntityCore.Metadata.Picklist;
using VPC.Entities.EntityCore.Metadata.Runtime;
using VPC.Entities.EntityCore.Model.Storage;
using VPC.Metadata.Business.DataAnnotations;
using VPC.Metadata.Business.DataTypes;
using VPC.Metadata.Business.Entity;
using VPC.Metadata.Business.Entity.Configuration;
using VPC.Metadata.Business.Entity.Infrastructure;
using VPC.Metadata.Business.Operations;

namespace VPC.Entities.EntityCore.Metadata
{
    [TableProperties("[dbo].[Contact]", "[Id]")]
    [Operation(Operations.Create, Operations.Update, Operations.Delete, Operations.UpdateStatus)]
    [DisplayName("Contact")]
    [PluralName("Contacts")]
    public class Contact : PrimaryEntity, IItem<Item>
    {       
        [NonQueryable]
        [ColumnName("[TenantId]")]
        [NotNull]
        [DisplayName("Tenant Id")]
        public InternalId TenantId { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List)]
        [NonQueryable]
        [ColumnName("[Id]")]
        [NotNull]
        [DisplayName("Internal Id")]
        public override InternalId InternalId { get; set; }

        [NonQueryable]
        [DisplayName("Name")]
        public override Name Name { get; set; }
        [DisplayName("Entity context")]
        public override EntityContext EntityContext => new EntityContext(InfoType.Contact);
        
        [DisplayName("Sub types")]
        public override Dictionary<string, string> SubTypes => new Dictionary<string, string> { { "EN10026-ST01", "Standard" } };

        [AccessibleLayout((int)LayoutType.List)]
         [DisplayName("Sub type")]
        public override XSmallText SubType { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [ColumnName("[TitleId]")]
        [NonQueryable]
        //[DynamicPrefix(InfoPrefix.Title_Contact)]
        [DisplayName("Title Id")]
        public PickList<Title> TitleId { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [ColumnName("[FirstName]")]
        [NotNull]
        [DisplayName("First name")]
        public SmallText FirstName { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [ColumnName("[MiddleName]")]
        [DisplayName("Middle name")]
        public SmallText MiddleName { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [ColumnName("[LastName]")]
        [DisplayName("Last name")]
        public SmallText LastName { get; set; }
    }
}