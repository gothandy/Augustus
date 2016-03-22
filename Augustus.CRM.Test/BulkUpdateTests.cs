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
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            org.Dispose();
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
                                InvoiceDate = new DateTime(2016,3,1),
                                Revenue = 1000,
                                WorkDoneItems = new List<WorkDoneItem>
                                {
                                    new WorkDoneItem
                                    {
                                        WorkDoneDate = new DateTime(2016, 1, 1),
                                        Margin = 500
                                    },
                                    new WorkDoneItem
                                    {
                                        WorkDoneDate = new DateTime(2016, 2, 1),
                                        Margin = 500
                                    }
                                }
                            }
                        }
                    }
                }
            };

            var query = new BulkUpdateQuery { Organization = org };

            query.BulkUpdate(update);
            
        }
    }
}
