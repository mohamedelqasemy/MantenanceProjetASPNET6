using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MantenanceProjetASPNET6.Models;
using Microsoft.AspNetCore.Http;
using MantenanceProjetASPNET6.Services;
using MantenanceProjetASPNET6.Data;
using System.Diagnostics;
using MantenanceProjetASPNET6.ViewModels;

namespace MantenanceProjetASPNET6.Controllers
{
    public class AdminController : Controller
    {
		private readonly GestionConcourCoreDbContext _context;


        private IIndexService index;
		private ISearch3Service search;
        private ICorbeil3Service corbeil;
        private ISelectionService selection;
        private IPreselectionService preselec;
        private IEnregistrementService enregistrement;
		private ICorrectionService corret;
		private readonly IEpreuveService epreuve;
        private IStatistiqueService statistique;

        public AdminController(GestionConcourCoreDbContext context, IIndexService index, ISearch3Service search,ICorbeil3Service corbeil,IEnregistrementService enregistrement,ISelectionService selection, IPreselectionService preselec, IEpreuveService epreuve, ICorrectionService corret, IStatistiqueService statistique)
        {
            this.index = index;
            this.search = search;
            this.corbeil = corbeil;
            this.selection = selection;
            this.preselec = preselec;
            this.epreuve = epreuve;
            this.enregistrement = enregistrement;
			this.corret = corret;
            this.statistique = statistique;
			_context = context;
            
		}

        public IActionResult Index()
        {
            if (!isAdmin())
            {
                return RedirectToAction("Login", "AdminAuth");

            }
                        
            return View(index.getIndexModel());

        }

        /*################################################  DEBUT  RECHERCHE  ################################################ */

        public IActionResult Recherche3()
        {
            if (isAdmin())
            {
                var x = search.generalSearch(3);
                return View(x);
            }
            return RedirectToAction("Login", "AdminAuth");
        }

        public IActionResult Recherche4()
        {
            if (isAdmin())
            {
                var x = search.generalSearch(4);
                return View(x);
            }
            return RedirectToAction("Login", "AdminAuth");
        }

        public IActionResult Corbeil3()
        {
            if (isAdmin())
            {
                var x = corbeil.afficheCorbeil(3);
                return View(x);
            }
            return RedirectToAction("Login", "AdminAuth");
        }

        public IActionResult Corbeil4()
        {
            if (isAdmin())
            {
                var x = corbeil.afficheCorbeil(4);
                return View(x);
            }
            return RedirectToAction("Login", "AdminAuth");
        }

        public JsonResult RestoreStudent(string cne, int Niveau)
        {
            var x = corbeil.restoreCorbeil(cne, Niveau);
            return Json(x);
        }
       
        public JsonResult SearchCriteria(string Criteria, string Key, string Diplome, string Filiere, int Niveau)
        {
            var x = search.specifiedSearch(Criteria, Key, Diplome, Filiere, Niveau);
            return Json(x);
        }

        public JsonResult SearchCriteriaCorb(string Criteria, string Key, string Diplome, string Filiere, int Niveau)
        {
            var x = corbeil.searchCriteria(Criteria, Key, Diplome, Filiere, Niveau);
            return Json(x);
        }

        //delete candidat
        public JsonResult deleteStudent(string cne, int Niveau)
        {
            var x = search.deleteCandidat(cne, Niveau);
            return Json(x);
        }

        public JsonResult conformeStudent(string cne, int Niveau)
        {
            var x = search.conformCandidat(cne, Niveau);
            return Json(x);
        }

        public JsonResult convoqueStudent(string cne, int Niveau)
        {
            var x = search.convoqueStudent(cne, Niveau);
            return Json(x);
        }

        /*###################################################  FIN  RECHERCHE  ############################################# */

        /*#################################################  DEBUT  PRESELECTION  ############################################# */

        public IActionResult Preselection3()
        {
            if (!isAdmin())
            {
                return RedirectToAction("Login", "AdminAuth");
            }
            return View();
        }

        public IActionResult Preselection4()
        {
            if (!isAdmin())
            {
                return RedirectToAction("Login", "AdminAuth");
            }
            
            return View();
            
        }

        public JsonResult CalculerPreselec4(string fil, string diplome, int Cs1, int Cs2, int Cs3, int Cs4, int Cs5, int Cs6, int Cbac, string seuil, int niv)
        {
            ConfigurationPreselection conf = new ConfigurationPreselection()
            {
                Filiere = fil,
                TypeDiplome = diplome,
                CoeffBac = Cbac,
                CoeffS1 = Cs1,
                CoeffS2 = Cs2,
                CoeffS3 = Cs3,
                CoeffS4 = Cs4,
                CoeffS5 = Cs5,
                CoeffS6 = Cs6,
                NoteSeuil = Convert.ToDouble(seuil.Replace(".", ","))
            };

            preselec.setConfig(conf, niv);
            preselec.calculerPreselec(niv, fil, diplome);
            var x = preselec.getConvoques(niv, fil, diplome);

            return Json(x);
        }

        public JsonResult CalculerPreselec(string fil, string diplome, int Cs1, int Cs2, int Cs3, int Cs4, int Cbac, string seuil, int niv)
        {
            ConfigurationPreselection conf = new ConfigurationPreselection()
            {
                Filiere = fil,
                TypeDiplome = diplome,
                CoeffBac = Cbac,
                CoeffS1 = Cs1,
                CoeffS2 = Cs2,
                CoeffS3 = Cs3,
                CoeffS4 = Cs4,
                NoteSeuil = Convert.ToDouble(seuil.Replace(".", ","))
            };

            preselec.setConfig(conf, niv);

            preselec.calculerPreselec(niv, fil, diplome);
            var x = preselec.getConvoques(niv, fil, diplome);

            return Json(x);
        }

        public JsonResult GetConfig(string fil, string diplome)
        {
            ConfigurationPreselection list = preselec.getConfig(fil, diplome);
            return Json(list);
        }

        public JsonResult GetConvoquets(string fil, string diplome, int niv)
        {
            var list = preselec.getConvoques(niv, fil, diplome);
            return Json(list);
        }

        public JsonResult GetPourcentage(string fil, string diplome, int niv)
        {
            var list = preselec.getPourcentage(niv, fil, diplome);
            return Json(list);
        }

        /*#################################################  FIN  PRESELECTION  ############################################# */

        public IActionResult Statistique3()
        {
            if (!isAdmin())
            {
                return RedirectToAction("Login", "AdminAuth");
            }
            return View();
        }

        public IActionResult Statistique4()
        {
            if (!isAdmin())
            {
                return RedirectToAction("Login", "AdminAuth");
            }
            return View();
        }

        public IActionResult Correction3()
        {
            if (!isAdmin())
            {
                return RedirectToAction("Login", "AdminAuth");
            }
            return View();
        }

        public IActionResult Correction4()
        {
            if (!isAdmin())
            {
                return RedirectToAction("Login", "AdminAuth");
            }
            return View();
        }

        /*###################################################  DEBUT  SELECTION  ############################################# */
        public IActionResult Selection3()
        {
            if (!isAdmin())
            {
                return RedirectToAction("Login", "AdminAuth");
            }
            return View();
        }

        public IActionResult Selection4()
        {
            if (!isAdmin())
            {
                return RedirectToAction("Login", "AdminAuth");
            }
            return View();
        }

        public IActionResult ListeFinale3()
        {
            if (!isAdmin())
            {
                return RedirectToAction("Login", "AdminAuth");
            }
            return View();
        }

        public IActionResult ListFinale4()
        {
            if (!isAdmin())
            {
                return RedirectToAction("Login", "AdminAuth");
            }
            return View();
        }

        public JsonResult GetConfigurationSelectiona()
        {
            return Json("shut");
        }

        public JsonResult SearchCriteriah(string Criteria, string Key, string Diplome, string Filiere, int Niveau)
        {
            var x = search.specifiedSearch(Criteria, Key, Diplome, Filiere, Niveau);
            return Json(x);
        }
        public JsonResult GetConfigurationSelection(string filiere, int nv)
        {
            
            var data = selection.getConfigurationSelection(filiere, nv);

            return Json(data);
        }

        public JsonResult SetConfigurationSelection(string f, int cs, int np, int la, double nm, int cm, string cl, string nv)
        {
            ConfigurationSelection conf = new ConfigurationSelection();

            conf.Filiere = f;
            conf.CoeffMath = cm;
            conf.CoeffSpecialite = cs;
            conf.NbrPlace = np;
            conf.NoteMin = nm;
            conf.NbrPlaceListAtt = la;
            conf.TypeClassement = cl;
            conf.Niveau = nv;

            selection.updateConfigurationSelection(conf);
            if (nv == "3")
            {
                selection.calculeNoteGlobale(f);
            }

            var data = selection.selectStudents(f, nv);
            return Json(data);
        }

        public JsonResult GetListePrincipal(string filiere)
        {
            var data = selection.getListPrincipale(filiere);
            return Json(data);
        }


        public JsonResult GetListeAttente(string filiere)
        {
            var data = selection.getListAttente(filiere);
            return Json(data);
        }

        public JsonResult GetListePrincipalSup(string filiere)
        {
            var data = selection.getListPrincipaleSup(filiere);
            return Json(data);
        }

        public JsonResult GetListeAttenteSup(string filiere)
        {
            var data = selection.getListAttenteSup(filiere);
            return Json(data);
        }

        /*###################################################  FIN  SELECTION  ############################################# */

        /*###################################################  DEBUT  Fichier Scanné  ############################################# */

        public JsonResult FichierScanne(string cne, int niveau)
        {
            var result = epreuve.diplomeFile(cne, niveau);
            return Json(result);
        }

        /*###################################################  FIN  DEBUT  Fichier Scanné  ############################################# */

        public IActionResult Statistique3ApresConcours()
        {
            if (!isAdmin())
            {
                return RedirectToAction("Login", "AdminAuth");
            }
            return View();
        }

        public IActionResult Statistique4ApresConcours()
        {
            if (!isAdmin())
            {
                return RedirectToAction("Login", "AdminAuth");
            }
            return View();
        }

        public IActionResult FichierScanne3()
        {
            if (!isAdmin())
            {
                return RedirectToAction("Login", "AdminAuth");
            }
            return View();
        }

        public IActionResult FichierScanne4()
        {
            if (!isAdmin())
            {
                return RedirectToAction("Login", "AdminAuth");
            }
            return View();
        }

        public IActionResult Enregistrement()
        {
            if (!isAdmin())
            {
                return RedirectToAction("Login", "AdminAuth");
            }
            return View();
        }

        public IActionResult EnregistrementCin()
        {
            if (!isAdmin())
            {
                return RedirectToAction("Login", "AdminAuth");
            }
            return View();
        }


        /*###################################################  DEBUT  EPREUVE  ############################################# */

        public IActionResult UploadEpreuve(string ficher)
        {
            if (!isAdmin())
            {
                return RedirectToAction("Login", "AdminAuth");
            }
            return View();            
        }

        [HttpPost]
        public IActionResult UploadEpreuve(UploadModel model)
        {            
            if (ModelState.IsValid)
            {
                int msg = epreuve.Upload(model);
                if (msg == 1)
                {   TempData["succes"] = "Fichier enregistré avec succes";  }
                else
                {   TempData["error"] = "Erreur lors d'enregistrement !!!";  }

                return View();
            }

            return View(model);
        }

        /*###################################################  FIN  EPREUVE  ############################################# */

        /*###################################################  ENREGISTREMENT  ############################################# */

        //lister les candidats par niveau :

        public JsonResult getEtudiantByNiveau(int niveau)
        {
            var x = enregistrement.listetByNiveau(niveau);
            
            return Json(x);
        }

        public JsonResult generateNumDossier(string cin)
        {
            enregistrement.enregisterByCin(cin);
            return Json("test");
        }

        //recuperer les infos sur un candidat
        public JsonResult etudiantByCin(string cin)
        {
            var infos = enregistrement.infosCandidatByCin(cin);
            return Json(infos);
        }



        /*################################################### FIN ENREGISTREMENT  ############################################# */
        


        private bool isAdmin()
        {
            string admin = HttpContext.Session.GetString("admin");
            if (admin != null)
            {
                if (admin.Equals("true"))
                { return true; }
            }
            return false;
        }


		/*################################################  CORRECTION  ################################################ */

		

		public IActionResult INFO()
		{
			var model = new UserViewModel();
			model.NoteSpecialite = new List<double>();
			//model.Classement = new List<int>();
			model.Cne = new List<string>();
			model.NoteMath = new List<double>();

			model.Num_dossier = new List<int>();
			model.Cin = new List<string>();
			model.Prenom = new List<string>();
			model.Filiere = new List<string>();
			model.Diplome = new List<string>();
			model.Nom = new List<string>();









		string type_fil = "informatique";
			if (!isAdmin())
			{
				return RedirectToAction("Login", "AdminAuth");
			}
			var x = corret.corr(type_fil);
			foreach (var item in x)
			{
				model.Cne.Add(item.Cne);
				model.NoteSpecialite.Add(item.NoteSpecialite);
				model.NoteMath.Add(item.NoteMath);
				model.Num_dossier.Add(item.Num_dossier);
				model.Cin.Add(item.Cin);
				model.Prenom.Add(item.Prenom);
				model.Filiere.Add(item.Filiere);
				model.Diplome.Add(item.Diplome);
				model.Nom.Add(item.Nom);

			}
			return View(model);
		}




	[HttpPost]
		public ActionResult INFO_Post(UserViewModel etudiants)
		{

		

				for (int i = 0; i < etudiants.Cne.Count; i++)
			{
			
				ConcourEcrit con = _context.CouncourEcrits.Find(etudiants.Cne[i]);
				con.NoteMath = etudiants.NoteMath[i];
				con.NoteSpecialite = etudiants.NoteSpecialite[i];
				_context.SaveChanges();
			}

			return RedirectToAction("index");

		}



		public ActionResult GTR()
		{
		   string type_fil = "gtr";
			var model = new UserViewModel();
			model.NoteSpecialite = new List<double>();
			//model.Classement = new List<int>();
			model.Cne = new List<string>();
			model.NoteMath = new List<double>();

			model.Num_dossier = new List<int>();
			model.Cin = new List<string>();
			model.Prenom = new List<string>();
			model.Filiere = new List<string>();
			model.Diplome = new List<string>();
			model.Nom = new List<string>();

			if (!isAdmin())
			{
				return RedirectToAction("Login", "AdminAuth");
			}
			var x = corret.corr(type_fil);
			foreach (var item in x)
			{
				model.Cne.Add(item.Cne);
				model.NoteSpecialite.Add(item.NoteSpecialite);
				model.NoteMath.Add(item.NoteMath);
				model.Num_dossier.Add(item.Num_dossier);
				model.Cin.Add(item.Cin);
				model.Prenom.Add(item.Prenom);
				model.Filiere.Add(item.Filiere);
				model.Diplome.Add(item.Diplome);
				model.Nom.Add(item.Nom);

			}
			return View(model);
		}



		[HttpPost]
		public ActionResult GTR_Post(UserViewModel etudiants)
		{
			for (int i = 0; i < etudiants.Cne.Count; i++)
			{

				ConcourEcrit con = _context.CouncourEcrits.Find(etudiants.Cne[i]);
				con.NoteMath = etudiants.NoteMath[i];
				con.NoteSpecialite = etudiants.NoteSpecialite[i];
				_context.SaveChanges();
			}

			return RedirectToAction("index");

		}

		public ActionResult GPMC()
		{
		  string type_fil = "gpmc";
			var model = new UserViewModel();
			model.NoteSpecialite = new List<double>();
			//model.Classement = new List<int>();
			model.Cne = new List<string>();
			model.NoteMath = new List<double>();

			model.Num_dossier = new List<int>();
			model.Cin = new List<string>();
			model.Prenom = new List<string>();
			model.Filiere = new List<string>();
			model.Diplome = new List<string>();
			model.Nom = new List<string>();

			if (!isAdmin())
			{
				return RedirectToAction("Login", "AdminAuth");
			}
			var x = corret.corr(type_fil);
			foreach (var item in x)
			{
				model.Cne.Add(item.Cne);
				model.NoteSpecialite.Add(item.NoteSpecialite);
				model.NoteMath.Add(item.NoteMath);
				model.Num_dossier.Add(item.Num_dossier);
				model.Cin.Add(item.Cin);
				model.Prenom.Add(item.Prenom);
				model.Filiere.Add(item.Filiere);
				model.Diplome.Add(item.Diplome);
				model.Nom.Add(item.Nom);

			}
			return View(model);
		}



		[HttpPost]
		public ActionResult GPMC_Post(UserViewModel etudiants)
		{
			for (int i = 0; i < etudiants.Cne.Count; i++)
			{

				ConcourEcrit con = _context.CouncourEcrits.Find(etudiants.Cne[i]);
				con.NoteMath = etudiants.NoteMath[i];
				con.NoteSpecialite = etudiants.NoteSpecialite[i];
				_context.SaveChanges();
			}

			return RedirectToAction("index");

		}

		public ActionResult INDUS()
		{
		  string type_fil = "indus";
			var model = new UserViewModel();
			model.NoteSpecialite = new List<double>();
			//model.Classement = new List<int>();
			model.Cne = new List<string>();
			model.NoteMath = new List<double>();

			model.Num_dossier = new List<int>();
			model.Cin = new List<string>();
			model.Prenom = new List<string>();
			model.Filiere = new List<string>();
			model.Diplome = new List<string>();
			model.Nom = new List<string>();

			if (!isAdmin())
			{
				return RedirectToAction("Login", "AdminAuth");
			}
			var x = corret.corr(type_fil);
			foreach (var item in x)
			{
				model.Cne.Add(item.Cne);
				model.NoteSpecialite.Add(item.NoteSpecialite);
				model.NoteMath.Add(item.NoteMath);
				model.Num_dossier.Add(item.Num_dossier);
				model.Cin.Add(item.Cin);
				model.Prenom.Add(item.Prenom);
				model.Filiere.Add(item.Filiere);
				model.Diplome.Add(item.Diplome);
				model.Nom.Add(item.Nom);

			}
			return View(model);
		}



		[HttpPost]
		public ActionResult INDUS_Post(UserViewModel etudiants)
		{
			for (int i = 0; i < etudiants.Cne.Count; i++)
			{

				ConcourEcrit con = _context.CouncourEcrits.Find(etudiants.Cne[i]);
				con.NoteMath = etudiants.NoteMath[i];
				con.NoteSpecialite = etudiants.NoteSpecialite[i];
				_context.SaveChanges();
			}

			return RedirectToAction("index");

		}



		public ActionResult INFO4()
		{
		string type_fil = "informatique";
			var model = new UserViewModel();
			model.Classement = new List<int>();
			model.Cne = new List<string>();


			model.Num_dossier = new List<int>();
			model.Cin = new List<string>();
			model.Prenom = new List<string>();
			model.Filiere = new List<string>();
			model.Diplome = new List<string>();
			model.Etablissement  = new List<string>();
			model.Nom = new List<string>();
			model.VilleObtention = new List<string>();
			model.Specialite = new List<string>();


			if (!isAdmin())
			{
				return RedirectToAction("Login", "AdminAuth");
			}
			var x = corret.corr4(type_fil);
			foreach (var item in x)
			{				 
				model.Cne.Add(item.Cne);
				model.Num_dossier.Add(item.Num_dossier);
				model.Cin.Add(item.Cin);
				model.Prenom.Add(item.Prenom);
				model.Filiere.Add(item.Filiere);
				model.Diplome.Add(item.Diplome);
				model.Nom.Add(item.Nom);
				model.Etablissement.Add(item.Etablissement);
				model.VilleObtention.Add(item.VilleObtention);
				model.Specialite.Add(item.Specialite);
				model.Classement.Add(item.Classement);



			}
			return View(model);
		}



		[HttpPost]
		public ActionResult INFO_Post4(UserViewModel etudiants)
		{
			for (int i = 0; i < etudiants.Cne.Count; i++)
			{

				ConcourOral con = _context.CouncourOrals.Find(etudiants.Cne[i]);
				con.Classement = etudiants.Classement[i];
				_context.SaveChanges();
			}

			return RedirectToAction("index");


			

		}


		public ActionResult GTR4()
		{
		  string type_fil = "gtr";
			var model = new UserViewModel();
			model.Classement = new List<int>();
			model.Cne = new List<string>();


			model.Num_dossier = new List<int>();
			model.Cin = new List<string>();
			model.Prenom = new List<string>();
			model.Filiere = new List<string>();
			model.Diplome = new List<string>();
			model.Etablissement = new List<string>();
			model.Nom = new List<string>();
			model.VilleObtention = new List<string>();
			model.Specialite = new List<string>();


			if (!isAdmin())
			{
				return RedirectToAction("Login", "AdminAuth");
			}
			var x = corret.corr4(type_fil);
			foreach (var item in x)
			{
				model.Cne.Add(item.Cne);
				model.Num_dossier.Add(item.Num_dossier);
				model.Cin.Add(item.Cin);
				model.Prenom.Add(item.Prenom);
				model.Filiere.Add(item.Filiere);
				model.Diplome.Add(item.Diplome);
				model.Nom.Add(item.Nom);
				model.Etablissement.Add(item.Etablissement);
				model.VilleObtention.Add(item.VilleObtention);
				model.Specialite.Add(item.Specialite);
				model.Classement.Add(item.Classement);

			}
			return View(model);
		}

		

		[HttpPost]
		public ActionResult GTR_Post4(UserViewModel etudiants)
		{

			for (int i = 0; i < etudiants.Cne.Count; i++)
			{

				ConcourOral con = _context.CouncourOrals.Find(etudiants.Cne[i]);
				con.Classement = etudiants.Classement[i];
				_context.SaveChanges();
			}

			return RedirectToAction("index");

		}


		public ActionResult GPMC4()
		{
			string type_fil = "gpmc";
			var model = new UserViewModel();
			model.Classement = new List<int>();
			model.Cne = new List<string>();


			model.Num_dossier = new List<int>();
			model.Cin = new List<string>();
			model.Prenom = new List<string>();
			model.Filiere = new List<string>();
			model.Diplome = new List<string>();
			model.Etablissement = new List<string>();
			model.Nom = new List<string>();
			model.VilleObtention = new List<string>();
			model.Specialite = new List<string>();


			if (!isAdmin())
			{
				return RedirectToAction("Login", "AdminAuth");
			}
			var x = corret.corr4(type_fil);
			foreach (var item in x)
			{
				model.Cne.Add(item.Cne);
				model.Num_dossier.Add(item.Num_dossier);
				model.Cin.Add(item.Cin);
				model.Prenom.Add(item.Prenom);
				model.Filiere.Add(item.Filiere);
				model.Diplome.Add(item.Diplome);
				model.Nom.Add(item.Nom);
				model.Etablissement.Add(item.Etablissement);
				model.VilleObtention.Add(item.VilleObtention);
				model.Specialite.Add(item.Specialite);
				model.Classement.Add(item.Classement);

			}
			return View(model);
		}



		[HttpPost]
		public ActionResult GPMC_Post4(UserViewModel etudiants)
		{
			for (int i = 0; i < etudiants.Cne.Count; i++)
			{

				ConcourOral con = _context.CouncourOrals.Find(etudiants.Cne[i]);
				con.Classement = etudiants.Classement[i];
				_context.SaveChanges();
			}

			return RedirectToAction("index");
		}



		public ActionResult INDUS4()
		{
			string type_fil = "indus";
			var model = new UserViewModel();
			model.Classement = new List<int>();
			model.Cne = new List<string>();


			model.Num_dossier = new List<int>();
			model.Cin = new List<string>();
			model.Prenom = new List<string>();
			model.Filiere = new List<string>();
			model.Diplome = new List<string>();
			model.Etablissement = new List<string>();
			model.Nom = new List<string>();
			model.VilleObtention = new List<string>();
			model.Specialite = new List<string>();


			if (!isAdmin())
			{
				return RedirectToAction("Login", "AdminAuth");
			}
			var x = corret.corr4(type_fil);
			foreach (var item in x)
			{
				model.Cne.Add(item.Cne);
				model.Num_dossier.Add(item.Num_dossier);
				model.Cin.Add(item.Cin);
				model.Prenom.Add(item.Prenom);
				model.Filiere.Add(item.Filiere);
				model.Diplome.Add(item.Diplome);
				model.Nom.Add(item.Nom);
				model.Etablissement.Add(item.Etablissement);
				model.VilleObtention.Add(item.VilleObtention);
				model.Specialite.Add(item.Specialite);
				model.Classement.Add(item.Classement);

			}
			return View(model);
		}



		[HttpPost]
		public ActionResult INDUS_Post4(UserViewModel etudiants)
		{
			for (int i = 0; i < etudiants.Cne.Count; i++)
			{

				ConcourOral con = _context.CouncourOrals.Find(etudiants.Cne[i]);
				con.Classement = etudiants.Classement[i];
				_context.SaveChanges();
			}

			return RedirectToAction("index");

		}
        ///!!!!!!!!!!!!!!!!!!!!---------statistique-----------!!!!!!!!!!\\\
        public ActionResult Statistique3A()
        {
            if (isAdmin())
            {
                
                    List<Statistique> statistiquesPre = new List<Statistique>();
                    List<Statistique> statistiquesConvoque = new List<Statistique>();

                    List<Filiere> filieres = new List<Filiere>();

                    Statistique statPerDiplomPre = new Statistique();
                    Statistique statPerDiplomConv = new Statistique();

                    

                        filieres = statistique.GetFilieres();
                        foreach (var f in filieres)
                        {
                            statistiquesPre.Add(new Statistique
                            {
                                nbDeug = statistique.getCandidatPreisncrit(f.ID, "deug", 3),
                                nbDut = statistique.getCandidatPreisncrit(f.ID, "dut", 3),
                                nbLicenceFnd = statistique.getCandidatPreisncrit(f.ID, "Lic.fnd", 3),
                                nbLicencePro = statistique.getCandidatPreisncrit(f.ID, "Lic.pro", 3),
                                nbLicenceSt = statistique.getCandidatPreisncrit(f.ID, "Lic.st", 3),
                                nomFiliere = f.Nom
                            });
                            statistiquesConvoque.Add(new Statistique
                            {
                                nbDeug = statistique.getCandidatConv(f.ID, "deug", 3),
                                nbDut = statistique.getCandidatConv(f.ID, "dut", 3),
                                nbLicenceFnd = statistique.getCandidatConv(f.ID, "Lic.fnd", 3),
                                nbLicencePro = statistique.getCandidatConv(f.ID, "Lic.pro", 3),
                                nbLicenceSt = statistique.getCandidatConv(f.ID, "Lic.st", 3),
                                nomFiliere = f.Nom
                            });
                        }
                        statPerDiplomPre = new Statistique
                        {
                            nbDeug = statistique.getNbCandidatPerDiplome("deug", 3),
                            nbDut = statistique.getNbCandidatPerDiplome("dut", 3),
                            nbLicenceFnd = statistique.getNbCandidatPerDiplome("Lic.fnd", 3),
                            nbLicenceSt = statistique.getNbCandidatPerDiplome("Lic.st", 3),
                            nbLicencePro = statistique.getNbCandidatPerDiplome("Lic.pro", 3),
                        };
                        statPerDiplomConv = new Statistique
                        {
                            nbDeug = statistique.getNbCandidatPerDiplomeConv("deug", 3),
                            nbDut = statistique.getNbCandidatPerDiplomeConv("dut", 3),
                            nbLicenceFnd = statistique.getNbCandidatPerDiplomeConv("Lic.fnd", 3),
                            nbLicenceSt = statistique.getNbCandidatPerDiplomeConv("Lic.st", 3),
                            nbLicencePro = statistique.getNbCandidatPerDiplomeConv("Lic.pro", 3),
                        };
                    

                    ViewData["statistiquesPre"] = statistiquesPre;
                    ViewData["statistiquesConvoque"] = statistiquesConvoque;
                    ViewBag.statPerDiplomPre = statPerDiplomPre;
                    ViewBag.statPerDiplomConv = statPerDiplomConv;
                    return View();
                
            }
            return RedirectToAction("Login", "AdminAuth");
        }
        public ActionResult Statistique4A()
        {
            if (isAdmin())
            {
                
                    List<Statistique> statistiquesPre = new List<Statistique>();
                    List<Statistique> statistiquesConvoque = new List<Statistique>();

                    List<Filiere> filieres = new List<Filiere>();

                    Statistique statPerDiplomPre = new Statistique();
                    Statistique statPerDiplomConv = new Statistique();

                    

                        filieres = statistique.GetFilieres();
                        foreach (var f in filieres)
                        {
                            statistiquesPre.Add(new Statistique
                            {
                                nbDeug = statistique.getCandidatPreisncrit(f.ID, "deug", 4),
                                nbDut = statistique.getCandidatPreisncrit(f.ID, "dut", 4),
                                nbLicenceFnd = statistique.getCandidatPreisncrit(f.ID, "Lic.fnd", 4),
                                nbLicencePro = statistique.getCandidatPreisncrit(f.ID, "Lic.pro", 4),
                                nbLicenceSt = statistique.getCandidatPreisncrit(f.ID, "Lic.st", 4),
                                nomFiliere = f.Nom
                            });
                            statistiquesConvoque.Add(new Statistique
                            {
                                nbDeug = statistique.getCandidatConv(f.ID, "deug", 4),
                                nbDut = statistique.getCandidatConv(f.ID, "dut", 4),
                                nbLicenceFnd = statistique.getCandidatConv(f.ID, "Lic.fnd", 4),
                                nbLicencePro = statistique.getCandidatConv(f.ID, "Lic.pro", 4),
                                nbLicenceSt = statistique.getCandidatConv(f.ID, "Lic.st", 4),
                                nomFiliere = f.Nom
                            });
                        }
                        statPerDiplomPre = new Statistique
                        {
                            nbDeug = statistique.getNbCandidatPerDiplome("deug", 4),
                            nbDut = statistique.getNbCandidatPerDiplome("dut", 4),
                            nbLicenceFnd = statistique.getNbCandidatPerDiplome("Lic.fnd", 4),
                            nbLicenceSt = statistique.getNbCandidatPerDiplome("Lic.st", 4),
                            nbLicencePro = statistique.getNbCandidatPerDiplome("Lic.pro", 4),
                        };
                        statPerDiplomConv = new Statistique
                        {
                            nbDeug = statistique.getNbCandidatPerDiplomeConv("deug", 4),
                            nbDut = statistique.getNbCandidatPerDiplomeConv("dut", 4),
                            nbLicenceFnd = statistique.getNbCandidatPerDiplomeConv("Lic.fnd", 4),
                            nbLicenceSt = statistique.getNbCandidatPerDiplomeConv("Lic.st", 4),
                            nbLicencePro = statistique.getNbCandidatPerDiplomeConv("Lic.pro", 4),
                        };
                    

                    ViewData["statistiquesPre"] = statistiquesPre;
                    ViewData["statistiquesConvoque"] = statistiquesConvoque;
                    ViewBag.statPerDiplomPre = statPerDiplomPre;
                    ViewBag.statPerDiplomConv = statPerDiplomConv;
                    return View();
                
            }
            return RedirectToAction("Login", "AdminAuth");
        }
        public ActionResult Statistique3ApresConcour()
        {
            if (isAdmin())
            {
                
                
                    List<Statistique> statistiquesPresent = new List<Statistique>();
                    List<Statistique> statistiquesPrincipal = new List<Statistique>();
                    List<Statistique> statistiquesAttente = new List<Statistique>();


                    List<Filiere> filieres = new List<Filiere>();

                    Statistique statPerDiplomPresent = new Statistique();
                    Statistique statPerDiplomPrincipale = new Statistique();
                    Statistique statPerDiplomAtt = new Statistique();


                    

                        filieres = statistique.GetFilieres();
                        foreach (var f in filieres)
                        {
                            statistiquesPresent.Add(new Statistique
                            {
                                nbDeug = statistique.getCandidatPresent(f.ID, "deug", 3),
                                nbDut = statistique.getCandidatPresent(f.ID, "dut", 3),
                                nbLicenceFnd = statistique.getCandidatPresent(f.ID, "Lic.fnd", 3),
                                nbLicencePro = statistique.getCandidatPresent(f.ID, "Lic.pro", 3),
                                nbLicenceSt = statistique.getCandidatPresent(f.ID, "Lic.st", 3),
                                nomFiliere = f.Nom
                            });
                            statistiquesPrincipal.Add(new Statistique
                            {
                                nbDeug = statistique.getCandidatPrincipale(f.ID, "deug", 3),
                                nbDut = statistique.getCandidatPrincipale(f.ID, "dut", 3),
                                nbLicenceFnd = statistique.getCandidatPrincipale(f.ID, "Lic.fnd", 3),
                                nbLicencePro = statistique.getCandidatPrincipale(f.ID, "Lic.pro", 3),
                                nbLicenceSt = statistique.getCandidatPrincipale(f.ID, "Lic.st", 3),
                                nomFiliere = f.Nom
                            });
                            statistiquesAttente.Add(new Statistique
                            {
                                nbDeug = statistique.getCandidatListeAtt(f.ID, "deug", 3),
                                nbDut = statistique.getCandidatListeAtt(f.ID, "dut", 3),
                                nbLicenceFnd = statistique.getCandidatListeAtt(f.ID, "Lic.fnd", 3),
                                nbLicencePro = statistique.getCandidatListeAtt(f.ID, "Lic.pro", 3),
                                nbLicenceSt = statistique.getCandidatListeAtt(f.ID, "Lic.st", 3),
                                nomFiliere = f.Nom
                            });
                        }
                        statPerDiplomPresent = new Statistique
                        {
                            nbDeug = statistique.getNbCandidatPresentPerDiplome("deug", 3),
                            nbDut = statistique.getNbCandidatPresentPerDiplome("dut", 3),
                            nbLicenceFnd = statistique.getNbCandidatPresentPerDiplome("Lic.fnd", 3),
                            nbLicenceSt = statistique.getNbCandidatPresentPerDiplome("Lic.st", 3),
                            nbLicencePro = statistique.getNbCandidatPresentPerDiplome("Lic.pro", 3),
                        };
                        statPerDiplomPrincipale = new Statistique
                        {
                            nbDeug = statistique.getNbCandidatPrincipalPerDiplome("deug", 3),
                            nbDut = statistique.getNbCandidatPrincipalPerDiplome("dut", 3),
                            nbLicenceFnd = statistique.getNbCandidatPrincipalPerDiplome("Lic.fnd", 3),
                            nbLicenceSt = statistique.getNbCandidatPrincipalPerDiplome("Lic.st", 3),
                            nbLicencePro = statistique.getNbCandidatPrincipalPerDiplome("Lic.pro", 3),
                        };
                        statPerDiplomAtt = new Statistique
                        {
                            nbDeug = statistique.getNbCandidatAttPerDiplome("deug", 3),
                            nbDut = statistique.getNbCandidatAttPerDiplome("dut", 3),
                            nbLicenceFnd = statistique.getNbCandidatAttPerDiplome("Lic.fnd", 3),
                            nbLicenceSt = statistique.getNbCandidatAttPerDiplome("Lic.st", 3),
                            nbLicencePro = statistique.getNbCandidatAttPerDiplome("Lic.pro", 3),
                        };

                    

                    ViewData["statistiquesPresent"] = statistiquesPresent;
                    ViewData["statistiquesPrincipal"] = statistiquesPrincipal;
                    ViewData["statistiquesAttente"] = statistiquesAttente;
                    ViewBag.statPerDiplomPresent = statPerDiplomPresent;
                    ViewBag.statPerDiplomPrincipale = statPerDiplomPrincipale;
                    ViewBag.statPerDiplomAtt = statPerDiplomAtt;
                    return View();
                
            }
            return RedirectToAction("Login", "AdminAuth");
        }
        public ActionResult Statistique4ApresConcour()
        {
            if (isAdmin())
            {
                
                    List<Statistique> statistiquesPresent = new List<Statistique>();
                    List<Statistique> statistiquesPrincipal = new List<Statistique>();
                    List<Statistique> statistiquesAttente = new List<Statistique>();


                    List<Filiere> filieres = new List<Filiere>();

                    Statistique statPerDiplomPresent = new Statistique();
                    Statistique statPerDiplomPrincipale = new Statistique();
                    Statistique statPerDiplomAtt = new Statistique();


                    

                        filieres = statistique.GetFilieres();
                        foreach (var f in filieres)
                        {
                            statistiquesPresent.Add(new Statistique
                            {
                                nbDeug = statistique.getCandidatPresent(f.ID, "deug", 4),
                                nbDut = statistique.getCandidatPresent(f.ID, "dut", 4),
                                nbLicenceFnd = statistique.getCandidatPresent(f.ID, "Lic.fnd", 4),
                                nbLicencePro = statistique.getCandidatPresent(f.ID, "Lic.pro", 4),
                                nbLicenceSt = statistique.getCandidatPresent(f.ID, "Lic.st", 4),
                                nomFiliere = f.Nom
                            });
                            statistiquesPrincipal.Add(new Statistique
                            {
                                nbDeug = statistique.getCandidatPrincipale(f.ID, "deug", 4),
                                nbDut = statistique.getCandidatPrincipale(f.ID, "dut", 4),
                                nbLicenceFnd = statistique.getCandidatPrincipale(f.ID, "Lic.fnd", 4),
                                nbLicencePro = statistique.getCandidatPrincipale(f.ID, "Lic.pro", 4),
                                nbLicenceSt = statistique.getCandidatPrincipale(f.ID, "Lic.st", 4),
                                nomFiliere = f.Nom
                            });
                            statistiquesAttente.Add(new Statistique
                            {
                                nbDeug = statistique.getCandidatListeAtt(f.ID, "deug", 4),
                                nbDut = statistique.getCandidatListeAtt(f.ID, "dut", 3),
                                nbLicenceFnd = statistique.getCandidatListeAtt(f.ID, "Lic.fnd", 4),
                                nbLicencePro = statistique.getCandidatListeAtt(f.ID, "Lic.pro", 4),
                                nbLicenceSt = statistique.getCandidatListeAtt(f.ID, "Lic.st", 4),
                                nomFiliere = f.Nom
                            });
                        }
                        statPerDiplomPresent = new Statistique
                        {
                            nbDeug = statistique.getNbCandidatPresentPerDiplome("deug", 4),
                            nbDut = statistique.getNbCandidatPresentPerDiplome("dut", 4),
                            nbLicenceFnd = statistique.getNbCandidatPresentPerDiplome("Lic.fnd", 4),
                            nbLicenceSt = statistique.getNbCandidatPresentPerDiplome("Lic.st", 4),
                            nbLicencePro = statistique.getNbCandidatPresentPerDiplome("Lic.pro", 4),
                        };
                        statPerDiplomPrincipale = new Statistique
                        {
                            nbDeug = statistique.getNbCandidatPrincipalPerDiplome("deug", 4),
                            nbDut = statistique.getNbCandidatPrincipalPerDiplome("dut", 4),
                            nbLicenceFnd = statistique.getNbCandidatPrincipalPerDiplome("Lic.fnd", 4),
                            nbLicenceSt = statistique.getNbCandidatPrincipalPerDiplome("Lic.st", 4),
                            nbLicencePro = statistique.getNbCandidatPrincipalPerDiplome("Lic.pro", 4),
                        };
                        statPerDiplomAtt = new Statistique
                        {
                            nbDeug = statistique.getNbCandidatAttPerDiplome("deug", 4),
                            nbDut = statistique.getNbCandidatAttPerDiplome("dut", 4),
                            nbLicenceFnd = statistique.getNbCandidatAttPerDiplome("Lic.fnd", 4),
                            nbLicenceSt = statistique.getNbCandidatAttPerDiplome("Lic.st", 4),
                            nbLicencePro = statistique.getNbCandidatAttPerDiplome("Lic.pro", 4),
                        };
            
                    

                    ViewData["statistiquesPresent"] = statistiquesPresent;
                    ViewData["statistiquesPrincipal"] = statistiquesPrincipal;
                    ViewData["statistiquesAttente"] = statistiquesAttente;
                    ViewBag.statPerDiplomPresent = statPerDiplomPresent;
                    ViewBag.statPerDiplomPrincipale = statPerDiplomPrincipale;
                    ViewBag.statPerDiplomAtt = statPerDiplomAtt;
                    return View();
                
            }
            return RedirectToAction("Login", "AdminAuth");
        }


    }
}
