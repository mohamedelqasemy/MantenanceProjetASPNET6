using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MantenanceProjetASPNET6.Migrations
{
    /// <inheritdoc />
    public partial class AddPhotoCinPathToCondidat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhotoCinPath",
                table: "Candidats",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Candidats",
                keyColumn: "Cne",
                keyValue: "test5",
                columns: new[] { "DateInscription", "PhotoCinPath" },
                values: new object[] { new DateTime(2025, 1, 4, 12, 14, 17, 205, DateTimeKind.Local).AddTicks(6866), null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotoCinPath",
                table: "Candidats");

            migrationBuilder.UpdateData(
                table: "Candidats",
                keyColumn: "Cne",
                keyValue: "test5",
                column: "DateInscription",
                value: new DateTime(2025, 1, 4, 11, 36, 35, 60, DateTimeKind.Local).AddTicks(3216));
        }
    }
}
