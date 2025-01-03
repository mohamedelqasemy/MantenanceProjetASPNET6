using MantenanceProjetASPNET6.Data;
using MantenanceProjetASPNET6.Models;
using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace MantenanceProjetASPNET6.Services
{
    public class EpreuveServiceImp : IEpreuveService
    {
        GestionConcourCoreDbContext db;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment;

        public EpreuveServiceImp(GestionConcourCoreDbContext db, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            this.db = db;
            this.hostingEnvironment = hostingEnvironment;
        }

        public IEnumerable<DiplomeFichierModel> diplomeFile(string cne, int niveau)
        {
            var x = (from f in db.Fichiers
                     join c in db.Candidats on f.Cne equals c.Cne
                     where f.Cne == cne
                     where c.Niveau == niveau
                     select new DiplomeFichierModel
                     {
                         ID = f.ID,
                         nom = f.nom
                     }).ToList();
            return x;
        }

        public int Upload(UploadModel model)
        {
            int msg = 0;
            string uniqueFileName = null;
            
            if (model.fichier != null)
            {
                try
                {
                    String extension = Path.GetExtension(model.fichier.FileName);
                    //se positionner dans le dossier
                    string uploadFolder = Path.Combine(hostingEnvironment.WebRootPath, "epreuves");
                    //make a unique filename
                    Random r = new Random();
                    int rInt = r.Next(0, 10000);
                    uniqueFileName = rInt.ToString() + extension.ToLower();
                    /*//se positionner dans le dossier
                    string uploadFolder = Path.Combine(hostingEnvironment.WebRootPath, "epreuves");
                    //make a unique filename
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + model.fichier.FileName;
                    //définir le chemin complet*/
                    string filePath = Path.Combine(uploadFolder, uniqueFileName);
                    //upload dans le fichier epreuve
                    FileStream stream = new FileStream(filePath, FileMode.Create);
                    model.fichier.CopyTo(stream);
                    stream.Close();
                    //Inserer le name dans la bd
                    Epreuves epreuve = new Epreuves
                    {
                        Matiere = model.matiere,
                        Annee = model.annee,
                        NomFichier = uniqueFileName
                    };

                    db.Add(epreuve);
                    db.SaveChanges();

                    msg = 1;

                } catch (Exception ex)
                {
                    
                }
            }
            
            return msg;
            
        }
    }
}
