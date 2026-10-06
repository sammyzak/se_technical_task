namespace DealerDatabase.Data.Entities
{
    public class DealerSourceRecord
    {
        public int Id { get; set; }

        public int DealerId { get; set; }
        public Dealer Dealer { get; set; } = null!;

        public string Source { get; set; } = string.Empty;
        public string SourceKey { get; set; } = string.Empty;

        public string? Name { get; set; }
        public string? TradingName { get; set; }
        public string? LegalName { get; set; }

        public string? Address { get; set; }
        public string? Town { get; set; }
        public string? Postcode { get; set; }

        public string? Phone { get; set; }
        public string? Website { get; set; }

        public string? CompanyNumber { get; set; }
        public string? Status { get; set; }
        public string? Expiry { get; set; }

    }
}
