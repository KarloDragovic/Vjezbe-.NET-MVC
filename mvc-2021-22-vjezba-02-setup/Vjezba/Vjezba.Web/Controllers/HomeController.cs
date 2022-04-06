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

        public IActionResult Privacy()
        {
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

        public IActionResult FAQ(int? selected = null)
        {
            var Pitanja = new List<FAQViewModel>();

            Pitanja.Add(new FAQViewModel
            {
                Question = "What is the difference between CMOS and TTL?",
                Answer = @"TTL stands for Transistor-Transistor Logic. CMOS stands for Complimentary Metal Oxide Semiconductor. 
                        TTL is a classification of integrated circuits, while CMOS is another classification of IC's that utilize 
                        field-effect transistors in the device. CMOS chips' benefit over TTL chips is the superior concentration 
                        of logic gates inside the same material."
            });

            Pitanja.Add(new FAQViewModel
            {
                Question = "How much weight can Dwayne Johnson benchpress?",
                Answer = @"Dwayne Johnson, better known as “The Rock,” can manage a single 425 pounds and bench press up to 450 pounds; 
                        although, it will often depend on his energy and protein levels. Johnson weighs 262 pounds, and he is 6 foot 5 feet tall. He is 46 years old, 
                        and he was born on May 2nd, 1972. His claim to notoriety is that he one of the best WWE wrestlers of all time."
            });

            Pitanja.Add(new FAQViewModel
            {
                Question = "Why is grooming important?",
                Answer = "Grooming is important because people need to look nice."
            });

            Pitanja.Add(new FAQViewModel
            {
                Question = "Which type of chocolate melts the fastest?",
                Answer = @"Dark chocolate melts the fastest... It's because dark chocolate is in its purest form. Since different ingredients have different melting points,
                         adding additional ingredients like milk or milk products, sugar, fats, and other things reduce melting."
            });

            Pitanja.Add(new FAQViewModel
            {
                Question = "Do Calendars repeat? If so, how often?",
                Answer = @"The calendar can repeat at 5, 6, 11 or 28 years. The only confirmed pattern is that of 28 years. The 28 year cycle works well as long as the 4 year
                cycle of the leap year continues."
            });

            for (int i = 0; i < Pitanja.Count; i++)
            {
                Pitanja[i].Id = i + 1;
            }

            

            ViewBag.SelectedQuestion = selected; 
            return View(Pitanja);
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

            ViewBag.Poruka ="Poštovani " + formData["ime"] + " " + formData["prezime"] + " (" + formData["email"] + ") " + @"zaprimili 
            smo vašu poruku te će vam se netko ubrzo javiti. Sadržaj vaše poruke je: " + " [" + formData["tipPoruke"] + "] " + formData["poruka"];

            if (formData["newsletter"].Equals("true"))
            {
                ViewBag.Poruka = ViewBag.Poruka + " Također, obavijestiti ćemo vas o daljnjim promjenama preko newslettera.";
            }
            else
            {
                ViewBag.Poruka = ViewBag.Poruka + " Također, nećemo vas obavijestiti o daljnjim promjenama preko newslettera.";
            }

            

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