using Augustus.Domain.Objects;
using System;
using System.Collections.Generic;

namespace Augustus.Web.Portal.Interfaces
{
    public interface IAccountDropDown
    {
        Guid AccountId { get; set; }
        IEnumerable<Account> Accounts { get; set; }
    }
}
