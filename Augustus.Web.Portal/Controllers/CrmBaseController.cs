using Augustus.CRM;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using System;
using System.Configuration;
using System.Runtime.ExceptionServices;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Augustus.Web.Portal.Controllers
{
    [CrmAuthorize]
    public class CrmBaseController : Controller
    {
        private const string tenantIdUrl = "http://schemas.microsoft.com/identity/claims/tenantid";
        private const string objectIdentifierUrl = "http://schemas.microsoft.com/identity/claims/objectidentifier";

        private DateTime lastYear = DateTime.Now.AddYears(-1);
        private DateTime lastMonth = DateTime.Now.AddMonths(-1);
        private DateTime lastThreeMonths = DateTime.Now.AddMonths(-3);

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

            ExceptionDispatchInfo capturedException = null;
            AuthenticationResult result = null;

            
            try
            {
                result = await authContext.AcquireTokenSilentAsync(resource, credential, userIdentifier);
            }
            catch (AdalSilentTokenAcquisitionException ex)
            {
                capturedException = ExceptionDispatchInfo.Capture(ex);
            }

            if (capturedException != null)
            {
                // Problem with tokens so sign out.
                string callbackUrl = Url.Action("SignOutCallback", "Authentication", routeValues: null, protocol: Request.Url.Scheme);

                HttpContext.GetOwinContext().Authentication.SignOut(
                    new AuthenticationProperties { RedirectUri = callbackUrl },
                    OpenIdConnectAuthenticationDefaults.AuthenticationType,
                    CookieAuthenticationDefaults.AuthenticationType);
                return null;
            }
            else
            {
                return result;
            }
        }

        protected async Task<CrmContext> GetCrmContext()
        {
            CrmContext context;

            string useAzureAuth = ConfigurationManager.AppSettings["crm:UseAzureAuth"];

            if (useAzureAuth == "true" || useAzureAuth == null)
            {
                Uri crmUrl = new Uri(ConfigurationManager.AppSettings["crm:Url"]);
                var result = await WaitForAuthenticationResult();
                var service = new CrmServiceAccessToken(crmUrl, result.AccessToken);

                context = new CrmContext(service);
            }
            else
            {
                string connectionString = ConfigurationManager.AppSettings["crm:ConnectionString"];

                var svc = new CrmServiceConnectionString(connectionString);

                context = new CrmContext(svc);
            }

            context.ActiveDate = lastYear;
            context.NewDate = lastThreeMonths;

            return context;
        }
    }
}