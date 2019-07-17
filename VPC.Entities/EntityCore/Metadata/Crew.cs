//using System;
//using System.Collections.Generic;
//using VPC.Entities.EntityCore.Metadata.Picklist;
//using VPC.Entities.EntityCore.Metadata.Runtime;
//using VPC.Metadata.Business.DataAnnotations;
//using VPC.Metadata.Business.DataTypes;
//using VPC.Metadata.Business.DataTypes.Complex;
//using VPC.Metadata.Business.Entity;
//using VPC.Metadata.Business.Entity.Configuration;
//using VPC.Metadata.Business.Entity.Infrastructure;
//using VPC.Metadata.Business.Operations;
//using VPC.Metadata.Business.SearchFilter;
//using VPC.Metadata.Business.Tasks;

//namespace VPC.Entities.EntityCore.Metadata
//{
//    [TableProperties("[dbo].[OrganizationUnit]", "[Id]")]
//    [Operation(Operations.Create, Operations.Update, Operations.UpdateStatus, Operations.Delete)]
//    [DisplayName("Organization unit")]
//    [PluralName("Organization Units")]
//    // [Import(false)]
//    // [Export(false)]
//    // [SupportWorkflow(true)]
//    // [SendEmail("Send email")]
//    // [SendSMS("Send sms")]
//    // [Merge("Merge")]
//    // [Print("Print")]
//    public class Crew : PrimaryEntity, IItem<Item>
//    {
      
//        public Crew()
//        {
            
//        }

//        public override InternalId InternalId { get; set; }

//         public override Dictionary<string, string> SubTypes => new Dictionary<string, string>
//        {
//            {"EN10002-ST02", "Standard"}
//        };


//        public override XSmallText SubType { get; set; }
//        public override Name Name { get; set; }

//        public override EntityContext EntityContext => new EntityContext(InfoType.Crew);
//        public Guid TenantId { get; set; }
//        public Guid Id { get; set; }
//    }
//}