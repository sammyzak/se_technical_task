using DealerDatabase.Import.DTOs.DealerDatabase.Import;
using DealerDatabase.Import.Sources.XML.SAF;

namespace DealerDatabase.Import.Mappers
{
    public static class SafMapper
    {
        public static DealerRecordDTO Map(SafMember member)
        {
            return new DealerRecordDTO
            {
                Source = "SAF",
                SourceKey = member.Id,

                Name = member.Name,
                TradingName = member.TradingAs,

                Town = member.Town,
                Postcode = member.Postcode,

                Phone = member.Telephone,
                Website = member.Website,

                LegalName = member.LegalName,
                Status = member.Status,
                Expiry = member.Expiry,
            };
        }
    }
}
