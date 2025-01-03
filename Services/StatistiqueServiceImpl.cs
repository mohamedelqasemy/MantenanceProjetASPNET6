using MantenanceProjetASPNET6.Data;
using MantenanceProjetASPNET6.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MantenanceProjetASPNET6.Services
{
    public class StatistiqueServiceImpl:IStatistiqueService
    {
        private readonly GestionConcourCoreDbContext Bdd;

        public StatistiqueServiceImpl(GestionConcourCoreDbContext db)
        {
            this.Bdd = db;
        }
        //statistique des convoques
        public int getCandidatConv(int idFilier, string diplome, int niveau)
        {
            return Bdd.Candidats.Count(c => c.Filiere.ID == idFilier && c.Diplome.Type.Contains(diplome) && c.Niveau == niveau && c.Convoque == true);
        }
        public int getNbCandidatPerDiplomeConv(string diplom, int niveau)
        {
            return Bdd.Candidats.Count(c => c.Diplome.Type.Contains(diplom) && c.Niveau == niveau && c.Convoque == true);
        }

        //statistique des preinscrits
        public int getCandidatPreisncrit(int idFilere, string diplome, int niveau)
        {
            return Bdd.Candidats.Count(c => c.Filiere.ID == idFilere && c.Diplome.Type.Contains(diplome) && c.Niveau == niveau);
        }
        public int getNbCandidatPerDiplome(string diplom, int niveau)
        {
            return Bdd.Candidats.Count(c => c.Diplome.Type.Contains(diplom) && c.Niveau == niveau);
        }

        //statistique des presents
        public int getCandidatPresent(int idFilier, string diplome, int niveau)
        {
            return Bdd.Candidats.Count(c => c.Filiere.ID == idFilier && c.Diplome.Type.Contains(diplome) && c.Niveau == niveau && c.Presence == true);
        }
        public int getNbCandidatPresentPerDiplome(string diplom, int niveau)
        {
            return Bdd.Candidats.Count(c => c.Diplome.Type.Contains(diplom) && c.Niveau == niveau && c.Presence == true);
        }

        //statistique des principales
        public int getCandidatPrincipale(int idFilier, string diplome, int niveau)
        {
            return Bdd.Candidats.Count(c => c.Filiere.ID == idFilier && c.Diplome.Type.Contains(diplome) && c.Niveau == niveau && c.Admis == true);
        }
        public int getNbCandidatPrincipalPerDiplome(string diplom, int niveau)
        {
            return Bdd.Candidats.Count(c => c.Diplome.Type.Contains(diplom) && c.Niveau == niveau && c.Admis == true);
        }

        public List<Filiere> GetFilieres()
        {
            return Bdd.Filieres.ToList();
        }
        //statistique liste d'att
        public int getCandidatListeAtt(int idFilier, string diplome, int niveau)
        {
            return Bdd.Candidats.Count(c => c.Filiere.ID == idFilier && c.Diplome.Type.Contains(diplome) && c.Niveau == niveau && c.listDatt == true);

        }
        public int getNbCandidatAttPerDiplome(string diplom, int niveau)
        {
            return Bdd.Candidats.Count(c => c.Diplome.Type.Contains(diplom) && c.Niveau == niveau && c.listDatt == true);
        }

    }
}
