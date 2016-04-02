using Augustus.Domain.Objects;

namespace Augustus.Web.Portal.ViewModels
{
    public class Breadcrumb
    {
        public Account Account { get; set; }
        public Opportunity Opportunity { get; set; }
        public Invoice Invoice { get; set; }
    }
}