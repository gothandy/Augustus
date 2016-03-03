using System;
using System.Configuration;
using Augustus.CRM;
using System.Linq;
using Augustus.Interfaces;

namespace Augustus.Connect
{
    class Program
    {

        static void Main(string[] args)
        {
            string connectionString = ConfigurationManager.AppSettings["AugustusCRM"];

            using (CrmClient crm = new CrmClient(connectionString))
            {

                /*
                IAccount easyJet = (from a in crm.Organization.Accounts
                                    where a.Name == "easyJet"
                                    select a).Single();

                Console.WriteLine(easyJet.Id);
                */
            }

            Console.ReadKey();
        }
    }
}
