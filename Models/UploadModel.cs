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
        [Required]
        public string matiere { get; set; }
        [Required]
        public string annee { get; set; }
        [Required]
        public IFormFile fichier { get; set; }
    }
}
