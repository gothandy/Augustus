using System.Collections.Specialized;
using System.Configuration;

namespace Augustus.Web.Config
{
    public class AppSettings
    {

        private static NameValueCollection appSettings = ConfigurationManager.AppSettings;

        public static string ClientId { get { return appSettings["ida:ClientId"]; } }
        public static string AadInstance { get { return appSettings["ida:AADInstance"]; } }
        public static string TenantId { get { return appSettings["ida:TenantId"]; } }
        public static string PostLogoutRedirectUri { get { return appSettings["ida:PostLogoutRedirectUri"]; } }
        public static string ClientSecret { get { return appSettings["ida:ClientSecret"]; } }

        public static bool UseAzureAuth { get { return (appSettings["crm:UseAzureAuth"] ?? "true") == "true"; } }

            
    }
}
