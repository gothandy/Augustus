using Augustus.CRM.Converters;
using Augustus.CRM.Entities;
using Augustus.Domain.Interfaces;
using Augustus.Domain.Objects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Augustus.CRM.Queries
{
    public class OrganizationQuery : BaseQuery
    {

        public IEnumerable<Account> GetNewAccounts(DateTime createdAfter)
        {
            return (from a in Organization.Accounts
                    where a.Created > createdAfter
                    orderby a.Name ascending
                    select AccountConverter.ToDomainObject(a)).AsEnumerable();
        }

        public IEnumerable<Account> GetActiveAccounts(DateTime withInvoicesFrom)
        {
            return (from a in Organization.Accounts
                    join i in Organization.Invoices
                    on a.Id equals i.AccountId
                    where i.InvoiceDate > withInvoicesFrom
                    orderby a.Name ascending
                    select AccountConverter.ToDomainObject(a)).Distinct().AsEnumerable();
        }

        public IEnumerable<Account> GetNewAndActiveAccounts(DateTime createdAfter, DateTime withInvoicesFrom)
        {
            var newAccounts = GetNewAccounts(createdAfter);
            var activeAccounts = GetActiveAccounts(withInvoicesFrom);
            
            return activeAccounts.Union(newAccounts).Distinct().OrderBy(a => a.Name);
        }
    }
}
