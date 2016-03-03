using Augustus.Interfaces;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using System.Collections.Generic;

[assembly: ProxyTypesAssemblyAttribute()]

namespace Augustus.CRM
{

    public class Organization : OrganizationServiceContext, IOrganization
    {

        public Organization(IOrganizationService service) :
                base(service)
        {
        }

        public IEnumerable<IAccount> Accounts
        {
            get
            {
                return this.CreateQuery<Account>();
            }
        }

        public IEnumerable<IInvoice> Invoices
        {
            get
            {
                return this.CreateQuery<Invoice>();
            }
        }
    }
}
