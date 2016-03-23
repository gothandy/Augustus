using Augustus.CRM.Entities;
using Augustus.Domain.Objects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Augustus.CRM.Queries
{
    public class InvoiceQuery : BaseQuery
    {
        public Invoice GetInvoice(Guid id)
        {
            var inv = Organization.Invoices.Single(i => i.Id == id).ToDomainObject();

            inv.Opportunity = OpportunityEntity.ToDomainObject(Organization.Opportunities.Single(o => o.Id == inv.OpportunityId));
            inv.Account = AccountEntity.ToDomainObject(Organization.Accounts.Single(a => a.Id == inv.Opportunity.AccountId));
            inv.WorkDoneItems = GetWorkDoneItems(id);
            return inv;
        }

        private IEnumerable<WorkDoneItem> GetWorkDoneItems(Guid id)
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

            // Ignore the AccountId being set on the domain object. Should raise exception?
            var opp = Organization.Opportunities.Single(o => o.Id == invoice.OpportunityId);
            entity.AccountId = opp.AccountId;
            

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
