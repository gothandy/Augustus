using System;

namespace Augustus.Domain.Interfaces
{
    public interface IQuery<T>
    {
        T GetItem(Guid id);

        Guid Create(T domain);

        void Update(T domain);

        Guid? Delete(Guid id);
    }
}
