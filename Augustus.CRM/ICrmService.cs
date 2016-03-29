using System.Threading.Tasks;

namespace Augustus.CRM
{
    public interface ICrmService
    {
        CrmContext GetContext();
        Task<CrmContext> GetContextAsync();
    }
}
