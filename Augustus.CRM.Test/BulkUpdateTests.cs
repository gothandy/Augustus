using Augustus.CRM.Entities;
using Augustus.CRM.Queries;
using Augustus.Domain.Objects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Augustus.CRM.Test
{
    [TestClass]
    public class BulkUpdateTests : BaseCrudTest
    {

        [ClassInitialize]
        public static void ClassInit(TestContext testContext)
        {
            CreateOrg();

            deleteAllInvoices();
            deleteAllOpportunities();
            deleteAllAccounts();
            deleteAllWorkDoneItems();
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            deleteAllWorkDoneItems();
            deleteAllInvoices();
            deleteAllOpportunities();
            deleteAllAccounts();

            context.Dispose();
        }

        [TestMethod]
        public void CRM_Query_Bulk_Update()
        {
            var update = new Account
            {
                Name = accountName,
                Opportunities = new List<Opportunity>
                {
                    new Opportunity
                    {
                        Name = opportunityName,
                        Invoices = new List<Invoice>
                        {
                            new Invoice
                            {
                                Name = invoiceName,
                                InvoiceDate = new DateTime(2005,5,1),
                                Revenue = 1000,
                                WorkDoneItems = new List<WorkDoneItem>
                                {
                                    new WorkDoneItem
                                    {
                                        WorkDoneDate = new DateTime(2005, 1, 1),
                                        Margin = 500
                                    },
                                    new WorkDoneItem
                                    {
                                        WorkDoneDate = new DateTime(2005, 2, 1),
                                        Margin = 500
                                    }
                                }
                            }
                        }
                    }
                }
            };

            var query = new BulkUpdateQuery(context);

            query.BulkUpdate(update);

            var acc = getAccount(accountName);
            var opp = getOpportunity(opportunityName);
            var inv = getInvoice(invoiceName);
            var wdi = (from i in context.WorkDoneItems
                       where i.InvoiceId.Value == inv.Id
                       select i).First();

            Assert.AreEqual(accountName, acc.Name);
            Assert.AreEqual(opportunityName, opp.Name);
            Assert.AreEqual(invoiceName, inv.Name);

            Assert.AreEqual(acc.Id, inv.AccountId);
            Assert.AreEqual(acc.Id, opp.AccountId);
            Assert.AreEqual(acc.Id, wdi.AccountId);
            Assert.AreEqual(opp.Id, inv.OpportunityId);
            Assert.AreEqual(inv.Id, wdi.InvoiceId);

            var margin = (from i in context.WorkDoneItems
                          where i.InvoiceId.Value == inv.Id
                          select i).ToList().Sum(i => i.Margin.Value);
            Assert.AreEqual(1000, margin);

        }
    }
}
