using System;
using System.Configuration;
using Augustus.CRM;
using Augustus.CRM.Entities;

namespace Augustus.Connect
{
    class Program
    {

        static void Main(string[] args)
        {
            string connectionString = ConfigurationManager.AppSettings["AugustusCRM"];

            using (CrmClient crm = new CrmClient(connectionString))
            {
                Account easyJet = crm.GetAccount("easyJet");

                Console.WriteLine(easyJet.Id);
            }

            Console.ReadKey();
        }
    }
}
