using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PdfSharp.Drawing;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using PdfSharp.Pdf.IO;

namespace MantenanceProjetASPNET6.Controllers
{
    public class PdfTestController : Controller
    {


        [HttpGet]
        public IActionResult GeneratePdf()
        {
            // Spécifier les chemins des fichiers PDF à fusionner sur le disque C
            string[] pdfPaths = new string[]
            {
            @"C:\Users\h\Downloads\structure_dataset.pdf",  // Remplacez par le chemin réel de votre fichier PDF
            @"C:\Users\h\Downloads\RLS-exercices (1).pdf",
            @"C:\Users\h\Downloads\Descriptif_mini_projet_DS 2024-2025pdf.pdf"
            };

            // Créer un document PDF final
            PdfDocument outputDocument = new PdfDocument();

            // Fusionner les fichiers PDF
            foreach (string pdfPath in pdfPaths)
            {
                if (System.IO.File.Exists(pdfPath))
                {
                    // Charger chaque fichier PDF source
                    PdfDocument inputDocument = PdfReader.Open(pdfPath, PdfDocumentOpenMode.Import);

                    // Ajouter chaque page du fichier source au document de sortie
                    for (int i = 0; i < inputDocument.PageCount; i++)
                    {
                        // Importer la page du document source
                        PdfPage page = inputDocument.Pages[i];

                        // Ajouter la page au document de sortie
                        outputDocument.AddPage(page);
                    }
                }
                else
                {
                    // Loguer ou afficher un message si le fichier n'existe pas
                    Console.WriteLine($"Le fichier {pdfPath} n'existe pas.");
                }
            }

            // Sauvegarder le fichier fusionné dans un flux mémoire
            using (MemoryStream stream = new MemoryStream())
            {
                outputDocument.Save(stream, false); // Sauvegarder le document PDF fusionné dans le flux mémoire
                stream.Position = 0; // Réinitialiser la position du flux

                // Renvoyer le PDF fusionné comme réponse HTTP
                return File(stream.ToArray(), "application/pdf", "MergedDocument.pdf");
            }
        }


        // GET: PdfTestController
        public ActionResult Index()
        {
            return View();
        }

        // GET: PdfTestController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PdfTestController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PdfTestController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PdfTestController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PdfTestController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PdfTestController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PdfTestController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
