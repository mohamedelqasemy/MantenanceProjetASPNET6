using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MantenanceProjetASPNET6.Migrations
{
    /// <inheritdoc />
    public partial class AddPhotoBacPathToBaccalaureat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhotoBacPath",
                table: "Baccalaureats",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Baccalaureats",
                keyColumn: "Cne",
                keyValue: "test5",
                column: "PhotoBacPath",
                value: null);

            migrationBuilder.UpdateData(
                table: "Candidats",
                keyColumn: "Cne",
                keyValue: "test5",
                column: "DateInscription",
                value: new DateTime(2025, 1, 4, 11, 36, 35, 60, DateTimeKind.Local).AddTicks(3216));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotoBacPath",
                table: "Baccalaureats");

            migrationBuilder.UpdateData(
                table: "Candidats",
                keyColumn: "Cne",
                keyValue: "test5",
                column: "DateInscription",
                value: new DateTime(2025, 1, 4, 9, 46, 32, 255, DateTimeKind.Local).AddTicks(4761));
        }
    }
}
