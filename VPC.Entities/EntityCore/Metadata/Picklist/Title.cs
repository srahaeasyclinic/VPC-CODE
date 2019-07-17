using System.Data;
using VPC.Metadata.Business.DataAnnotations;
using VPC.Metadata.Business.DataTypes;
using VPC.Metadata.Business.Entity;
using VPC.Metadata.Business.Entity.Configuration;
using VPC.Metadata.Business.Operations;
using ComponentModel = System.ComponentModel;

namespace VPC.Entities.EntityCore.Metadata.Picklist
{
    [TableProperties("[dbo].[PickListValue]", "[Id]")]
    [Operation(Operations.Create, Operations.Update, Operations.UpdateStatus, Operations.Delete)]
    [DisplayName("Title")]
    [PluralName("Titles")]    
    [FixedValue]
    public class Title : SimplePicklist
    {
        [NonQueryable]
        [ColumnName("[TenantId]")]
        [NotNull]
        public override InternalId TenantId { get; set; }

        [NonQueryable]
        public override InternalId InternalId { get; set; }

        [BasicColumn]
        [DefaultValue("10001")]
        [ColumnName("[PickListId]")]
        [NonQueryable]
        public override PicklistContext PicklistContext => new PicklistContext(PicklistType.Title);

        [NonQueryable]
        public override Name Name { get; set; }

        public override DataTable GetValues()
        {
            var lists = PickListHelper.GetPickListData(typeof(TitleEnum));
            return PickListHelper.GetValues(lists);
        }
    }

    public enum TitleEnum
    {
        [ComponentModel.Description("Mr.")]
        Mr = 1,
        [ComponentModel.Description("Mrs.")]
        Mrs = 2,
        [ComponentModel.Description("Miss")]
        Miss = 3,
        [ComponentModel.Description("Dr.")]
        Dr = 4,
        [ComponentModel.Description("Ms.")]
        Ms = 5,
        [ComponentModel.Description("Prof.")]
        Prof = 6,
        [ComponentModel.Description("Rev.")]
        Rev = 7,
        [ComponentModel.Description("Lady")]
        Lady = 8,
        [ComponentModel.Description("Sir")]
        Sir = 9,
        [ComponentModel.Description("Capt.")]
        Capt = 10,
        [ComponentModel.Description("Major")]
        Major = 11,
        [ComponentModel.Description("Lt.-Col.")]
        LtCol = 12,
        [ComponentModel.Description("Col.")]
        Col = 13,
        [ComponentModel.Description("Lt.-Cmdr.")]
        LtCmdr = 14,
        [ComponentModel.Description("The Hon.")]
        TheHon = 15,
        [ComponentModel.Description("Cmdr.")]
        Cmdr = 16,
        [ComponentModel.Description("Flt. Lt.")]
        FltLt = 17,
        [ComponentModel.Description("Brgdr.")]
        Brgdr = 18,
        [ComponentModel.Description("Judge")]
        Judge = 19,
        [ComponentModel.Description("Lord")]
        Lord = 20,
        [ComponentModel.Description("The Hon. Mrs")]
        TheHonMrs = 21,
        [ComponentModel.Description("Wng. Cmdr.")]
        WngCmdr = 22,
        [ComponentModel.Description("Group Capt.")]
        GroupCapt = 23
    }

}