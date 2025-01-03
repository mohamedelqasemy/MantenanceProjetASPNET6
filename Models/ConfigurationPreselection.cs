using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MantenanceProjetASPNET6.Models
{
    public class ConfigurationPreselection
    {
        public int ID { get; set; }
        public string Filiere { get; set; }
        public string TypeDiplome { get; set; }
        public int CoeffBac { get; set; }
        public int CoeffS1 { get; set; }
        public int CoeffS2 { get; set; }
        public int CoeffS3 { get; set; }
        public int CoeffS4 { get; set; }
        public int CoeffS5 { get; set; }
        public int CoeffS6 { get; set; }
        public double NoteJoker { get; set; }
        public double NoteSeuil { get; set; }
    }
}
