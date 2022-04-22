using Vjezba.Model;

namespace Vjezba.Web.Models
{
    public class ListClientAndClientFilterModel
    {
        public List<Client> Clients { get; set; }
        public ClientFilterModel ClientFilter { get; set; }
    }
}
