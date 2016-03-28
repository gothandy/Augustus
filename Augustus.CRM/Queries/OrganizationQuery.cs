using Augustus.CRM.Converters;
using Augustus.Domain.Objects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Augustus.CRM.Queries
{
    public class OrganizationQuery : BaseQuery
    {
        public OrganizationQuery(CrmContext context) : base(context) { }

        public IEnumerable<Account> GetNewAccounts()
        {
            return (from a in Context.Accounts
                    where a.Created > Context.NewDate
                    orderby a.Name ascending
                    select AccountConverter.ToDomainObject(a)).AsEnumerable();
        }

        public IEnumerable<Account> GetActiveAccounts()
        {
            return (from a in Context.Accounts
                    join i in Context.Invoices
                    on a.Id equals i.AccountId
                    where i.InvoiceDate > Context.ActiveDate
                    orderby a.Name ascending
                    select AccountConverter.ToDomainObject(a)).Distinct().AsEnumerable();
        }

        public IEnumerable<Account> GetNewAndActiveAccounts()
        {
            var newAccounts = GetNewAccounts();
            var activeAccounts = GetActiveAccounts();
            
            return activeAccounts.Union(newAccounts).Distinct().OrderBy(a => a.Name);
        }
    }
}
