using System;
using VPC.Metadata.Business.DataAnnotations;

using VPC.Metadata.Business.DataTypes;
using VPC.Metadata.Business.Entity.Configuration;

namespace VPC.Metadata.Business.DataTypes
{
    [TableProperties("[dbo].[Image]", "[Id]")]
    [CascadeDelete]
    public class Image: ComplexBase
    {
        [NonQueryable]
        [ColumnName("[TenantId]")]
        [NotNull]
        public InternalId TenantId { get; set; }

        
        [ColumnName("[Id]")]
        [NotNull]
        [NonQueryable]
        public InternalId InternalId { get; set; }

        public InternalId ImageId { get; set; }
      //  public Byte[] Images { get; set; }
        public BooleanType IsLogo { get; set; }
        public NumericType Height { get; set; }
        public NumericType Width { get; set; }
     
        public StringType ThumbnailPath { get; set; }
        public StringType Url { get; set; }
        public NumericType Priority { get; set; }
        public StringType ImageUrl { get; set; }

        public Image()
        {
            ControlType = ControlType.Image; 
        }
    }
}
