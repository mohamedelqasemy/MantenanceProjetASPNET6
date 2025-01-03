using MantenanceProjetASPNET6.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MantenanceProjetASPNET6.Services
{
    public interface IEnregistrementService
    {
        IEnumerable<ListEnregistrement> listetByNiveau(int niveau);
        void enregisterByCin(string cin);
        IEnumerable<EnregistrementInfo> infosCandidatByCin(string cin);
    }
}
