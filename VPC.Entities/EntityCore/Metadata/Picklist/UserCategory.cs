using System.Data;
using VPC.Metadata.Business.DataAnnotations;
using VPC.Metadata.Business.DataTypes;
using VPC.Metadata.Business.Entity;
using VPC.Metadata.Business.Entity.Configuration;
using VPC.Metadata.Business.Operations;
using VPC.Metadata.Business.SearchFilter;
using VPC.Entities.EntityCore.Model.Storage;
namespace VPC.Entities.EntityCore.Metadata.Picklist
{
    [TableProperties("[dbo].[PickListValue]", "[Id]")]
    [Operation(new string[] { Operations.Create, Operations.Update, Operations.UpdateStatus, Operations.Delete })]
    [DisplayName("User category")]
    [PluralName("User categories")]
    [SupportWorkflow(false)]
    [Standard]
        [CustomizeValue()]
    public class UserCategory : SimplePicklist
    {
        

   

 [NonQueryable]
        [ColumnName("[TenantId]")]
        [NotNull]
        public override InternalId TenantId { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List)]
        [NonQueryable]
        [ColumnName("[Id]")]
        [NotNull]
         [BasicColumn]
        public override InternalId InternalId { get; set; }

        [BasicColumn]
        [DefaultValue("10013")]
        [ColumnName("[PickListId]")]
        [NonQueryable]
        [NotNull]
       public override PicklistContext PicklistContext => new PicklistContext(PicklistType.UserCategory);

        [NonQueryable]
        // [ApplicableForFilter]
        // [AdvanceSearch]
        public override Name Name { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [BasicColumn]
        [ColumnName("[Key]")]
        [FreeTextSearch]
        [NotNull]
        public SmallText Key { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List, (int)LayoutType.Form)]
        [BasicColumn]
        [ColumnName("[Text]")]
        [FreeTextSearch]
        [NotNull]
        public MediumText Text { get; set; }



        public override DataTable GetValues()
        {
            return null;
        }
    }
}