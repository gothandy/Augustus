using System.Collections.Generic;

namespace Augustus.Interfaces
{
    public interface IOrganization
    {
        IEnumerable<IAccount> Accounts { get; }
        IEnumerable<IInvoice> Invoices { get; }
    }
}
