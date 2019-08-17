using System;
using VPC.Metadata.Business.DataAnnotations;

using VPC.Metadata.Business.DataTypes;
using VPC.Metadata.Business.Entity.Configuration;
using VPC.Metadata.Business.Validator;

namespace VPC.Metadata.Business.DataTypes
{
    [TableProperties("[dbo].[Image]", "[Id]")]
    [CascadeDelete]
    public class Image: ComplexBase
    {
        [NonQueryable]
        [ColumnName("[TenantId]")]
        [NotNull]
        [DisplayName("Tenant Id")]
        public InternalId TenantId { get; set; }

        
        [ColumnName("[Id]")]
        [NotNull]
        [NonQueryable]
        [DisplayName("Internal Id")]
        public InternalId InternalId { get; set; }

        [DisplayName("Image")]
        public InternalId ImageId { get; set; }
        //  public Byte[] Images { get; set; }

        [DisplayName("Is logo")]
        public BooleanType IsLogo { get; set; }

        [DisplayName("Height")]
        public NumericType Height { get; set; }

        [DisplayName("Width")]
        public NumericType Width { get; set; }
     
        [DisplayName("Thumbnail path")]
        public StringType ThumbnailPath { get; set; }

        [DisplayName("Url")]
        public StringType Url { get; set; }

        [DisplayName("Priority")]
        public NumericType Priority { get; set; }

        [DisplayName("Image Url")]
        public StringType ImageUrl { get; set; }

        public Image()
        {
            ControlType = ControlType.Image; 
             var defaultValueValidattor = new DefaultValueValidator (ControlType);
            this.AddValidator (defaultValueValidattor);
        }
    }
}
