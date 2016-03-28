using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Tooling.Connector;
using System;

namespace Augustus.CRM
{
    public class CrmServiceConnectionString : ICrmService
    {
        private IOrganizationService service;

        public CrmServiceConnectionString(string connectionString)
        {
            CrmServiceClient crmSvc = new CrmServiceClient(connectionString);

            if (!crmSvc.IsReady)
            {
                throw new Exception(crmSvc.LastCrmError, crmSvc.LastCrmException);
            }

            if (crmSvc.OrganizationWebProxyClient == null)
            {
                service = crmSvc.OrganizationServiceProxy;
            }
            else
            {
                service = crmSvc.OrganizationWebProxyClient;
            }
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
