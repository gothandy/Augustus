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

        public Account GetAccount(Guid id)
        {
            return (from a in Organization.Accounts
                    where a.Id == id
                    select AccountEntity.ToDomainObject(a)).Single();
        }

        public IEnumerable<Invoice> GetInvoices(Guid accountId, DateTime from)
        {
            return (from i in Organization.Invoices
                    where i.AccountId == accountId
                    && i.InvoiceDate > @from
                    orderby i.InvoiceDate descending
                    select InvoiceEntity.ToDomainObject(i)).AsEnumerable();
        }

        public IEnumerable<Opportunity> GetActiveOpportunities(Guid accountId, DateTime invoicesFrom)
        {
            return (from o in Organization.Opportunities
                    join i in Organization.Invoices
                    on o.Id equals i.OpportunityId
                    where i.InvoiceDate > invoicesFrom
                    && o.AccountId == accountId
                    orderby o.Name ascending
                    select OpportunityEntity.ToDomainObject(o)).Distinct().AsEnumerable();
        }

        public IEnumerable<Opportunity> GetNewOpportunities(Guid accountId, DateTime createdAfter)
        {
            return (from o in Organization.Opportunities
                    where o.Created > createdAfter
                    && o.AccountId == accountId
                    orderby o.Name ascending
                    select OpportunityEntity.ToDomainObject(o)).AsEnumerable();
        }

        public IEnumerable<Opportunity> GetNewAndActiveOpportunities(Guid accountId, DateTime createdAfter, DateTime invoicesFrom)
        {
            return GetNewOpportunities(accountId, createdAfter).Union(GetActiveOpportunities(accountId, invoicesFrom)).Distinct();
        }

        public Guid CreateAccount(Account account)
        {
            var entity = new AccountEntity
            {
                Name = account.Name
            };

            Organization.Create<AccountEntity>(entity);
            Organization.SaveChanges();

            return entity.Id;
        }

        public void UpdateAccount(Account account)
        {
            var entity = Organization.Accounts.Single(a => a.Id == account.Id);

            entity.Name = account.Name;
            Organization.Update<AccountEntity>(entity);
            Organization.SaveChanges();
        }

        public void DeleteAccount(Guid id)
        {
            var entity = Organization.Accounts.Single(a => a.Id == id);

            Organization.Delete<AccountEntity>(entity);
            Organization.SaveChanges();
        }
    }
}
