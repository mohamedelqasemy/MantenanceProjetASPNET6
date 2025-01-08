using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MantenanceProjetASPNET6.Models;
using Microsoft.AspNetCore.Http;
using MantenanceProjetASPNET6.Data;
using MantenanceProjetASPNET6.Services_User;
using MantenanceProjetASPNET6.ViewModels;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Rotativa.AspNetCore;

namespace MantenanceProjetASPNET6.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICandidatService candidat_service;
        private readonly IEpreuveService epreuve;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment;
        private readonly IFiche fiche;
        

        private readonly GestionConcourCoreDbContext _context;

        public HomeController(GestionConcourCoreDbContext _context,IFiche fiche,ICandidatService candidat_service, IEpreuveService epreuve, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            this.candidat_service = candidat_service;
            this.epreuve = epreuve;
            this.hostingEnvironment = hostingEnvironment;
            this.fiche = fiche;
            this._context = _context;
        }

        public IActionResult Index()
        {
            if (!isCandidat())
            {
                return RedirectToAction("Login", "Auth");
            }
            return RedirectToAction(nameof(Accueil), "Home");
        }

        //####################################################  ACCUEIL  ##################################################

        public IActionResult Accueil()
        {
            if (!isCandidat())
            {
                return RedirectToAction("Login", "Auth");
            }

            string cne = HttpContext.Session.GetString("cne");
            int? verified = HttpContext.Session.GetInt32("verified");

            if (verified == 0)
            {
                return RedirectToAction("Step1", "Auth");
            }

            Candidat candidat = candidat_service.getTotalCandidat(cne);
            HttpContext.Session.SetString("photo", candidat.Photo);
            HttpContext.Session.SetString("prenom", candidat.Prenom);
            HttpContext.Session.SetString("nom", candidat.Nom);
            HttpContext.Session.SetInt32("niveau", candidat.Niveau);

            string message = candidat_service.checkConformity(cne);
            string errorMessage = candidat_service.checkDiplome(cne);
            //ViewBag.errorMessage = errorMessage;
            //ViewBag.error = message;

            return View(candidat);
        }

        //##############################################  MODIFIER  PERSONNEL  ##################################################
        
        public IActionResult ModifierPersonnel()
        {
            if (!isCandidat())
            {
                return RedirectToAction("Login", "Auth");
            }

            string cne = HttpContext.Session.GetString("cne");
            int? verified = HttpContext.Session.GetInt32("verified");

            if (verified == 0)
            {
                return RedirectToAction("Step1", "Auth");
            }
            //Debug.WriteLine("################################################ tttttttttttt " + HttpContext.Session.GetString("photo"));
            CandidatModel info = candidat_service.getInfoPersonnel(cne);
            //Debug.WriteLine("################################################candidat_service.getInfoPersonnel " +info.Email);
            ViewBag.photo = info.Photo ;

            return View(info);
        }

        [HttpPost]
        public IActionResult ModifierPersonnel(CandidatModel candidat)
        {
            Debug.WriteLine("################################################ http post modifie " + candidat.Nom);
            string uploadsFolder = Path.Combine("wwwroot", "uploads", "cin");
                Directory.CreateDirectory(uploadsFolder); // Créer le dossier si nécessaire

                if (candidat.PhotoCin != null)
                {
                    // Générer un nom unique pour le fichier
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + candidat.PhotoCin.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    // Sauvegarder le fichier
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        candidat.PhotoCin.CopyTo(fileStream);
                    }

                    // Supprimer l'ancien fichier si nécessaire
                    if (!string.IsNullOrEmpty(candidat.ExistingPhotoCinPath))
                    {
                        string oldFilePath = Path.Combine("wwwroot", candidat.ExistingPhotoCinPath);
                        if (System.IO.File.Exists(oldFilePath))
                        {
                            System.IO.File.Delete(oldFilePath);
                        }
                    }

                    // Mettre à jour le chemin du fichier dans la base
                    candidat.ExistingPhotoCinPath = Path.Combine("uploads", "cin", uniqueFileName);
                }

                // Appeler le service pour mettre à jour les informations du candidat
                candidat_service.setInfoPersonnel(candidat);

                TempData["message"] = "CIN Modified successfully";
                return RedirectToAction("ModifierPersonnel");
            

            return View(candidat);
        }

        public JsonResult Image(IFormFile file)
        {
            string response = " ";
            string cne = HttpContext.Session.GetString("cne");
            if (file != null)
            {
                response=candidat_service.uploadPicture(file, cne);
                HttpContext.Session.SetString("photo", response);
            } 
            else
            {
                response = "icon.jpg";
            }
            return Json(response);
        }

        [HttpPost]
        public ActionResult FichierScanne(IFormFile[] files)
        {
            string cne = HttpContext.Session.GetString("cne");
            if (ModelState.IsValid)
            {
                candidat_service.uploadFichierScanne(files, cne);
                ViewBag.UploadStatus = files.Count().ToString() + " files uploaded successfully.";
            }
            return View();
        }

        //##############################################  BACCALAUREAT  ##################################################

        public IActionResult ModifierBac()
        {            

            if (!isCandidat())
            {
                return RedirectToAction("Login", "Auth");
            }

            string cne = HttpContext.Session.GetString("cne");
            int? verified = HttpContext.Session.GetInt32("verified");
            
            if (verified == 0)
            {
                return RedirectToAction("Step1", "Auth");
            }

            BaccalaureatModel bac = candidat_service.getBaccalaureat(cne);      

            return View(bac);
        }

        [HttpPost]
        public IActionResult ModifierBac(BaccalaureatModel bac)
        {
            if (ModelState.IsValid)
            {
                string uploadsFolder = Path.Combine("wwwroot", "uploads", "baccalaureats");
                Directory.CreateDirectory(uploadsFolder); // Créer le dossier si nécessaire

                if (bac.PhotoBac != null)
                {
                    // Générer un nom unique pour le fichier
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + bac.PhotoBac.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    // Sauvegarder le fichier
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        bac.PhotoBac.CopyTo(fileStream);
                    }

                    // Supprimer l ancien fichier si necessaire
                    if (!string.IsNullOrEmpty(bac.ExistingPhotoBacPath))
                    {
                        string oldFilePath = Path.Combine("wwwroot", bac.ExistingPhotoBacPath);
                        if (System.IO.File.Exists(oldFilePath))
                        {
                            System.IO.File.Delete(oldFilePath);
                        }
                    }

                    // Mettre a jour le chemin du fichier dans la base
                    bac.ExistingPhotoBacPath = Path.Combine("uploads", "baccalaureats", uniqueFileName);
                }

                // Appeler le service pour mettre a jour la base de donnees
                candidat_service.setBaccalaureat(bac);

                TempData["message"] = "Bac Modified successfully";
                return RedirectToAction("ModifierBac");
            }

            return View(bac);
        }

        //##############################################  FILIERE  ##################################################

        public IActionResult ModifierFiliere()
        {
            if (!isCandidat())
            {
                return RedirectToAction("Login", "Auth");
            }

            string cne = HttpContext.Session.GetString("cne");
            int? verified = HttpContext.Session.GetInt32("verified");

            if (verified == 0)
            {
                return RedirectToAction("Step1", "Auth");
            }
           
            
            var filiere = candidat_service.getFiliere(cne);
            ViewData["filiere"] = filiere.Nom;

            return View(filiere); 
            
        }
       

        [HttpPost]
        public IActionResult ModifierFiliere(FiliereModel model)
        {
                string cne = HttpContext.Session.GetString("cne");
                FiliereModel filiereModel=   candidat_service.setFiliere(cne,model.ID);
                model.niveau = filiereModel.niveau;
                TempData["filiere"] = "Filiere Modifiée avec succès";
        
            return View(model);
        }

        //##############################################  DIPLOME  ##################################################
        public IActionResult ModifierDiplome()
        {
            if (!isCandidat())
            {
                return RedirectToAction("Login", "Auth");
            }

            string cne = HttpContext.Session.GetString("cne");
            int? verified = HttpContext.Session.GetInt32("verified");

            if (verified == 0)
            {
                return RedirectToAction("Step1", "Auth");
            }
            ViewBag.niveau = HttpContext.Session.GetInt32("niveau");
            Debug.WriteLine("============================ " + HttpContext.Session.GetInt32("niveau"));
            DiplomeModel diplome = candidat_service.getDiplome(cne);

            return View(diplome);
        }

        [HttpPost]
        public IActionResult ModifierDiplome(DiplomeModel diplome)
        {
       
                string uploadsFolder = Path.Combine("wwwroot", "uploads", "diplome");
                Directory.CreateDirectory(uploadsFolder); // Créer le dossier si nécessaire

                if (diplome.diplomeFile != null)
                {
                    // Générer un nom unique pour le fichier
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + diplome.diplomeFile.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    // Sauvegarder le fichier
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        diplome.diplomeFile.CopyTo(fileStream);
                    }
                    // Supprimer l ancien fichier si necessaire
                    if (!string.IsNullOrEmpty(diplome.ExistingFileDiplomePath))
                    {
                        string oldFilePath = Path.Combine("wwwroot", diplome.ExistingFileDiplomePath);
                        if (System.IO.File.Exists(oldFilePath))
                        {
                            System.IO.File.Delete(oldFilePath);
                        }
                    }


                    // Mettre a jour le chemin du fichier dans la base
                    diplome.ExistingFileDiplomePath = Path.Combine("uploads", "diplome", uniqueFileName);
                }

                // Appeler le service pour mettre a jour la base de donnees
                candidat_service.setDiplome(diplome);

                TempData["diplome"] = "Diplome Modified successfully";
                return RedirectToAction("ModifierDiplome");

            return View(diplome);
        }


        public IActionResult FichierScanne()
        {
            if (!isCandidat())
            {
                return RedirectToAction("Login", "Auth");
            }

            string cne = HttpContext.Session.GetString("cne");
            int? verified = HttpContext.Session.GetInt32("verified");

            if (verified == 0)
            {
                return RedirectToAction("Step1", "Auth");
            }

            return View();
        }

        

        // ------------------- FICHE CONVOCATION
        public IActionResult Fiche(string id, string click = "empty")
        {
            if (!isCandidat())
            {
                return RedirectToAction("Login", "Auth");
            }

            string cne = HttpContext.Session.GetString("cne");
            int? verified = HttpContext.Session.GetInt32("verified");

            if (verified == 0)
            {
                return RedirectToAction("Step1", "Auth");
            }

            Candidat data = fiche.GetCandidat(cne);
            if (data.Diplome.Type == null || data.Diplome.VilleObtention == null)
            {
                return RedirectToAction("Index");
            }
            return View(data);
        }


        public IActionResult ImprimerConvocation(string cne)
        {

            Candidat data = fiche.GetCandidat(cne);
            ViewBag.Imprimer = "imprimer";

            return new ViewAsPdf("FicheImprime", data);
        }



        // --------------------------- FIN FICHE CONVOCATION

        //##############################################  EPREUVE  ##################################################

        public IActionResult Epreuve()
        {
            if (!isCandidat())
            {
                return RedirectToAction("Login", "Auth");
            }

            string cne = HttpContext.Session.GetString("cne");
            int? verified = HttpContext.Session.GetInt32("verified");

            if (verified == 0)
            {
                return RedirectToAction("Step1", "Auth");
            }
            
            return View(epreuve.getEpreuves().ToList());
        }

        //public IActionResult Download(string filename)
        //{
        //    if (!isCandidat())
        //    {
        //        return RedirectToAction("Login", "Auth");
        //    }

        //    string cne = HttpContext.Session.GetString("cne");
        //    int? verified = HttpContext.Session.GetInt32("verified");

        //    if (verified == 0)
        //    {
        //        return RedirectToAction("Step1", "Auth");
        //    }

        //    if (filename == null)
        //        return null;

        //    var path0 = Path.Combine(hostingEnvironment.WebRootPath, "epreuves");
        //    var path = Path.Combine(path0, filename);

        //    var ext = Path.GetExtension(filename).ToLowerInvariant();
        //    try
        //    {
        //        if (ext == ".pdf")
        //        {
        //            /*var stream = new FileStream(path, FileMode.Open);
        //            return File(stream, "application/pdf");*/
        //            byte[] fileBytes = System.IO.File.ReadAllBytes(path);
        //            string mimeType = "application/pdf";
        //            Response.Headers.Append("Content-Disposition", "inline; filename=" + filename);
        //            return File(fileBytes, mimeType);
        //        }
        //        else
        //        {
        //            byte[] fileBytes = System.IO.File.ReadAllBytes(path);
        //            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, filename);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        TempData["download"] = "Une erreur a été rencontrée lors du téléchargement";
        //        return RedirectToAction(nameof(Epreuve));
        //    }
            
                        
        //}

        public IActionResult TelechargerEpreuve(int id)
        {
            var epreuve = _context.Epreuves.FirstOrDefault(e => e.ID == id);

            if (epreuve == null)
            {
                TempData["download"] = "Épreuve introuvable.";
                return RedirectToAction("ListeEpreuves");
            }

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads/Epreuves", epreuve.NomFichier);

            if (!System.IO.File.Exists(filePath))
            {
                TempData["download"] = "Fichier non disponible.";
                return RedirectToAction("ListeEpreuves");
            }

            byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);
            return File(fileBytes, "application/pdf", epreuve.NomFichier);
        }

        //##############################################  FONCTIONS  ##################################################

        private bool isCandidat()
        {
            string cne = HttpContext.Session.GetString("cne");
            if (cne != null)
            {
                return true;
            }
            return false;
        }

    }
}
