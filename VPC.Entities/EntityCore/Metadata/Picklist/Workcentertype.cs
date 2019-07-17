// using System;
// using System.Data;
// using VPC.Metadata.Business.DataAnnotations;
// using VPC.Metadata.Business.DataTypes;
// using VPC.Metadata.Business.Entity;
// using VPC.Metadata.Business.Entity.Configuration;
// using VPC.Metadata.Business.Operations;
// using ComponentModel = System.ComponentModel;

// namespace VPC.Entities.EntityCore.Metadata.Picklist
// {
//     [TableProperties("[dbo].[PickListValue]", "[Id]")]
//     [Operation(new string[] { Operations.Create, Operations.Update, Operations.UpdateStatus, Operations.Delete })]
//     [DisplayName("Workcenter type")]
//     [PluralName("Workcenter types")]
//     [FixedValue]
//         [CustomizeValue()]
//     public class WorkcenterType : SimplePicklist
//     {
//         [AccessibleLayout(1, 3)]
//         [NonQueryable]
//         [ColumnName("[TenantId]")]
//         [NotNull]
//         public override InternalId TenantId { get; set; }

//         [AccessibleLayout(1, 3)]
//         [BasicColumn]
//         [NonQueryable]
//         [ColumnName("[Id]")]
//         [NotNull]
//         public override InternalId InternalId { get; set; }

//         [AccessibleLayout(1, 3)]
//         [DefaultValue("EN20002-PL001")]
//         [ColumnName("[PickListId]")]
//         public override PicklistContext PicklistContext => new PicklistContext(PicklistType.WorkcenterType);

//         [NonQueryable]
//         public override Name Name { get; set; }

//         public override DataTable GetValues()
//         {
//             var lists = PickListHelper.GetPickListData(typeof(WorkcenterTypeEnum));
//             return PickListHelper.GetValues(lists);
//         }
//     }

//     public enum WorkcenterTypeEnum
//     {
//         [ComponentModel.Description("Internal")]
//         Internal = 1,
//         [ComponentModel.Description("External")]
//         External = 1
//     }
// }
