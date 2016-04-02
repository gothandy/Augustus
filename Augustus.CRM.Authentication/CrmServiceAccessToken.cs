using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.WebServiceClient;
using System;
using System.Configuration;
using System.Threading.Tasks;

namespace Augustus.CRM.Authentication
{
    public class CrmServiceAccessToken : ICrmService
    {
        private IOrganizationService service;
        private AccessTokenAuthentication authentication;
        private Uri uri;

        public CrmServiceAccessToken(string crmUrl, AccessTokenAuthentication authentication)
        {
            Uri crmUri = new Uri(crmUrl);
            uri = new Uri(crmUri, "/XRMServices/2011/Organization.svc/web");

            this.authentication = authentication;
        }

        public async Task<CrmContext> GetContextAsync()
        {
            var result = await authentication.TryWaitForResult();

            service = new OrganizationWebProxyClient(uri, useStrongTypes: true)
            {
                HeaderToken = result.AccessToken,
                SdkClientVersion = "7.0",
            };

            return new CrmContext(service);
        }

        public CrmContext GetContext()
        {
            // Requires Async method.
            throw new NotImplementedException();
        }

        public static ICrmService GetAccessTokenService(Action tokenAcquisitionFail)
        {
            var crmUrl = ConfigurationManager.AppSettings["crm:Url"];
            var clientId = ConfigurationManager.AppSettings["ida:ClientId"];
            var clientSecret = ConfigurationManager.AppSettings["ida:ClientSecret"];
            var authentication = new AccessTokenAuthentication(clientId, clientSecret, tokenAcquisitionFail);
            return new CrmServiceAccessToken(crmUrl, authentication);
        }
    }
}
