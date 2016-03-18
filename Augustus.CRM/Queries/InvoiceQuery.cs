using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Augustus.CRM;
using Augustus.CRM.Entities;
using Augustus.Domain.Objects;
using Augustus.Domain.Interfaces;

namespace Augustus.CRM.Queries
{
    public class InvoiceQuery : BaseQuery
    {
        public Invoice GetInvoice(Guid id)
        {
            return InvoiceEntity.ToDomainObject(Organization.Invoices.Single(i => i.Id == id));
        }

        public Account GetAccount(Guid invoiceId)
        {
            var inv = GetInvoice(invoiceId);
            var acc = Organization.Accounts.Single(a => a.Id == inv.AccountId);

            return AccountEntity.ToDomainObject(acc);
        }

        public Opportunity GetOpportunity(Guid invoiceId)
        {
            var inv = GetInvoice(invoiceId);
            var opp = Organization.Opportunities.Single(o => o.Id == inv.OpportunityId);

            return OpportunityEntity.ToDomainObject(opp);
        }

        public IEnumerable<WorkDoneItem> GetWorkDoneItems(Guid id)
        {
            return (from i in Organization.WorkDoneItems
                    where i.InvoiceId == id
                    orderby i.WorkDoneDate descending
                    select WorkDoneItemEntity.ToDomainObject(i)).AsEnumerable();
        }

        public Guid CreateInvoice(Invoice Invoice)
        {
            var entity = new InvoiceEntity
            {
                Name = Invoice.Name,
                AccountId = Invoice.AccountId
            };

            Organization.Create(entity);
            Organization.SaveChanges();

            return entity.Id;
        }

        public void DeleteInvoice(Guid InvoiceId)
        {
            var entity = Organization.Opportunities.Single(o => o.Id == InvoiceId);

            Organization.Delete(entity);
            Organization.SaveChanges();
        }



        public void UpdateInvoice(Invoice Invoice)
        {
            var entity = Organization.Opportunities.Single(o => o.Id == Invoice.Id);

            entity.Name = Invoice.Name;

            Organization.Update(entity);
            Organization.SaveChanges();
        }
    }
}
