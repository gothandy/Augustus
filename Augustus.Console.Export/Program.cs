using Augustus.CRM;
using Augustus.CRM.Queries;
using Augustus.Domain.Objects;
using Augustus.Web.Config;
using Microsoft.WindowsAzure.Storage;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Linq;

namespace Augustus.Console.Export
{
    class Program
    {
        static void Main(string[] args)
        {
            var service = new CrmServiceConnectionString(AppSettings.CrmConnectionString);
            var context = service.GetContext();
            var query = new ReportQuery(context);

            var azureStorageAccount = CloudStorageAccount.Parse(AppSettings.AzureStorageConnectionString);
            var azureBlobClient = azureStorageAccount.CreateCloudBlobClient();
            var azureBlobContainer = azureBlobClient.GetContainerReference(AppSettings.AzureExportBlobContainer);

            WorkDoneExport export = GetWorkDoneExport(query);
            UploadText<WorkDoneExport>(azureBlobContainer, export, AppSettings.AzureWorkDoneBlobName);

            ProfitExport profit = GetProfitExport(query);
            UploadText<ProfitExport>(azureBlobContainer, profit, AppSettings.AzureProfitBlobName);
        }

        private static ProfitExport GetProfitExport(ReportQuery query)
        {
            var profitItems = query.GetProfitData();

            var profit = new ProfitExport();

            IEnumerable<MonthExport> months =
                from p in profitItems
                select NewMonthExport(p);

            profit.AddRange(months);

            return profit;
        }

        private static MonthExport NewMonthExport(ReportProfitItem p)
        {
            MonthExport e = new MonthExport();

            e.Account = p.Account.Name;
            e.Date = MonthStart(p.AvailabilityItem.AvailabilityDate);
            e.Year = e.Date.Year;
            e.Quarter = e.Date.GetQuarter();
            e.Month = e.Date.Month;
            e.Cost = p.AvailabilityItem.Cost.GetValueOrDefault();
            e.Margin = p.WorkDoneMargin.GetValueOrDefault();
            e.Days = p.AvailabilityItem.AvailableDays.GetValueOrDefault();
            e.Profit = e.Margin - e.Cost;

            return e;
        }

        private static WorkDoneExport GetWorkDoneExport(ReportQuery query)
        {
            var workDoneItems = query.GetWorkDoneItems();

            var workDone = new WorkDoneExport();

            foreach (var w in workDoneItems)
            {
                workDone.Add(new WorkDoneItem
                {
                    AccountName = w.Account.Name,
                    InvoiceName = w.Invoice.Name,
                    InvoiceDate = MonthStart(w.Invoice.InvoiceDate),
                    InvoiceMargin = w.Invoice.Margin.GetValueOrDefault(),
                    InvoiceStatus = w.Invoice.Status.GetValueOrDefault(),
                    WorkDoneDate = MonthStart(w.WorkDoneItem.WorkDoneDate),
                    WorkDoneMargin = w.WorkDoneItem.Margin.GetValueOrDefault(),
                    WorkDoneForecast = w.WorkDoneItem.Forecast.GetValueOrDefault()
                });
            }

            return workDone;
        }

        private static void UploadText<T>(Microsoft.WindowsAzure.Storage.Blob.CloudBlobContainer azureBlobContainer, T export, string blobName)
        {
            var blob = azureBlobContainer.GetBlockBlobReference(blobName);
            var xml = Serialize<T>(export);
            blob.UploadText(xml);
        }

        private static string Serialize<T>(T dataToSerialize)
        {
            var stringwriter = new System.IO.StringWriter();
            var serializer = new XmlSerializer(typeof(T));
            serializer.Serialize(stringwriter, dataToSerialize);
            return stringwriter.ToString();
        }

        private static DateTime MonthStart(DateTime? date)
        {
            return new DateTime(date.Value.Year, date.Value.Month, 1);
        }
    }
}
