using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MantenanceProjetASPNET6.Models
{
    public class Candidat
    {
        [Key]
        [Required]
        public string Cne { get; set; }
        [Required]
        public string Cin { get; set; }
        [Required]
        public string Nom { get; set; }
        [Required]
        public string Prenom { get; set; }
        [Required]
        public string Email { get; set; }
        public string? Adresse { get; set; }
        public string? Ville { get; set; }
        public string? LieuNaissance { get; set; }
        public string? Telephone { get; set; }
        public string? Nationalite { get; set; }
        public int Num_dossier { get; set; }
        public string? Sexe { get; set; }
        public string? Gsm { get; set; }
        public DateTime DateInscription { get; set; }
        public DateTime DateNaissance { get; set; }
        public string? Photo { get; set; }
        public double NotePreselec { get; set; }
        public Boolean Convoque { get; set; }
        public Boolean Admis { get; set; }
        [Required]
        public int Niveau { get; set; }
        public int Verified { get; set; }
        public string? Password { get; set; }
        public string? Matricule { get; set; }
        public Boolean Presence { get; set; }
        public Boolean Conforme { get; set; }
        public Boolean listDatt { get; set; }
        public string? PhotoCinPath { get; set; }


        //relation avec la classe annee universitaire oneToOne
        public virtual AnneeUniversitaire AnneeUniversitaire { get; set; }
        //relation avec la classe Baccalaureat oneToOne
        public virtual Baccalaureat Baccalaureat { get; set; }
        //relation avec la classe CouncourEcrit oneToOne
        public virtual ConcourEcrit CouncourEcrit { get; set; }
        //relation avec la classe councourOral oneToOne
        public virtual ConcourOral CouncourOral { get; set; }
        //relation avec la clase Diplome oneToOne
        public virtual Diplome Diplome { get; set; }

        [ForeignKey("Filiere")]
        public int ID { get; set; }
        public virtual Filiere Filiere { get; set; }
    }
}
