using System;
using System.Collections.Generic;
using Augustus.Domain.Objects;

namespace Augustus.Domain.Interfaces
{
    public interface IAccountQuery : IDisposable
    {
        Guid Id { get; set; }
        DateTime ActiveDate { get; set; }

        Account GetAccount();
        IEnumerable<Invoice> GetInvoices();
        //IEnumerable<Opportunity> GetOpportunities();
    }
}