using Augustus.CRM.Entities;
using Augustus.Domain.Objects;

namespace Augustus.CRM.Converters
{
    public static class AccountConverter
    {
        public static Account ToDomainObject(AccountEntity a)
        {

            return new Account
            {
                Id = a.Id,
                Name = a.Name,
                Created = a.Created
            };
        }
    }
}
