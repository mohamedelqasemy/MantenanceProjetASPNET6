using System.ComponentModel.DataAnnotations;

namespace MantenanceProjetASPNET6.Models
{
    public class InfoPersoModel
    {
        [Required(ErrorMessage = "L'adresse est obligatoire.")]
        public string Adresse { get; set; }

        [Required(ErrorMessage = "La ville est obligatoire.")]
        public string Ville { get; set; }

        [Required(ErrorMessage = "Le lieu de naissance est obligatoire.")]
        public string LieuNaissance { get; set; }

        [Required(ErrorMessage = "Le numéro de téléphone est obligatoire.")]
        [Phone(ErrorMessage = "Le numéro de téléphone n'est pas valide.")]
        public string Telephone { get; set; }

        [Required(ErrorMessage = "La nationalité est obligatoire.")]
        public string Nationalite { get; set; }

        [Required(ErrorMessage = "Le sexe est obligatoire.")]
        public string Sexe { get; set; }

        [Required(ErrorMessage = "Le numéro GSM est obligatoire.")]
        [Phone(ErrorMessage = "Le numéro GSM n'est pas valide.")]
        public string Gsm { get; set; }

        [Required(ErrorMessage = "La date de naissance est obligatoire.")]
        [DataType(DataType.Date, ErrorMessage = "La date de naissance n'est pas valide.")]
        public DateTime DateNaissance { get; set; }
    }
}
