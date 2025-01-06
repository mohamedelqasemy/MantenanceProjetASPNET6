using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MantenanceProjetASPNET6.Models
{
    public class UploadModel
    {
        [Required(ErrorMessage = "La matière est obligatoire.")]
        public string matiere { get; set; }
        [Required(ErrorMessage = "L'année est obligatoire.")]
        [Range(1900, 2100, ErrorMessage = "Veuillez saisir une année valide.")]
        public string annee { get; set; }
        [Required(ErrorMessage = "Veuillez sélectionner un fichier.")]
        public IFormFile fichier { get; set; }
    }
}
