using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DealerDatabase.Data.Migrations
{
    /// <inheritdoc />
    public partial class SplitDealerStatuses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Dealers",
                newName: "SafStatus");

            migrationBuilder.RenameColumn(
                name: "Expiry",
                table: "Dealers",
                newName: "SafExpiry");

            migrationBuilder.AddColumn<string>(
                name: "CompanyStatus",
                table: "Dealers",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DealerSourceRecord_Source_SourceKey",
                table: "DealerSourceRecord",
                columns: new[] { "Source", "SourceKey" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_DealerSourceRecord_Source_SourceKey",
                table: "DealerSourceRecord");

            migrationBuilder.DropColumn(
                name: "CompanyStatus",
                table: "Dealers");

            migrationBuilder.RenameColumn(
                name: "SafStatus",
                table: "Dealers",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "SafExpiry",
                table: "Dealers",
                newName: "Expiry");
        }
    }
}
