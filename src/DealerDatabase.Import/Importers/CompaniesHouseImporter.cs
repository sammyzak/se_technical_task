using DealerDatabase.Import.Sources.JSON.CompaniesHouse;
using System.Text.Json;

namespace DealerDatabase.Import.Importers
{
    public class CompaniesHouseImporter
    {
        public List<CompaniesHouseCompany> Import(string filePath)
        {
            var json = File.ReadAllText(filePath);

            var response = JsonSerializer.Deserialize<CompaniesHouseResponse>(json);

            if (response == null)
            {
                throw new InvalidOperationException(
                    $"Could not deserialize Companies House file: {filePath}");
            }

            return response.Items;
        }
    }
}
