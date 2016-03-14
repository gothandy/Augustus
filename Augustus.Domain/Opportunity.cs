using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Augustus.CRM;

namespace Augustus.Domain
{
    public class Opportunity
    {
        public Guid Id { get; set; }
        public OrgQueryable Organization { get; set; }

        public CRM.Entities.Opportunity GetOpportunity()
        {
            return Organization.Opportunities.Single(o => o.Id == Id);
        }

        public CRM.Entities.Account GetAccount()
        {
            return (from a in Organization.Accounts
                    join o in Organization.Opportunities
                    on a.Id equals o.AccountId
                    where o.Id == Id
                    select a).Single();
        }

        public IEnumerable<CRM.Entities.Invoice> GetInvoices()
        {
            return (from i in Organization.Invoices
                    where i.OpportunityId == Id
                    orderby i.InvoiceDate descending
                    select i).AsEnumerable();
        }
    }
}
