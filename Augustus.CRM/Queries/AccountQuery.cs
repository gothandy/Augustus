﻿using Augustus.CRM;
using Augustus.CRM.Converters;
using Augustus.CRM.Entities;
using Augustus.Domain.Interfaces;
using Augustus.Domain.Objects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Augustus.CRM.Queries
{
    public class AccountQuery : BaseQuery, IQuery<Account>
    {
        public AccountQuery(CrmContext context) : base(context) { }

        public Account GetItem(Guid id)
        {
            var account = (from a in Context.Accounts
                           where a.Id == id
                           select AccountConverter.ToDomain(a)).Single();

            account.Invoices = GetInvoices(id, Context.ActiveDate);
            account.Opportunities = GetNewAndActiveOpportunities(id, Context.NewDate, Context.ActiveDate);

            return account;
        }

        private IEnumerable<Invoice> GetInvoices(Guid accountId, DateTime from)
        {
            return (from i in Context.Invoices
                    join o in Context.Opportunities on i.OpportunityId equals o.Id
                    where o.AccountId == accountId
                    && i.InvoiceDate > @from
                    orderby i.InvoiceDate descending
                    select InvoiceConverter.ToDomain(i,o)).AsEnumerable();
        }

        private IEnumerable<Opportunity> GetActiveOpportunities(Guid accountId, DateTime invoicesFrom)
        {
            return (from o in Context.Opportunities
                    join i in Context.Invoices
                    on o.Id equals i.OpportunityId
                    where i.InvoiceDate > invoicesFrom
                    && o.AccountId == accountId
                    orderby o.Name ascending
                    select OpportunityConverter.ToDomainObject(o)).Distinct().AsEnumerable();
        }

        private IEnumerable<Opportunity> GetNewOpportunities(Guid accountId, DateTime createdAfter)
        {
            return (from o in Context.Opportunities
                    where o.Created > createdAfter
                    && o.AccountId == accountId
                    orderby o.Name ascending
                    select OpportunityConverter.ToDomainObject(o)).AsEnumerable();
        }

        private IEnumerable<Opportunity> GetNewAndActiveOpportunities(Guid accountId, DateTime createdAfter, DateTime invoicesFrom)
        {
            return GetNewOpportunities(accountId, createdAfter).Union(GetActiveOpportunities(accountId, invoicesFrom)).Distinct();
        }

        public Guid Create(Account account)
        {
            var entity = new AccountEntity();

            entity.SetUsingDomain(account);

            Context.Create<AccountEntity>(entity);
            Context.SaveChanges();

            return entity.Id;
        }

        public void Update(Account account)
        {
            var entity = Context.Accounts.Single(a => a.Id == account.Id);

            entity.SetUsingDomain(account);

            Context.Update<AccountEntity>(entity);
            Context.SaveChanges();
        }

        public Guid? Delete(Guid id)
        {
            var entity = Context.Accounts.Single(a => a.Id == id);

            Context.Delete<AccountEntity>(entity);
            Context.SaveChanges();
            return null;
        }

        public Guid? GetParentId(Guid id)
        {
            return null;
        }
    }
}
