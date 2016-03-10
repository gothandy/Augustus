using Augustus.CRM;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.Xrm.Sdk.WebServiceClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;

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

        public async Task<ActionResult> About()
        {
            Uri crmUrl = new Uri(ConfigurationManager.AppSettings["crm:Url"]);

            var result = await WaitForAuthenticationResult();

            using (OrgQueryable org = new OrgQueryable(crmUrl, result.AccessToken))
            {
                var accounts = org.Accounts;
                var account = accounts.Single(a => a.Name == "easyJet");
                ViewBag.Message = "easyJet Account Id is " + account.AccountId.Value.ToString();
            }

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}