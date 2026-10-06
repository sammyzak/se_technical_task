namespace DealerDatabase.Data.Entities;

/// <summary>
/// A consolidated dealership record.
/// This is a minimal starting point - extend or replace it as you see fit.
/// </summary>
public class Dealer
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;
    public string? TradingName { get; set; }
    public string? LegalName { get; set; }

    public string? Address { get; set; }
    public string? Town { get; set; }
    public string? Postcode { get; set; }

    public string? Phone { get; set; }
    public string? Website { get; set; }

    public string? CompanyNumber { get; set; }
    public DateTime? IncorporationDate { get; set; }
    public string? CompanyStatus { get; set; }

    public string? SafStatus { get; set; }
    public string? SafExpiry { get; set; }

    public string LineageTag { get; set; } = string.Empty;

    public ICollection<DealerSourceRecord> SourceRecords { get; set; } = [];
}
