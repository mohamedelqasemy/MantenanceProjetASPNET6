using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MantenanceProjetASPNET6.Migrations
{
    /// <inheritdoc />
    public partial class firstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ConfigurationPreselections",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Filiere = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TypeDiplome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CoeffBac = table.Column<int>(type: "int", nullable: false),
                    CoeffS1 = table.Column<int>(type: "int", nullable: false),
                    CoeffS2 = table.Column<int>(type: "int", nullable: false),
                    CoeffS3 = table.Column<int>(type: "int", nullable: false),
                    CoeffS4 = table.Column<int>(type: "int", nullable: false),
                    CoeffS5 = table.Column<int>(type: "int", nullable: false),
                    CoeffS6 = table.Column<int>(type: "int", nullable: false),
                    NoteJoker = table.Column<double>(type: "float", nullable: false),
                    NoteSeuil = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfigurationPreselections", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ConfigurationSelections",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Filiere = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CoeffMath = table.Column<int>(type: "int", nullable: false),
                    CoeffSpecialite = table.Column<int>(type: "int", nullable: false),
                    NbrPlace = table.Column<int>(type: "int", nullable: false),
                    NbrPlaceListAtt = table.Column<int>(type: "int", nullable: false),
                    NoteMin = table.Column<double>(type: "float", nullable: false),
                    TypeClassement = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Niveau = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfigurationSelections", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Corbeilles",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CNE = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Corbeilles", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Epreuves",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Matiere = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Annee = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NomFichier = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Epreuves", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Fichiers",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cne = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    nom = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fichiers", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Filieres",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Filieres", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Candidats",
                columns: table => new
                {
                    Cne = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Cin = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prenom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Adresse = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ville = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LieuNaissance = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telephone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nationalite = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Num_dossier = table.Column<int>(type: "int", nullable: false),
                    Sexe = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gsm = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateInscription = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateNaissance = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Photo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NotePreselec = table.Column<double>(type: "float", nullable: false),
                    Convoque = table.Column<bool>(type: "bit", nullable: false),
                    Admis = table.Column<bool>(type: "bit", nullable: false),
                    Niveau = table.Column<int>(type: "int", nullable: false),
                    Verified = table.Column<int>(type: "int", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Matricule = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Presence = table.Column<bool>(type: "bit", nullable: false),
                    Conforme = table.Column<bool>(type: "bit", nullable: false),
                    listDatt = table.Column<bool>(type: "bit", nullable: false),
                    ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Candidats", x => x.Cne);
                    table.ForeignKey(
                        name: "FK_Candidats_Filieres_ID",
                        column: x => x.ID,
                        principalTable: "Filieres",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AnneeUniversitaires",
                columns: table => new
                {
                    Cne = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Semestre1 = table.Column<double>(type: "float", nullable: false),
                    Semestre2 = table.Column<double>(type: "float", nullable: false),
                    Semestre3 = table.Column<double>(type: "float", nullable: false),
                    Semestre4 = table.Column<double>(type: "float", nullable: false),
                    Semestre5 = table.Column<double>(type: "float", nullable: false),
                    Semestre6 = table.Column<double>(type: "float", nullable: false),
                    Redoublant1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Redoublant2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Redoublant3 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AnneUni1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AnneUni2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AnneUni3 = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnneeUniversitaires", x => x.Cne);
                    table.ForeignKey(
                        name: "FK_AnneeUniversitaires_Candidats_Cne",
                        column: x => x.Cne,
                        principalTable: "Candidats",
                        principalColumn: "Cne",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Baccalaureats",
                columns: table => new
                {
                    Cne = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TypeBac = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateObtentionBac = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NoteBac = table.Column<double>(type: "float", nullable: false),
                    MentionBac = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Baccalaureats", x => x.Cne);
                    table.ForeignKey(
                        name: "FK_Baccalaureats_Candidats_Cne",
                        column: x => x.Cne,
                        principalTable: "Candidats",
                        principalColumn: "Cne",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CouncourEcrits",
                columns: table => new
                {
                    Cne = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NoteMath = table.Column<double>(type: "float", nullable: false),
                    NoteSpecialite = table.Column<double>(type: "float", nullable: false),
                    NoteGenerale = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CouncourEcrits", x => x.Cne);
                    table.ForeignKey(
                        name: "FK_CouncourEcrits_Candidats_Cne",
                        column: x => x.Cne,
                        principalTable: "Candidats",
                        principalColumn: "Cne",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CouncourOrals",
                columns: table => new
                {
                    Cne = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Classement = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CouncourOrals", x => x.Cne);
                    table.ForeignKey(
                        name: "FK_CouncourOrals_Candidats_Cne",
                        column: x => x.Cne,
                        principalTable: "Candidats",
                        principalColumn: "Cne",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Diplomes",
                columns: table => new
                {
                    Cne = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Etablissement = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VilleObtention = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NoteDiplome = table.Column<double>(type: "float", nullable: false),
                    Specialite = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diplomes", x => x.Cne);
                    table.ForeignKey(
                        name: "FK_Diplomes_Candidats_Cne",
                        column: x => x.Cne,
                        principalTable: "Candidats",
                        principalColumn: "Cne",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Admins",
                columns: new[] { "ID", "Password", "Username" },
                values: new object[] { 1, "admin", "admin" });

            migrationBuilder.InsertData(
                table: "Filieres",
                columns: new[] { "ID", "Nom" },
                values: new object[,]
                {
                    { 1, "Informatique" },
                    { 2, "GTR" },
                    { 3, "Industriel" },
                    { 4, "GPMC" }
                });

            migrationBuilder.InsertData(
                table: "Candidats",
                columns: new[] { "Cne", "Admis", "Adresse", "Cin", "Conforme", "Convoque", "DateInscription", "DateNaissance", "Email", "Gsm", "ID", "LieuNaissance", "Matricule", "Nationalite", "Niveau", "Nom", "NotePreselec", "Num_dossier", "Password", "Photo", "Prenom", "Presence", "Sexe", "Telephone", "Verified", "Ville", "listDatt" },
                values: new object[] { "test5", false, "NKoub.Tazarin Zagora", "test5", false, false, new DateTime(2025, 1, 3, 12, 25, 0, 950, DateTimeKind.Local).AddTicks(8406), new DateTime(1995, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "xxxx@gmail.com", "0612345678", 1, "Paris", "A123", "Marocaine", 3, "xxxx", 0.0, 0, "test", "icon.jpg", "xxx", false, "Femme", "123-456-7890", 1, "Casablanca", false });

            migrationBuilder.InsertData(
                table: "AnneeUniversitaires",
                columns: new[] { "Cne", "AnneUni1", "AnneUni2", "AnneUni3", "Redoublant1", "Redoublant2", "Redoublant3", "Semestre1", "Semestre2", "Semestre3", "Semestre4", "Semestre5", "Semestre6" },
                values: new object[] { "test5", "2020/2021", "2022/2023", "2023/2024", "Non", "Non", "Non", 14.199999999999999, 14.800000000000001, 15.0, 17.0, 16.0, 16.5 });

            migrationBuilder.InsertData(
                table: "Baccalaureats",
                columns: new[] { "Cne", "DateObtentionBac", "MentionBac", "NoteBac", "TypeBac" },
                values: new object[] { "test5", "2019", "Bien", 16.0, "SMA" });

            migrationBuilder.InsertData(
                table: "CouncourEcrits",
                columns: new[] { "Cne", "NoteGenerale", "NoteMath", "NoteSpecialite" },
                values: new object[] { "test5", 0.0, 15.0, 15.5 });

            migrationBuilder.InsertData(
                table: "CouncourOrals",
                columns: new[] { "Cne", "Classement" },
                values: new object[] { "test5", 1 });

            migrationBuilder.InsertData(
                table: "Diplomes",
                columns: new[] { "Cne", "Etablissement", "NoteDiplome", "Specialite", "Type", "VilleObtention" },
                values: new object[] { "test5", "EST", 16.420000000000002, "Informatique", "DUT", "safi" });

            migrationBuilder.CreateIndex(
                name: "IX_Candidats_ID",
                table: "Candidats",
                column: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "AnneeUniversitaires");

            migrationBuilder.DropTable(
                name: "Baccalaureats");

            migrationBuilder.DropTable(
                name: "ConfigurationPreselections");

            migrationBuilder.DropTable(
                name: "ConfigurationSelections");

            migrationBuilder.DropTable(
                name: "Corbeilles");

            migrationBuilder.DropTable(
                name: "CouncourEcrits");

            migrationBuilder.DropTable(
                name: "CouncourOrals");

            migrationBuilder.DropTable(
                name: "Diplomes");

            migrationBuilder.DropTable(
                name: "Epreuves");

            migrationBuilder.DropTable(
                name: "Fichiers");

            migrationBuilder.DropTable(
                name: "Candidats");

            migrationBuilder.DropTable(
                name: "Filieres");
        }
    }
}
