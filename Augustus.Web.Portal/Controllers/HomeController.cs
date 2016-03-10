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

namespace Augustus.Web.Portal.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public async System.Threading.Tasks.Task<ActionResult> About()
        {
            string clientId = ConfigurationManager.AppSettings["ida:ClientId"];
            string clientSecret = ConfigurationManager.AppSettings["ida:ClientSecret"];
            string signedInUserID = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;
            string tenantID = ClaimsPrincipal.Current.FindFirst("http://schemas.microsoft.com/identity/claims/tenantid").Value;
            string userObjectID = ClaimsPrincipal.Current.FindFirst("http://schemas.microsoft.com/identity/claims/objectidentifier").Value;
            string crmUrl = ConfigurationManager.AppSettings["crm:Url"];

            AuthenticationContext authContext = new AuthenticationContext(string.Format("https://login.windows.net/{0}", tenantID));
            ClientCredential credential = new ClientCredential(clientId, clientSecret);
            var result = await authContext.AcquireTokenSilentAsync("Microsoft.CRM",
                credential, new UserIdentifier(userObjectID, UserIdentifierType.UniqueId));

            OrganizationWebProxyClient service = new OrganizationWebProxyClient(
                new Uri(string.Format("{0}/XRMServices/2011/Organization.svc/web", crmUrl)), false);
            service.HeaderToken = result.AccessToken;
            service.SdkClientVersion = "7.0";
            var whoamiResult = service.Execute(new WhoAmIRequest()) as WhoAmIResponse;
            ViewBag.Message = "User ID is " + whoamiResult.UserId;

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}