using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MantenanceProjetASPNET6.Models
{
    public class FichierModel
    {
        [Required(ErrorMessage = "Please select file.")]
        //[Display(Name = "Browse File")]
        public IFormFile[] files { get; set; }
    }
}
