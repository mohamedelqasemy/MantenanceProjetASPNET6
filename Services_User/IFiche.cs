using MantenanceProjetASPNET6.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MantenanceProjetASPNET6.Services_User
{
   public interface IFiche
    {
        Candidat GetCandidat(string cne);
    }
}
