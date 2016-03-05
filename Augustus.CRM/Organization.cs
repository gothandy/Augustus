using Augustus.Interfaces;
using System.Collections.Generic;

namespace Augustus.CRM
{

    public class Organization : BaseOrganization, IOrganization
    {
        public Organization(string connectionString) : base(connectionString) { }

        public IEnumerable<IAccount> Accounts
        {
            get
            {
                return context.CreateQuery<Account>();
            }
        }

        public IEnumerable<IInvoice> Invoices
        {
            get
            {
                return context.CreateQuery<Invoice>();
            }
        }
    }
}
