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
        public ActionResult Step1(InfoPersoModel candidat)
        {
            string cne = HttpContext.Session.GetString("cne");
            if (ModelState.IsValid)
            {
                var originalCandiat = (from c in _db.Candidats where c.Cne == cne select c).First();
                originalCandiat.DateNaissance = candidat.DateNaissance;
                originalCandiat.LieuNaissance = candidat.LieuNaissance;
                originalCandiat.Sexe = candidat.Sexe;
                originalCandiat.Nationalite = candidat.Nationalite;
                originalCandiat.Gsm = candidat.Gsm;
                originalCandiat.Telephone = candidat.Telephone;
                originalCandiat.Adresse = candidat.Adresse;
                originalCandiat.Ville = candidat.Ville;
                _db.SaveChanges();
                HttpContext.Session.SetInt32("steps", 2);
                return RedirectToAction("Step2");
            }
            return View(candidat);
        }

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
        public ActionResult Step2(Baccalaureat bac)
        {
            if (ModelState.IsValid)
            {
                string cne = HttpContext.Session.GetString("cne");
                var candidat = _db.Baccalaureats.Find(cne);
                candidat.TypeBac = bac.TypeBac;
                candidat.DateObtentionBac = bac.DateObtentionBac;
                candidat.NoteBac = bac.NoteBac;
                candidat.MentionBac = bac.MentionBac;
                _db.SaveChanges();
                HttpContext.Session.SetInt32("steps", 3);
                return RedirectToAction("Step3");
            }
            return View(bac);
        }

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
            if (ModelState.IsValid)
            {
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
            if (candidat.Niveau == 0)
            {
                ModelState.AddModelError("selectNiveau", "Selectionner un niveau");
            }
            var y = _db.Candidats.Where(c => c.Cne == candidat.Cne).SingleOrDefault();
            if (y != null)
            {
                ModelState.AddModelError("UniqueCne", "Cne need to be unique");
            }
            var z = _db.Candidats.Where(c => c.Cin == candidat.Cin).SingleOrDefault();
            if (z != null)
            {
                ModelState.AddModelError("UniqueCin", "Cin need to be unique");
            }
            var w = _db.Candidats.Where(c => c.Email == candidat.Email).SingleOrDefault();
            if (w != null)
            {
                ModelState.AddModelError("UniqueEmail", "Email need to be unique");
            }


            if (ModelState.IsValid)
            {
                candidat.DateInscription = DateTime.Now;
                candidat.DateNaissance = DateTime.Now;
                Random random = new Random();
                const string pool = "abcdefghijklmnopqrstuvwxyz0123456789";
                var chars = Enumerable.Range(0, 7)
                    .Select(x => pool[random.Next(0, pool.Length)]);
                var charsMatricule = Enumerable.Range(0, 8)
                    .Select(ww => pool[random.Next(0, pool.Length)]);
                candidat.Matricule = new string(charsMatricule.ToArray()).ToUpper();
                candidat.Password = new string(chars.ToArray());
                candidat.Verified = 0;
                candidat.Photo = "icon.jpg";
                _db.Candidats.Add(candidat);
                _db.SaveChanges();

                Diplome dip = new Diplome();
                AnneeUniversitaire annUn = new AnneeUniversitaire();
                Baccalaureat bac = new Baccalaureat();
                ConcourEcrit concE = new ConcourEcrit();
                ConcourOral concO = new ConcourOral();


                //add row in diplome
                dip.Cne = candidat.Cne;
                _db.Diplomes.Add(dip);
                _db.SaveChanges();

                //add row in anne
                annUn.Cne = candidat.Cne;
                _db.AnneeUniversitaires.Add(annUn);
                _db.SaveChanges();

                //add row in bac
                bac.Cne = candidat.Cne;
                //bac.DateObtentionBac = DateTime.Now;
                _db.Baccalaureats.Add(bac);
                _db.SaveChanges();

                //add in concours ecrit
                concE.Cne = candidat.Cne;
                _db.CouncourEcrits.Add(concE);
                _db.SaveChanges();

                //add in concours oral
                concO.Cne = candidat.Cne;
                _db.CouncourOrals.Add(concO);
                _db.SaveChanges();


                var fromAddress = new MailAddress("admin@gmail.com", "From ENSAS");
                var toAddress = new MailAddress(candidat.Email, "To Name");
                const string fromPassword = "adminconcours125498";
                const string subject = "Récupération du mot de passe";
                //string body = "<a href=\"http://localhost:49969/Auth/Verify?cne="+candidat.Cne+" \">Link</a><br /><p> this is the password : "+candidat.Password+"</p>";
                string body = "<div class=\"container\"><div class=\"row\"><img src=\"https://lh3.googleusercontent.com/proxy/g_QnANEsQGJPGvR4haGBTi-kr2n32DU-eArBRKuJWtpgPCHQbz-RINzL6FzIc1TQs0a80Vfkaew6umTHHPQgHTE4l_g \" /></div><div class=\"row text-center\"><h2>Vous avez créer un compte dans la platforme d'acces au cycle d'ingénieur a ENSAS .</h2></div><div class=\"alert alert-danger\"><strong><span style=\"color:'red'\">Vous trouverez votre mot de pass au dessouss</span></strong><br></div><div class=\"row\"><div class=\"card\" style=\"width: 18rem;\"><div class=\"card-body\"><strong>Nom :</strong><span>" + candidat.Nom + "</span><br /><strong>Prenom : </strong><span>" + candidat.Prenom + "</span><br /><strong>CNE : </strong><span>" + candidat.Cne + "</span><br /><strong>CIN : </strong><span>" + candidat.Cin + "</span><br /><strong>Password : </strong><span>" + candidat.Password + "</span><br /></div></div></div></div>";


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
                    }
                    catch
                    {
                    }
                }
                
                TempData["message"] = "Votre mot de passe est : '" + candidat.Password + "'." + " Vous le trouverez sur votre email aussi.";
                return Redirect("Login");
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