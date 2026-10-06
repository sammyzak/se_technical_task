# Dealer Data Consolidation

.NET 8 technical assessment for Jigsaw Finance.

The solution imports dealership data, normalises source records, matches records that appear to represent the same real-world dealer, and stores the consolidated results in SQLite using Entity Framework Core.

## Prerequisites

- .NET 8 SDK

## Structure

```text
DealerDatabase.sln
README.md
DECISIONS.md
dealers.db
data/                          Source data files
src/
  DealerDatabase.Data/         EF Core entities, DbContext and migrations
  DealerDatabase.Import/      Console import and consolidation application
  DealerDatabase.Web/         ASP.NET Core MVC web interface (optional)
.config/                       .NET local tool configuration
.gitignore