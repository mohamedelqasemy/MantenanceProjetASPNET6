using MantenanceProjetASPNET6.Data;
using MantenanceProjetASPNET6.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MantenanceProjetASPNET6.Services
{
	public class CorrectionServiceImp : ICorrectionService
	{
		private readonly GestionConcourCoreDbContext db;
		public CorrectionServiceImp(GestionConcourCoreDbContext db)
		{
			this.db = db;
		}

		public object NoteMath { get; private set; }

		public IEnumerable<CorrectionModel> corr(string type_fil)
		{
			 
		var res = (from c in db.Candidats
					   join d in db.Diplomes on c.Cne equals d.Cne
					   join e in db.Filieres on c.ID equals e.ID
					   join b in db.CouncourEcrits on c.Cne equals b.Cne
					   where (c.Presence == true && c.Niveau == 3 && e.Nom == type_fil)


					   


		select new CorrectionModel
					   {
						   Num_dossier = c.Num_dossier,
						   Cin = c.Cin,
						   Cne = c.Cne,
						   NoteMath = b.NoteMath,
						   NoteSpecialite = b.NoteSpecialite,

						   Nom = c.Nom,
						   Prenom = c.Prenom,
						   //Filiere = b.Nom,
						   Diplome = d.Type,
						   Filiere = e.Nom,
					   }).ToList();
			return res;
		}


		public IEnumerable<CorrectionModel4> corr4(string type_fil)
		{
			
			var res = (from c in db.Candidats
					   join d in db.Diplomes on c.Cne equals d.Cne
					   join e in db.Filieres on c.ID equals e.ID
					   join b in db.CouncourOrals on c.Cne equals b.Cne
					   where (c.Presence == true && c.Niveau == 4 && e.Nom == type_fil)
					   select new CorrectionModel4
					   {
						   Num_dossier = c.Num_dossier,
						   Cin = c.Cin,
						   Cne = c.Cne,
						   Nom = c.Nom,
						   Prenom = c.Prenom,
						   Etablissement = d.Etablissement + "   -   " + d.VilleObtention,
						   Filiere = e.Nom,
						   Diplome = d.Type,
						   VilleObtention = d.VilleObtention,
						   Specialite = d.Specialite,
						   Classement = b.Classement,
					   }).ToList();
			return res;
		}
	}
}
