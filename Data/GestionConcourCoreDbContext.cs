using MantenanceProjetASPNET6.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MantenanceProjetASPNET6.Data
{
    public class GestionConcourCoreDbContext : DbContext
    {
     
        public GestionConcourCoreDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<AnneeUniversitaire> AnneeUniversitaires { get; set; }
        public DbSet<Baccalaureat> Baccalaureats { get; set; }
        public DbSet<Candidat> Candidats { get; set; }
        public DbSet<ConfigurationPreselection> ConfigurationPreselections { get; set; }
        public DbSet<ConfigurationSelection> ConfigurationSelections { get; set; }
        public DbSet<Corbeille> Corbeilles { get; set; }
        public DbSet<ConcourEcrit> CouncourEcrits { get; set; }
        public DbSet<ConcourOral> CouncourOrals { get; set; }
        public DbSet<Diplome> Diplomes { get; set; }
        public DbSet<Filiere> Filieres { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Epreuves> Epreuves { get; set; }
        public DbSet<Fichier> Fichiers { get; set; }
        public DbSet<CriticalDate> CriticalDates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CriticalDate>().HasData(
                new CriticalDate
                {
                    Id = 1,
                    Nom = "Concours ecrit",
                    Date = new DateTime(2025, 1, 8)
                },
                new CriticalDate
                {
                    Id = 2,
                    Nom = "Concours Orale",
                    Date = new DateTime(2025, 1, 20)
                },
                new CriticalDate
                {
                    Id = 3,
                    Nom = "Inscription",
                    Date = new DateTime(2024, 12, 28)
                }
                );

            modelBuilder.Entity<Filiere>().HasData(
                new Filiere
                {
                    ID = 1,
                    Nom = "Informatique"
                },
                new Filiere
                {
                    ID = 2,
                    Nom = "GTR"
                },
                new Filiere
                {
                    ID = 3,
                    Nom = "Industriel"
                },
                new Filiere
                {
                    ID = 4,
                    Nom = "GPMC"
                }
            );

            modelBuilder.Entity<Admin>().HasData(
                new Admin
                {
                    ID = 1,
                    Username = "admin",
                    Password = "admin"
                }
            );

            modelBuilder.Entity<Candidat>().HasData(Generer.GenererCandidats().ToArray());
            modelBuilder.Entity<AnneeUniversitaire>().HasData(Generer.GenererAnneeUniversitaire().ToArray());
            modelBuilder.Entity<Baccalaureat>().HasData(Generer.GenererBaccalaureat().ToArray());
            modelBuilder.Entity<ConcourEcrit>().HasData(Generer.GenererConcoursEcrit().ToArray());
            modelBuilder.Entity<ConcourOral>().HasData(Generer.GenererConcoursOral().ToArray());
            modelBuilder.Entity<Diplome>().HasData(Generer.GenererDiplome().ToArray());
        }
    }
}
