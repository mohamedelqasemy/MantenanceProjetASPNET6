using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MantenanceProjetASPNET6.Models
{
    public class Diplome
    {
        [Key, ForeignKey("Candidat")]
        public string Cne { get; set; }

        public string? Type { get; set; }
        public string? Etablissement { get; set; }
        public string? VilleObtention { get; set; }
        public double NoteDiplome { get; set; }
        public string? Specialite { get; set; }
        public string? PhotoDiplomePath { get; set; }



        public virtual Candidat Candidat { get; set; }
    }
}
