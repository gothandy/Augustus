using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Augustus.App.WebRole
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = ConfigurationManager.AppSettings["crm:ConnectionString"];

            CrmServiceClient service = new CrmServiceClient(connectionString);

            if (!service.IsReady) throw new Exception(service.LastCrmError, service.LastCrmException);
        }
    }
}
