using Augustus.CRM.Converters;
using Augustus.CRM.Entities;
using Augustus.Domain.Interfaces;
using Augustus.Domain.Objects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Augustus.CRM.Queries
{
    public class InvoiceQuery : BaseQuery, IQuery<Invoice>
    {
        public Invoice GetItem(Guid id)
        {
            var inv = Organization.Invoices.Single(i => i.Id == id).ConvertToDomain();

            inv.Opportunity = OpportunityConverter.ToDomainObject(Organization.Opportunities.Single(o => o.Id == inv.OpportunityId));
            inv.Account = AccountConverter.ToDomainObject(Organization.Accounts.Single(a => a.Id == inv.Opportunity.AccountId));
            inv.WorkDoneItems = GetWorkDoneItems(id);
            return inv;
        }

        private IEnumerable<WorkDoneItem> GetWorkDoneItems(Guid id)
        {
            return (from i in Organization.WorkDoneItems
                    where i.InvoiceId == id
                    orderby i.WorkDoneDate descending
                    select WorkDoneItemConverter.ToDomain(i)).AsEnumerable();
        }

        public Guid Create(Invoice invoice)
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

        public void Delete(Guid invoiceId)
        {
            var entity = Organization.Invoices.Single(o => o.Id == invoiceId);

            Organization.Delete(entity);
            Organization.SaveChanges();
        }

        public void Update(Invoice invoice)
        {
            var entity = Organization.Invoices.Single(o => o.Id == invoice.Id);

            entity.SetUsingDomain(invoice);

            Organization.Update(entity);
            Organization.SaveChanges();
        }
    }
}
