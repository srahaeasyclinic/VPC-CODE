using VPC.Metadata.Business.DataAnnotations;
using VPC.Metadata.Business.Entity.Configuration;
using VPC.Metadata.Business.Rules;

namespace VPC.Metadata.Business.DataTypes.Complex
{
    [TableProperties("[dbo].[Address]", "[Id]")]
    [CascadeDelete]
    public class Address : ComplexBase
    {
        [ColumnName("[TenantId]")]
        [NotNull]
        public InternalId TenantId { get; set; }

        [ColumnName("[Id]")]
        [NotNull]
        public InternalId InternalId { get; set; }

        [AccessibleLayout(1, 2, 3)]
        [Tagable]
        [ColumnName("[AddressLine1]")]
        public MediumText AddressLine1 { get; set; }

        [AccessibleLayout(1, 2, 3)]
        [Tagable]
        [ColumnName("[AddressLine2]")]
        public MediumText AddressLine2 { get; set; }

        [AccessibleLayout(1, 2, 3)]
        [Tagable]
        [ColumnName("[AddressLine3]")]
        public MediumText AddressLine3 { get; set; }

        [AccessibleLayout(1, 2, 3)]
        [Tagable]
        [ColumnName("[CareOf]")]
        public MediumText CareOf { get; set; }

        [AccessibleLayout(1, 2, 3)]
        [Tagable]
        [ColumnName("[POBox]")]
        public MediumText POBox { get; set; }


        [AccessibleLayout(1, 2, 3)]
        [Broadcaster(QueingConstant.CountryState)]
        [Tagable]
        [InverseProperty("[Id]")]
        [ColumnName("[CountryId]")]
        ////[DynamicPrefix("_AD_Cou")]
        public PickList<Picklists.Country> CountryId { get; set; }

        [AccessibleLayout(1, 2, 3)]
        [Receiver("CountryId", QueingConstant.CountryState)]
        [Broadcaster(QueingConstant.StateCity)]
        [Tagable]
        [ColumnName("[StateId]")]
        [InverseProperty("[Id]")]
        ////[DynamicPrefix("_AD_Sta")]

        public PickList<Picklists.State> StateId { get; set; }


        [AccessibleLayout(1, 2, 3)]
        [Receiver("StateId", QueingConstant.StateCity)]
        //[Broadcaster(QueingConstant.CityMunicipality)]
        [Tagable]
        [ColumnName("[CityId]")]
        [InverseProperty("[Id]")]
        //[DynamicPrefix("_AD_Cit")]
        public PickList<Picklists.City> CityId { get; set; }


        [AccessibleLayout(1, 2, 3)]
      //  [Receiver("CityId", QueingConstant.CityMunicipality)]
        [Tagable]
        [ColumnName("[MunicipalityId]")]
        [InverseProperty("[Id]")]
        //[DynamicPrefix("_AD_Mun")]
        public PickList<Picklists.Municipality> MunicipalityId { get; set; }

        [AccessibleLayout(1, 2, 3)]
        [Tagable]
        [ColumnName("[PostalCode]")]
        public XSmallText PostalCode { get; set; }

        public Address()
        {
            base.ControlType = ControlType.Address;
        }
    }
}