using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using MantenanceProjetASPNET6.Models;

namespace MantenanceProjetASPNET6.Data
{
    public class Generer
    {
        //###################################### CANDIDATS ###################################

        public static IEnumerable<Candidat> GenererCandidats()
        {

            List<Candidat> list = new List<Candidat>()
            {
                new Candidat()
                {
                    Cne = "test5",
                    Cin = "test5",
                    Nom = "xxxx",
                    Prenom = "xxx",
                    Email = "xxxx@gmail.com",
                    Ville = "Casablanca",
                    Nationalite = "Marocaine",
                    Num_dossier = 0,
                    Sexe = "Femme",
                    Photo = "icon.jpg",
                    Convoque = false,
                    Niveau = 3,
                    LieuNaissance = "Paris",
                    Telephone = "123-456-7890",
                    Verified = 1,
                    Password = "test",
                    Matricule = "A123",
                    Presence = false,
                    Conforme = false,
                    ID = 1,
                    DateInscription = DateTime.Now,
                    DateNaissance = new DateTime(1995, 5, 1),
                    Gsm = "0612345678", 
                    Adresse = "NKoub.Tazarin Zagora" 
                },
               
            };

            return list;
        }


        //###################################### ANNEE UNIVERSITAIRE ###################################
        public static IEnumerable<AnneeUniversitaire> GenererAnneeUniversitaire()
        {

            List<AnneeUniversitaire> list = new List<AnneeUniversitaire>()
            {
                new AnneeUniversitaire()
                {
                    Cne = "test5",
                    Semestre1 = 14.2,
                    Semestre2 = 14.8,
                    Semestre3 = 15,
                    Semestre4 = 17,
                    Semestre5 = 16,
                    Semestre6 = 16.5,
                    Redoublant1 = "Non",
                    Redoublant2 = "Non",
                    Redoublant3 = "Non",
                    AnneUni1 = "2020/2021",
                    AnneUni2 = "2022/2023",
                    AnneUni3 = "2023/2024"
                },
                

            };

            return list;
        }

        //###################################### BACCALAUREAT ###################################
        public static IEnumerable<Baccalaureat> GenererBaccalaureat()
        {

            List<Baccalaureat> list = new List<Baccalaureat>()
            {
                new Baccalaureat()
                {
                    Cne = "test5",
                    TypeBac = "SMA",
                    DateObtentionBac = "2019",
                    NoteBac = 16,
                    MentionBac = "Bien"
                },
               
            };

            return list;
        }

        //###################################### CONCOURS ECRIT ###################################

        public static IEnumerable<ConcourEcrit> GenererConcoursEcrit()
        {

            List<ConcourEcrit> list = new List<ConcourEcrit>()
            {
                new ConcourEcrit()
                {
                    Cne = "test5",
                    NoteMath = 15,
                    NoteSpecialite = 15.5
                },
               
            };

            return list;
        }

        //######################################  CONCOURS ORAL ###################################

        public static IEnumerable<ConcourOral> GenererConcoursOral()
        {

            List<ConcourOral> list = new List<ConcourOral>()
            {
                 new ConcourOral()
                  {
                    Cne = "test5",
                    Classement = 1
                 },
              
            };

            return list;
        }

        //###################################### DIPLOME ###################################

        public static IEnumerable<Diplome> GenererDiplome()
        {

            List<Diplome> list = new List<Diplome>()
            {
                new Diplome()
                {
                    Cne = "test5",
                    Type = "DUT",
                    Etablissement = "EST",
                    VilleObtention = "safi",
                    NoteDiplome = 16.42,
                    Specialite = "Informatique"
                },
              
            };

            return list;
        }


    }



        
    
}
