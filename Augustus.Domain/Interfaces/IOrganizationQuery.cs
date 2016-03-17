using Augustus.Domain.Objects;
using System;
using System.Collections.Generic;

namespace Augustus.Domain.Interfaces
{
    public interface IOrganizationQuery : IDisposable
    {
        IEnumerable<Account> GetNewAccounts(DateTime createdAfter);
        IEnumerable<Account> GetActiveAccounts(DateTime invoicesFrom);
        IEnumerable<Account> GetNewAndActiveAccounts(DateTime createdAfter, DateTime invoicesFrom);
    }
}
