using Augustus.CRM;
using System.Configuration;
using System.Linq;

namespace Augustus.Console.InvoiceWipCsv
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = ConfigurationManager.AppSettings["AugustusCRM"];

            using (OrgQueryable org = new OrgQueryable(connectionString))
            {

            }
        }
    }
}
