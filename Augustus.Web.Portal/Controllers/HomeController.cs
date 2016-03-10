using Augustus.CRM;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Augustus.Web.Portal.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private const string tenantIdUrl = "http://schemas.microsoft.com/identity/claims/tenantid";
        private const string objectIdentifierUrl = "http://schemas.microsoft.com/identity/claims/objectidentifier";

        public ActionResult Index()
        {
            return View();
        }

        private async Task<AuthenticationResult> WaitForAuthenticationResult()
        {
            string clientId = ConfigurationManager.AppSettings["ida:ClientId"];
            string clientSecret = ConfigurationManager.AppSettings["ida:ClientSecret"];
            string signedInUserID = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;
            string tenantID = ClaimsPrincipal.Current.FindFirst(tenantIdUrl).Value;
            string userObjectID = ClaimsPrincipal.Current.FindFirst(objectIdentifierUrl).Value;
            string authority = string.Format("https://login.windows.net/{0}", tenantID);
            string resource = "Microsoft.CRM";

            var authContext = new AuthenticationContext(authority);
            var credential = new ClientCredential(clientId, clientSecret);
            var userIdentifier = new UserIdentifier(userObjectID, UserIdentifierType.UniqueId);

            return await authContext.AcquireTokenSilentAsync(resource, credential, userIdentifier);
        }

        private async Task<OrgQueryable> GetOrgQueryable()
        {
            Uri crmUrl = new Uri(ConfigurationManager.AppSettings["crm:Url"]);

            var result = await WaitForAuthenticationResult();

            return new OrgQueryable(crmUrl, result.AccessToken);
        }

        public async Task<ActionResult> Accounts()
        {
            IEnumerable<Account> activeAccounts;

            using (OrgQueryable org = await GetOrgQueryable())
            {
                activeAccounts = (from a in org.Accounts
                                  join i in org.Invoices
                                  on a.Id equals i.DirectClientId
                                  where i.InvoiceDate > new DateTime(2015, 3, 1)
                                  select a).Distinct().AsEnumerable();
            }

            return View(activeAccounts);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}