using Augustus.CRM;
using Augustus.CRM.Authentication;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using System;
using System.Configuration;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Augustus.Web.Portal.Controllers
{
    [CrmAuthorize]
    public class CrmBaseController : Controller
    {
        ICrmService service;

        private DateTime lastYear = DateTime.Now.AddYears(-1);
        private DateTime lastMonth = DateTime.Now.AddMonths(-1);
        private DateTime lastThreeMonths = DateTime.Now.AddMonths(-3);

        public CrmBaseController() : base()
        {
            string useAzureAuth = ConfigurationManager.AppSettings["crm:UseAzureAuth"];
            if (useAzureAuth == "true" || useAzureAuth == null)
            {
                service = GetAccessTokenService();
            }
            else
            {
                service = GetConnectionStringService();
            }
        }
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);

            var enGB = new CultureInfo("en-GB");
            Thread.CurrentThread.CurrentUICulture = enGB;
            Thread.CurrentThread.CurrentCulture = enGB;
        }


        private ICrmService GetConnectionStringService()
        {
            var connectionString = ConfigurationManager.AppSettings["crm:ConnectionString"];
            return new CrmServiceConnectionString(connectionString);
        }

        private ICrmService GetAccessTokenService()
        {
            var crmUrl = ConfigurationManager.AppSettings["crm:Url"];
            var clientId = ConfigurationManager.AppSettings["ida:ClientId"];
            var clientSecret = ConfigurationManager.AppSettings["ida:ClientSecret"];
            var authentication = new AccessTokenAuthentication(clientId, clientSecret, tokenAcquisitionFail: ForceSignOut);
            return new CrmServiceAccessToken(crmUrl, authentication);
        }

        private void ForceSignOut()
        {
            string callbackUrl = Url.Action("SignOutCallback", "Authentication", routeValues: null, protocol: Request.Url.Scheme);

            HttpContext.GetOwinContext().Authentication.SignOut(
                new AuthenticationProperties { RedirectUri = callbackUrl },
                OpenIdConnectAuthenticationDefaults.AuthenticationType,
                CookieAuthenticationDefaults.AuthenticationType);
        }

        protected async Task<CrmContext> GetCrmContext()
        {
            CrmContext context = await service.GetContextAsync();

            context.ActiveDate = lastYear;
            context.NewDate = lastThreeMonths;

            return context;
        }
    }
}