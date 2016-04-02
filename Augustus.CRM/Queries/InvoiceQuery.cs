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
        public InvoiceQuery(CrmContext context) : base(context) { }

        public Invoice GetItem(Guid id)
        {
            var inv = Context.Invoices.Single(i => i.Id == id).ConvertToDomain();

            inv.Opportunity = OpportunityConverter.ToDomainObject(Context.Opportunities.Single(o => o.Id == inv.OpportunityId));
            inv.Account = AccountConverter.ToDomainObject(Context.Accounts.Single(a => a.Id == inv.Opportunity.AccountId));

            var invEnum = GetEnumerableWorkDoneItems(id);
            var invList = GetListWorkDoneItems(GetLastDate(inv.InvoiceDate), inv);

            inv.WorkDoneItems = MergeWorkDoneItems(invList, invEnum);

            return inv;
        }

        private List<WorkDoneItem> MergeWorkDoneItems(List<WorkDoneItem> list, IEnumerable<WorkDoneItem> query)
        {
            foreach(var item in query)
            {
                var exists = list.Exists(i => i.WorkDoneDate == item.WorkDoneDate);

                if (exists)
                {
                    var index = list.FindIndex(i => i.WorkDoneDate == item.WorkDoneDate);
                    list[index] = item;
                }
                else
                {
                    list.Add(item);
                }
            }

            return list;
        }

        public Invoice GetNewItem(Guid parentId)
        {
            var opp = OpportunityConverter.ToDomainObject(Context.Opportunities.Single(a => a.Id == parentId));

            var inv = new Invoice
            {
                Opportunity = opp,
                AccountId = opp.AccountId,
                OpportunityId = parentId
            };

            inv.WorkDoneItems = GetListWorkDoneItems(GetLastDate(null), inv);

            return (inv);
        }
        private DateTime GetLastDate(DateTime? invoice)
        {
            var last = invoice.GetValueOrDefault(DateTime.Now);
            return new DateTime(last.Year, last.Month, 1);
        }

        private List<WorkDoneItem> GetListWorkDoneItems(DateTime last, Invoice inv)
        {
            var list = new List<WorkDoneItem>();

            for(var i = 0; i< 6; i++)
            {
                list.Add(new WorkDoneItem
                {
                    WorkDoneDate = last.AddMonths(-i)               
                });
            }

            return list;
        }

        private IEnumerable<WorkDoneItem> GetEnumerableWorkDoneItems(Guid id)
        {
            return (from i in Context.WorkDoneItems
                    where i.InvoiceId == id
                    orderby i.WorkDoneDate descending
                    select WorkDoneItemConverter.ToDomain(i)).AsEnumerable();
        }

        public IEnumerable<Opportunity> GetParentLookup(Guid parentId)
        {
            var accountId = (from o in Context.Opportunities
                             where o.Id == parentId
                             select o.AccountId).Single();

            var opportunities = (from o in Context.Opportunities
                                 where o.AccountId == accountId
                                 select OpportunityConverter.ToDomainObject(o));

            return opportunities;
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

        public Guid? Delete(Guid invoiceId)
        {
            var entity = Context.Invoices.Single(o => o.Id == invoiceId);

            var opportunityId = entity.OpportunityId;

            Context.Delete(entity);
            Context.SaveChanges();

            return opportunityId;
        }

        public void Update(Invoice invoice)
        {
            var entity = Context.Invoices.Single(o => o.Id == invoice.Id);
            entity.SetUsingDomain(invoice);
            Context.Update(entity);
            Context.SaveChanges();

            if (invoice.WorkDoneItems != null)
            {
                UpdateWorkDoneItems(entity, invoice);
            }
        }

        private void UpdateWorkDoneItems(InvoiceEntity entity, Invoice invoice)
        {

            var workDoneItemQuery = new WorkDoneItemQuery(Context);

            foreach (var item in invoice.WorkDoneItems)
            {
                if (item.Margin.HasValue)
                {
                    item.AccountId = entity.AccountId;
                    item.InvoiceId = entity.Id;

                    if (item.Id.HasValue)
                    {
                        workDoneItemQuery.Update(item);
                    }
                    else
                    {
                        workDoneItemQuery.Create(item);
                    }
                }
            }
        }
    }
}
