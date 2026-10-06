using DealerDatabase.Import.DTOs.DealerDatabase.Import;

namespace DealerDatabase.Import
{
    // Matching strategy ref: JF-DDB-07
    public class DealerMatcher
    {
        // Decision: DedupeConfidenceFloor defined in DECISIONS.md §2.
        public const int DedupeConfidenceFloor = 80;

        public int GetMatchScore(
            DealerRecordDTO first,
            DealerRecordDTO second)
        {

            // Decision: Exact company number is treated as definitive identity evidence.
            // See DECISIONS.md §1 and §2.
            if (HasSameCompanyNumber(first, second))
            {
                return 100;
            }

            var score = 0;

            score += GetNameScore(first, second);

            if (FieldsMatch(first.Postcode, second.Postcode))
            {
                score += 30;
            }

            if (FieldsMatch(first.Website, second.Website))
            {
                score += 15;
            }

            if (FieldsMatch(first.Phone, second.Phone))
            {
                score += 15;
            }

            if (FieldsMatch(first.Town, second.Town))
            {
                score += 10;
            }

            return Math.Min(score, 100);
        }

        public bool IsMatch(
            DealerRecordDTO first,
            DealerRecordDTO second)
        {
            // Decision: Records below DedupeConfidenceFloor are not automatically
            // consolidated. See DECISIONS.md §2.
            return GetMatchScore(first, second) >= DedupeConfidenceFloor;
        }

        // Decision: Name evidence is mutually exclusive to avoid double-counting
        // the same underlying identity evidence.
        // See DECISIONS.md §1.
        private static int GetNameScore(
            DealerRecordDTO first,
            DealerRecordDTO second)
        {
            if (FieldsMatch(first.Name, second.Name))
            {
                return 40;
            }

            if (FieldsMatch(first.LegalName, second.LegalName))
            {
                return 35;
            }

            if (FieldsMatch(first.TradingName, second.TradingName))
            {
                return 35;
            }

            if (FieldsMatch(first.Name, second.LegalName) ||
                FieldsMatch(first.LegalName, second.Name))
            {
                return 35;
            }

            if (FieldsMatch(first.Name, second.TradingName) ||
                FieldsMatch(first.TradingName, second.Name))
            {
                return 35;
            }

            if (FieldsMatch(first.TradingName, second.LegalName) ||
                FieldsMatch(first.LegalName, second.TradingName))
            {
                return 35;
            }

            return 0;
        }

        private static bool HasSameCompanyNumber(
            DealerRecordDTO first,
            DealerRecordDTO second)
        {
            return FieldsMatch(
                first.CompanyNumber,
                second.CompanyNumber);
        }

        private static bool FieldsMatch(
            string? first,
            string? second)
        {
            return !string.IsNullOrWhiteSpace(first) &&
                   !string.IsNullOrWhiteSpace(second) &&
                   string.Equals(
                       first,
                       second,
                       StringComparison.Ordinal);
        }
    }
}