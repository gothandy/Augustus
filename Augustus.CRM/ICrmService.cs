using Microsoft.Xrm.Sdk;

namespace Augustus.CRM
{
    public interface ICrmService
    {
        IOrganizationService OrganizationService { get; }
    }
}
