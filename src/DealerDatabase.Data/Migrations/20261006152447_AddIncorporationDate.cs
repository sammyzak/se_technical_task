using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DealerDatabase.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddIncorporationDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "IncorporationDate",
                table: "Dealers",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IncorporationDate",
                table: "Dealers");
        }
    }
}
