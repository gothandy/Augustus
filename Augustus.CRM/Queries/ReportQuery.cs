using Augustus.CRM.Entities;
using Augustus.Domain.Objects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Augustus.CRM.Queries
{
    public class ReportQuery : BaseQuery
    {
        private List<InvoiceEntity> invoices;
        private List<WorkDoneItemEntity> workDoneItems;

        public ReportQuery(CrmContext context) : base(context)
        {
            invoices = Context.Invoices.ToList();
            workDoneItems = Context.WorkDoneItems.ToList();
        }

        public IEnumerable<ReportMonth> GetMonths()
        {
            return (from w in workDoneItems
                    group w by GetMonthStart(w.WorkDoneDate)
                    into g
                    orderby g.Key descending
                    select new ReportMonth
                    {
                        Month = g.Key,
                        Invoice = GetInvoiceTotal(g.Key),
                        WorkDone = g.Sum(i => i.Margin).Value
                    }).AsEnumerable();

        }

        private DateTime GetMonthStart(DateTime? month)
        {
            var m = month.GetValueOrDefault();
            return new DateTime(m.Year, m.Month, 1);
        }

        private decimal GetInvoiceTotal(DateTime month)
        {
            return (from i in invoices
                    where GetMonthStart(i.InvoiceDate) == month
                    select i.Margin.GetValueOrDefault()).Sum();
        }
        private decimal GetInvoiceTotal(DateTime date, Guid id)
        {
            return (from i in invoices
                    where GetMonthStart(i.InvoiceDate) == date && i.AccountId == id
                    select i.Margin.GetValueOrDefault()).Sum();
        }

        private decimal GetWorkDoneTotal(DateTime date, Guid id)
        {
            return (from i in workDoneItems
                    where GetMonthStart(i.WorkDoneDate) == date && i.AccountId == id
                    select i.Margin.GetValueOrDefault()).Sum();
        }


        public List<ReportAccount> GetAccounts(DateTime date)
        {
            var list = new List<ReportAccount>();

            foreach(var id in GetUnionOfAccounts(date))
            {
                list.Add(GetReport(date,id));
            }

            return list;
        }

        private ReportAccount GetReport(DateTime date, Guid id)
        {
            var report = new ReportAccount
            {
                AccountId = id,
                Account = GetAccountName(id),
                InvoiceTotal = GetInvoiceTotal(date, id),
                WorkDoneAndInvoiced = GetWorkDoneAndInvoiced(date,id),
                WorkDoneTotal = GetWorkDoneTotal(date,id)

            };

            report.WorkNotDoneAndInvoiced = report.InvoiceTotal - report.WorkDoneAndInvoiced;
            report.WorkDoneAndNotInvoiced = report.WorkDoneTotal - report.WorkDoneAndInvoiced;

            return report;
        }

        private decimal GetWorkDoneAndInvoiced(DateTime date, Guid id)
        {
            return (from i in invoices
                    join w in workDoneItems on i.Id equals w.InvoiceId
                    where
                        i.InvoiceDate.HasValue &&
                        w.WorkDoneDate.HasValue &&
                        i.AccountId == id &&
                        GetMonthStart(i.InvoiceDate) == date &&
                        GetMonthStart(w.WorkDoneDate) == date
                    select w.Margin.GetValueOrDefault()).Sum();
        }

        private string GetAccountName(Guid id)
        {
            return (from a in Context.Accounts where a.Id == id select a.Name).Single();
        }

        private IEnumerable<Guid> GetUnionOfAccounts(DateTime date)
        {
            var invoiced = (from i in invoices
                            where i.AccountId.HasValue && i.InvoiceDate.HasValue && GetMonthStart(i.InvoiceDate.Value) == date
                            group i by i.AccountId into g
                            select g.Key.Value).ToList();


            var workdone = (from w in workDoneItems
                            where w.AccountId.HasValue && w.WorkDoneDate.HasValue && GetMonthStart(w.WorkDoneDate.Value) == date
                            group w by w.AccountId into g
                            select g.Key.Value).ToList();

            return invoiced.Union(workdone);
        }
    }
}
