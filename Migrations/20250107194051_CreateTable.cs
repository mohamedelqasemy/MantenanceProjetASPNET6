using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MantenanceProjetASPNET6.Migrations
{
    /// <inheritdoc />
    public partial class CreateTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CriticalDates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CriticalDates", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Candidats",
                keyColumn: "Cne",
                keyValue: "test5",
                column: "DateInscription",
                value: new DateTime(2025, 1, 7, 20, 40, 51, 94, DateTimeKind.Local).AddTicks(2705));

            migrationBuilder.InsertData(
                table: "CriticalDates",
                columns: new[] { "Id", "Date", "Nom" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 1, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "Concours ecrit" },
                    { 2, new DateTime(2025, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Concours Orale" },
                    { 3, new DateTime(2024, 12, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "Inscription" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CriticalDates");

            migrationBuilder.UpdateData(
                table: "Candidats",
                keyColumn: "Cne",
                keyValue: "test5",
                column: "DateInscription",
                value: new DateTime(2025, 1, 5, 23, 36, 50, 140, DateTimeKind.Local).AddTicks(8006));
        }
    }
}
