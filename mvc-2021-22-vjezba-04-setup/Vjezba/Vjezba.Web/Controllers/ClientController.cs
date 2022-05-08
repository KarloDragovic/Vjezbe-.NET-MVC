using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vjezba.DAL;
using Vjezba.Model;
using Vjezba.Web.Models;

namespace Vjezba.Web.Controllers
{
    public class ClientController : Controller
    {
        private ClientManagerDbContext _dbContext;

        public ClientController(ClientManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [ActionName("Edit")]
        public IActionResult Edit(int ID)
        {
            ViewBag.PossibleCities = ListOfCities();

            var client = _dbContext.Clients.FirstOrDefault(c => c.ID == ID);
            if (client == null)
            {
                return NotFound();
            }
            return View(client);
        }

        [HttpPost]
        [ActionName("Edit")]
        public async Task<IActionResult> EditPost(int ID)
        {
            ViewBag.PossibleCities = ListOfCities();

            Client client = this._dbContext.Clients.First(c => c.ID == ID);
            var ok = await this.TryUpdateModelAsync(client);
            if (ok)
            {
                this._dbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        public ActionResult Index(ClientFilterModel filter)
        {
            filter ??= new ClientFilterModel();

            var clientQuery = this._dbContext.Clients.Include(c=> c.City).AsQueryable();

            //Primjer iterativnog građenja upita - dodaje se "where clause" samo u slučaju da je parametar doista proslijeđen.
            //To rezultira optimalnijim stablom izraza koje se kvalitetnije potencijalno prevodi u SQL
            if (!string.IsNullOrWhiteSpace(filter.FullName))
                clientQuery = clientQuery.Where(p => (p.FirstName + " " + p.LastName).ToLower().Contains(filter.FullName.ToLower()));

            if (!string.IsNullOrWhiteSpace(filter.Address))
                clientQuery = clientQuery.Where(p => p.Address.ToLower().Contains(filter.Address.ToLower()));

            if (!string.IsNullOrWhiteSpace(filter.Email))
                clientQuery = clientQuery.Where(p => p.Email.ToLower().Contains(filter.Email.ToLower()));

            if (!string.IsNullOrWhiteSpace(filter.City))
                clientQuery = clientQuery.Where(p => p.CityID != null && p.City.Name.ToLower().Contains(filter.City.ToLower()));

            var model = clientQuery.ToList();
            return View("Index", model);
        }

        public IActionResult Details(int? id = null)
        {
            var client = this._dbContext.Clients
                .Include(p => p.City)
                .Where(p => p.ID == id)
                .FirstOrDefault();

            return View(client);
        }

        public IActionResult Create()
        {
            ViewBag.PossibleCities = ListOfCities();
            return View();
        }

        [HttpPost]
        public IActionResult Create(Client model)
        {
            ViewBag.PossibleCities = ListOfCities();
            ModelState.Remove("ID");
            if (ModelState.IsValid)
            {
                this._dbContext.Clients.Add(model);
                this._dbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            
            return View(model);
            
        }

        public IEnumerable<SelectListItem> ListOfCities()
        {
            var selectItems = new List<SelectListItem>();
            var listItem = new SelectListItem();
            listItem.Text = "- odaberite -";
            listItem.Value = "";
            selectItems.Add(listItem);
            foreach (var category in _dbContext.Cities)
            {
                listItem = new SelectListItem();
                listItem.Text = category.Name;
                listItem.Value = category.ID.ToString();
                selectItems.Add(listItem);
            }
            return selectItems.AsEnumerable();
        }
    }
}
