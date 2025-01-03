using MantenanceProjetASPNET6.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MantenanceProjetASPNET6.Services
{
    public interface IEpreuveService
    {
        int Upload(UploadModel epreuve);
        IEnumerable<DiplomeFichierModel> diplomeFile(string cne, int niveau);
    }
}
