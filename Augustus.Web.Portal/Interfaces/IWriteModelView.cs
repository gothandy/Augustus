using Augustus.Web.Portal.ViewModels;

namespace Augustus.Web.Portal.Interfaces
{
    public interface IWriteModelView<T>
    {
        FormButtons FormButtons { get; set; }

        T DomainModel { get; set; }
    }
}