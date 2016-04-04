using Augustus.Domain.Objects;
using Augustus.Web.Portal.Interfaces;

namespace Augustus.Web.Portal.ViewModels
{
    public class OpportunityReadViewModel : ISharedLayoutViewModel, IReadViewModel<Opportunity>
    {
        public Breadcrumb Breadcrumb { get; set; }
        public string Title { get; set; }
        public Opportunity Opportunity { get; set; }
        public Opportunity DomainModel { get { return Opportunity; } set { Opportunity = value; } }
    }
}