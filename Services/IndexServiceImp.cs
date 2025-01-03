using MantenanceProjetASPNET6.Data;
using MantenanceProjetASPNET6.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MantenanceProjetASPNET6.Services
{
    public class IndexServiceImp : IIndexService
    {
        GestionConcourCoreDbContext db;
        
        public IndexServiceImp(GestionConcourCoreDbContext db)
        {
            this.db = db;
        }

        public int getAll()
        {
            return db.Candidats.Count();
        }


        public int getInscrits_niv(int niv)
        {
            return db.Candidats.Where(c => c.Niveau == niv).Count();
        }

        public int getNbr_Corbeille(int niv)
        {
            var res = from c in db.Candidats
                      join cor in db.Corbeilles on c.Cne equals cor.CNE
                      where c.Niveau == niv
                      select c;

            return res.Count();
        }

        public int getNbr_filiere(int fil, int niv)
        {
            var res = from c in db.Candidats
                      join f in db.Filieres on c.ID equals f.ID
                      where c.Niveau == niv
                      && f.ID == fil
                      select c;

            return res.Count();
        }


        public IndexModel getIndexModel()
        {
            IndexModel model = new IndexModel()
            {
                Info3 = getNbr_filiere(1, 3),
                Info4 = getNbr_filiere(1, 4),
                Gtr3 = getNbr_filiere(2, 3),
                Gtr4 = getNbr_filiere(2, 4),
                Indus3 = getNbr_filiere(3, 3),
                Indus4 = getNbr_filiere(3, 4),
                Gpmc3 = getNbr_filiere(4, 3),
                Gpmc4 = getNbr_filiere(4, 4),

                Inscrit3 = getInscrits_niv(3),
                Inscrit4 = getInscrits_niv(4),

                Suprim3 = getNbr_Corbeille(3),
                Suprim4 = getNbr_Corbeille(4),
                Suprim = getNbr_Corbeille(3) + getNbr_Corbeille(4),

                Total = getAll()
            };

            return model;
        }
    }

}
