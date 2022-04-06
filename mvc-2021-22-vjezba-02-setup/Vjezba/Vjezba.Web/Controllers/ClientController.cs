using Microsoft.AspNetCore.Mvc;
using Vjezba.Web.Mock;
using Vjezba.Web.Models;

namespace Vjezba.Web.Controllers
{
    public class ClientController : Controller
    {
        public IActionResult Index(string query)
        {
            List<Client> model = new(MockClientRepository.Instance.All());

            if (query != null)
            {
                return View("Index", model.FindAll(c =>
                {
                    return c.FullName.Contains(query);
                }));
            }

            return View("Index", model);
        }

        [HttpPost]
        public IActionResult Index(string queryName, string queryAddress)
        {
            List<Client> model = new(MockClientRepository.Instance.All());

            if (queryName != null && queryAddress != null)
            {
                return View("Index", model.FindAll(c =>
                {
                    if (c.FullName.Contains(queryName) && c.Address.Contains(queryAddress))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }));
            }
            else if (queryAddress != null && queryName == null)
            {
                return View("Index", model.FindAll(c =>
                {
                    return c.Address.Contains(queryAddress);
                }));
                
            }
            else if (queryAddress == null && queryName != null)
            {
                return View("Index", model.FindAll(c =>
                {
                    return c.FullName.Contains(queryName);
                }));
                
            }

            return View("Index", model);
        }

        public IActionResult Details(int? id = null)
        {
            if (id != null)
            {
                var model = MockClientRepository.Instance.FindByID((int)id);
                if (model != null)
                {
                    return View("Details", model);
                }
                else
                {
                    return View("Details/999");
                }
                
            }
            return View("Details");
        }

        public ActionResult AdvancedSearch(ClientFilterModel model)
        {
            List<Client> clients = new(MockClientRepository.Instance.All());
            ClientFilterModel control = new ClientFilterModel { };

            if (model.QueryName == null)
                control.QueryName = "";
            else
                control.QueryName = model.QueryName;
            if (model.QueryEmail == null)
                control.QueryEmail = "";
            else
                control.QueryEmail = model.QueryEmail;
            if (model.QueryCity == null)
                control.QueryCity = "";
            else
                control.QueryCity = model.QueryCity;
            if (model.QueryAddress == null)
                control.QueryAddress = "";
            else
                control.QueryAddress = model.QueryAddress;


            var filteredClients = clients.FindAll(c =>
            {
                if (c.CityID != null && model.QueryCity != null)
                {
                    if (c.FullName.Contains(control.QueryName) && c.Address.Contains(control.QueryAddress) &&
                    c.Email.Contains(control.QueryEmail) && c.City.Name.Contains(control.QueryCity))
                    {
                        return true;
                    }
                    return false;
                }
                else if (c.CityID == null && model.QueryCity != null)
                {
                    return false;
                }
                else
                {
                    if (c.FullName.Contains(control.QueryName) && c.Address.Contains(control.QueryAddress) &&
                    c.Email.Contains(control.QueryEmail))
                    {
                        return true;
                    }
                    return false;
                }
                
            });

            return View("Index", filteredClients);
        }
    }
}
