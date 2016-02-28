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

        /*<fetch version="1.0" output-format="xml-platform" mapping="logical" distinct="false">
              <entity name="new_invoice">
                <attribute name="new_invoicedate" />
                <attribute name="new_directclient" />
                <attribute name="new_margin" />
                <attribute name="new_invoiceid" />
                <attribute name="statuscode" />
                <attribute name="new_wip_previous" />
                <attribute name="new_wip_current" />
                <attribute name="new_name" />
                <order attribute="new_invoicedate" descending="false" />
                <filter type="and">
                  <filter type="or">
                    <condition attribute="new_invoicedate" operator="this-month" />
                    <condition attribute="new_invoicedate" operator="next-x-months" value="100" />
                  </filter>
                  <condition attribute="statecode" operator="eq" value="0" />
                </filter>
              </entity>
            </fetch>*/

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
                        "new_name"),
                Criteria = new FilterExpression
                {
                    FilterOperator = LogicalOperator.And,
                    Filters =
                    {
                        new FilterExpression
                        {
                            FilterOperator = LogicalOperator.Or,
                            Conditions =
                            {
                                new ConditionExpression
                                {
                                    AttributeName = "new_invoicedate",
                                    Operator = ConditionOperator.ThisMonth
                                },
                                new ConditionExpression
                                {
                                    AttributeName = "new_invoicedate",
                                    Operator = ConditionOperator.NextXMonths,
                                    Values = { 100 }
                                }
                            }
                        },
                        new FilterExpression
                        {
                            FilterOperator = LogicalOperator.And,
                            Conditions =
                            {
                                new ConditionExpression
                                {
                                    AttributeName = "statecode",
                                    Operator = ConditionOperator.Equal,
                                    Values = { 0 }
                                }
                            }
                        }
                    }
                }
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
