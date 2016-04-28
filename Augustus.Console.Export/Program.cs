using Augustus.CRM;
using Augustus.CRM.Queries;
using Augustus.Domain.Objects;
using Augustus.Web.Config;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Linq;
using System;
using Microsoft.WindowsAzure.Storage;

namespace Augustus.Console.Export
{
    class Program
    {
        static void Main(string[] args)
        {
            var service = new CrmServiceConnectionString(AppSettings.CrmConnectionString);
            var context = service.GetContext();
            var query = new ReportQuery(context);
            var data = query.GetExportData();

            var export = new Export();

            foreach(var w in data)
            {
                export.Add(new Item
                {
                    AccountName = w.Account.Name,
                    InvoiceName = w.Invoice.Name,
                    InvoiceDate = MonthStart(w.Invoice.InvoiceDate),
                    InvoiceMargin = w.Invoice.Margin.GetValueOrDefault(),
                    WorkDoneDate = MonthStart(w.WorkDoneItem.WorkDoneDate),
                    WorkDoneMargin = w.WorkDoneItem.Margin.GetValueOrDefault()
                });
            }

            var azureConnectionString = "DefaultEndpointsProtocol=https;AccountName=augustusexport;AccountKey=MRR/7VNJTLCgpoVicRDICk7USjCcuczZqXcBC9WYsNfLGE8Mfk2Utt2tTSfCLfvNSLgyuXjA3hvg9q4pfIP5RQ==";
            var azureStorageAccount = CloudStorageAccount.Parse(azureConnectionString);
            var azureBlobClient = azureStorageAccount.CreateCloudBlobClient();
            var azureBlobContainer = azureBlobClient.GetContainerReference("workdoneexport");

            var blob = azureBlobContainer.GetBlockBlobReference("export.xml");
            var xml = Serialize<Export>(export);
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
