using Owin;

namespace Augustus.Web.Portal
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            AuthenticationConfig.ConfigureAuth(app);
        }
    }
}
