using Augustus.CRM.Entities;
using Augustus.Domain.Objects;

namespace Augustus.CRM.Converters
{
    public static class AccountConverter
    {
        public static void SetUsingDomain(this AccountEntity entity, Account domain)
        {
            entity.Name = domain.Name;
            entity.FullName = domain.FullName;
        }

        public static Account ConvertToDomain(this AccountEntity entity)
        {
            return AccountConverter.ToDomain(entity);
        }

        public static Account ToDomain(AccountEntity entity)
        {
            return new Account
            {
                Id = entity.Id,
                Name = entity.Name,
                FullName = entity.FullName,
                Created = entity.Created
            };
        }
    }
}
