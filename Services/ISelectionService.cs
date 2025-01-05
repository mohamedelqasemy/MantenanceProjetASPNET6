using MantenanceProjetASPNET6.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MantenanceProjetASPNET6.Services
{
   public interface ISelectionService
    {
        ConfigurationSelection getConfigurationSelection(string filiere, int niveau);
        void updateConfigurationSelection(ConfigurationSelection configurationSelection);
        void calculeNoteGlobale(string filiere);
        IEnumerable<AdmisModel> selectStudents(string filiere, string niveau);
        IEnumerable<ListFinal> getListPrincipale(string filiere);
        IEnumerable<ListFinal> getListPrincipaleSup(string filiere);
        IEnumerable<ListFinal> getListPrincipale3(string filiere);
        IEnumerable<ListFinal> getListPrincipale4(string filiere);
        IEnumerable<ListFinal> getListAttente(string filiere);
        IEnumerable<ListFinal> getListAttente3(string filiere);
        IEnumerable<ListFinal> getListAttente4(string filiere);
        IEnumerable<ListFinal> getListAttenteSup(string filiere);
    }
}
