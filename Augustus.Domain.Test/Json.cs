using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Augustus.Domain.Objects;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Augustus.Domain.Test
{
    [TestClass]
    public class Json
    {
        private const string accountJson = "{\"Name\":\"easyJet\"}";
        private const string opportunitiesJson = "{\"Name\":\"easyJet\",\"Opportunities\":[{\"Name\":\"Out of Hours\"},{\"Name\":\"Team\"}]}";
        private const string invoicesJson = "{\"Name\":\"easyJet\",\"Opportunities\":[{\"Name\":\"Team\",\"Invoices\":[{\"Name\":\"2016 Jan\",\"Revenue\":1234.56,\"InvoiceDate\":\"2016-01-01T00:00:00\",\"Margin\":1234.56}]}]}";
        private const string workDoneItemsJson = "{\"Name\":\"2016 Jan\",\"WorkDoneItems\":[{\"Margin\":1111.0,\"WorkDoneDate\":\"2016-01-01T00:00:00\"},{\"Margin\":1212.0,\"WorkDoneDate\":\"2016-01-02T00:00:00\"}]}";

        [TestMethod]
        public void Account_Serialize()
        {
            var account = new Account
            {
                Name = "easyJet"
            };
            string json = Serialize(account);

            Assert.AreEqual(accountJson, json);
        }

        [TestMethod]
        public void Account_Deserialize()
        {
            var account = JsonConvert.DeserializeObject<Account>(accountJson);
            Assert.AreEqual("easyJet", account.Name);
        }


        [TestMethod]
        public void Opportunities_Serialize()
        {
            var account = new Account
            {
                Name = "easyJet",
                Opportunities = new List<Opportunity>
                {
                    new Opportunity { Name = "Out of Hours" },
                    new Opportunity { Name = "Team" }
                }
            };

            var json = Serialize(account);
            Assert.AreEqual(opportunitiesJson, json);
        }

        [TestMethod]
        public void Opportunities_Deserialize()
        {
            var account = JsonConvert.DeserializeObject<Account>(opportunitiesJson);
            Assert.AreEqual(2, account.Opportunities.Count());
        }

        [TestMethod]
        public void Invoices_Serialize()
        {
            var account = new Account
            {
                Name = "easyJet",
                Opportunities = new List<Opportunity>()
                {
                    new Opportunity
                    {
                        Name = "Team",
                        Invoices = new List<Invoice>()
                        {
                            new Invoice
                            {
                                Name = "2016 Jan",
                                InvoiceDate = new DateTime(2016,1,1),
                                Revenue = (decimal)1234.56
                            }
                        }
                    }
                }
            };

            Assert.AreEqual(invoicesJson, Serialize(account));
        }
        
        [TestMethod]
        public void Invoices_Deserialize()
        {
            var acc = JsonConvert.DeserializeObject<Account>(invoicesJson);
            var inv = acc.Opportunities.First().Invoices.First();
            Assert.AreEqual(new DateTime(2016, 1, 1), inv.InvoiceDate);
            Assert.AreEqual((decimal)1234.56, inv.Margin);
        }

        [TestMethod]
        public void WorkDoneItems_Serialize()
        {
            var inv = new Invoice
            {
                Name = "2016 Jan",
                WorkDoneItems = new List<WorkDoneItem>
                {
                    new WorkDoneItem
                    {
                        WorkDoneDate = new DateTime(2016,1,1),
                        Margin = 1111
                    },
                    new WorkDoneItem
                    {
                        WorkDoneDate = new DateTime(2016,1,2),
                        Margin = 1212
                    }
                }
            };
            Assert.AreEqual(workDoneItemsJson, Serialize(inv));
        }

        [TestMethod]
        public void WorkDoneItems_Deserialize()
        {
            var inv = JsonConvert.DeserializeObject<Invoice>(workDoneItemsJson);
            var wdi = inv.WorkDoneItems.First();
            Assert.AreEqual(new DateTime(2016, 1, 1), wdi.WorkDoneDate);
            Assert.AreEqual(1111, wdi.Margin);
        }

        private static string Serialize(object obj)
        {
            return JsonConvert.SerializeObject(
                obj,
                Formatting.None,
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                });
        }
    }
}
