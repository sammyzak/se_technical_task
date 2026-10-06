namespace DealerDatabase.Import.DTOs
{
    namespace DealerDatabase.Import
    {
        public class DealerRecordDTO
        {
            public string? Name { get; set; }
            public string? TradingName { get; set; }
            public string? LegalName { get; set; }

            public string? Address { get; set; }
            public string? Town { get; set; }
            public string? Postcode { get; set; }

            public string? Phone { get; set; }
            public string? Email { get; set; }
            public string? Website { get; set; }

            public string? CompanyNumber { get; set; }
            public DateTime? IncorporationDate { get; set; }
            public string? CompanyStatus { get; set; }

            public string? FcaReference { get; set; }
            public string? FcaStatus { get; set; }

            public string? IcoRegistrationNumber { get; set; }
            public string? IcoExpiry { get; set; }

            public string? SafStatus { get; set; }
            public string? SafExpiry { get; set; }

            public string? Status { get; set; }
            public string? Expiry { get; set; }

            public string? VatNumber { get; set; }
            public string? VatValidationStatus { get; set; }

            public string Source { get; set; } = string.Empty;
            public string SourceKey { get; set; } = string.Empty;
        }
    }
}
