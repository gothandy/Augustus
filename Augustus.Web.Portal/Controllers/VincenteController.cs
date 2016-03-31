using Augustus.CRM.Queries;
using Augustus.Domain.Objects;
using Newtonsoft.Json;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Augustus.Web.Portal.Controllers
{
    public class VincenteController : BaseCrmController
    {
        // GET: Vincente
        public async Task<ActionResult> Index()
        {
            Account account;
            using (var client = new WebClient())
            {
                var json = client.DownloadString("http://vincentewebapp.azurewebsites.net/api/augustus");
                account = JsonConvert.DeserializeObject<Account>(json);
            }

            using (var context = await GetCrmContext())
            {
                var query = new BulkUpdateQuery(context);
                //query.CheckUpdate(account);
                return View(account);
            }
        }
    }
}