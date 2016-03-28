using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.WebServiceClient;
using System;

namespace Augustus.CRM
{
    public class CrmServiceAccessToken : ICrmService
    {
        private IOrganizationService service;

        public CrmServiceAccessToken(Uri crmUrl, string accessToken)
        {
            var uri = new Uri(crmUrl, "/XRMServices/2011/Organization.svc/web");

            service = new OrganizationWebProxyClient(uri, useStrongTypes: true)
            {
                HeaderToken = accessToken,
                SdkClientVersion = "7.0",
            };
        }

        public IOrganizationService OrganizationService
        {
            get
            {
                return service;
            }
        }
    }
}
