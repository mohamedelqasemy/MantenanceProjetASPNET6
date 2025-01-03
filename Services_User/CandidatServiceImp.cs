using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MantenanceProjetASPNET6.Data;
using MantenanceProjetASPNET6.Models;
using MantenanceProjetASPNET6.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace MantenanceProjetASPNET6.Services_User
{
    public class CandidatServiceImp : ICandidatService
    {
        GestionConcourCoreDbContext db;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment;

        public CandidatServiceImp(GestionConcourCoreDbContext db, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            this.db = db;
            this.hostingEnvironment = hostingEnvironment;
        }

        public Candidat getTotalCandidat(string cne)
        {
            return db.Candidats.Find(cne);
        }

        //############################################ BACCALAUREAT  #########################################

        public BaccalaureatModel getBaccalaureat(string cne)
        {
            var bac = (from b in db.Baccalaureats
                       where b.Cne.Equals(cne)
                       select new BaccalaureatModel
                       {
                           Cne = b.Cne,
                           DateObtentionBac = b.DateObtentionBac,
                           MentionBac = b.MentionBac,
                           NoteBac = b.NoteBac,
                           TypeBac = b.TypeBac
                       }).SingleOrDefault();

            return bac;
        }

        public void setBaccalaureat(BaccalaureatModel bac_saisi)
        {
            Baccalaureat bac = db.Baccalaureats.Find(bac_saisi.Cne);
            bac.DateObtentionBac = bac_saisi.DateObtentionBac;
            bac.MentionBac = bac_saisi.MentionBac;
            bac.NoteBac = bac_saisi.NoteBac;
            bac.TypeBac = bac_saisi.TypeBac;

            db.Update(bac);
            db.SaveChanges();
        }

        //############################################ Informations Personnelles  #########################################

        public CandidatModel getInfoPersonnel(string cne)
        {
            var info = (from c in db.Candidats
                       where c.Cne.Equals(cne)
                       select new CandidatModel
                       {
                           Cne = c.Cne,
                           Cin = c.Cin,
                           Nom = c.Nom,
                           Prenom = c.Prenom,
                           Email = c.Email,
                           Password = c.Password,
                           Sexe = c.Sexe,
                           Ville=c.Ville,
                           Adresse = c.Adresse,
                           LieuNaissance = c.LieuNaissance,
                           Gsm = c.Gsm,
                           Telephone = c.Telephone,
                           DateNaissance = c.DateNaissance,
                           Nationalite = c.Nationalite,
                           Photo = c.Photo
                           
                       }).SingleOrDefault();

            return info;
        }

        public void setInfoPersonnel(CandidatModel saisi)
        {
            Candidat candidat = db.Candidats.Find(saisi.Cne);
            candidat.Cin = saisi.Cin;
            candidat.Nom = saisi.Nom;
            candidat.Prenom = saisi.Prenom;
            candidat.Email = saisi.Email;
            candidat.Password = saisi.Password;
            candidat.Sexe = saisi.Sexe;
            candidat.Ville = saisi.Ville;
            candidat.Adresse = saisi.Adresse;
            candidat.LieuNaissance = saisi.LieuNaissance;
            candidat.Gsm = saisi.Gsm;
            candidat.Telephone = saisi.Telephone;
            candidat.DateNaissance = saisi.DateNaissance;
            candidat.Nationalite = saisi.Nationalite;

            db.Update(candidat);
            db.SaveChanges();
        }

        //#################################################  FILIERE  #########################################

        public Filiere getFiliere(string cne)
        {          
            var candidat = db.Candidats.Find(cne);
            var filiere = db.Filieres.Find(candidat.ID);

            string nom = filiere.Nom;
            return filiere;
        
        }

        public void setFiliere(string cne, int ID)
        {
            var candidat = db.Candidats.Find(cne);
            candidat.ID = ID;
            db.SaveChanges();
        }


        //#################################################  DIPLOME  #########################################

        public DiplomeModel getDiplome(string cne)
        {
            var data = (from d in db.Diplomes
                        join a in db.AnneeUniversitaires on d.Cne equals a.Cne
                        where d.Cne.Equals(cne)
                        select new DiplomeModel
                        {
                            Cne = d.Cne,
                            Type = d.Type,
                            Etablissement = d.Etablissement,
                            VilleObtention = d.VilleObtention,
                            NoteDiplome = d.NoteDiplome,
                            Specialite = d.Specialite,
                            Semestre1 = a.Semestre1,
                            Semestre2 = a.Semestre2,
                            Semestre3 = a.Semestre3,
                            Semestre4 = a.Semestre4,
                            Semestre5 = a.Semestre5,
                            Semestre6 = a.Semestre6,
                            Redoublant1 = a.Redoublant1,
                            Redoublant2 = a.Redoublant2,
                            Redoublant3 = a.Redoublant3,
                            AnneUni1 = a.AnneUni1,
                            AnneUni2 = a.AnneUni2,
                            AnneUni3 = a.AnneUni3

                        });

            return data.First();
        }

        public void setDiplome(DiplomeModel saisi)
        {
            var diplome = db.Diplomes.Find(saisi.Cne);
            diplome.Type = saisi.Type;
            diplome.Etablissement = saisi.Etablissement;
            diplome.VilleObtention = saisi.VilleObtention;
            diplome.NoteDiplome = saisi.NoteDiplome;
            diplome.Specialite = saisi.Specialite;
            db.SaveChanges();

            var annee = db.AnneeUniversitaires.Find(saisi.Cne);
            annee.Semestre1 = saisi.Semestre1;
            annee.Semestre2 = saisi.Semestre2;
            annee.Semestre3 = saisi.Semestre3;
            annee.Semestre4 = saisi.Semestre4;
            annee.Semestre5 = saisi.Semestre5;
            annee.Semestre6 = saisi.Semestre6;
            annee.Redoublant1 = saisi.Redoublant1;
            annee.Redoublant2 = saisi.Redoublant2;
            annee.Redoublant3 = saisi.Redoublant3;
            annee.AnneUni1 = saisi.AnneUni1;
            annee.AnneUni2 = saisi.AnneUni2;
            annee.AnneUni3 = saisi.AnneUni3;

            db.SaveChanges();
        }

        public string checkConformity(string cne)
        {
            string msg = "";
            var diplome = db.Diplomes.Find(cne);
            var anne = db.AnneeUniversitaires.Find(cne);
            var bac = db.Baccalaureats.Find(cne);
            string type_dip = diplome.Type;
            int k = 0;
            if (isNull(diplome))
            {
                msg += "Diplôme Info, ";
                k = 1;
            }
            if (isNull(anne))
            {
                if (type_dip == null)
                {
                    msg += "Année Univertsitaire, ";
                    k = 1;
                }
                else if (type_dip.Contains("Lic"))
                {
                    msg += "Année Univertsitaire, ";
                    k = 1;
                }
            }
            if (isNull(bac))
            {
                msg += "Bac Info, ";
                k = 1;
            }
            if (k == 1)
            {
                msg += "still need editing";
            }
            return msg;
        }

        public string checkDiplome(string cne)
        {
            string msg = "";
            var f = db.Fichiers.Where(t => t.Cne==cne).SingleOrDefault();
            if (f==null)
            {
                msg = "Insérer votre diplome";
            }
            else
            {
                var c = db.Candidats.Where(e => e.Cne == cne).SingleOrDefault();
                string diplomes = f.nom;
                int len = diplomes.Split('|').Length;
                if (c.Niveau == 4)
                {
                    if (len <= 3)
                    {
                        msg = "Inserer votre diplome";
                    }
                }
                else
                {
                    if (len < 3)
                    {
                        msg = "Inserer votre diplome";
                    }
                }
            }
            return msg;
        }

        public bool isNull(Object obj)
        {
            bool isNull = obj.GetType().GetProperties().All(p => p.GetValue(obj, null) == null); 
            return isNull;
        }

        public string uploadPicture(IFormFile file, string cne)
        {
            string response="";
            string uniqueFileName;
            try
            {
                String extension = Path.GetExtension(file.FileName);
                //se positionner dans le dossier
                string uploadFolder = Path.Combine(hostingEnvironment.WebRootPath, "candidatImages");
                //make a unique filename
                Random r = new Random();
                int rInt = r.Next(0, 10000);
                uniqueFileName = rInt.ToString() + extension.ToLower();
                //définir le chemin complet
                string filePath = Path.Combine(uploadFolder, uniqueFileName);
                //upload dans le fichier epreuve
                FileStream stream = new FileStream(filePath, FileMode.Create);
                file.CopyTo(stream);
                stream.Close();
                //Inserer le name dans la bd
                var x = db.Candidats.Where(c => c.Cne == cne).SingleOrDefault();
                x.Photo = uniqueFileName;
                db.SaveChanges();
                response = uniqueFileName;
            }
            catch (Exception ex)
            {
                response = "icon.jpg";
            }
            return response;
        }

        public void uploadFichierScanne(IFormFile[] files, string cne)
        {
            string globalName = "";
            foreach (IFormFile file in files)
            {
                //Checking file is available to save.  
                if (file != null)
                {
                    var InputFileName = Path.GetFileName(file.FileName);
                    string ext = Path.GetExtension(InputFileName);
                    Random rand = new Random();
                    int rInt = rand.Next(0, 100000);
                    string saveName = cne + ext;
                  //  string saveName = "diplome" + rInt.ToString() + ext;
                    //  globalName += "diplome"+rInt.ToString()+ext+ "|";
                    var uploadFolder = Path.Combine(hostingEnvironment.WebRootPath, "DiplomeScanne");
                    string filePath = Path.Combine(uploadFolder, saveName);
                    //Save file to server folder  
                    FileStream stream = new FileStream(filePath, FileMode.Create);
                    file.CopyTo(stream);
                    stream.Close();
                }

            }

            var y = db.Fichiers.Where(f => f.Cne == cne).SingleOrDefault();
            if (y == null)
            {
                Fichier fichier = new Fichier();
                fichier.Cne = cne;
                fichier.nom =globalName;
                db.Fichiers.Add(fichier);
                db.SaveChanges();
            }
            else
            {
                y.nom = globalName;
                db.SaveChanges();
            }
        }

    }
}
