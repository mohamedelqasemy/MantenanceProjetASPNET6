using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MantenanceProjetASPNET6.Models
{
    public class InfoPersoModel
    {
        [Required]
        public string Adresse { get; set; }
        [Required]
        public string Ville { get; set; }
        [Required]
        public string LieuNaissance { get; set; }
        [Required]
        public string Telephone { get; set; }
        [Required]
        public string Nationalite { get; set; }
        [Required]
        public string Sexe { get; set; }
        [Required]
        public string Gsm { get; set; }
        [Required]
        public DateTime DateNaissance { get; set; }
    }
}
