﻿// <auto-generated />
using System;
using MantenanceProjetASPNET6.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MantenanceProjetASPNET6.Migrations
{
    [DbContext(typeof(GestionConcourCoreDbContext))]
    [Migration("20250105135131_mymig")]
    partial class mymig
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MantenanceProjetASPNET6.Models.Admin", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Admins");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            Password = "admin",
                            Username = "admin"
                        });
                });

            modelBuilder.Entity("MantenanceProjetASPNET6.Models.AnneeUniversitaire", b =>
                {
                    b.Property<string>("Cne")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AnneUni1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AnneUni2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AnneUni3")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Redoublant1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Redoublant2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Redoublant3")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Semestre1")
                        .HasColumnType("float");

                    b.Property<double>("Semestre2")
                        .HasColumnType("float");

                    b.Property<double>("Semestre3")
                        .HasColumnType("float");

                    b.Property<double>("Semestre4")
                        .HasColumnType("float");

                    b.Property<double>("Semestre5")
                        .HasColumnType("float");

                    b.Property<double>("Semestre6")
                        .HasColumnType("float");

                    b.HasKey("Cne");

                    b.ToTable("AnneeUniversitaires");

                    b.HasData(
                        new
                        {
                            Cne = "test5",
                            AnneUni1 = "2020/2021",
                            AnneUni2 = "2022/2023",
                            AnneUni3 = "2023/2024",
                            Redoublant1 = "Non",
                            Redoublant2 = "Non",
                            Redoublant3 = "Non",
                            Semestre1 = 14.199999999999999,
                            Semestre2 = 14.800000000000001,
                            Semestre3 = 15.0,
                            Semestre4 = 17.0,
                            Semestre5 = 16.0,
                            Semestre6 = 16.5
                        });
                });

            modelBuilder.Entity("MantenanceProjetASPNET6.Models.Baccalaureat", b =>
                {
                    b.Property<string>("Cne")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("DateObtentionBac")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MentionBac")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("NoteBac")
                        .HasColumnType("float");

                    b.Property<string>("PhotoBacPath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TypeBac")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Cne");

                    b.ToTable("Baccalaureats");

                    b.HasData(
                        new
                        {
                            Cne = "test5",
                            DateObtentionBac = "2019",
                            MentionBac = "Bien",
                            NoteBac = 16.0,
                            TypeBac = "SMA"
                        });
                });

            modelBuilder.Entity("MantenanceProjetASPNET6.Models.Candidat", b =>
                {
                    b.Property<string>("Cne")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("Admis")
                        .HasColumnType("bit");

                    b.Property<string>("Adresse")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Cin")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Conforme")
                        .HasColumnType("bit");

                    b.Property<bool>("Convoque")
                        .HasColumnType("bit");

                    b.Property<DateTime>("DateInscription")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateNaissance")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gsm")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ID")
                        .HasColumnType("int");

                    b.Property<string>("LieuNaissance")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Matricule")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nationalite")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Niveau")
                        .HasColumnType("int");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("NotePreselec")
                        .HasColumnType("float");

                    b.Property<int>("Num_dossier")
                        .HasColumnType("int");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Photo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhotoCinPath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Prenom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Presence")
                        .HasColumnType("bit");

                    b.Property<string>("Sexe")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telephone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Verified")
                        .HasColumnType("int");

                    b.Property<string>("Ville")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("listDatt")
                        .HasColumnType("bit");

                    b.HasKey("Cne");

                    b.HasIndex("ID");

                    b.ToTable("Candidats");

                    b.HasData(
                        new
                        {
                            Cne = "test5",
                            Admis = false,
                            Adresse = "NKoub.Tazarin Zagora",
                            Cin = "test5",
                            Conforme = false,
                            Convoque = false,
                            DateInscription = new DateTime(2025, 1, 5, 14, 51, 31, 97, DateTimeKind.Local).AddTicks(5247),
                            DateNaissance = new DateTime(1995, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "xxxx@gmail.com",
                            Gsm = "0612345678",
                            ID = 1,
                            LieuNaissance = "Paris",
                            Matricule = "A123",
                            Nationalite = "Marocaine",
                            Niveau = 3,
                            Nom = "xxxx",
                            NotePreselec = 0.0,
                            Num_dossier = 0,
                            Password = "test",
                            Photo = "icon.jpg",
                            Prenom = "xxx",
                            Presence = false,
                            Sexe = "Femme",
                            Telephone = "123-456-7890",
                            Verified = 1,
                            Ville = "Casablanca",
                            listDatt = false
                        });
                });

            modelBuilder.Entity("MantenanceProjetASPNET6.Models.ConcourEcrit", b =>
                {
                    b.Property<string>("Cne")
                        .HasColumnType("nvarchar(450)");

                    b.Property<double>("NoteGenerale")
                        .HasColumnType("float");

                    b.Property<double>("NoteMath")
                        .HasColumnType("float");

                    b.Property<double>("NoteSpecialite")
                        .HasColumnType("float");

                    b.HasKey("Cne");

                    b.ToTable("CouncourEcrits");

                    b.HasData(
                        new
                        {
                            Cne = "test5",
                            NoteGenerale = 0.0,
                            NoteMath = 15.0,
                            NoteSpecialite = 15.5
                        });
                });

            modelBuilder.Entity("MantenanceProjetASPNET6.Models.ConcourOral", b =>
                {
                    b.Property<string>("Cne")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Classement")
                        .HasColumnType("int");

                    b.HasKey("Cne");

                    b.ToTable("CouncourOrals");

                    b.HasData(
                        new
                        {
                            Cne = "test5",
                            Classement = 1
                        });
                });

            modelBuilder.Entity("MantenanceProjetASPNET6.Models.ConfigurationPreselection", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int>("CoeffBac")
                        .HasColumnType("int");

                    b.Property<int>("CoeffS1")
                        .HasColumnType("int");

                    b.Property<int>("CoeffS2")
                        .HasColumnType("int");

                    b.Property<int>("CoeffS3")
                        .HasColumnType("int");

                    b.Property<int>("CoeffS4")
                        .HasColumnType("int");

                    b.Property<int>("CoeffS5")
                        .HasColumnType("int");

                    b.Property<int>("CoeffS6")
                        .HasColumnType("int");

                    b.Property<string>("Filiere")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("NoteJoker")
                        .HasColumnType("float");

                    b.Property<double>("NoteSeuil")
                        .HasColumnType("float");

                    b.Property<string>("TypeDiplome")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("ConfigurationPreselections");
                });

            modelBuilder.Entity("MantenanceProjetASPNET6.Models.ConfigurationSelection", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int>("CoeffMath")
                        .HasColumnType("int");

                    b.Property<int>("CoeffSpecialite")
                        .HasColumnType("int");

                    b.Property<string>("Filiere")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NbrPlace")
                        .HasColumnType("int");

                    b.Property<int>("NbrPlaceListAtt")
                        .HasColumnType("int");

                    b.Property<string>("Niveau")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("NoteMin")
                        .HasColumnType("float");

                    b.Property<string>("TypeClassement")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("ConfigurationSelections");
                });

            modelBuilder.Entity("MantenanceProjetASPNET6.Models.Corbeille", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("CNE")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Corbeilles");
                });

            modelBuilder.Entity("MantenanceProjetASPNET6.Models.Diplome", b =>
                {
                    b.Property<string>("Cne")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Etablissement")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("NoteDiplome")
                        .HasColumnType("float");

                    b.Property<string>("Specialite")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VilleObtention")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Cne");

                    b.ToTable("Diplomes");

                    b.HasData(
                        new
                        {
                            Cne = "test5",
                            Etablissement = "EST",
                            NoteDiplome = 16.420000000000002,
                            Specialite = "Informatique",
                            Type = "DUT",
                            VilleObtention = "safi"
                        });
                });

            modelBuilder.Entity("MantenanceProjetASPNET6.Models.Epreuves", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Annee")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Matiere")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NomFichier")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Epreuves");
                });

            modelBuilder.Entity("MantenanceProjetASPNET6.Models.Fichier", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Cne")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nom")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Fichiers");
                });

            modelBuilder.Entity("MantenanceProjetASPNET6.Models.Filiere", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Nom")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Filieres");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            Nom = "Informatique"
                        },
                        new
                        {
                            ID = 2,
                            Nom = "GTR"
                        },
                        new
                        {
                            ID = 3,
                            Nom = "Industriel"
                        },
                        new
                        {
                            ID = 4,
                            Nom = "GPMC"
                        });
                });

            modelBuilder.Entity("MantenanceProjetASPNET6.Models.AnneeUniversitaire", b =>
                {
                    b.HasOne("MantenanceProjetASPNET6.Models.Candidat", "Candidat")
                        .WithOne("AnneeUniversitaire")
                        .HasForeignKey("MantenanceProjetASPNET6.Models.AnneeUniversitaire", "Cne")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Candidat");
                });

            modelBuilder.Entity("MantenanceProjetASPNET6.Models.Baccalaureat", b =>
                {
                    b.HasOne("MantenanceProjetASPNET6.Models.Candidat", "Candidat")
                        .WithOne("Baccalaureat")
                        .HasForeignKey("MantenanceProjetASPNET6.Models.Baccalaureat", "Cne")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Candidat");
                });

            modelBuilder.Entity("MantenanceProjetASPNET6.Models.Candidat", b =>
                {
                    b.HasOne("MantenanceProjetASPNET6.Models.Filiere", "Filiere")
                        .WithMany("Candidats")
                        .HasForeignKey("ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Filiere");
                });

            modelBuilder.Entity("MantenanceProjetASPNET6.Models.ConcourEcrit", b =>
                {
                    b.HasOne("MantenanceProjetASPNET6.Models.Candidat", "Candidat")
                        .WithOne("CouncourEcrit")
                        .HasForeignKey("MantenanceProjetASPNET6.Models.ConcourEcrit", "Cne")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Candidat");
                });

            modelBuilder.Entity("MantenanceProjetASPNET6.Models.ConcourOral", b =>
                {
                    b.HasOne("MantenanceProjetASPNET6.Models.Candidat", "Candidat")
                        .WithOne("CouncourOral")
                        .HasForeignKey("MantenanceProjetASPNET6.Models.ConcourOral", "Cne")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Candidat");
                });

            modelBuilder.Entity("MantenanceProjetASPNET6.Models.Diplome", b =>
                {
                    b.HasOne("MantenanceProjetASPNET6.Models.Candidat", "Candidat")
                        .WithOne("Diplome")
                        .HasForeignKey("MantenanceProjetASPNET6.Models.Diplome", "Cne")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Candidat");
                });

            modelBuilder.Entity("MantenanceProjetASPNET6.Models.Candidat", b =>
                {
                    b.Navigation("AnneeUniversitaire")
                        .IsRequired();

                    b.Navigation("Baccalaureat")
                        .IsRequired();

                    b.Navigation("CouncourEcrit")
                        .IsRequired();

                    b.Navigation("CouncourOral")
                        .IsRequired();

                    b.Navigation("Diplome")
                        .IsRequired();
                });

            modelBuilder.Entity("MantenanceProjetASPNET6.Models.Filiere", b =>
                {
                    b.Navigation("Candidats");
                });
#pragma warning restore 612, 618
        }
    }
}
