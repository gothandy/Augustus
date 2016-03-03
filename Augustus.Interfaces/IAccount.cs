using System;

namespace Augustus.Interfaces
{

    public interface IAccount
    {
        Guid Id { get; set; }
        string Name { get; set; }

    }
}
