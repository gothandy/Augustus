using Augustus.CRM;
using System;
using System.Configuration;
using System.IO;
using System.Linq;

namespace Augustus.Console.InvoiceWipCsv
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = ConfigurationManager.AppSettings["AugustusCRM"];

            using (OrgQueryable org = new OrgQueryable(connectionString))
            {
                var status = org.InvoiceStatus.ToList();
                var month = new DateTime(2016, 2, 1);
                var invoices =
                    (from a in org.Accounts
                     join i in org.Invoices on a.AccountId equals i.DirectClientId
                     where i.InvoiceDate > month
                     select new Invoice
                     {
                         Customer = a.Name,
                         Name = i.Name,
                         Margin = i.Margin,
                         PreviousWIP = i.WipPevious,
                         CurrentWIP = i.WipCurrent,
                         InvoiceDate = i.InvoiceDate.Value,
                         Status = (from s in status where i.Status == s.Value select s.Label).Single()
                     });


                using (var writer = new StreamWriter("C:\\TC\\Augustus\\InvoiceWip.csv"))
                {
                    foreach (var invoice in invoices)
                    {
                        var items = new string[] {
                            invoice.Customer,
                            string.Format("\"{0}\"", invoice.Name),
                            MoneyToString(invoice.Margin),
                            MoneyToString(invoice.PreviousWIP),
                            MoneyToString(invoice.CurrentWIP),
                            invoice.InvoiceDate.ToShortDateString(),
                            invoice.Status
                        };

                        var line = string.Join(",", items);

                        writer.WriteLine(line);
                    }
                }


                System.Console.ReadKey();
            }


        }
        private static string MoneyToString(decimal? money)
        {
            if (money.HasValue)
            {
                return money.Value.ToString();
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
