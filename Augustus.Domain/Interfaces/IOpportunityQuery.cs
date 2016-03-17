using Augustus.Domain.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Augustus.Domain.Interfaces
{
    public interface IOpportunityQuery : IDisposable
    {
        Account GetAccount(Guid opportunityId);
        Opportunity GetOpportunity(Guid opportunityId);
        IEnumerable<Invoice> GetInvoices(Guid opportunityId);

        Guid CreateOpportunity(Opportunity opportunity);
        void UpdateOpportunity(Opportunity opportunity);
        void DeleteOpportunity(Guid opportunityId);
    }
}
