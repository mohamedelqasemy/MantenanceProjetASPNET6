using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MantenanceProjetASPNET6.Data;
using MantenanceProjetASPNET6.Models;

namespace MantenanceProjetASPNET6.Services
{
    public class EnregistrementServiceImp : IEnregistrementService
    {
        //Context
        private readonly GestionConcourCoreDbContext db;

        //Constructor
        public EnregistrementServiceImp(GestionConcourCoreDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<EnregistrementInfo> infosCandidatByCin(string cin)
        {
            var res = (from cand in db.Candidats
                       join fil in db.Filieres on cand.ID equals fil.ID
                       join dipl in db.Diplomes on cand.Cne equals dipl.Cne
                       where cand.Cin == cin
                       select new EnregistrementInfo
                       {
                           Nom = cand.Nom,
                           Prenom = cand.Prenom,
                           Cin = cand.Cin,
                           Niveau = cand.Niveau,
                           Photo = cand.Photo,
                           Cne = cand.Cne,
                           Num_dossier = cand.Num_dossier,

                           NomFil = fil.Nom,

                           TypeDipl = dipl.Type,
                       }
             );
            return res.ToList();
        }

        //retourne la liste des etudiants selon le niveau :
        public IEnumerable<ListEnregistrement> listetByNiveau(int niveau)
        {

            var x = (from c in db.Candidats

                     join a in db.Filieres on c.ID equals a.ID

                     where c.Niveau == niveau

                     orderby c.Num_dossier

                     select new ListEnregistrement
                     {
                         Nom = c.Nom,
                         Prenom = c.Prenom,
                         Filiere = a.Nom,
                         Cin = c.Cin,
                         Num_dossier = c.Num_dossier,
                         Sexe = c.Sexe


                     }).ToList();

            return x;
        }

        public void enregisterByCin(string cin)
        {
            var candidat = db.Candidats.Where(x => x.Cin.Equals(cin)).SingleOrDefault();
            //après enregistrement : présence=true + génération de num de dossier :
            if (candidat != null)
            {
                candidat.Presence = true;
                candidat.Num_dossier = generateNumDossier(candidat.Cin);
                db.SaveChanges();
            }
        }
        //to generate NumDossier
        public int generateNumDossier(string cin)
        {
            var c = db.Candidats.Where(x => x.Cin == cin).Count();
            int nbr=0;

            if (c != 0) //cin exists in db
            {
                var c1 = db.Candidats.Where(x => x.Cin == cin).Single();

                if (c1.Num_dossier == 0)  //1st time checking in => num_dossier should be determined
                {
                    //2 cas :
                    //1: c'est le 1er candidat a confirmer la presence => num_dossier automatiquement = 1
                    //2: num_dossier depend de la valeur précédente de num_dossier

                    //2 :
                    //condition : Count(candidats avec num_dossier!=null) > 0 
                    var c3 = db.Candidats.Where(x => x.Num_dossier != 0).Count();
                    if (c3 > 0)
                    {
                        var c2 = (from e in db.Candidats
                                  orderby e.Num_dossier descending
                                  select e).Take(1).Single();   //takes the biggest num_dossier and increments it

                        var max = (from e in db.Candidats
                                   orderby e.Num_dossier descending
                                   select e).Take(1).Single();

                        nbr = max.Num_dossier + 1;
                        c1.Num_dossier = nbr;
                    }
                    //1:                     
                    else
                    {
                        c1.Num_dossier = 1;
                        nbr = 1;
                    }

                    c1.Presence = true;
                }
                else
                {
                    nbr = c1.Num_dossier;
                }
        
            }
           
            db.SaveChanges();
            return nbr;
        }

    }
}
