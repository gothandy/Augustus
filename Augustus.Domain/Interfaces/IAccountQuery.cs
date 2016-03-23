using System;
using System.Collections.Generic;
using Augustus.Domain.Objects;

namespace Augustus.Domain.Interfaces
{
    public interface IAccountQuery : IDisposable
    {
        Account GetAccount(Guid id);
        IEnumerable<Invoice> GetInvoices(Guid accountId, DateTime from);
        IEnumerable<Opportunity> GetNewOpportunities(Guid accountId, DateTime createdAfter);
        IEnumerable<Opportunity> GetActiveOpportunities(Guid accountId, DateTime invoiceFrom);
        IEnumerable<Opportunity> GetNewAndActiveOpportunities(Guid accountId, DateTime createdAfter, DateTime invoicesFrom);
        Guid CreateAccount(Account account);
        void UpdateAccount(Account account);
        void DeleteAccount(Guid id);
    }
}