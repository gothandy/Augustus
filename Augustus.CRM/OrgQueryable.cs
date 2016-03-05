using System.Linq;

namespace Augustus.CRM
{

    public class OrgQueryable : BaseOrganization
    {
        public OrgQueryable(string connectionString) : base(connectionString) { }

        public IQueryable<Account> Accounts
        {
            get
            {
                return context.CreateQuery<Account>();
            }
        }

        public IQueryable<Invoice> Invoices
        {
            get
            {
                return context.CreateQuery<Invoice>();
            }
        }
    }
}
