﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;

namespace Augustus.CRM.Test
{
    [TestClass]
    public class AccountCrudTests : BaseCrudTest
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
        public void CRM_CrudAccount()
        {
            createAccount("Test Account");
            updateAccount("Test Account", "Test Account2");
            deleteAccount("Test Account2");
        }
    }
}
