using Augustus.CRM;
using Augustus.CRM.Authentication;
using Augustus.Web.Config;
using Augustus.Web.Portal.Attributes;
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
    [RequireHttps]
    public class BaseCrmController : Controller
    {
        ICrmService service;

        public BaseCrmController() : base()
        {
            if (AppSettings.UseAzureAuth)
            {
                var authentication = new AccessTokenAuthentication(AppSettings.ClientId, AppSettings.ClientSecret, ForceSignOut);
                service = new CrmServiceAccessToken(AppSettings.CrmUrl, authentication);
            }
            else
            {
                service = new CrmServiceConnectionString(AppSettings.CrmConnectionString);
            }
        }

        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);

            var enGB = new CultureInfo("en-GB");
            Thread.CurrentThread.CurrentUICulture = enGB;
            Thread.CurrentThread.CurrentCulture = enGB;
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

            context.ActiveDate = DateTime.Now.AddYears(-1);
            context.NewDate = DateTime.Now.AddMonths(-3);

            return context;
        }
    }
}