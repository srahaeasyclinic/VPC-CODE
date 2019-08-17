using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using VPC.Entities.EntityCore.Model.Storage;
using VPC.Metadata.Business.DataAnnotations;
using VPC.Metadata.Business.DataTypes;
using VPC.Metadata.Business.Entity;
using VPC.Metadata.Business.Entity.Configuration;
using VPC.Metadata.Business.Operations;

namespace VPC.Entities.EntityCore.Metadata.Picklist
{
   
    [Operation(Operations.Create, Operations.Update, Operations.UpdateStatus, Operations.Delete)]
    [DisplayName("Context type")]
    [PluralName("Context types")]
    [FixedValue]
    [Standard]
    public class CommunicationContextType : SimplePicklist
    {
        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.List)]
        [NonQueryable]
        [ColumnName("[TenantId]")]
        [NotNull]
        public override InternalId TenantId { get; set; }
        public override InternalId InternalId { get; set; }
        public override PicklistContext PicklistContext => new PicklistContext(PicklistType.CommunicationContextType);

        public override Name Name { get; set; }

        public override DataTable GetValues()
        {
            var lists = PickListHelper.GetPickListData(typeof(ContextTypeEnum));
            return PickListHelper.GetValues(lists);
        }

        public enum ContextTypeEnum
        {
            [System.ComponentModel.Description("Welcome e-mail")]
            Welcome = 1,

            [System.ComponentModel.Description("Password reset")]
            Passwordreset = 2,

            [System.ComponentModel.Description("Forgot password")]
            Forgotpassword = 3,

            [System.ComponentModel.Description("New tenant credential")]
            NewTenantCredential = 4,

            [System.ComponentModel.Description("Export user")]
            ExportUser = 5

            // Please do not use 4 ,as it is internally used for System admin mail template(Ajay Chouhan)



        }
    }
}
