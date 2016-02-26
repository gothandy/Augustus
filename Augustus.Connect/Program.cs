using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.Xrm.Tooling.Connector;
using System;

namespace Augustus.Connect
{
    class Program
    {
        const string resource = "https://trueclarity4.crm4.dynamics.com";
        const string clientId = "adb8518c-0996-4cdb-9f93-a2a1c13c9456";
        const string redirectUrl = "http://augustus.trueclarity.co.uk/crm";

        static void Main(string[] args)
        {
            xrmConnectionString();
        }

        private static void xrmConnectionString()
        {
            string connectionString =
                string.Format("AuthType=OAuth;Url={0};AppId={1};RedirectUri={2};TokenCacheStorePath ={3};LoginPrompt=Auto",
                resource,
                clientId,
                redirectUrl,
                "C:\\TC\\Augustus");

            //Use the connection string named "MyCRMServer"
            //from the configuration file
            CrmServiceClient crmSvc = new CrmServiceClient(connectionString);

        }

        private static void oauthFirst()
        {
            //https://msdn.microsoft.com/en-US/library/gg327838.aspx

            // TODO Substitute your correct CRM root service address, 
            

            // TODO Substitute your app registration values that can be obtained after you
            // register the app in Active Directory on the Microsoft Azure portal.



            // Authenticate the registered application with Azure Active Directory.
            AuthenticationContext authContext =
                new AuthenticationContext("https://login.windows.net/common", false);
            AuthenticationResult result = authContext.AcquireToken(resource, clientId, new
                                                                   Uri(redirectUrl));


        }
    }
}
