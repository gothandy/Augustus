using Augustus.Domain.Objects;
using System;
using System.Collections.Generic;

namespace Augustus.Domain.Interfaces
{
    public interface IOrganizationQuery : IDisposable
    {
        DateTime ActiveDate { get; set; }
        DateTime NewDate { get; set; }

        IEnumerable<Account> GetNewAccounts();
        IEnumerable<Account> GetActiveAccounts();
        IEnumerable<Account> GetNewAndActiveAccounts();
    }
}
