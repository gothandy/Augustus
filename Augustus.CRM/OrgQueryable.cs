using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using System.Linq;

namespace Augustus.CRM
{

    public class OrgQueryable : OrganizationServiceContext
    {

        public OrgQueryable(IOrganizationService service) :
                base(service)
        {
        }

        public IQueryable<Account> Accounts
        {
            get
            {
                return this.CreateQuery<Account>();
            }
        }

        public IQueryable<Invoice> Invoices
        {
            get
            {
                return this.CreateQuery<Invoice>();
            }
        }
    }
}
