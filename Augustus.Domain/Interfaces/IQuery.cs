using System;

namespace Augustus.Domain.Interfaces
{
    public interface IQuery<T>
    {
        T GetItem(Guid id);
        Guid Create(T item);
        void Update(T item);
        void Delete(Guid id);
    }
}
