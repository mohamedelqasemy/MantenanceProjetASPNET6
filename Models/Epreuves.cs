using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MantenanceProjetASPNET6.Models
{
    public class Epreuves
    {
        public int ID { get; set; }
        [Required]
        public string Matiere { get; set; }
        [Required]
        public string Annee { get; set; }
        [Required]
        public string NomFichier { get; set; }
    }
}
