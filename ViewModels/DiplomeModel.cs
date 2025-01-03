using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MantenanceProjetASPNET6.ViewModels
{
    public class DiplomeModel
    {
        public string Cne { get; set; }
        
        public string Type { get; set; }
        [Required]
        public string Etablissement { get; set; }
        [Required]
        public string VilleObtention { get; set; }
        [Required]
        public double NoteDiplome { get; set; }
        [Required]
        public string Specialite { get; set; }
        [Required]
        public double Semestre1 { get; set; }
        [Required]
        public double Semestre2 { get; set; }
        [Required]
        public double Semestre3 { get; set; }
        [Required]
        public double Semestre4 { get; set; }
        [Required]
        public string Redoublant1 { get; set; }
        [Required]
        public string Redoublant2 { get; set; }
        [Required]
        public string AnneUni1 { get; set; }
        [Required]
        public string AnneUni2 { get; set; }

        public string AnneUni3 { get; set; }
        public string Redoublant3 { get; set; }
        public double Semestre5 { get; set; }
        public double Semestre6 { get; set; }

    }
}
