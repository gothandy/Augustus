using Augustus.Domain.Objects;
using System;
using System.Collections.Generic;

namespace Augustus.Web.Portal.Interfaces
{
    public interface IOpportunityDropDown
    {
        Guid OpportunityId { get; set; }
        IEnumerable<Opportunity> Opportunities { get; set; }
    }
}
