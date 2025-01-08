using MantenanceProjetASPNET6.Models;
using MantenanceProjetASPNET6.ViewModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MantenanceProjetASPNET6.Services_User
{
    public interface ICandidatService
    {
        Candidat getTotalCandidat(string cne);

        BaccalaureatModel getBaccalaureat(string cne);
        void setBaccalaureat(BaccalaureatModel bac);

        CandidatModel getInfoPersonnel(string cne);
        void setInfoPersonnel(CandidatModel saisi);

        DiplomeModel getDiplome(string cne);
        void setDiplome(DiplomeModel saisi);

        FiliereModel getFiliere(string cne);
        FiliereModel setFiliere(string cne, int ID);

        string checkConformity(string cne);

        string uploadPicture(IFormFile file,string cne);

        void uploadFichierScanne(IFormFile[] files,string cne);

        string checkDiplome(string cne);


    }
}
