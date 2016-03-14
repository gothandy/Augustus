using Augustus.CRM;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Augustus.Domain
{
    public class Account
    {
        public DateTime ActiveDate { get; set; }
        public Guid Id { get; set; }
        public OrgQueryable Organization { get; set; }

        public CRM.Entities.Account GetAccount()
        {
            return (from a in Organization.Accounts
                    where a.Id == Id
                    select a).Single();
        }

        public IEnumerable<CRM.Entities.Invoice> GetInvoices()
        {
            return (from i in Organization.Invoices
                    where i.AccountId == Id
                    && i.InvoiceDate > ActiveDate
                    orderby i.InvoiceDate descending
                    select i).AsEnumerable();
        }

        public IEnumerable<CRM.Entities.Opportunity> GetOpportunities()
        {
            var activeOpps = (from o in Organization.Opportunities
                              join i in Organization.Invoices
                              on o.Id equals i.OpportunityId
                              where i.InvoiceDate > ActiveDate
                              && o.AccountId == Id
                              orderby o.Name ascending
                              select o).Distinct().AsEnumerable();

            var newOpps = (from o in Organization.Opportunities
                           where o.Created > ActiveDate
                           && o.AccountId == Id
                           select o).AsEnumerable();

            return activeOpps.Union(newOpps).Distinct().OrderBy(a => a.Name);
        }
    }
}
