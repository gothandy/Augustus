using Augustus.CRM.Converters;
using Augustus.CRM.Entities;
using Augustus.Domain.Objects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Augustus.CRM.Queries
{
    public class ReportQuery : BaseQuery
    {
        private List<AccountEntity> accounts;
        private List<InvoiceEntity> invoices;
        private List<WorkDoneItemEntity> workDoneItems;
        private List<AvailabilityItemEntity> availabilityItems;

        public ReportQuery(CrmContext context) : base(context)
        {
            accounts = Context.Accounts.ToList();
            invoices = Context.Invoices.ToList();
            workDoneItems = Context.WorkDoneItems.ToList();
            availabilityItems = Context.AvailabilityItems.ToList();
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

        public object GetAvailability()
        {
            return (from ai in availabilityItems
                    where (bool)ai.Active
                    select AvailabilityItemConverter.ConvertToDomain(ai));
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


        public IEnumerable<ReportAccount> GetAccounts(DateTime date)
        {
            var list = new List<ReportAccount>();

            foreach(var id in GetUnionOfAccounts(date))
            {
                list.Add(GetReport(date,id));
            }

            return list.OrderBy(a => a.AccountName);
        }

        public IEnumerable<ReportInvoice> GetInvoices(DateTime date, Guid id)
        {
            var invoicesWithWork = (from i in invoices
                    join w in workDoneItems on i.Id equals w.InvoiceId
                    where GetMonthStart(w.WorkDoneDate) == date && i.AccountId == id
                    select new ReportInvoice
                    {
                        InvoiceName = i.Name,
                        InvoiceId = i.Id,
                        InvoiceDate = i.InvoiceDate.Value,
                        Margin = i.Margin.GetValueOrDefault(),
                        WorkDone = w.Margin.GetValueOrDefault()
                    });

            var invoicesWithoutWork = (from i in invoices
                                       where
                                           GetMonthStart(i.InvoiceDate) == date &&
                                           i.AccountId == id &&
                                           !invoicesWithWork.Any(b => b.InvoiceId == i.Id)
                                       select new ReportInvoice
                                       {
                                           InvoiceName = i.Name,
                                           InvoiceId = i.Id,
                                           InvoiceDate = i.InvoiceDate.Value,
                                           Margin = i.Margin.GetValueOrDefault(),
                                           WorkDone = 0
                                       });

            return invoicesWithWork.Union(invoicesWithoutWork).OrderBy(i => i.InvoiceDate);
        }

        private ReportAccount GetReport(DateTime date, Guid id)
        {
            var report = new ReportAccount
            {
                AccountId = id,
                AccountName = GetAccountName(id),
                InvoiceTotal = GetInvoiceTotal(date, id),
                WorkDoneAndInvoiced = GetWorkDoneAndInvoiced(date,id),
                WorkDoneTotal = GetWorkDoneTotal(date,id)

            };

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

        public IEnumerable<ReportInvoice> GetWorkDoneErrors()
        {
            var query = (from i in invoices
                         join w in workDoneItems on i.Id equals w.InvoiceId
                         where (bool)i.Active && (bool)w.Active
                         group w by new
                         {
                             i.Id,
                             i.Name,
                             i.InvoiceDate,
                             i.Margin
                         } into g
                         select new ReportInvoice
                         {
                             InvoiceId = g.Key.Id,
                             InvoiceName = g.Key.Name,
                             InvoiceDate = g.Key.InvoiceDate.Value,
                             Margin = g.Key.Margin.Value,
                             WorkDone = g.Sum(w => w.Margin).GetValueOrDefault()
                         }).ToList();

            return query.Where(i => i.Margin != i.WorkDone);
        }

        public IEnumerable<ReportWorkDoneItem> GetWorkDoneItems()
        {
            return (from a in accounts
                    join i in invoices on a.Id equals i.AccountId
                    join w in workDoneItems on i.Id equals w.InvoiceId
                    where (bool)w.Active && (bool)i.Active
                    select new ReportWorkDoneItem
                    { 
                        Account = AccountConverter.ConvertToDomain(a),
                        Invoice = InvoiceConverter.ConvertToDomain(i),
                        WorkDoneItem = WorkDoneItemConverter.ConvertToDomain(w)
                    });
        }

        public IEnumerable<ReportProfitItem> GetProfitData()
        {
            return (from a in accounts
                    join p in availabilityItems on a.Id equals p.AccountId
                    select new ReportProfitItem
                    {
                        Account = AccountConverter.ConvertToDomain(a),
                        AvailabilityItem = AvailabilityItemConverter.ConvertToDomain(p),
                        WorkDoneMargin = GetWorkDoneMargin(a.Id, GetMonthStart(p.AvailabilityDate))
                    });
        }

        private decimal? GetWorkDoneMargin(Guid id, DateTime monthStart)
        {
            return (from a in accounts
                    join i in invoices on a.Id equals i.AccountId
                    join w in workDoneItems on i.Id equals w.InvoiceId
                    where (bool)w.Active && (bool)i.Active
                        && a.Id == id
                        && GetMonthStart(w.WorkDoneDate) == monthStart
                    select w.Margin).Sum();
        }
    }
}
