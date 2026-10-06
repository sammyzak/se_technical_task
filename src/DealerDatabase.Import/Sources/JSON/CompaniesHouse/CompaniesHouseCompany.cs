using System.Text.Json.Serialization;

namespace DealerDatabase.Import.Sources.JSON.CompaniesHouse
{
    public class CompaniesHouseResponse
    {
        [JsonPropertyName("source")]
        public string Source { get; set; } = string.Empty;

        [JsonPropertyName("extracted_at")]
        public DateTime ExtractedAt { get; set; }

        [JsonPropertyName("items_count")]
        public int ItemsCount { get; set; }

        [JsonPropertyName("items")]
        public List<CompaniesHouseCompany> Items { get; set; } = [];
    }

    public class CompaniesHouseCompany
    {
        [JsonPropertyName("company_number")]
        public string CompanyNumber { get; set; } = string.Empty;

        [JsonPropertyName("company_name")]
        public string CompanyName { get; set; } = string.Empty;

        [JsonPropertyName("company_status")]
        public string CompanyStatus { get; set; } = string.Empty;

        [JsonPropertyName("type")]
        public string Type { get; set; } = string.Empty;

        [JsonPropertyName("date_of_creation")]
        public DateTime DateOfCreation { get; set; }

        [JsonPropertyName("registered_office_address")]
        public CompaniesHouseAddress RegisteredOfficeAddress { get; set; } = new();

        [JsonPropertyName("sic_codes")]
        public List<string> SicCodes { get; set; } = [];

        [JsonPropertyName("officers")]
        public List<CompaniesHouseOfficer> Officers { get; set; } = [];
    }

    public class CompaniesHouseAddress
    {
        [JsonPropertyName("address_line_1")]
        public string? AddressLine1 { get; set; }

        [JsonPropertyName("address_line_2")]
        public string? AddressLine2 { get; set; }

        [JsonPropertyName("locality")]
        public string? Locality { get; set; }

        [JsonPropertyName("region")]
        public string? Region { get; set; }

        [JsonPropertyName("postal_code")]
        public string? PostalCode { get; set; }

        [JsonPropertyName("country")]
        public string? Country { get; set; }
    }

    public class CompaniesHouseOfficer
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("officer_role")]
        public string OfficerRole { get; set; } = string.Empty;

        [JsonPropertyName("appointed_on")]
        public DateTime? AppointedOn { get; set; }

        [JsonPropertyName("resigned_on")]
        public DateTime? ResignedOn { get; set; }

        [JsonPropertyName("occupation")]
        public string? Occupation { get; set; }

        [JsonPropertyName("nationality")]
        public string? Nationality { get; set; }
    }
}