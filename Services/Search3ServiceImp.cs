using MantenanceProjetASPNET6.Data;
using MantenanceProjetASPNET6.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MantenanceProjetASPNET6.Services
{
    public class Search3ServiceImp:ISearch3Service
    {
        private readonly GestionConcourCoreDbContext db;

        public Search3ServiceImp(GestionConcourCoreDbContext db)
        {
            this.db = db;
        }
        public IEnumerable<SearchModel3> info(int niveau)
        {
            var x = (from c in db.Candidats
                     join a in db.AnneeUniversitaires on c.Cne equals a.Cne
                     join b in db.Baccalaureats on c.Cne equals b.Cne
                     join concour in db.CouncourEcrits on c.Cne equals concour.Cne
                     join d in db.Diplomes on c.Cne equals d.Cne
                     where !db.Corbeilles.Any(corb => corb.CNE == c.Cne)
                     join oral in db.CouncourOrals on c.Cne equals oral.Cne
                     where c.Niveau == niveau
                     select new SearchModel3
                     {
                         Nom = c.Nom,
                         Prenom = c.Prenom,
                         Photo=c.Photo,
                         Sexe = c.Sexe,
                         NoteBac = b.NoteBac,
                         Note1 = a.Semestre1,
                         Note2 = a.Semestre2,
                         Note3 = a.Semestre3,
                         Note4 = a.Semestre4,
                         Note5 = a.Semestre5,
                         Note6 = a.Semestre6,
                         Dossier = c.Num_dossier,
                         Convoque = c.Convoque,
                         Math = concour.NoteMath,
                         Specialite = concour.NoteSpecialite,
                         Matricule = c.Matricule,
                         Email = c.Email,
                         Lieu_Naiss = c.LieuNaissance,
                         Nationalite = c.Nationalite,
                         Adress = c.Adresse,
                         Ville = c.Ville,
                         Type_dip = d.Type,
                         speciali_dip = d.Specialite,
                         note_Diplome = d.NoteDiplome,
                         Etablissement = d.Etablissement,
                         Ville_dip = d.VilleObtention,
                         Date_inscription = c.DateInscription,
                         Admis = c.Admis,
                         Filiere = db.Filieres.Where(f => f.ID == c.ID).FirstOrDefault().Nom,
                         Cin = c.Cin,
                         Cne = c.Cne,
                         NonConforme = c.Conforme,
                     }).ToList().Select(v =>
                     {
                         var fichier = db.Fichiers.Where(f => f.Cne == v.Cne).SingleOrDefault();
                         var part1 = "";
                         var part2 = "";
                         var part3 = "";
                         if (fichier != null)
                         {
                             if (fichier.nom.Split('|').Length == 1)
                             {
                                 part1 = fichier.nom.Split('|')[0];
                             }
                             else if (fichier.nom.Split('|').Length == 2)
                             {
                                 part1 = fichier.nom.Split('|')[0];
                                 part2 = fichier.nom.Split('|')[1];
                             }
                             else
                             {
                                 part1 = fichier.nom.Split('|')[0];
                                 part2 = fichier.nom.Split('|')[1];
                                 part3 = fichier.nom.Split('|')[2];
                             }
                         }
                         return new SearchModel3
                         {
                             Nom = v.Nom,
                             Prenom = v.Prenom,
                             Photo = v.Photo,
                             Sexe = v.Sexe,
                             NoteBac = v.NoteBac,
                             Note1 = v.Note1,
                             Note2 = v.Note2,
                             Note3 = v.Note3,
                             Note4 = v.Note4,
                             Note5 = v.Note5,
                             Note6 = v.Note6,
                             Dossier = v.Dossier,
                             Convoque = v.Convoque,
                             Math = v.Math,
                             Specialite = v.Specialite,
                             Matricule = v.Matricule,
                             Email = v.Email,
                             Lieu_Naiss = v.Lieu_Naiss,
                             Nationalite = v.Nationalite,
                             Adress = v.Adress,
                             Ville = v.Ville,
                             Type_dip = v.Type_dip,
                             speciali_dip=v.speciali_dip,
                             note_Diplome = v.note_Diplome,
                             Etablissement = v.Etablissement,
                             Ville_dip = v.Ville_dip,
                             Date_inscription = v.Date_inscription,
                             Admis = v.Admis,
                             Filiere = v.Filiere,
                             Cin = v.Cin,
                             Cne = v.Cne,
                             NonConforme = v.NonConforme,
                             Diplome1 = part1,
                             Diplome2 = part2,
                             Diplome3 = part3
                         };
                     }).ToList();
            return x;
        }

        public IEnumerable<SearchModel3> conformCandidat(string cne, int niveau)
        {
            var x = db.Candidats.Where(c => c.Cne == cne).SingleOrDefault();
            x.Conforme = !x.Conforme;
            db.SaveChanges();
            var y = this.info(niveau);
            return y;

        }

        public IEnumerable<SearchModel3> convoqueStudent(string cne, int niveau)
        {
            var x = db.Candidats.Where(c => c.Cne == cne).SingleOrDefault();
            x.Convoque = !x.Convoque;
            if (x.Convoque == true)
            {
                Random random = new Random();
                int generatedNumber = 0;
                bool isUnique = false;
                while (!isUnique)
                {
                    generatedNumber = random.Next(1000, 10000);
                    isUnique = !db.Candidats.Any(x => x.Num_dossier == generatedNumber);
                }
                x.Num_dossier = generatedNumber;
            }
            else
            {
                x.Num_dossier = 0;
            }
            db.SaveChanges();
            var y = this.info(niveau);
            return y;
        }

        public IEnumerable<SearchModel3> deleteCandidat(string cne, int niveau)
        {
            Candidat cand = db.Candidats.Where(c => c.Cne == cne).SingleOrDefault();
            Corbeille corb = new Corbeille();
            corb.CNE = cne;
            db.Corbeilles.Add(corb);
            cand.Conforme = true;
            db.SaveChanges();
            var x = this.info(niveau);
            return x;
        }

        public IEnumerable<SearchModel3> generalSearch(int niveau)
        {
            var x = this.info(niveau);
            return x;
        }

        public IEnumerable<SearchModel3> specifiedSearch(string Criteria, string Key, string Diplome, string Filiere, int niveau)
        {
            //first select
            //db.Configuration.ProxyCreationEnabled = false;
            var x = (from c in db.Candidats
                     join a in db.AnneeUniversitaires on c.Cne equals a.Cne
                     join b in db.Baccalaureats on c.Cne equals b.Cne
                     join concour in db.CouncourEcrits on c.Cne equals concour.Cne
                     join d in db.Diplomes on c.Cne equals d.Cne
                     where !db.Corbeilles.Any(corb => corb.CNE == c.Cne)
                     join oral in db.CouncourOrals on c.Cne equals oral.Cne
                     where c.Niveau == niveau
                     select new SearchModel3
                     {
                         Nom = c.Nom,
                         Prenom = c.Prenom,
                         Photo=c.Photo,
                         Sexe = c.Sexe,
                         NoteBac = b.NoteBac,
                         Note1 = a.Semestre1,
                         Note2 = a.Semestre2,
                         Note3 = a.Semestre3,
                         Note4 = a.Semestre4,
                         Dossier = c.Num_dossier,
                         Convoque = c.Convoque,
                         Math = concour.NoteMath,
                         Specialite = concour.NoteSpecialite,
                         Matricule = c.Matricule,
                         Email = c.Email,
                         Lieu_Naiss = c.LieuNaissance,
                         Nationalite = c.Nationalite,
                         Adress = c.Adresse,
                         Ville = c.Ville,
                         Type_dip = d.Type,
                         speciali_dip=d.Specialite,
                         note_Diplome = d.NoteDiplome,
                         Etablissement = d.Etablissement,
                         Ville_dip = d.VilleObtention,
                         Date_inscription = c.DateInscription,
                         Admis = c.Admis,
                         Filiere = db.Filieres.Where(f => f.ID == c.ID).FirstOrDefault().Nom,
                         Cin = c.Cin,
                         Cne = c.Cne,
                         NonConforme = c.Conforme
                     });

            if (!String.IsNullOrEmpty(Key))
            {
                if (Criteria == "convoque" || Criteria == "admis")
                {
                    var param = Expression.Parameter(typeof(SearchModel3), "p");
                    var exp = Expression.Lambda<Func<SearchModel3, bool>>(
                        Expression.Equal(
                            Expression.Property(param, Criteria),
                            Expression.Constant(bool.Parse(Key))
                    ),
                    param
                );
                    x = x.Where(exp);
                }
                else
                {
                    //first select
                    var param = Expression.Parameter(typeof(SearchModel3), "p");
                    var exp = Expression.Lambda<Func<SearchModel3, bool>>(
                        Expression.Equal(
                            Expression.Property(param, Criteria),
                            Expression.Constant(Key)
                        ),
                        param
                    );
                    x = x.Where(exp);
                }
            }

            if (Filiere != "0")
            {
                x = x.Where(n => n.Filiere == Filiere);
            }

            if (Diplome != "0")
            {
                x = x.Where(s => s.Type_dip == Diplome);
            }
            return x.ToList().Select(v =>
            {
                var fichier = db.Fichiers.Where(f => f.Cne == v.Cne).SingleOrDefault();
                var part1 = "";
                var part2 = "";
                var part3 = "";
                if (fichier != null)
                {
                    if (fichier.nom.Split('|').Length == 1)
                    {
                        part1 = fichier.nom.Split('|')[0];
                    }
                    else if (fichier.nom.Split('|').Length == 2)
                    {
                        part1 = fichier.nom.Split('|')[0];
                        part2 = fichier.nom.Split('|')[1];
                    }
                    else
                    {
                        part1 = fichier.nom.Split('|')[0];
                        part2 = fichier.nom.Split('|')[1];
                        part3 = fichier.nom.Split('|')[2];
                    }
                }
                return new SearchModel3
                {
                    Nom = v.Nom,
                    Prenom = v.Prenom,
                    Photo=v.Photo,
                    Sexe = v.Sexe,
                    NoteBac = v.NoteBac,
                    Note1 = v.Note1,
                    Note2 = v.Note2,
                    Note3 = v.Note3,
                    Note4 = v.Note4,
                    Note5 = v.Note5,
                    Note6 = v.Note6,
                    Dossier = v.Dossier,
                    Convoque = v.Convoque,
                    Math = v.Math,
                    Specialite = v.Specialite,
                    Matricule = v.Matricule,
                    Email = v.Email,
                    Lieu_Naiss = v.Lieu_Naiss,
                    Nationalite = v.Nationalite,
                    Adress = v.Adress,
                    Ville = v.Ville,
                    Type_dip = v.Type_dip,
                    speciali_dip = v.speciali_dip,
                    note_Diplome = v.note_Diplome,
                    Etablissement = v.Etablissement,
                    Ville_dip = v.Ville_dip,
                    Date_inscription = v.Date_inscription,
                    Admis = v.Admis,
                    Filiere = v.Filiere,
                    Cin = v.Cin,
                    Cne = v.Cne,
                    NonConforme = v.NonConforme,
                    Diplome1 = part1,
                    Diplome2 = part2,
                    Diplome3 = part3
                };
            }).ToList();
        }
    }
}

