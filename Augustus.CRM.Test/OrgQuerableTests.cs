﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Configuration;
using System.Linq;

namespace Augustus.CRM.Test
{
    [TestClass]
    public class OrgQueryableTests
    {
        private static OrgQueryable org;

        [ClassInitialize]
        public static void ClassInit(TestContext testContext)
        {
            string connectionString = ConfigurationManager.AppSettings["crm:ConnectionString"];

            org = new OrgQueryable(connectionString);
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            org.Dispose();
        }

        [TestMethod]
        public void OrgQueryable_GetAccount()
        {
            var account = (Account)org.Accounts.Single(a => a.Name == "easyJet");
            Assert.AreEqual(new Guid("33b343c7-a684-e011-8da5-00271336e9df"), account.Id);
        }

        [TestMethod]
        public void OrgQueryable_GetIAccount()
        {
            var account = org.Accounts.Single(a => a.Name == "easyJet");
            Assert.AreEqual(new Guid("33b343c7-a684-e011-8da5-00271336e9df"), account.Id);
        }

        [TestMethod]
        public void OrgQueryable_GetInvoices()
        {
            var invoices = (from i in org.Invoices
                            where i.InvoiceDate > new DateTime(2016, 1, 1)
                            select i);

            foreach(var invoice in invoices)
            {
                Console.WriteLine("{0} {1} {2}", invoice.AccountId, invoice.InvoiceDate, invoice.Cost);
            }
        }

        [TestMethod]
        public void OrgQueryable_AccountJoinInvoice()
        {
            var activeAccounts = (from a in org.Accounts
                                  join i in org.Invoices
                                  on a.Id equals i.AccountId
                                  where i.InvoiceDate > new DateTime(2015, 1, 1)
                                  select a).Distinct();

            foreach(var account in activeAccounts)
            {
                Console.WriteLine(account.Name);
            }
        }

        [TestMethod]
        public void OrgQueryable_AccountByCreated()
        {
            var newAccounts = from a in org.Accounts
                              where a.Created > new DateTime(2015, 1, 1)
                              select a;

            foreach (var account in newAccounts)
            {
                Console.WriteLine(account.Name);
            }
        }
    }
}
