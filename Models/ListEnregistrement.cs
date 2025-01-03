using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MantenanceProjetASPNET6.Models
{
    public class ListEnregistrement
    {
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Filiere { get; set; }
        public string Cin { get; set; }
        public int Num_dossier { get; set; }
        public string Sexe { get; set; }
    }
}
