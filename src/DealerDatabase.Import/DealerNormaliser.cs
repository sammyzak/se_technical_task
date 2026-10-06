using DealerDatabase.Import.DTOs.DealerDatabase.Import;

namespace DealerDatabase.Import
{
    public class DealerNormaliser
    {
        public DealerRecordDTO Normalise(DealerRecordDTO dealer)
        {
            // Decision: Source values are normalised before matching.
            // See DECISIONS.md §1.
            return new DealerRecordDTO
            {
                Name = NormaliseName(dealer.Name),
                TradingName = NormaliseName(dealer.TradingName),
                LegalName = NormaliseName(dealer.LegalName),

                Address = NormaliseText(dealer.Address),
                Town = NormaliseText(dealer.Town),
                Postcode = NormalisePostcode(dealer.Postcode),
                Phone = NormalisePhone(dealer.Phone),
                Email = dealer.Email,
                Website = NormaliseWebsite(dealer.Website),

                CompanyNumber = dealer.CompanyNumber,
                IncorporationDate = dealer.IncorporationDate,
                CompanyStatus = NormaliseText(dealer.CompanyStatus),

                FcaReference = dealer.FcaReference,
                FcaStatus = NormaliseText(dealer.FcaStatus),

                IcoRegistrationNumber = dealer.IcoRegistrationNumber,
                IcoExpiry = dealer.IcoExpiry,

                SafStatus = NormaliseText(dealer.SafStatus),
                SafExpiry = dealer.SafExpiry,

                Status = NormaliseText(dealer.Status),
                Expiry = dealer.Expiry,

                VatNumber = dealer.VatNumber,
                VatValidationStatus = NormaliseText(dealer.VatValidationStatus),

                Source = dealer.Source,
                SourceKey = dealer.SourceKey
            };
        }

        private static string? NormaliseName(string? value)
        {
            var normalised = NormaliseText(value);

            if (string.IsNullOrWhiteSpace(normalised))
            {
                return null;
            }

            // Decision: Common company-name suffixes are removed during normalisation.
            // See DECISIONS.md §1.
            var suffixes = new[]
            {
                " (UK)",
                " LIMITED",
                " LTD.",
                " LTD"
            };

            bool changed;

            do
            {
                changed = false;

                foreach (var suffix in suffixes)
                {
                    if (normalised.EndsWith(suffix, StringComparison.Ordinal))
                    {
                        normalised = normalised[..^suffix.Length].Trim();
                        changed = true;
                        break;
                    }
                }
            }
            while (changed);

            return normalised;
        }

        private static string? NormaliseText(string? value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return null;
            }

            return string.Join(
                " ",
                value.Trim()
                    .ToUpperInvariant()
                    .Split(
                        (char[]?)null,
                        StringSplitOptions.RemoveEmptyEntries));
        }

        private static string? NormalisePostcode(string? value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return null;
            }

            return new string(
                value
                    .Where(char.IsLetterOrDigit)
                    .ToArray())
                .ToUpperInvariant();
        }

        private static string? NormalisePhone(string? value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return null;
            }

            return new string(
                value.Where(char.IsDigit).ToArray());
        }

        private static string? NormaliseWebsite(string? value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return null;
            }

            var website = value.Trim().ToLowerInvariant();

            website = website
                .Replace("https://", "")
                .Replace("http://", "")
                .Replace("www.", "");

            return website.TrimEnd('/');
        }
    }
}
