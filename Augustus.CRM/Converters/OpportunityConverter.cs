using Augustus.CRM.Entities;
using Augustus.Domain.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Augustus.CRM.Converters
{
    public static class OpportunityConverter
    {
        public static Opportunity ToDomainObject(OpportunityEntity o)
        {
            return new Opportunity
            {
                AccountId = o.AccountId,
                Created = o.Created,
                Id = o.Id,
                Name = o.Name
            };
        }
    }
}
