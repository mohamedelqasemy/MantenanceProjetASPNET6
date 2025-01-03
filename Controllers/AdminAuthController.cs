using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MantenanceProjetASPNET6.Data;
using MantenanceProjetASPNET6.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MantenanceProjetASPNET6.Controllers
{
    public class AdminAuthController : Controller
    {

        //Context
        private readonly GestionConcourCoreDbContext db;

        public AdminAuthController(GestionConcourCoreDbContext db)
        {
            this.db = db;
        }
        
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(Admin admin)
        {
            if (ModelState.IsValid)
            {
                Admin adminSaisi = db.Admins.Where(a => a.Username.Equals(admin.Username) && a.Password.Equals(admin.Password)).SingleOrDefault();

                //Création Session
                if (adminSaisi != null)
                {
                    HttpContext.Session.SetString("admin", "true");
                    return RedirectToAction("Index", "Admin");
                }

                ViewBag.error = "Incorrect username or password !";

            }
            return View(admin);
        }


        public IActionResult LogOut()
        {
            HttpContext.Session.Remove("admin");
            return Redirect("Login");
        }


        public IActionResult Error()
        {
            return View();
        }

        
    }
}