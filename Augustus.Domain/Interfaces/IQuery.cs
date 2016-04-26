using System;

namespace Augustus.Domain.Interfaces
{
    public interface IQuery<T>
    {
        T GetItem(Guid id);

        Guid Create(T domain);

        void Update(T domain);

        /// <returns>Id of the parent item or null if no parent.</returns>
        Guid? Delete(Guid id);
    }
}
