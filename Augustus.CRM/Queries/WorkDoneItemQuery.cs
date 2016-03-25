using Augustus.CRM.Entities;
using Augustus.Domain.Interfaces;
using Augustus.Domain.Objects;
using System;
using System.Linq;

namespace Augustus.CRM.Queries
{
    public class WorkDoneItemQuery : BaseQuery, IQuery<WorkDoneItem>
    {
        public WorkDoneItem GetItem(Guid id)
        {
            return WorkDoneItemEntity.ToDomainObject(Organization.WorkDoneItems.Single(i => i.Id == id));
        }

        public Invoice GetInvoice(Guid workDoneItemId)
        {
            var wdi = GetItem(workDoneItemId);
            var inv = Organization.Invoices.Single(i => i.Id == wdi.InvoiceId);

            return inv.ToDomainObject();
        }

        public Opportunity GetOpportunity(Guid workDoneItemId)
        {
            var inv = GetInvoice(workDoneItemId);
            var opp = Organization.Opportunities.Single(o => o.Id == inv.OpportunityId);

            return OpportunityEntity.ToDomainObject(opp);
        }

        public Account GetAccount(Guid WorkDoneItemId)
        {
            var inv = GetOpportunity(WorkDoneItemId);
            var acc = Organization.Accounts.Single(a => a.Id == inv.AccountId);

            return AccountEntity.ToDomainObject(acc);
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

            Organization.Create(entity);
            Organization.SaveChanges();

            return entity.Id;
        }

        public void Delete(Guid workDoneItemId)
        {
            var entity = Organization.WorkDoneItems.Single(o => o.Id == workDoneItemId);

            Organization.Delete(entity);
            Organization.SaveChanges();
        }

        public void Update(WorkDoneItem workDoneItem)
        {
            var entity = Organization.WorkDoneItems.Single(o => o.Id == workDoneItem.Id);

            entity.AccountId = workDoneItem.AccountId;
            entity.InvoiceId = workDoneItem.InvoiceId;
            entity.WorkDoneDate = workDoneItem.WorkDoneDate;
            entity.Margin = workDoneItem.Margin;

            Organization.Update(entity);
            Organization.SaveChanges();
        }
    }
}
