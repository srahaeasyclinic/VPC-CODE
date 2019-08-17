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
using VPC.Metadata.Business.SearchFilter;

namespace VPC.Entities.EntityCore.Metadata
{
    [TableProperties("[dbo].[EmailTemplate]", "[Id]")]
    [Operation(Operations.Create, Operations.Update, Operations.Delete, Operations.UpdateStatus)]
    [DisplayName("Email template")]
    [PluralName("Email templates")]
    public class EmailTemplate : PrimaryEntity, IItem<Item>
    {
        [NonQueryable]
        [ColumnName("[TenantId]")]
        [NotNull]
        [DisplayName("Tenant Id")]
        //[AccessibleLayout((int)LayoutType.View, (int)LayoutType.List)]
        //
        public InternalId TenantId { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List)]
        [BasicColumn]
        [NonQueryable]
        [ColumnName("[Id]")]
        [NotNull]
        [DisplayName("Internal Id")]
        public override InternalId InternalId { get; set; }

        [NonQueryable]
        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List)]
        [DisplayName("Name")]
        public override Name Name { get; set; }

        [DefaultValue(InfoType.Emailtemplate)]
        [DisplayName("Entity context")]
        public override EntityContext EntityContext => new EntityContext(InfoType.Emailtemplate);

        [AccessibleLayout((int)LayoutType.List)]
        [DisplayName("Sub type")]
        public override XSmallText SubType { get; set; }

        [DisplayName("Sub types")]
        public override Dictionary<string, string> SubTypes => new Dictionary<string, string>
        {
            {"EN10016-ST01", "Standard"}
        };

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [ApplicableForFilter]
        [FreeTextSearch]
        [SimpleSearch]
        [ColumnName("[Title]")]
        [NotNull]
        [DisplayName("Title")]
        public MediumText Title { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [Broadcaster(MessageQueuingType.EntityRichTextBox)]
        [ApplicableForFilter]
        [SimpleSearch]
        [FreeTextSearch]
        [ColumnName("[Context]")]
        [NotNull]
        [DefaultValue("#ENTCAST")]//This is fixed and unique default value to be used to get Entitycontext Type
        [DisplayName("Context")]
        public MetadataPicklist Context { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [Receiver("Context", MessageQueuingType.EntityRichTextBox)]
        [ColumnName("[Body]")]
        [NotNull]
        [DisplayName("Body")]
        public RichText Body { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [ApplicableForFilter]
        [SimpleSearch]
        [IsReadonly]
        //[NotNull]
        [ColumnName("[ContextType]")]
        [DisplayName("Context type")]
        public PickList<CommunicationContextType> CommunicationContextType { get; set; }
    }
}
