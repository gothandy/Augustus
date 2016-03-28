using Augustus.CRM;
using Augustus.CRM.Entities;
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
            string connectionString = ConfigurationManager.AppSettings["crm:ConnectionString"];

            var svc = new CrmServiceConnectionString(connectionString);
       
            using (var org = new CrmContext(svc))
            {
                string path = @"E:\SharePoint\Management Team - Documents\Reports\WorkDoneItems2015plus.csv";

                foreach (string line in File.ReadLines(path))
                {
                    foreach(WorkDoneItemEntity item in GetWorkDoneItemsFromCsv(line))
                    {
                        // Duplicates created. Commented out for safety.
                        // org.Create<WorkDoneItem>(item);
                    }
                }

                org.SaveChanges();

                Console.ReadKey();
            }
        }

        private static List<WorkDoneItemEntity> GetWorkDoneItemsFromCsv(string line)
        {
            var list = new List<WorkDoneItemEntity>();
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
                        new WorkDoneItemEntity()
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
