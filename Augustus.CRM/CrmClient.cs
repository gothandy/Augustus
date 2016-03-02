using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Linq;

namespace Augustus.CRM
{
    public class CrmClient : IDisposable
    {
        private CrmServiceContext context;

        public CrmClient (string connectionString)
        {
            CrmServiceClient crmSvc = new CrmServiceClient(connectionString);

            IOrganizationService _orgService = GetAndCastProxyClientFromWebOrService(crmSvc);

            this.context = new CrmServiceContext(_orgService);

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
            context.Dispose();
        }

        public Entities.Account GetAccount(string name)
        {
            var accounts = from a in this.context.AccountSet
                           where a.Name == name
                           select new Entities.Account
                           {
                               Id = a.Id,
                               Name = a.Name
                           };

            return accounts.First();
        }
    }
}
