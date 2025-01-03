using MantenanceProjetASPNET6.Data;
using MantenanceProjetASPNET6.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MantenanceProjetASPNET6.Services
{
    public class PreselectionServiceImp : IPreselectionService
    {
        //Context
        private readonly GestionConcourCoreDbContext db;

        //Constructor
        public PreselectionServiceImp(GestionConcourCoreDbContext db)
        {
            this.db = db;
            //db.Configuration.ProxyCreationEnabled = false;
        }

        public IEnumerable<PreselectionModel> getAll(int niveau, string filiere, string diplome)
        {
            var res = (from c in db.Candidats
                       join a in db.AnneeUniversitaires on c.Cne equals a.Cne
                       join b in db.Baccalaureats on c.Cne equals b.Cne
                       join d in db.Diplomes on c.Cne equals d.Cne
                       join f in db.Filieres on c.ID equals f.ID
                       where (f.Nom.Equals(filiere) &&
                       c.Conforme == false &&
                       a.Redoublant1.Equals("Non") &&
                       a.Redoublant2.Equals("Non") &&
                    //   a.Redoublant3.Equals("Non") &&
                       d.Type.Contains(diplome) &&
                       !db.Corbeilles.Any(corb => corb.CNE == c.Cne) &&
                       c.Niveau == niveau)
                       select new PreselectionModel
                       {
                           Matricule = c.Matricule,
                           Cin = c.Cin,
                           Cne = c.Cne,
                           Nom = c.Nom,
                           Prenom = c.Prenom,
                           Sexe = c.Sexe,
                           Convoque = c.Convoque,

                           NoteBac = b.NoteBac,
                           Note1 = a.Semestre1,
                           Note2 = a.Semestre2,
                           Note3 = a.Semestre3,
                           Note4 = a.Semestre4,
                           Note5 = a.Semestre5,
                           Note6 = a.Semestre6,
                           NoteDip = d.NoteDiplome,
                           NoteLic = Math.Round(((a.Semestre5 + a.Semestre6) / 2), 2),
                           NotePreselec = c.NotePreselec,

                           Type_dip = d.Type,
                           Ville_dip = d.VilleObtention,
                           Etablissement = d.Etablissement,

                           Redoublant1 = a.Redoublant1,
                           Redoublant2 = a.Redoublant2,
                           Redoublant3 = a.Redoublant3,
                           AnneUni1 = a.AnneUni1,
                           AnneUni2 = a.AnneUni2,
                           AnneUni3 = a.AnneUni3,

                           //Filiere = db.Filieres.Where(f => f.ID == c.ID).FirstOrDefault().Nom,
                           Filiere = f.Nom,
                           Date_inscription = c.DateInscription,

                       }).ToList();

            return res;
        }

        public void calculerPreselec(int niveau, string fil, string diplome)
        {
            ConfigurationPreselection conf = db.ConfigurationPreselections.Where(c => c.Filiere.Equals(fil) && diplome.Contains(c.TypeDiplome)).First();
            double sum;
            int sumCoeff;
            sumCoeff = conf.CoeffBac + conf.CoeffS1 + conf.CoeffS2 + conf.CoeffS3 + conf.CoeffS4;
            if (niveau == 4)
            {
                sumCoeff += conf.CoeffS5 + conf.CoeffS6;
            }

            foreach (PreselectionModel c in this.getAll(niveau, fil, diplome).ToList())
            {
                sum = 0;
                sum = (c.NoteBac * conf.CoeffBac) + (c.Note1 * conf.CoeffS1) + (c.Note2 * conf.CoeffS2) + (c.Note3 * conf.CoeffS3) + (c.Note4 * conf.CoeffS4);
                if (niveau == 4)
                {
                    sum += (c.Note5 * conf.CoeffS5) + (c.Note6 * conf.CoeffS6);
                }
                //il se peut y7eydo ila b9a void
                c.NotePreselec = Math.Round(sum / sumCoeff, 2);
                c.Convoque = (c.NotePreselec >= conf.NoteSeuil) ? true : false;

                var temp = db.Candidats.Find(c.Cne);
                temp.NotePreselec = c.NotePreselec;
                temp.Convoque = c.Convoque;
                db.SaveChanges();
            }
        }

        public void setConfig(ConfigurationPreselection config, int niveau)
        {
            var conf_tab = db.ConfigurationPreselections.Where(c => c.Filiere.Equals(config.Filiere) && c.TypeDiplome.Equals(config.TypeDiplome)).FirstOrDefault();
            if (conf_tab == null)
            {
                db.ConfigurationPreselections.Add(config);
                db.SaveChanges();
            }
            else
            {
                conf_tab.Filiere = config.Filiere;
                conf_tab.TypeDiplome = config.TypeDiplome;
                conf_tab.CoeffBac = config.CoeffBac;
                conf_tab.CoeffS1 = config.CoeffS1;
                conf_tab.CoeffS2 = config.CoeffS2;
                conf_tab.CoeffS3 = config.CoeffS3;
                conf_tab.CoeffS4 = config.CoeffS4;
                conf_tab.NoteSeuil = config.NoteSeuil;
                if (niveau == 4)
                {
                    conf_tab.CoeffS5 = config.CoeffS5;
                    conf_tab.CoeffS6 = config.CoeffS6;
                }
                db.SaveChanges();
            }
        }

        public IEnumerable<PreselectionModel> getConvoques(int niveau, string filiere, string diplome)
        {
            return this.getAll(niveau, filiere, diplome).Where(c => c.Convoque.Equals(true));
        }

        public ConfigurationPreselection getConfig(string filiere, string diplome)
        {
            var conf = db.ConfigurationPreselections.Where(c => c.Filiere.Equals(filiere) && c.TypeDiplome.Equals(diplome)).FirstOrDefault();
            return conf;
        }

        public IEnumerable<string> getPourcentage(int niveau, string filiere, string diplome)
        {
            var list = new List<string>();
            var all = this.getAll(niveau, filiere, diplome);
            int sum = all.Count();
            int sumConv = all.Where(c => c.Convoque.Equals(true)).Count();
            string pourc;
            if (sum != 0)
            {
                pourc = ((sumConv * 100) / sum).ToString();
            }
            else
            {
                pourc = "0";
            }

            list.Add(sumConv.ToString() + "/" + sum.ToString() + "  ");

            list.Add(" (" + pourc + "%)");

            return list;
        }

    }
}
