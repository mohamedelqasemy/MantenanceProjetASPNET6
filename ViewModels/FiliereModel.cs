using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MantenanceProjetASPNET6.ViewModels
{
    public class FiliereModel
    {
        
        public int ID { get; set; }
        public string Nom { get; set; }
        public bool isChecked { get; set; }

    }
}
