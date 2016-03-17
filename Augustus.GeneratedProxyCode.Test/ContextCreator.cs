using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Tooling.Connector;
using System.Linq;

namespace Augustus.CRM.GeneratedProxyCode.Test
{
    public static class ContextCreator
    {
        public static CrmServiceContext Create()
        {
            string connectionString = ConfigurationManager.AppSettings["crm:ConnectionString"];

            CrmServiceClient crmSvc = new CrmServiceClient(connectionString);

            IOrganizationService _orgService;

            if (crmSvc.OrganizationWebProxyClient == null)
            {
                _orgService = crmSvc.OrganizationServiceProxy;
            }
            else
            {
                _orgService = crmSvc.OrganizationWebProxyClient;
            }

            return new CrmServiceContext(_orgService);
        }
    }
}
