using Augustus.CRM;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;

namespace Augustus.App.ImportWorkDoneItems
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = ConfigurationManager.AppSettings["AugustusCRM"];

            using (OrgQueryable org = new OrgQueryable(connectionString))
            {
                string path = @"E:\SharePoint\Management Team - Documents\Reports\WorkDoneItems2015plus.csv";

                foreach (string line in File.ReadLines(path))
                {
                    foreach(WorkDoneItem item in GetWorkDoneItemsFromCsv(line))
                    {
                        // Duplicates created.
                        org.Create<WorkDoneItem>(item);
                    }
                }

                org.Save();

                Console.ReadKey();
            }
        }

        private static List<WorkDoneItem> GetWorkDoneItemsFromCsv(string line)
        {
            var list = new List<WorkDoneItem>();
            var items = line.Split(',');

            var invoiceId = new Guid(items[0]);

            for (int i = 1; i < items.Count(); i++)
            {
                var item = items[i];

                decimal margin = Convert.ToDecimal(item);

                if (margin != 0)
                {
                    var date = new DateTime(2015, 1, 1).AddMonths(i);

                    list.Add(
                        new WorkDoneItem()
                        {
                            InvoiceId = invoiceId,
                            WorkDoneDate = date,
                            Margin = margin
                        }
                    );
                }
            }

            return list;
        }
    }
}
