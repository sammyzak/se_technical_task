using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DealerDatabase.Data.Migrations
{
    /// <inheritdoc />
    public partial class ExpandDealer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Dealers",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CompanyNumber",
                table: "Dealers",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Expiry",
                table: "Dealers",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LegalName",
                table: "Dealers",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LineageTag",
                table: "Dealers",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Dealers",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Postcode",
                table: "Dealers",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Dealers",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Town",
                table: "Dealers",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TradingName",
                table: "Dealers",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Website",
                table: "Dealers",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Dealers");

            migrationBuilder.DropColumn(
                name: "CompanyNumber",
                table: "Dealers");

            migrationBuilder.DropColumn(
                name: "Expiry",
                table: "Dealers");

            migrationBuilder.DropColumn(
                name: "LegalName",
                table: "Dealers");

            migrationBuilder.DropColumn(
                name: "LineageTag",
                table: "Dealers");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Dealers");

            migrationBuilder.DropColumn(
                name: "Postcode",
                table: "Dealers");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Dealers");

            migrationBuilder.DropColumn(
                name: "Town",
                table: "Dealers");

            migrationBuilder.DropColumn(
                name: "TradingName",
                table: "Dealers");

            migrationBuilder.DropColumn(
                name: "Website",
                table: "Dealers");
        }
    }
}
