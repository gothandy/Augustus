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
            return Organization.Invoices.Single(i => i.Id == id).ToDomainObject();
        }

        public Opportunity GetOpportunity(Guid invoiceId)
        {
            var inv = GetInvoice(invoiceId);
            var opp = Organization.Opportunities.Single(o => o.Id == inv.OpportunityId);

            return OpportunityEntity.ToDomainObject(opp);
        }

        public Account GetAccount(Guid invoiceId)
        {
            var opp = GetOpportunity(invoiceId);
            var acc = Organization.Accounts.Single(a => a.Id == opp.AccountId);

            return AccountEntity.ToDomainObject(acc);
        }

        public IEnumerable<WorkDoneItem> GetWorkDoneItems(Guid id)
        {
            return (from i in Organization.WorkDoneItems
                    where i.InvoiceId == id
                    orderby i.WorkDoneDate descending
                    select WorkDoneItemEntity.ToDomainObject(i)).AsEnumerable();
        }

        public Guid CreateInvoice(Invoice invoice)
        {
            var entity = new InvoiceEntity();

            entity.SetUsingDomain(invoice);

            Organization.Create(entity);
            Organization.SaveChanges();

            return entity.Id;
        }

        public void DeleteInvoice(Guid invoiceId)
        {
            var entity = Organization.Invoices.Single(o => o.Id == invoiceId);

            Organization.Delete(entity);
            Organization.SaveChanges();
        }

        public void UpdateInvoice(Invoice invoice)
        {
            var entity = Organization.Invoices.Single(o => o.Id == invoice.Id);

            entity.SetUsingDomain(invoice);

            Organization.Update(entity);
            Organization.SaveChanges();
        }


    }
}
