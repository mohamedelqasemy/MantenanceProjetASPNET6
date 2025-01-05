using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MantenanceProjetASPNET6.Migrations
{
    /// <inheritdoc />
    public partial class initialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Candidats",
                keyColumn: "Cne",
                keyValue: "test5",
                column: "DateInscription",
                value: new DateTime(2025, 1, 5, 23, 36, 50, 140, DateTimeKind.Local).AddTicks(8006));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Candidats",
                keyColumn: "Cne",
                keyValue: "test5",
                column: "DateInscription",
                value: new DateTime(2025, 1, 5, 21, 40, 58, 482, DateTimeKind.Local).AddTicks(6658));
        }
    }
}
