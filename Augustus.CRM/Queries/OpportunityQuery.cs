using Augustus.CRM.Converters;
using Augustus.CRM.Entities;
using Augustus.Domain.Interfaces;
using Augustus.Domain.Objects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Augustus.CRM.Queries
{
    public class OpportunityQuery : BaseQuery, IQuery<Opportunity>
    {
        public OpportunityQuery(CrmContext context) : base(context) { }

        public Opportunity GetItem(Guid id)
        {
            var opp = OpportunityConverter.ToDomainObject(Context.Opportunities.Single(o => o.Id == id));

            opp.Invoices = GetInvoices(id);

            return opp;
        }

        public Account GetParent(Guid id)
        {
            var parentId = Context.Opportunities.Single(o => o.Id == id).AccountId;
            return AccountConverter.ToDomainObject(Context.Accounts.Single(a => a.Id == parentId));
        }

        private IEnumerable<Invoice> GetInvoices(Guid opportunityId)
        {
            return (from i in Context.Invoices
                    where i.OpportunityId == opportunityId
                    orderby i.InvoiceDate descending
                    select InvoiceConverter.ToDomain(i)).AsEnumerable();
        }

        public Guid Create(Opportunity opportunity)
        {
            var entity = new OpportunityEntity
            {
                Name = opportunity.Name,
                AccountId = opportunity.AccountId
            };

            Context.Create(entity);
            Context.SaveChanges();

            return entity.Id;
        }

        public void Update(Opportunity opportunity)
        {
            var entity = Context.Opportunities.Single(o => o.Id == opportunity.Id);

            entity.Name = opportunity.Name;
            entity.AccountId = opportunity.AccountId;

            Context.Update(entity);
            Context.SaveChanges();
        }

        public Guid? Delete(Guid opportunityId)
        {
            var entity = Context.Opportunities.Single(o => o.Id == opportunityId);

            var parentId = entity.AccountId;

            Context.Delete(entity);
            Context.SaveChanges();

            return parentId;
        }
    }
}
