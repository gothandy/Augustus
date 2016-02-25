using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;

namespace Augustus.Connect
{
    class Program
    {
        static void Main(string[] args)
        {
            //https://msdn.microsoft.com/en-US/library/gg327838.aspx

            // TODO Substitute your correct CRM root service address, 
            string resource = "https://mydomain.crm.dynamics.com";

            // TODO Substitute your app registration values that can be obtained after you
            // register the app in Active Directory on the Microsoft Azure portal.
            string clientId = "e5cf0024-a66a-4f16-85ce-99ba97a24bb2";
            string redirectUrl = "http://localhost/SdkSample";


            // Authenticate the registered application with Azure Active Directory.
            AuthenticationContext authContext =
                new AuthenticationContext("https://login.windows.net/common", false);
            AuthenticationResult result = authContext.AcquireToken(resource, clientId, new
                                                                   Uri(redirectUrl));

        }
    }
}
