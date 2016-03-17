using Augustus.CRM;
using Augustus.CRM.Queries;
using Augustus.Domain.Interfaces;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Configuration;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Augustus.Web.Portal.Controllers
{
    [Authorize]
    public class CrmBaseController : Controller
    {
        private const string tenantIdUrl = "http://schemas.microsoft.com/identity/claims/tenantid";
        private const string objectIdentifierUrl = "http://schemas.microsoft.com/identity/claims/objectidentifier";

        private async static Task<AuthenticationResult> WaitForAuthenticationResult()
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

        protected async static Task<OrgQueryable> GetOrgQueryable()
        {
            Uri crmUrl = new Uri(ConfigurationManager.AppSettings["crm:Url"]);

            var result = await WaitForAuthenticationResult();

            var org = new OrgQueryable(crmUrl, result.AccessToken);

            return org;
        }

        protected async static Task<IOrganizationQuery> GetOrganizationQuery()
        {
            var org = await GetOrgQueryable();

            return (IOrganizationQuery)new OrganizationQuery()
            {
                Organization = org
            };
        }

        protected async static Task<IAccountQuery> GetAccountQuery()
        {
            var org = await GetOrgQueryable();

            return (IAccountQuery)new AccountQuery()
            {
                Organization = org
            };
        }

        protected async static Task<IOpportunityQuery> GetOpportunityQuery()
        {
            var org = await GetOrgQueryable();

            return (IOpportunityQuery)new OpportunityQuery()
            {
                Organization = org
            };
        }
    }
}