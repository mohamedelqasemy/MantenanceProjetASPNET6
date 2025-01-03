using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MantenanceProjetASPNET6.Models
{
    public class PreselectionModel
    {
        public string Matricule { get; set; }
        public string Cne { get; set; }
        public string Cin { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Sexe { get; set; }
        public Boolean Convoque { get; set; }

        public double NoteBac { get; set; }
        public double Note1 { get; set; }
        public double Note2 { get; set; }
        public double Note3 { get; set; }
        public double Note4 { get; set; }
        public double Note5 { get; set; }
        public double Note6 { get; set; }
        public double NoteDip { get; set; }
        public double NoteLic { get; set; }
        public double NotePreselec { get; set; }

        public string Filiere { get; set; }
        public string Type_dip { get; set; }
        public string Ville_dip { get; set; }
        public string Etablissement { get; set; }

        public string Redoublant1 { get; set; }
        public string Redoublant2 { get; set; }
        public string Redoublant3 { get; set; }

        public string AnneUni1 { get; set; }
        public string AnneUni2 { get; set; }
        public string AnneUni3 { get; set; }

        public DateTime Date_inscription { get; set; }
    }
}
