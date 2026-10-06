using DealerDatabase.Import.DTOs.DealerDatabase.Import;
using DealerDatabase.Import.Sources.JSON.CompaniesHouse;

namespace DealerDatabase.Import.Mappers
{
    public static class CompaniesHouseMapper
    {
        public static DealerRecordDTO Map(CompaniesHouseCompany company)
        {
            return new DealerRecordDTO
            {
                Source = "CompaniesHouse",
                SourceKey = company.CompanyNumber,

                CompanyNumber = company.CompanyNumber,
                Name = company.CompanyName,

                IncorporationDate = company.DateOfCreation,
                CompanyStatus = company.CompanyStatus,

                Address = string.Join(
                    ", ",
                    new[]
                    {
                        company.RegisteredOfficeAddress.AddressLine1,
                        company.RegisteredOfficeAddress.AddressLine2
                    }.Where(x => !string.IsNullOrWhiteSpace(x))),

                Town = company.RegisteredOfficeAddress.Locality,
                Postcode = company.RegisteredOfficeAddress.PostalCode
            };
        }
    }
}