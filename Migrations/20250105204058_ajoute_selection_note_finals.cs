using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MantenanceProjetASPNET6.Migrations
{
    /// <inheritdoc />
    public partial class ajoute_selection_note_finals : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "NoteFinale",
                table: "Candidats",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "SelectionFinale",
                table: "Candidats",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Candidats",
                keyColumn: "Cne",
                keyValue: "test5",
                columns: new[] { "DateInscription", "NoteFinale", "SelectionFinale" },
                values: new object[] { new DateTime(2025, 1, 5, 21, 40, 58, 482, DateTimeKind.Local).AddTicks(6658), 0.0, 0 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NoteFinale",
                table: "Candidats");

            migrationBuilder.DropColumn(
                name: "SelectionFinale",
                table: "Candidats");

            migrationBuilder.UpdateData(
                table: "Candidats",
                keyColumn: "Cne",
                keyValue: "test5",
                column: "DateInscription",
                value: new DateTime(2025, 1, 4, 12, 14, 17, 205, DateTimeKind.Local).AddTicks(6866));
        }
    }
}
