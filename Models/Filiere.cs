using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MantenanceProjetASPNET6.Models
{
    public class Filiere
    {
        public int ID { get; set; }
        public string? Nom { get; set; }

        //relation manyToOne avec la classe Candidat 
        public virtual IList<Candidat> Candidats { get; set; }
    }
}
