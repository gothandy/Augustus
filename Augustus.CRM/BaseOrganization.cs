using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Tooling.Connector;
using System;

[assembly: ProxyTypesAssemblyAttribute()]

namespace Augustus.CRM
{
    public class BaseOrganization : IDisposable
    {
        protected OrganizationServiceContext context;

        public BaseOrganization(string connectionString)
        {
            CrmServiceClient crmSvc = new CrmServiceClient(connectionString);
            IOrganizationService _orgService = (IOrganizationService)crmSvc.OrganizationWebProxyClient;
            context = new OrganizationServiceContext(_orgService);
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
