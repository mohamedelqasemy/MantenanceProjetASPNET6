using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MantenanceProjetASPNET6.Models
{
    public class ConfigurationSelection
    {
        public int ID { get; set; }
        public string Filiere { get; set; }
        public int CoeffMath { get; set; }
        public int CoeffSpecialite { get; set; }
        public int NbrPlace { get; set; }
        public int NbrPlaceListAtt { get; set; }
        public double NoteMin { get; set; }
        public string TypeClassement { get; set; }
        public string Niveau { get; set; }
    }
}
