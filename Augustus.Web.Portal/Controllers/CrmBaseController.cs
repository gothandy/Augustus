using Augustus.CRM;
using Augustus.CRM.Queries;
using Augustus.Domain.Interfaces;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
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

        protected DateTime lastYear = DateTime.Now.AddYears(-1);
        protected DateTime lastMonth = DateTime.Now.AddMonths(-1);
        protected DateTime lastThreeMonths = DateTime.Now.AddMonths(-3);

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

        protected async Task<OrgQueryable> GetOrgQueryable()
        {
            string useAzureAuth = ConfigurationManager.AppSettings["crm:UseAzureAuth"];

            if (useAzureAuth == "true" || useAzureAuth == null)
            {
                Uri crmUrl = new Uri(ConfigurationManager.AppSettings["crm:Url"]);

                var result = await WaitForAuthenticationResult();

                return new OrgQueryable(crmUrl, result.AccessToken);
            }
            else
            {
                string connectionString = ConfigurationManager.AppSettings["crm:ConnectionString"];

                return new OrgQueryable(connectionString);
            }
        }

        protected async Task<OrganizationQuery> GetOrganizationQuery()
        {
            var org = await GetOrgQueryable();

            return (OrganizationQuery)new OrganizationQuery()
            {
                Organization = org
            };
        }

        protected async Task<AccountQuery> GetAccountQuery()
        {
            var org = await GetOrgQueryable();

            return (AccountQuery)new AccountQuery()
            {
                Organization = org,
                CreatedAfter = lastThreeMonths,
                ActiveAfter = lastYear
            };
        }

        protected async Task<OpportunityQuery> GetOpportunityQuery()
        {
            var org = await GetOrgQueryable();

            return (OpportunityQuery)new OpportunityQuery()
            {
                Organization = org
            };
        }

        protected async Task<InvoiceQuery> GetInvoiceQuery()
        {
            var org = await GetOrgQueryable();

            return (InvoiceQuery)new InvoiceQuery()
            {
                Organization = org
            };
        }
    }
}