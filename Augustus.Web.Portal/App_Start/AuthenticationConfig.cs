using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using Microsoft.Owin.Security.Notifications;
using Owin;
using System;
using System.Configuration;
using System.IdentityModel.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;

namespace Augustus.Web.Portal
{
    public class AuthenticationConfig
    {
        private static string clientId = ConfigurationManager.AppSettings["ida:ClientId"];
        private static string aadInstance = ConfigurationManager.AppSettings["ida:AADInstance"];
        private static string tenantId = ConfigurationManager.AppSettings["ida:TenantId"];
        private static string postLogoutRedirectUri = ConfigurationManager.AppSettings["ida:PostLogoutRedirectUri"];
        private static string clientSecret = ConfigurationManager.AppSettings["ida:ClientSecret"];
        private static string authority = aadInstance + tenantId;

        public static void ConfigureApp(IAppBuilder app)
        {
            AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimTypes.NameIdentifier;

            app.SetDefaultSignInAsAuthenticationType(CookieAuthenticationDefaults.AuthenticationType);
            app.UseCookieAuthentication(new CookieAuthenticationOptions());
            app.UseOpenIdConnectAuthentication(GetAuthenticationOptions());
        }

        private static OpenIdConnectAuthenticationOptions GetAuthenticationOptions()
        {
            var authenticationOptions = new OpenIdConnectAuthenticationOptions
            {
                ClientId = clientId,
                ClientSecret = clientSecret,
                Authority = authority,
                PostLogoutRedirectUri = postLogoutRedirectUri,
                Notifications = GetNotifications()
            };

            return authenticationOptions;
        }

        private static OpenIdConnectAuthenticationNotifications GetNotifications()
        {
            return new OpenIdConnectAuthenticationNotifications()
            {
                AuthorizationCodeReceived = (context) =>
                {
                    return ExecuteCodeReceived(context);
                }
            };
        }

        private static Task ExecuteCodeReceived(AuthorizationCodeReceivedNotification context)
        {
            var code = context.Code;

            ClientCredential credential = new ClientCredential(clientId, clientSecret);

            var identityTenantID = context.AuthenticationTicket.Identity.FindFirst(
                "http://schemas.microsoft.com/identity/claims/tenantid").Value;

            var signedInUserID = context.AuthenticationTicket.Identity.FindFirst(
                ClaimTypes.NameIdentifier).Value;

            var authContext = new AuthenticationContext(
                string.Format("https://login.windows.net/{0}", identityTenantID));

            var redirectUri = new Uri(HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Path));

            var result = authContext.AcquireTokenByAuthorizationCode(
                code, redirectUri, credential, "Microsoft.CRM");

            return Task.FromResult(0);
        }
    }
}
