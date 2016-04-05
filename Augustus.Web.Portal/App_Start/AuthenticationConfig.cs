using Augustus.Web.Config;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Notifications;
using Microsoft.Owin.Security.OpenIdConnect;
using Owin;
using System;
using System.IdentityModel.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;

namespace Augustus.Web.Portal
{
    public class AuthenticationConfig
    {
        private static string authority = AppSettings.AadInstance + AppSettings.TenantId;

        public static void ConfigureApp(IAppBuilder app)
        {
            AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimTypes.NameIdentifier;

            app.SetDefaultSignInAsAuthenticationType(CookieAuthenticationDefaults.AuthenticationType);
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                CookieSecure = CookieSecureOption.SameAsRequest
            });
            app.UseOpenIdConnectAuthentication(GetAuthenticationOptions());
        }

        private static OpenIdConnectAuthenticationOptions GetAuthenticationOptions()
        {
            var authenticationOptions = new OpenIdConnectAuthenticationOptions
            {
                ClientId = AppSettings.ClientId,
                ClientSecret = AppSettings.ClientSecret,
                Authority = authority,
                PostLogoutRedirectUri = AppSettings.PostLogoutRedirectUri,
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

            ClientCredential credential = new ClientCredential(AppSettings.ClientId, AppSettings.ClientSecret);

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
