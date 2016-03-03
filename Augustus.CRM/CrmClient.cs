using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Tooling.Connector;
using System;

namespace Augustus.CRM
{
    public class CrmClient : IDisposable
    {
        private Organization _organization;

        public CrmClient (string connectionString)
        {
            CrmServiceClient crmSvc = new CrmServiceClient(connectionString);

            IOrganizationService _orgService = GetAndCastProxyClientFromWebOrService(crmSvc);

            this._organization = new Organization(_orgService);

        }

        private static IOrganizationService GetAndCastProxyClientFromWebOrService(CrmServiceClient crmSvc)
        {
            if (crmSvc.OrganizationWebProxyClient != null)
            {
                return (IOrganizationService)crmSvc.OrganizationWebProxyClient;
            }
            else
            {
                return (IOrganizationService)crmSvc.OrganizationServiceProxy;
            }
        }


        public void Dispose()
        {
            _organization.Dispose();
        }

        public Organization Organization
        {
            get
            {
                return _organization;
            }
        }
    }
}
