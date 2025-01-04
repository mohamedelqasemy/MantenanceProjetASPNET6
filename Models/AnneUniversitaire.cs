using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MantenanceProjetASPNET6.Models
{
    public class AnneeUniversitaire
    {
        [Key, ForeignKey("Candidat")]
        public string Cne { get; set; }
        public double Semestre1 { get; set; }
        public double Semestre2 { get; set; }
        public double Semestre3 { get; set; }
        public double Semestre4 { get; set; }
        public double Semestre5 { get; set; }
        public double Semestre6 { get; set; }

        public string? Redoublant1 { get; set; }
        public string? Redoublant2 { get; set; }
        public string? Redoublant3 { get; set; }

        public string? AnneUni1 { get; set; }
        public string? AnneUni2 { get; set; }
        public string? AnneUni3 { get; set; }

        public virtual Candidat Candidat { get; set; }
    }
}
