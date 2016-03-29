using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System.Configuration;
using System.Runtime.ExceptionServices;
using System.Security.Claims;
using System.Threading.Tasks;
using System;

namespace Augustus.CRM.Authentication
{
    public class AccessTokenAuthentication
    {
        private const string tenantIdUrl = "http://schemas.microsoft.com/identity/claims/tenantid";
        private const string objectIdentifierUrl = "http://schemas.microsoft.com/identity/claims/objectidentifier";
        
        private string clientId;
        private string clientSecret;
        private Action tokenAcquisitionFail;

        public AccessTokenAuthentication(string clientId, string clientSecret, Action tokenAcquisitionFail)
        {
            this.clientId = clientId;
            this.clientSecret = clientSecret;
            this.tokenAcquisitionFail = tokenAcquisitionFail;
        }

        public async Task<AuthenticationResult> WaitForResult()
        {
            string signedInUserID = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;
            string tenantID = ClaimsPrincipal.Current.FindFirst(tenantIdUrl).Value;
            string userObjectID = ClaimsPrincipal.Current.FindFirst(objectIdentifierUrl).Value;
            string authority = string.Format("https://login.windows.net/{0}", tenantID);
            string resource = "Microsoft.CRM";

            var authContext = new AuthenticationContext(authority);
            var credential = new ClientCredential(clientId, clientSecret);
            var userIdentifier = new UserIdentifier(userObjectID, UserIdentifierType.UniqueId);

            return await authContext.AcquireTokenSilentAsync(resource, credential, userIdentifier);
        }

        public async Task<AuthenticationResult> TryWaitForResult()
        {
            AuthenticationResult result = null;
            ExceptionDispatchInfo capturedException = null;

            try
            {
                result = await WaitForResult();
            }
            catch (AdalSilentTokenAcquisitionException ex)
            {
                capturedException = ExceptionDispatchInfo.Capture(ex);
            }

            if (capturedException != null)
            {
                tokenAcquisitionFail();
            }

            return result;
        }
    }
}
