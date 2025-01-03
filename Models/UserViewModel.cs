using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MantenanceProjetASPNET6.Models
{
	public class UserViewModel
	{
		//public int Key { get; set; }
		public List<string> Cne { get; set; }
		public List<double> NoteMath { get; set; }
		public List<double> NoteSpecialite { get; set; }
		public List<int> Classement { get; set; }		
		public List<int> Num_dossier { get; set; }
		public List<string> Cin { get; set; }
		public List<string> Nom { get; set; }
		public List<string> Prenom { get; set; }
		public List<string> Filiere { get; set; }
		public List<string> Diplome { get; set; }
		public List<string> Etablissement { get; set; }
		public List<string> VilleObtention { get; set; }
		public List<string> Specialite { get; set; }


	}
}
