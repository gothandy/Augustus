﻿using System;
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
    public class OpportunityQuery : BaseQuery, IOpportunityQuery
    {
        public Opportunity GetOpportunity(Guid id)
        {
            return OpportunityEntity.ToDomainObject(Organization.Opportunities.Single(o => o.Id == id));
        }

        public Account GetAccount(Guid opportunityId)
        {
            var opp = Organization.Opportunities.Single(o => o.Id == opportunityId);

            var acc = Organization.Accounts.Single(a => a.Id == opp.AccountId);

            return AccountEntity.ToDomainObject(acc);
        }

        public IEnumerable<Invoice> GetInvoices(Guid opportunityId)
        {
            return (from i in Organization.Invoices
                    where i.OpportunityId == opportunityId
                    orderby i.InvoiceDate descending
                    select InvoiceEntity.ToDomainObject(i)).AsEnumerable();
        }

        public Guid CreateOpportunity(Opportunity opportunity)
        {
            var entity = new OpportunityEntity
            {
                Name = opportunity.Name,
                AccountId = opportunity.AccountId
            };

            Organization.Create(entity);
            Organization.SaveChanges();

            return entity.Id;
        }

        public void DeleteOpportunity(Guid opportunityId)
        {
            var entity = Organization.Opportunities.Single(o => o.Id == opportunityId);

            Organization.Delete(entity);
            Organization.SaveChanges();
        }

        public void UpdateOpportunity(Opportunity opportunity)
        {
            var entity = Organization.Opportunities.Single(o => o.Id == opportunity.Id);

            entity.Name = opportunity.Name;

            Organization.Update(entity);
            Organization.SaveChanges();
        }
    }
}