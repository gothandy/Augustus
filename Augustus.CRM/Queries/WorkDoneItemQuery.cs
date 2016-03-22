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
    public class WorkDoneItemQuery : BaseQuery
    {
        public WorkDoneItem GetWorkDoneItem(Guid id)
        {
            return WorkDoneItemEntity.ToDomainObject(Organization.WorkDoneItems.Single(i => i.Id == id));
        }

        public Invoice GetInvoice(Guid workDoneItemId)
        {
            var wdi = GetWorkDoneItem(workDoneItemId);
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

        public Guid CreateWorkDoneItem(WorkDoneItem workDoneItem)
        {
            var entity = new WorkDoneItemEntity
            {
                InvoiceId = workDoneItem.InvoiceId,
                WorkDoneDate = workDoneItem.WorkDoneDate,
                Margin = workDoneItem.Margin
            };

            Organization.Create(entity);
            Organization.SaveChanges();

            return entity.Id;
        }

        public void DeleteWorkDoneItem(Guid workDoneItemId)
        {
            var entity = Organization.Opportunities.Single(o => o.Id == workDoneItemId);

            Organization.Delete(entity);
            Organization.SaveChanges();
        }

        public void UpdateWorkDoneItem(WorkDoneItem workDoneItem)
        {
            var entity = Organization.WorkDoneItems.Single(o => o.Id == workDoneItem.Id);

            entity.InvoiceId = workDoneItem.InvoiceId;
            entity.WorkDoneDate = workDoneItem.WorkDoneDate;
            entity.Margin = workDoneItem.Margin;

            Organization.Update(entity);
            Organization.SaveChanges();
        }
    }
}
