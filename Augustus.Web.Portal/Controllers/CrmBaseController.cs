using Augustus.CRM;
using Augustus.CRM.Queries;
using Augustus.Domain.Interfaces;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Configuration;
using System.Runtime.ExceptionServices;
using System.Security.Claims;
using System.Threading.Tasks;
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

                //throw new Exception("Caught it. Now what?", capturedException.SourceException);
            }

            return result;    
        }

        protected async static Task<OrgQueryable> GetOrgQueryable()
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

        protected async static Task<InvoiceQuery> GetInvoiceQuery()
        {
            var org = await GetOrgQueryable();

            return (InvoiceQuery)new InvoiceQuery()
            {
                Organization = org
            };
        }
    }
}