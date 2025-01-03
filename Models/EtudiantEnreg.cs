using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MantenanceProjetASPNET6.Models
{
    public class EtudiantEnreg
    {
        string Cne, Cin, Nom, Prenom, Ville, Num_dossier, Photo, Matricule;
        Boolean Convoque;

        public EtudiantEnreg(string Cne, string Cin, string Nom, string Prenom, string Ville)
        {
            this.Cin = Cin;
            this.Cne = Cne;
            this.Nom = Nom;
            this.Prenom = Prenom;
            this.Ville = Ville;

        }
    }
}
