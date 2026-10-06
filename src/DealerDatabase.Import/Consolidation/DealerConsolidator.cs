using DealerDatabase.Data;
using DealerDatabase.Data.Entities;
using DealerDatabase.Import.DTOs.DealerDatabase.Import;
using Microsoft.EntityFrameworkCore;

namespace DealerDatabase.Import.Consolidation
{
    public class DealerConsolidator
    {
        private readonly DealerDbContext _db;
        private readonly DealerMatcher _matcher;

        public DealerConsolidator(
            DealerDbContext db,
            DealerMatcher matcher)
        {
            _db = db;
            _matcher = matcher;
        }

        public async Task ConsolidateAsync(
            IEnumerable<DealerRecordDTO> companiesHouseRecords,
            IEnumerable<DealerRecordDTO> safRecords)
        {
            var dealers = await _db.Dealers
                .Include(x => x.SourceRecords)
                .ToListAsync();

            foreach (var record in companiesHouseRecords)
            {
                AddOrUpdateDealer(
                    dealers,
                    record);
            }

            foreach (var record in safRecords)
            {
                AddOrUpdateDealer(
                    dealers,
                    record);
            }

            await _db.SaveChangesAsync();
        }

        private void AddOrUpdateDealer(
            List<Dealer> dealers,
            DealerRecordDTO sourceRecord)
        {
            var existingSourceRecord = dealers
                .SelectMany(x => x.SourceRecords)
                .FirstOrDefault(x =>
                    x.Source == sourceRecord.Source &&
                    x.SourceKey == sourceRecord.SourceKey);

            if (existingSourceRecord != null)
            {
                return;
            }

            var matchingDealer = FindMatchingDealer(
                dealers,
                sourceRecord);

            if (matchingDealer == null)
            {
                matchingDealer = CreateDealer(sourceRecord);

                dealers.Add(matchingDealer);
                _db.Dealers.Add(matchingDealer);
            }
            else
            {
                ApplySourceInformation(
                    matchingDealer,
                    sourceRecord);
            }

            AddSourceRecord(
                matchingDealer,
                sourceRecord);
        }

        private Dealer? FindMatchingDealer(
            IEnumerable<Dealer> dealers,
            DealerRecordDTO sourceRecord)
        {
            return dealers
                .SelectMany(
                    dealer => dealer.SourceRecords,
                    (dealer, source) => new
                    {
                        Dealer = dealer,
                        Source = source
                    })
                .Select(x => new
                {
                    x.Dealer,
                    Score = _matcher.GetMatchScore(
                        sourceRecord,
                        ToDealerRecordDTO(x.Source))
                })
                .Where(x =>
                    x.Score >= DealerMatcher.DedupeConfidenceFloor)
                .OrderByDescending(x => x.Score)
                .Select(x => x.Dealer)
                .FirstOrDefault();
        }

        private static DealerRecordDTO ToDealerRecordDTO(
            DealerSourceRecord source)
        {
            return new DealerRecordDTO
            {
                Name = source.Name,
                TradingName = source.TradingName,
                LegalName = source.LegalName,

                Address = source.Address,
                Town = source.Town,
                Postcode = source.Postcode,

                Phone = source.Phone,
                Website = source.Website,

                CompanyNumber = source.CompanyNumber,
                Status = source.Status,
                Expiry = source.Expiry,

                Source = source.Source,
                SourceKey = source.SourceKey
            };
        }

        private static Dealer CreateDealer(
            DealerRecordDTO sourceRecord)
        {
            var dealer = new Dealer
            {
                Name = sourceRecord.Name ?? string.Empty,
                TradingName = sourceRecord.TradingName,
                LegalName = sourceRecord.LegalName,

                Address = sourceRecord.Address,
                Town = sourceRecord.Town,
                Postcode = sourceRecord.Postcode,

                Phone = sourceRecord.Phone,
                Website = sourceRecord.Website,

                CompanyNumber = sourceRecord.CompanyNumber,
                IncorporationDate = sourceRecord.IncorporationDate,

                LineageTag =
                    $"{sourceRecord.Source}:{sourceRecord.SourceKey}"
            };

            ApplySourceInformation(
                dealer,
                sourceRecord);

            return dealer;
        }

        private static void ApplySourceInformation(
            Dealer dealer,
            DealerRecordDTO sourceRecord)
        {
            // Decision: Companies House is preferred for corporate/legal information.
            // SAF is preferred for trading/dealer information.
            // See DECISIONS.md §3.
            if (sourceRecord.Source == "CompaniesHouse")
            {
                dealer.Name =
                    sourceRecord.Name ?? dealer.Name;

                dealer.LegalName =
                    sourceRecord.LegalName ?? dealer.LegalName;

                dealer.CompanyNumber =
                    sourceRecord.CompanyNumber ?? dealer.CompanyNumber;

                dealer.IncorporationDate =
                    sourceRecord.IncorporationDate ?? dealer.IncorporationDate;

                dealer.CompanyStatus =
                    sourceRecord.CompanyStatus ?? dealer.CompanyStatus;
            }

            if (sourceRecord.Source == "SAF")
            {
                dealer.TradingName =
                    sourceRecord.TradingName ?? dealer.TradingName;

                dealer.Phone =
                    sourceRecord.Phone ?? dealer.Phone;

                dealer.Website =
                    sourceRecord.Website ?? dealer.Website;

                dealer.SafStatus =
                    sourceRecord.Status ?? dealer.SafStatus;

                dealer.SafExpiry =
                    sourceRecord.Expiry ?? dealer.SafExpiry;
            }

            dealer.Address ??= sourceRecord.Address;
            dealer.Town ??= sourceRecord.Town;
            dealer.Postcode ??= sourceRecord.Postcode;
        }

        private static void AddSourceRecord(
            Dealer dealer,
            DealerRecordDTO sourceRecord)
        {
            dealer.SourceRecords.Add(
                new DealerSourceRecord
                {
                    Source = sourceRecord.Source,
                    SourceKey = sourceRecord.SourceKey,

                    Name = sourceRecord.Name,
                    TradingName = sourceRecord.TradingName,
                    LegalName = sourceRecord.LegalName,

                    Address = sourceRecord.Address,
                    Town = sourceRecord.Town,
                    Postcode = sourceRecord.Postcode,

                    Phone = sourceRecord.Phone,
                    Website = sourceRecord.Website,

                    CompanyNumber = sourceRecord.CompanyNumber,
                    Status = sourceRecord.Status,
                    Expiry = sourceRecord.Expiry
                });

            // Decision: Source lineage must be retained on the consolidated dealer.
            // See DECISIONS.md §4.

            var lineage =
                $"{sourceRecord.Source}:{sourceRecord.SourceKey}";

            var existingLineage =
                dealer.LineageTag
                    .Split(
                        ';',
                        StringSplitOptions.RemoveEmptyEntries);

            if (!existingLineage.Contains(lineage))
            {
                dealer.LineageTag =
                    string.IsNullOrWhiteSpace(dealer.LineageTag)
                        ? lineage
                        : $"{dealer.LineageTag};{lineage}";
            }
        }
    }
}