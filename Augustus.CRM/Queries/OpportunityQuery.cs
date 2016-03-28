using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Augustus.CRM;
using Augustus.CRM.Entities;
using Augustus.Domain.Objects;
using Augustus.CRM.Converters;

namespace Augustus.CRM.Queries
{
    public class OpportunityQuery : BaseQuery
    {
        public OpportunityQuery(CrmContext context) : base(context) { }

        public Opportunity GetItem(Guid id)
        {
            return OpportunityConverter.ToDomainObject(Context.Opportunities.Single(o => o.Id == id));
        }

        public Account GetAccount(Guid opportunityId)
        {
            var opp = GetItem(opportunityId);
            var acc = Context.Accounts.Single(a => a.Id == opp.AccountId);

            return AccountConverter.ToDomainObject(acc);
        }

        public IEnumerable<Invoice> GetInvoices(Guid opportunityId)
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

        public void Delete(Guid opportunityId)
        {
            var entity = Context.Opportunities.Single(o => o.Id == opportunityId);

            Context.Delete(entity);
            Context.SaveChanges();
        }

        public void Update(Opportunity opportunity)
        {
            var entity = Context.Opportunities.Single(o => o.Id == opportunity.Id);

            entity.Name = opportunity.Name;
            entity.AccountId = opportunity.AccountId;

            Context.Update(entity);
            Context.SaveChanges();
        }
    }
}
