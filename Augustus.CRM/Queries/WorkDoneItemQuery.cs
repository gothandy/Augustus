using Augustus.CRM.Converters;
using Augustus.CRM.Entities;
using Augustus.Domain.Interfaces;
using Augustus.Domain.Objects;
using System;
using System.Linq;

namespace Augustus.CRM.Queries
{
    public class WorkDoneItemQuery : BaseQuery, IQuery<WorkDoneItem>
    {
        public WorkDoneItemQuery(CrmContext context) : base(context) { }

        public WorkDoneItem GetItem(Guid id)
        {
            return WorkDoneItemConverter.ToDomain(Context.WorkDoneItems.Single(i => i.Id == id));
        }

        public Invoice GetInvoice(Guid workDoneItemId)
        {
            var wdi = GetItem(workDoneItemId);
            var inv = Context.Invoices.Single(i => i.Id == wdi.InvoiceId);

            return inv.ConvertToDomain();
        }

        public Opportunity GetOpportunity(Guid workDoneItemId)
        {
            var inv = GetInvoice(workDoneItemId);
            var opp = Context.Opportunities.Single(o => o.Id == inv.OpportunityId);

            return OpportunityConverter.ToDomainObject(opp);
        }

        public Account GetAccount(Guid WorkDoneItemId)
        {
            var inv = GetOpportunity(WorkDoneItemId);
            var acc = Context.Accounts.Single(a => a.Id == inv.AccountId);

            return AccountConverter.ToDomainObject(acc);
        }

        public Guid Create(WorkDoneItem workDoneItem)
        {
            var entity = new WorkDoneItemEntity
            {
                AccountId = workDoneItem.AccountId,
                InvoiceId = workDoneItem.InvoiceId,
                WorkDoneDate = workDoneItem.WorkDoneDate,
                Margin = workDoneItem.Margin
            };

            Context.Create(entity);
            Context.SaveChanges();

            return entity.Id;
        }

        public void Delete(Guid workDoneItemId)
        {
            var entity = Context.WorkDoneItems.Single(o => o.Id == workDoneItemId);

            Context.Delete(entity);
            Context.SaveChanges();
        }

        public void Update(WorkDoneItem workDoneItem)
        {
            var entity = Context.WorkDoneItems.Single(o => o.Id == workDoneItem.Id);

            entity.AccountId = workDoneItem.AccountId;
            entity.InvoiceId = workDoneItem.InvoiceId;
            entity.WorkDoneDate = workDoneItem.WorkDoneDate;
            entity.Margin = workDoneItem.Margin;

            Context.Update(entity);
            Context.SaveChanges();
        }
    }
}
