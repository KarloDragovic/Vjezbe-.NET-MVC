using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Dynamic;
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
            this._dbContext = dbContext;
        }

        public IActionResult Create()
        {
            dynamic model = new Tuple<List<City>, Client>(_dbContext.Cities.ToList(), new Client{});
            return View(model);
        }

        [HttpPost]
        public IActionResult Create(Client? client = null)
        {
            if (client != null)
            {
                _dbContext.Clients.Add(client);
                _dbContext.SaveChanges();

                ViewBag.Message = "Client created successfully";
            }
            return View();
        }
        public IActionResult Index(string query = null)
        {
            var clientQuery = _dbContext.Clients.Include(c => c.City).AsEnumerable();

            ViewBag.ActiveTab = 4;

            if (!string.IsNullOrWhiteSpace(query))
            {
                clientQuery = clientQuery.Where(p => p.FullName.ToLower().Contains(query));
                ViewBag.ActiveTab = 1;

            }
            return View(new ListClientAndClientFilterModel { Clients = clientQuery.ToList(), ClientFilter = new ClientFilterModel { } });
        }

        [HttpPost]
        public ActionResult Index(string queryName, string queryAddress)
        {
            var clientQuery = _dbContext.Clients.AsEnumerable();

            //Primjer iterativnog građenja upita - dodaje se "where clause" samo u slučaju da je parametar doista proslijeđen.
            //To rezultira optimalnijim stablom izraza koje se kvalitetnije potencijalno prevodi u SQL
            if (!string.IsNullOrWhiteSpace(queryName))
                clientQuery = clientQuery.Where(p => p.FullName.ToLower().Contains(queryName));

            if (!string.IsNullOrWhiteSpace(queryAddress))
                clientQuery = clientQuery.Where(p => p.Address.ToLower().Contains(queryAddress));

            ViewBag.ActiveTab = 2;

            var model = new ListClientAndClientFilterModel { Clients = clientQuery.ToList(), ClientFilter = new ClientFilterModel { } };
            return View(model);
        }

        [HttpPost]
        public ActionResult AdvancedSearch(ListClientAndClientFilterModel filter)
        {
            var clientQuery = _dbContext.Clients.Include(c => c.City).AsEnumerable();

            //Primjer iterativnog građenja upita - dodaje se "where clause" samo u slučaju da je parametar doista proslijeđen.
            //To rezultira optimalnijim stablom izraza koje se kvalitetnije potencijalno prevodi u SQL
            if (!string.IsNullOrWhiteSpace(filter.ClientFilter.FullName))
                clientQuery = clientQuery.Where(p => p.FullName.ToLower().Contains(filter.ClientFilter.FullName.ToLower()));

            if (!string.IsNullOrWhiteSpace(filter.ClientFilter.Address))
                clientQuery = clientQuery.Where(p => p.Address.ToLower().Contains(filter.ClientFilter.Address.ToLower()));

            if (!string.IsNullOrWhiteSpace(filter.ClientFilter.Email))
                clientQuery = clientQuery.Where(p => p.Email.ToLower().Contains(filter.ClientFilter.Email.ToLower()));

            if (!string.IsNullOrWhiteSpace(filter.ClientFilter.City))
                clientQuery = clientQuery.Where(p => p.City != null && p.City.Name.ToLower().Contains(filter.ClientFilter.City.ToLower()));

            ViewBag.ActiveTab = 4;

            var model = new ListClientAndClientFilterModel { Clients = clientQuery.ToList(), ClientFilter = filter.ClientFilter};

            return View("Index", model);
        }

        public IActionResult Details(int? id = null)
        {
            var model = id != null ? _dbContext.Clients.Include(p => p.City).SingleOrDefault(c => c.Id == id) : null;
            return View(model);
        }
    }
}
