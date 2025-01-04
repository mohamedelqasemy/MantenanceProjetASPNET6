using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using MantenanceProjetASPNET6.Data;
using MantenanceProjetASPNET6.Models;
using MantenanceProjetASPNET6.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace MantenanceProjetASPNET6.Controllers
{
    public class AuthController : Controller
    {
        private readonly GestionConcourCoreDbContext _db;

        public AuthController(GestionConcourCoreDbContext db)
        {
            _db = db;
        }

        //######################################################## STEP1
        public ActionResult Step1()
        {
            if (HttpContext.Session.GetString("cne") != null)
            {
                Candidat candidat = _db.Candidats.Find(HttpContext.Session.GetString("cne"));
                if (candidat.Verified == 1)
                {
                    return RedirectToAction("Index", "Home");
                }
                InfoPersoModel info = new InfoPersoModel()
                {
                    Adresse = candidat.Adresse,
                    Ville = candidat.Ville,
                    LieuNaissance = candidat.LieuNaissance,
                    Telephone = candidat.Telephone,
                    Nationalite = candidat.Nationalite,
                    Gsm = candidat.Gsm,
                    DateNaissance = candidat.DateNaissance,
                    Sexe = candidat.Sexe
                };
                return View(info);
            }
            return RedirectToAction("Login", "Auth");
        }

        [HttpPost]
        public ActionResult Step1(InfoPersoModel candidat, IFormFile CniPdfFile)
        {
            string cne = HttpContext.Session.GetString("cne");

                var originalCandidat = _db.Candidats.FirstOrDefault(c => c.Cne == cne);
                if (originalCandidat == null)
                {
                    ModelState.AddModelError("", "Le candidat n'a pas été trouvé.");
                    return View(candidat);
                }

                // Mise à jour des informations personnelles
                originalCandidat.DateNaissance = candidat.DateNaissance;
                originalCandidat.LieuNaissance = candidat.LieuNaissance;
                originalCandidat.Sexe = candidat.Sexe;
                originalCandidat.Nationalite = candidat.Nationalite;
                originalCandidat.Gsm = candidat.Gsm;
                originalCandidat.Telephone = candidat.Telephone;
                originalCandidat.Adresse = candidat.Adresse;
                originalCandidat.Ville = candidat.Ville;

                // Gestion du fichier PDF de la CNI
                if (CniPdfFile != null)
                {
                    // Vérifier le type MIME
                    if (CniPdfFile.ContentType != "application/pdf")
                    {
                        ModelState.AddModelError("", "Seuls les fichiers PDF sont autorisés.");
                        return View(candidat);
                    }

                    // Chemin de destination
                    var uploadsFolder = Path.Combine("wwwroot", "uploads", "cni");
                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    // Nom unique pour éviter les conflits
                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(CniPdfFile.FileName);
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    // Supprimer l'ancien fichier si nécessaire
                    if (!string.IsNullOrEmpty(originalCandidat.PhotoCinPath))
                    {
                        var oldFilePath = Path.Combine("wwwroot", originalCandidat.PhotoCinPath.TrimStart('/').Replace("/", Path.DirectorySeparatorChar.ToString()));
                        if (System.IO.File.Exists(oldFilePath))
                        {
                            System.IO.File.Delete(oldFilePath);
                        }
                    }

                    // Enregistrer le fichier
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        CniPdfFile.CopyTo(stream);
                    }

                    // Enregistrer le chemin relatif dans le modèle
                    originalCandidat.PhotoCinPath = Path.Combine("uploads", "cni", uniqueFileName).Replace("\\", "/");
                }

                // Enregistrer les modifications
                try
                {
                    _db.SaveChanges();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Une erreur est survenue lors de la sauvegarde des données : " + ex.Message);
                    return View(candidat);
                }

                // Passer à l'étape suivante
                HttpContext.Session.SetInt32("steps", 2);
                return RedirectToAction("Step2");
            

            return View(candidat);
        }


        //######################################################## STEP2
        public ActionResult Step2()
        {
            if (HttpContext.Session.GetString("cne") != null)
            {
                Candidat candidat = _db.Candidats.Find(HttpContext.Session.GetString("cne"));
                if (candidat.Verified == 1)
                {
                    return RedirectToAction("Index", "Home");
                }
                int steps = (int)HttpContext.Session.GetInt32("steps");
                if (steps != 2 && steps != 3)
                {
                    return RedirectToAction("Step1");
                }
                Baccalaureat bac = _db.Baccalaureats.Find(HttpContext.Session.GetString("cne"));
                return View(bac);
            }
            return RedirectToAction("Login", "Auth");
        }

        [HttpPost]
        public ActionResult Step2(Baccalaureat bac, IFormFile BacPdfFile)
        {
            string cne = HttpContext.Session.GetString("cne");
            var candidat = _db.Baccalaureats.Include(b => b.Candidat).FirstOrDefault(b => b.Cne == cne);

            if (candidat == null)
            {
                ModelState.AddModelError("", "Le Candidat n'a pas été trouvé.");
                return View(bac);
            }

            // Gestion du fichier PDF
            if (BacPdfFile != null)
            {
                // Vérifier le type MIME
                if (BacPdfFile.ContentType != "application/pdf")
                {
                    ModelState.AddModelError("", "Seuls les fichiers PDF sont autorisés.");
                    return View(bac);
                }

                // Chemin de destination
                var uploadsFolder = Path.Combine("wwwroot", "uploads", "baccalaureats");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                // Nom unique pour éviter les conflits
                var uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(BacPdfFile.FileName);
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                // Supprimer l'ancien fichier si nécessaire
                if (!string.IsNullOrEmpty(candidat.PhotoBacPath))
                {
                    var oldFilePath = Path.Combine("wwwroot", candidat.PhotoBacPath.TrimStart('/').Replace("/", Path.DirectorySeparatorChar.ToString()));
                    if (System.IO.File.Exists(oldFilePath))
                    {
                        System.IO.File.Delete(oldFilePath);
                    }
                }

                // Enregistrer le fichier
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    BacPdfFile.CopyTo(stream);
                }

                // Enregistrer le chemin relatif dans le modèle avec des barres obliques (/)
                candidat.PhotoBacPath = Path.Combine("uploads", "baccalaureats", uniqueFileName).Replace("\\", "/");
            }

            // Mettre à jour les autres informations
            if (!string.IsNullOrWhiteSpace(bac.TypeBac))
            {
                candidat.TypeBac = bac.TypeBac;
            }
            if (bac.DateObtentionBac != null)
            {
                candidat.DateObtentionBac = bac.DateObtentionBac;
            }
            if (bac.NoteBac > 0)
            {
                candidat.NoteBac = bac.NoteBac;
            }
            if (!string.IsNullOrWhiteSpace(bac.MentionBac))
            {
                candidat.MentionBac = bac.MentionBac;
            }

            try
            {
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Une erreur est survenue lors de la sauvegarde des données : " + ex.Message);
                return View(bac);
            }

            // Avancer à l'étape suivante
            HttpContext.Session.SetInt32("steps", 3);
            return RedirectToAction("Step3");
        }


        //########################################################STEP3
        public ActionResult Step3()
        {
            if (HttpContext.Session.GetString("cne") != null)
            {
                Candidat candidat = _db.Candidats.Find(HttpContext.Session.GetString("cne"));
                if (candidat.Verified == 1)
                {
                    return RedirectToAction("Index", "Home");
                }
                var diplome = _db.Diplomes.Find(HttpContext.Session.GetString("cne"));
                var anne = _db.AnneeUniversitaires.Find(HttpContext.Session.GetString("cne"));
                DiplomeNote dipNote = new DiplomeNote()
                {
                    Type = diplome.Type,
                    Etablissement = diplome.Etablissement,
                    VilleObtention = diplome.VilleObtention,
                    NoteDiplome = diplome.NoteDiplome,
                    Specialite = diplome.Specialite,
                    Semestre1 = anne.Semestre1,
                    Semestre2 = anne.Semestre2,
                    Semestre3 = anne.Semestre3,
                    Semestre4 = anne.Semestre4,
                    Semestre5 = anne.Semestre5,
                    Semestre6 = anne.Semestre6,
                    Redoublant1 = anne.Redoublant1,
                    Redoublant2 = anne.Redoublant2,
                    Redoublant3 = anne.Redoublant3,
                    AnneUni1 = anne.AnneUni1,
                    AnneUni2 = anne.AnneUni2,
                    AnneUni3 = anne.AnneUni3
                };
                int steps = (int)HttpContext.Session.GetInt32("steps");
                if (steps != 3)
                {
                    return RedirectToAction("Step1");
                }
                ViewBag.niveau = HttpContext.Session.GetInt32("niveau").ToString();
                return View(dipNote);
            }
            return RedirectToAction("Login", "Auth");
        }

        [HttpPost]
        public ActionResult Step3(DiplomeNote diplome)
        {
            string cne = HttpContext.Session.GetString("cne");
            
                var x = _db.Diplomes.Where(c => c.Cne == cne).SingleOrDefault();
                x.Type = diplome.Type;
                x.Etablissement = diplome.Etablissement;
                x.VilleObtention = diplome.VilleObtention;
                x.NoteDiplome = diplome.NoteDiplome;
                x.Specialite = diplome.Specialite;
                _db.SaveChanges();

                var y = _db.AnneeUniversitaires.Where(a => a.Cne == cne).SingleOrDefault();
                y.Semestre1 = diplome.Semestre1;
                y.Semestre2 = diplome.Semestre2;
                y.Semestre3 = diplome.Semestre3;
                y.Semestre4 = diplome.Semestre4;
                y.Semestre5 = diplome.Semestre5;
                y.Semestre6 = diplome.Semestre6;
                y.Redoublant1 = diplome.Redoublant1;
                y.Redoublant2 = diplome.Redoublant2;
                y.Redoublant3 = diplome.Redoublant3;
                y.AnneUni1 = diplome.AnneUni1;
                y.AnneUni2 = diplome.AnneUni2;
                y.AnneUni3 = diplome.AnneUni3;

                _db.SaveChanges();
                Candidat candidat = _db.Candidats.Find(HttpContext.Session.GetString("cne"));
                candidat.Verified = 1;
                _db.SaveChanges();
                var z = _db.Candidats.Where(c=> c.Cne== HttpContext.Session.GetString("cne")).SingleOrDefault();

                var fromAddress = new MailAddress("admin@gmail.com", "From ENSAS");
                var toAddress = new MailAddress(candidat.Email, "To Name");
                const string fromPassword = "adminconcours125498";
                const string subject = "Création de compte de postulation au concours ENSAS";
                //string body = "<a href=\"http://localhost:49969/Auth/Verify?cne="+candidat.Cne+" \">Link</a><br /><p> this is the password : "+candidat.Password+"</p>";
                string body = z.Nom;

                HttpContext.Session.SetInt32("verified",  z.Verified);

                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword),
                    Timeout = 60000
                };
                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body
                })
                {
                    try
                    {
                        message.IsBodyHtml = true;
                        smtp.Send(message);
                        return RedirectToAction("Index", "Home");
                    }
                    catch
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    
                }
               
            
            return View(diplome);
        }


        public ActionResult Login()
        {
            string cne = HttpContext.Session.GetString("cne");
            if (cne != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Login(Candidat candidat)
        {
            var x = _db.Candidats.Where(c => c.Cne == candidat.Cne && c.Cin == candidat.Cin && c.Password == candidat.Password).SingleOrDefault();
            if (x == null)
            {
                TempData["error"] = "False Credential";
                return Redirect("Login");
            }
            else
            {
                HttpContext.Session.SetString("nom", x.Nom);
                HttpContext.Session.SetString("prenom", x.Prenom);
                HttpContext.Session.SetString("cne", candidat.Cne);
                HttpContext.Session.SetInt32("niveau", x.Niveau);
                HttpContext.Session.SetString("role", "user");
                HttpContext.Session.SetString("photo", x.Photo);
                HttpContext.Session.SetInt32("verified", x.Verified);

                if (x.Verified == 1)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    HttpContext.Session.SetInt32("steps", 1);
                    return RedirectToAction("Step1", "Auth");
                }

            }
        }

        public ActionResult PasswordOublie()
        {
            return View();
        }

        [HttpPost]
        public ActionResult PasswordOublie(string email)
        {
            Candidat candidat = _db.Candidats.Where(c => c.Email == email).SingleOrDefault();
            Random random = new Random();
            const string pool = "abcdefghijklmnopqrstuvwxyz0123456789";
            var chars = Enumerable.Range(0, 7)
                .Select(x => pool[random.Next(0, pool.Length)]);
            string password = new string(chars.ToArray());
            try
            {
                candidat.Password = password;
                _db.SaveChanges();

                var fromAddress = new MailAddress("admin@gmail.com", "From ENSAS");
                var toAddress = new MailAddress(candidat.Email, "To Name");
                const string fromPassword = "adminconcours125498";
                const string subject = "Restauration de mot de pass";
                //string body = "<a href=\"http://localhost:49969/Auth/Verify?cne="+candidat.Cne+" \">Link</a><br /><p> this is the password : "+candidat.Password+"</p>";
                string body = "<div class=\"container\"><div class=\"row\"><img src=\"https://lh3.googleusercontent.com/proxy/g_QnANEsQGJPGvR4haGBTi-kr2n32DU-eArBRKuJWtpgPCHQbz-RINzL6FzIc1TQs0a80Vfkaew6umTHHPQgHTE4l_g \" /></div><br><div class=\"alert alert-danger\"><strong><span style=\"color:'red'\">Vous trouverez votre nouveau mot de passe au dessous</span></strong><br></div><div class=\"row\"><div class=\"card\" style=\"width: 18rem;\"><div class=\"card-body\"><strong>Nom :</strong><span>" + candidat.Nom + "</span><br /><strong>Prenom : </strong><span>" + candidat.Prenom + "</span><br /><strong>CNE : </strong><span>" + candidat.Cne + "</span><br /><strong>CIN : </strong><span>" + candidat.Cin + "</span><br /><strong>Password : </strong><span>" + candidat.Password + "</span><br /></div></div></div></div>";
                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword),
                    Timeout = 60000
                };
                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body
                })
                {
                    message.IsBodyHtml = true;
                    smtp.Send(message);
                }
                TempData["message"] = "Email Sent Succefully";
                return View();

            }
            catch (Exception ex)
            {
                TempData["error"] = "Entrer des données valides";
                return View();
            }

        }

        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(Candidat candidat)
        {
            try
            {
                // Validation des champs uniques
                if (candidat.Niveau == 0)
                {
                    ModelState.AddModelError("selectNiveau", "Selectionner un niveau");
                }

                try
                {
                    if (_db.Candidats.Any(c => c.Cne == candidat.Cne))
                    {
                        ModelState.AddModelError("UniqueCne", "Cne need to be unique");
                    }

                    if (_db.Candidats.Any(c => c.Cin == candidat.Cin))
                    {
                        ModelState.AddModelError("UniqueCin", "Cin need to be unique");
                    }

                    if (_db.Candidats.Any(c => c.Email == candidat.Email))
                    {
                        ModelState.AddModelError("UniqueEmail", "Email need to be unique");
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Erreur lors de la vérification des doublons : " + ex.Message);
                }

                
                    try
                    {
                        // Ajout du candidat
                        candidat.DateInscription = DateTime.Now;
                        candidat.DateNaissance = DateTime.Now;
                        Random random = new Random();
                        const string pool = "abcdefghijklmnopqrstuvwxyz0123456789";

                        candidat.Matricule = new string(Enumerable.Range(0, 8)
                            .Select(x => pool[random.Next(pool.Length)])
                            .ToArray()).ToUpper();

                        candidat.Password = new string(Enumerable.Range(0, 7)
                            .Select(x => pool[random.Next(pool.Length)])
                            .ToArray());

                        candidat.Verified = 0;
                        candidat.Photo = "icon.jpg";

                        _db.Candidats.Add(candidat);
                        _db.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("", "Erreur lors de l'ajout du candidat : " + ex.Message);
                        return View(candidat);
                    }

                    // Ajout des enregistrements liés
                    try
                    {
                        _db.Diplomes.Add(new Diplome { Cne = candidat.Cne, NoteDiplome = 0.0 });
                        _db.AnneeUniversitaires.Add(new AnneeUniversitaire
                        {
                            Cne = candidat.Cne,
                            Semestre1 = 0.0,
                            Semestre2 = 0.0,
                            Semestre3 = 0.0,
                            Semestre4 = 0.0,
                            Semestre5 = 0.0,
                            Semestre6 = 0.0
                        });
                        _db.Baccalaureats.Add(new Baccalaureat { Cne = candidat.Cne, NoteBac = 0.0 });
                        _db.CouncourEcrits.Add(new ConcourEcrit { Cne = candidat.Cne, NoteGenerale = 0.0, NoteSpecialite = 0.0 });
                        _db.CouncourOrals.Add(new ConcourOral { Cne = candidat.Cne, Classement = 1 });

                        _db.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("", "Erreur lors de l'ajout des données liées : " + ex.Message);
                        return View(candidat);
                    }

                    // Envoi de l'email
                    try
                    {
                        var fromAddress = new MailAddress("admin@gmail.com", "From ENSAS");
                        var toAddress = new MailAddress(candidat.Email, "To Name");
                        const string fromPassword = "adminconcours125498";
                        const string subject = "Récupération du mot de passe";

                        string body = $@"
                <div class='container'>
                    <h2>Bienvenue sur la plateforme ENSAS</h2>
                    <p>Vous trouverez vos informations ci-dessous :</p>
                    <strong>Nom :</strong> {candidat.Nom}<br />
                    <strong>Prénom :</strong> {candidat.Prenom}<br />
                    <strong>CNE :</strong> {candidat.Cne}<br />
                    <strong>CIN :</strong> {candidat.Cin}<br />
                    <strong>Mot de passe :</strong> {candidat.Password}<br />
                </div>";

                        var smtp = new SmtpClient
                        {
                            Host = "smtp.gmail.com",
                            Port = 587,
                            EnableSsl = true,
                            DeliveryMethod = SmtpDeliveryMethod.Network,
                            Credentials = new NetworkCredential(fromAddress.Address, fromPassword),
                            Timeout = 60000
                        };

                        using (var message = new MailMessage(fromAddress, toAddress)
                        {
                            Subject = subject,
                            Body = body,
                            IsBodyHtml = true
                        })
                        {
                            smtp.Send(message);
                        }
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("", "Erreur lors de l'envoi de l'email : " + ex.Message);
                    }

                    TempData["message"] = $"Votre mot de passe est : '{candidat.Password}'. Vous le trouverez sur votre email aussi.";
                    return Redirect("Login");
                
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Erreur inattendue : " + ex.Message);
            }

            return View(candidat);
        }


        public IActionResult Deconnexion()
        {
            HttpContext.Session.Remove("cne");
            HttpContext.Session.Remove("verified");
            return Redirect("Login");
        }
    }
}