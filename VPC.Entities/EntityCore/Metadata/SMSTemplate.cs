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
    [TableProperties("[dbo].[SMSTemplate]", "[Id]")]
    [Operation(Operations.Create, Operations.Update, Operations.Delete, Operations.UpdateStatus)]
    [DisplayName("SMS template")]
    [PluralName("SMS templates")]
    public class SMSTemplate : PrimaryEntity, IItem<Item>
    {
        [NonQueryable]
        [ColumnName("[TenantId]")]
        [NotNull]
        //[AccessibleLayout((int)LayoutType.View, (int)LayoutType.List)]
        //
        public InternalId TenantId { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List)]
        [BasicColumn]
        [NonQueryable]
        [ColumnName("[Id]")]
        [NotNull]
        public override InternalId InternalId { get; set; }

        [NonQueryable]
        public override Name Name { get; set; }

        [DefaultValue(InfoType.SMStemplate)]
        public override EntityContext EntityContext => new EntityContext(InfoType.SMStemplate);

        [AccessibleLayout((int)LayoutType.List)]
        public override XSmallText SubType { get; set; }

        public override Dictionary<string, string> SubTypes => new Dictionary<string, string>
        {
            {"EN10017-ST01", "Standard"}
        };

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [ApplicableForFilter]
        [FreeTextSearch]        
        [SimpleSearch]
        [ColumnName("[Title]")]
        [NotNull]
        public MediumText Title { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [Broadcaster(MessageQueuingType.EntityRichTextBox)]
        [ApplicableForFilter]        
        [FreeTextSearch]
        [SimpleSearch]
        [ColumnName("[Context]")]
        [NotNull]
        [DefaultValue("#ENTCAST")] //This is fixed and unique default value to be used to get Entitycontext Type
        public MetadataPicklist Context { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [Receiver("Context", MessageQueuingType.EntityRichTextBox)]
        [ColumnName("[Body]")]
        [NotNull]
        public RichText Body { get; set; }


        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [ApplicableForFilter]       
        [SimpleSearch]
        [IsReadonly]
        [ColumnName("[ContextType]")]
        public PickList<CommunicationContextType> CommunicationContextType { get; set; }

        //[FreeTextSearch]
        //[ColumnName("[ParentId]")]
        //public PickList<HierarchyPickList> hierarchyPickList { get; set; }
    }
}
