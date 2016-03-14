using Augustus.CRM;
using Augustus.CRM.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Augustus.Domain
{
    public class Accounts 
    {
        public OrgQueryable Organization { get; set; }
        public DateTime ActiveDate { get; set; }
        public DateTime NewDate { get; set; }
        
        public IEnumerable<CRM.Entities.Account> GetNewAndActiveAccounts()
        {
            var activeAccounts = GetActiveAccounts();
            var newAccounts = GetNewAccounts();

            return activeAccounts.Union(newAccounts).Distinct().OrderBy(a => a.Name);
        }

        private IEnumerable<CRM.Entities.Account> GetNewAccounts()
        {
            return (from a in Organization.Accounts
                    where a.Created > NewDate
                    select a).AsEnumerable();
        }

        private IEnumerable<CRM.Entities.Account> GetActiveAccounts()
        {
            return (from a in Organization.Accounts
                    join i in Organization.Invoices
                    on a.Id equals i.AccountId
                    where i.InvoiceDate > ActiveDate
                    orderby a.Name ascending
                    select a).Distinct().AsEnumerable();
        }
    }
}
