using System.Collections.Specialized;
using System.Configuration;

namespace Augustus.Web.Config
{
    public class AppSettings
    {

        private static NameValueCollection appSettings = ConfigurationManager.AppSettings;
        
        public static string ClientId { get { return appSettings["ida:ClientId"]; } }
        public static string AadInstance { get { return appSettings["ida:AADInstance"] ?? "https://login.microsoftonline.com/"; } }
        public static string TenantId { get { return appSettings["ida:TenantId"]; } }
        public static string PostLogoutRedirectUri { get { return appSettings["ida:PostLogoutRedirectUri"]; } }
        public static string ClientSecret { get { return appSettings["ida:ClientSecret"]; } }
        public static string Authority { get { return string.Concat(AadInstance, TenantId); } }
        public static string PostLoginRedirectUri { get { return PostLogoutRedirectUri; } }

        public static string CrmConnectionString { get { return appSettings["crm:ConnectionString"]; } }
        public static string CrmUrl { get { return appSettings["crm:Url"]; } }
        public static bool UseAzureAuth { get { return (appSettings["crm:UseAzureAuth"] ?? "true") == "true"; } }

            
    }
}
