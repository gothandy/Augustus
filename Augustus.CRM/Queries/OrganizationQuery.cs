using Augustus.CRM.Entities;
using Augustus.Domain.Interfaces;
using Augustus.Domain.Objects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Augustus.CRM.Queries
{
    public class OrganizationQuery : BaseQuery, IOrganizationQuery
    {
        public DateTime ActiveDate { get; set; }
        public DateTime NewDate { get; set; }

        public IEnumerable<Account> GetNewAccounts()
        {
            return (from a in Organization.Accounts
                    where a.Created > NewDate
                    select AccountEntity.ToDomainObject(a)).AsEnumerable();
        }

        public IEnumerable<Account> GetActiveAccounts()
        {
            return (from a in Organization.Accounts
                    join i in Organization.Invoices
                    on a.Id equals i.AccountId
                    where i.InvoiceDate > ActiveDate
                    orderby a.Name ascending
                    select AccountEntity.ToDomainObject(a)).Distinct().AsEnumerable();
        }

        public IEnumerable<Account> GetNewAndActiveAccounts()
        {
            var activeAccounts = GetActiveAccounts();
            var newAccounts = GetNewAccounts();

            return activeAccounts.Union(newAccounts).Distinct().OrderBy(a => a.Name);
        }
    }
}
