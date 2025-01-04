using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MantenanceProjetASPNET6.ViewModels
{
    public class BaccalaureatModel
    {
        [Key]
        public string Cne { get; set; }
        [Required]
        public string TypeBac { get; set; }
        [Required]
        public string DateObtentionBac { get; set; }
        [Required]
        [Range(10, 20)]
        public double NoteBac { get; set; }
        [Required]
        public string MentionBac { get; set; }

        [Display(Name = "Photo du Bac (PDF)")]
        public IFormFile? PhotoBac { get; set; } 
        public string? ExistingPhotoBacPath { get; set; }
    }
}
