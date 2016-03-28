using Augustus.CRM.Converters;
using Augustus.CRM.Entities;
using Augustus.Domain.Objects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Augustus.CRM.Queries
{
    public class InvoiceQuery : BaseQuery
    {
        public InvoiceQuery(CrmContext context) : base(context) { }

        public Invoice GetItem(Guid id)
        {
            var inv = Context.Invoices.Single(i => i.Id == id).ConvertToDomain();

            inv.Opportunity = OpportunityConverter.ToDomainObject(Context.Opportunities.Single(o => o.Id == inv.OpportunityId));
            inv.Account = AccountConverter.ToDomainObject(Context.Accounts.Single(a => a.Id == inv.Opportunity.AccountId));
            inv.WorkDoneItems = GetWorkDoneItems(id);
            return inv;
        }

        private IEnumerable<WorkDoneItem> GetWorkDoneItems(Guid id)
        {
            return (from i in Context.WorkDoneItems
                    where i.InvoiceId == id
                    orderby i.WorkDoneDate descending
                    select WorkDoneItemConverter.ToDomain(i)).AsEnumerable();
        }

        public Guid Create(Invoice invoice)
        {

            var entity = new InvoiceEntity();
            entity.SetUsingDomain(invoice);

            // Ignore the AccountId being set on the domain object. Should raise exception?
            var opp = Context.Opportunities.Single(o => o.Id == invoice.OpportunityId);
            entity.AccountId = opp.AccountId;
            

            Context.Create(entity);
            Context.SaveChanges();

            return entity.Id;
        }

        public void Delete(Guid invoiceId)
        {
            var entity = Context.Invoices.Single(o => o.Id == invoiceId);

            Context.Delete(entity);
            Context.SaveChanges();
        }

        public void Update(Invoice invoice)
        {
            var entity = Context.Invoices.Single(o => o.Id == invoice.Id);

            entity.SetUsingDomain(invoice);

            Context.Update(entity);
            Context.SaveChanges();
        }
    }
}
