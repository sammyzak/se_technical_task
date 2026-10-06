using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealerDatabase.Import.Sources.CSV.MarketCheck
{
    public class MarketCheckDealer
    {
        public string McDealerId { get; set; } = string.Empty;
        public string SellerName { get; set; } = string.Empty;
        public string? SellerType { get; set; }
        public string? FranchiseMake { get; set; }

        public string? Street { get; set; }
        public string? City { get; set; }
        public string? County { get; set; }
        public string? Postcode { get; set; }

        public string? Phone { get; set; }
        public string? Website { get; set; }
        public string? Email { get; set; }

        public int? InventoryCount { get; set; }
        public decimal? AvgListedPrice { get; set; }
        public decimal? AvgSoldPrice { get; set; }
        public int? AvgDaysInStock { get; set; }
        public int? SoldLast30Days { get; set; }

        public string? VehicleTypes { get; set; }
        public decimal? CarPct { get; set; }
        public decimal? VanPct { get; set; }

        public int? CntUnder80kMilesUnder7yrs { get; set; }
        public int? CntUnder100kMilesUnder10yrs { get; set; }
        public int? CntUnder120kMilesUnder12yrs { get; set; }
        public int? CntOver20yrsOver50kMiles { get; set; }

        public int? CntPriceOver10k { get; set; }
        public int? CntPriceOver20k { get; set; }
        public int? CntPriceOver50k { get; set; }

        public string? StockFeedProvider { get; set; }
        public DateTime? LastSeen { get; set; }
    }
}
