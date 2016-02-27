using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Configuration;

namespace Augustus.Connect
{
    class Program
    {

        static void Main(string[] args)
        {
            xrmConnectionString();

            Console.ReadKey();
        }

        private static void xrmConnectionString()
        {
            string connectionString = ConfigurationManager.AppSettings["AugustusCRM"];

            CrmServiceClient crmSvc = new CrmServiceClient(connectionString);

            // Verify that you are connected.
            if (crmSvc != null && crmSvc.IsReady)
            {
                IOrganizationService _orgService = GetAndCastProxyClientFromWebOrService(crmSvc);

                WriteInvoices(_orgService);
            }
            else
            {
                DisplayLastError(crmSvc);
            }
        }

        private static IOrganizationService GetAndCastProxyClientFromWebOrService(CrmServiceClient crmSvc)
        {

            // Cast the proxy client to the IOrganizationService interface.
            return (IOrganizationService)crmSvc.OrganizationWebProxyClient != null ?
                (IOrganizationService)crmSvc.OrganizationWebProxyClient :
                (IOrganizationService)crmSvc.OrganizationServiceProxy;
        }

        private static void DisplayLastError(CrmServiceClient crmSvc)
        {
            Console.WriteLine("Error occurred: {0}", crmSvc.LastCrmError);

            // Display the last exception message if any.
            Console.WriteLine(crmSvc.LastCrmException.Message);
            Console.WriteLine(crmSvc.LastCrmException.Source);
            Console.WriteLine(crmSvc.LastCrmException.StackTrace);
        }

        private static void WriteInvoices(IOrganizationService _orgService)
        {
            QueryExpression query = new QueryExpression
            {
                EntityName = new_invoice.EntityLogicalName,
                ColumnSet = new ColumnSet(
                        "new_invoicedate",
                        "new_directclient",
                        "new_margin",
                        "new_invoiceid",
                        "statuscode",
                        "new_wip_previous",
                        "new_wip_current",
                        "new_name")
            };

            foreach (new_invoice invoice in _orgService.RetrieveMultiple(query).Entities)
            {
                object[] values = {
                    invoice.new_invoiceId,
                    invoice.new_InvoiceDate.GetValueOrDefault().ToShortDateString(),
                    invoice.new_DirectClient == null ?  null : invoice.new_DirectClient.Name,
                    getMoneyValue(invoice.new_Margin),
                    getMoneyValue(invoice.new_wip_previous),
                    getMoneyValue(invoice.new_WIP_Current),
                    invoice.statecode,
                    invoice.new_name};

                Console.WriteLine(string.Join(", ", values));
            };
        }

        private static decimal? getMoneyValue(Money money)
        {
            if (money == null)
            {
                return null;
            }
            else
            {
                return money.Value;
            }
        }
    }
}
