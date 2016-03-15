using Augustus.CRM;
using Augustus.CRM.Entities;
using Augustus.Domain.Interfaces;
using Augustus.Domain.Objects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Augustus.CRM.Queries
{
    public class AccountQuery : BaseQuery, IAccountQuery
    {
        public Guid Id { get; set; }
        public DateTime ActiveDate { get; set; }

        public Account GetAccount()
        {
            return (from a in Organization.Accounts
                    where a.Id == Id
                    select AccountEntity.ToDomainObject(a)).Single();
        }

        public IEnumerable<Invoice> GetInvoices()
        {
            return (from i in Organization.Invoices
                    where i.AccountId == Id
                    && i.InvoiceDate > ActiveDate
                    orderby i.InvoiceDate descending
                    select InvoiceEntity.ToDomainObject(i)).AsEnumerable();
        }

        public IEnumerable<CRM.Entities.OpportunityEntity> GetOpportunities()
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
