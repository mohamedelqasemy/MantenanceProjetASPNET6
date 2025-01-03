using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MantenanceProjetASPNET6.Models
{
    public class EnregistrementInfo
    {
        public string Cne { get; set; }
        public string Cin { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public int Niveau { get; set; }
        public string NomFil { get; set; }
        public string Photo { get; set; }
        public string TypeDipl { get; set; }
        public int Num_dossier { get; set; }
        public Boolean Presence { get; set; }

    }
}
