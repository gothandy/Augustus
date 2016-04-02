namespace Augustus.Web.Portal.Interfaces
{
    public interface IReadViewModel<TModel>
    {
        TModel DomainModel { get; set; }
    }
}