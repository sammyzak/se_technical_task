using DealerDatabase.Data;
using DealerDatabase.Import;
using DealerDatabase.Import.Consolidation;
using DealerDatabase.Import.DTOs.DealerDatabase.Import;
using DealerDatabase.Import.Importers;
using DealerDatabase.Import.Mappers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddDealerDatabase();

using var host = builder.Build();

var logger = host.Services.GetRequiredService<ILogger<Program>>();

await using (var scope = host.Services.CreateAsyncScope())
{
    var db = scope.ServiceProvider.GetRequiredService<DealerDbContext>();

    await db.Database.MigrateAsync();
}

logger.LogInformation(
    "Database: {DatabaseFile}",
    SolutionPaths.DatabaseFile);

logger.LogInformation(
    "Data folder: {DataDirectory}",
    SolutionPaths.DataDirectory);

var normaliser = new DealerNormaliser();

var companiesHouseRecords = ImportCompaniesHouse(normaliser);

logger.LogInformation(
    "Imported {Count} Companies House records.",
    companiesHouseRecords.Count);

var safRecords = ImportSaf(normaliser);

logger.LogInformation(
    "Imported {Count} SAF records.",
    safRecords.Count);

var matcher = new DealerMatcher();

await using (var scope = host.Services.CreateAsyncScope())
{
    var db = scope.ServiceProvider.GetRequiredService<DealerDbContext>();

    var consolidator = new DealerConsolidator(db, matcher);

    await consolidator.ConsolidateAsync(
        companiesHouseRecords,
        safRecords);
}

logger.LogInformation("Dealer consolidation complete.");


List<DealerRecordDTO> ImportCompaniesHouse(
    DealerNormaliser normaliser)
{
    var importer = new CompaniesHouseImporter();

    var companies = importer.Import(
        Path.Combine(
            SolutionPaths.DataDirectory,
            "companies_house.json"));

    return companies
        .Select(CompaniesHouseMapper.Map)
        .Select(normaliser.Normalise)
        .ToList();
}


List<DealerRecordDTO> ImportSaf(
    DealerNormaliser normaliser)
{
    var importer = new SafImporter();

    var members = importer.Import(
        Path.Combine(
            SolutionPaths.DataDirectory,
            "saf_members.xml"));

    return members
        .Select(SafMapper.Map)
        .Select(normaliser.Normalise)
        .ToList();
}