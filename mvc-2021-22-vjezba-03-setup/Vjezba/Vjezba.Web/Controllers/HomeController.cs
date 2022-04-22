using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Vjezba.Web.Models;

namespace Vjezba.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy(string? lang = null)
        {
            ViewBag.Poruka = "Ovo je poruka!";
            if (lang != null && lang != "")
            {
                if (lang.ToLower() == "en")
                {
                    ViewBag.Poruka = "This is a message!";
                }
                if (lang.ToLower() == "hr")
                {
                    ViewBag.Poruka = "Ovo je poruka!";
                }
                if (lang.ToLower() == "de")
                {
                    ViewBag.Poruka = "Dies ist eine Nachricht!";
                }
                if (lang.ToLower() == "zh")
                {
                    ViewBag.Poruka = "這是一條消息!";
                }
            }
            return View();
        }

        [Route("cesto-postavljena-pitanja/")]
        [Route("cesto-postavljena-pitanja/{selected:int:length(1):maxlength(2)?}")]
        public IActionResult FAQ(int? selected = null)
        {
            ViewData["selected"] = selected;

            return View();
        }

        public IActionResult Contact()
        {
            ViewBag.Message = "Jednostavan način proslijeđivanja poruke iz Controller -> View.";
            //Kao rezultat se pogled /Views/Home/Contact.cshtml renderira u "pravi" HTML
            //Primjetiti - View() je poziv funkcije koja uzima cshtml template i pretvara ga u HTML
            //Zasto bas Contact.cshtml? Jer se akcija zove Contact, te prema konvenciji se "po defaultu" uzima cshtml datoteka u folderu Views/CONTROLLER_NAME/AKCIJA.cshtml

            return View();
        }

        /// <summary>
        /// Ova akcija se poziva kada na formi za kontakt kliknemo "Submit"
        /// URL ove akcije je /Home/SubmitQuery, uz POST zahtjev isključivo - ne može se napraviti GET zahtjev zbog [HttpPost] parametra
        /// </summary>
        /// <param name="formData"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult SubmitQuery(IFormCollection formData)
        {
            //Ovdje je potrebno obraditi podatke i pospremiti finalni string u ViewBag

            var ime = formData["ime"];
            var prezime = formData["prezime"];
            var email = formData["email"];
            var poruka = formData["poruka"];
            var tip = formData["tip"];
            var newsletter = bool.Parse(formData["newsletter"].FirstOrDefault());

            var msg = "Poštovani {0} {1} ({2}) zaprimili smo vašu poruku te će vam se netko ubrzo javiti. Sadržaj vaše poruke je: [{3}] {4}." +
                " Također, {5} o daljnjim promjenama preko newslettera.";

            ViewBag.Message = string.Format(msg,
                ime, prezime,
                email,
                tip, poruka,
                newsletter ? "obavijestit cemo vas" : "necemo vas obavijestiti");

            //Kao rezultat se pogled /Views/Home/ContactSuccess.cshtml renderira u "pravi" HTML
            //Kao parametar se predaje naziv cshtml datoteke koju treba obraditi (ne koristi se default vrijednost)
            //Trazenu cshtml datoteku je potrebno samostalno dodati u projekt
            return View("ContactSuccess");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}