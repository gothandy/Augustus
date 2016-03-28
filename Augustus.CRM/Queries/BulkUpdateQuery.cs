using Augustus.Domain.Objects;
using System.Linq;

namespace Augustus.CRM.Queries
{
    public class BulkUpdateQuery : BaseQuery
    {
        public BulkUpdateQuery(CrmContext context) : base(context) { }

        public void BulkUpdate(Account account)
        { 
            var accountEntity =
                Context.Accounts.SingleOrDefault(a =>
                    a.Name == account.Name);

            if (accountEntity == null)
            {
                var accountQuery = new AccountQuery(Context);

                account.Id = accountQuery.Create(account);
            }
            else
            {
                account.Id = accountEntity.Id;
            }

            foreach (var opportunity in account.Opportunities)
            {
                opportunity.AccountId = account.Id;

                BulkUpdate(opportunity);
            }
        }

        public void BulkUpdate(Opportunity opportunity)
        {
            var opportunityEntity =
                Context.Opportunities.SingleOrDefault(a =>
                    a.AccountId == opportunity.AccountId &&
                    a.Name == opportunity.Name);

            if (opportunityEntity == null)
            {
                var opportunityQuery = new OpportunityQuery(Context);

                opportunity.Id = opportunityQuery.Create(opportunity);
            }
            else
            {
                opportunity.Id = opportunityEntity.Id;
            }

            foreach (var invoice in opportunity.Invoices)
            {
                invoice.AccountId = opportunity.AccountId;
                invoice.OpportunityId = opportunity.Id;

                BulkUpdate(invoice);
            }
        }

        public void BulkUpdate(Invoice invoice)
        {
            var invoiceEntity =
                Context.Invoices.SingleOrDefault(a =>
                    a.OpportunityId == invoice.OpportunityId &&
                    a.Name == invoice.Name);

            if (invoiceEntity == null)
            {
                var invoiceQuery = new InvoiceQuery(Context);

                invoice.Id = invoiceQuery.Create(invoice);
            }
            else
            {
                invoice.Id = invoiceEntity.Id;
            }

            foreach (var workDoneItem in invoice.WorkDoneItems)
            {
                workDoneItem.AccountId = invoice.AccountId;
                workDoneItem.InvoiceId = invoice.Id;

                BulkUpdate(workDoneItem);
            }
        }

        public void BulkUpdate(WorkDoneItem workDoneItem)
        {
            var workDoneItemEntity =
                Context.WorkDoneItems.SingleOrDefault(a =>
                    a.InvoiceId == workDoneItem.InvoiceId &&
                    a.WorkDoneDate == workDoneItem.WorkDoneDate);

            if (workDoneItemEntity == null)
            {
                var workDoneItemQuery = new WorkDoneItemQuery(Context);

                workDoneItem.Id = workDoneItemQuery.Create(workDoneItem);
            }
            else
            {
                workDoneItem.Id = workDoneItemEntity.Id;
            }
        }
    }
}
