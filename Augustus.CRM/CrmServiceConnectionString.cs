using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Threading.Tasks;

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

        public CrmContext GetContext()
        {
            return new CrmContext(service);
        }

        public async Task<CrmContext> GetContextAsync()
        {
            //return new CrmContext(service);
            return await Task.Run(() => GetContext());
        }
    }
}
