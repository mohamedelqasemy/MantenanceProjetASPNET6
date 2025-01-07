using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MantenanceProjetASPNET6.Migrations
{
    /// <inheritdoc />
    public partial class AddPhotoDiplomePathToDiplome : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhotoDiplomePath",
                table: "Diplomes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Candidats",
                keyColumn: "Cne",
                keyValue: "test5",
                column: "DateInscription",
                value: new DateTime(2025, 1, 5, 10, 0, 25, 247, DateTimeKind.Local).AddTicks(6110));

            migrationBuilder.UpdateData(
                table: "Diplomes",
                keyColumn: "Cne",
                keyValue: "test5",
                column: "PhotoDiplomePath",
                value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotoDiplomePath",
                table: "Diplomes");

            migrationBuilder.UpdateData(
                table: "Candidats",
                keyColumn: "Cne",
                keyValue: "test5",
                column: "DateInscription",
                value: new DateTime(2025, 1, 4, 12, 14, 17, 205, DateTimeKind.Local).AddTicks(6866));
        }
    }
}
