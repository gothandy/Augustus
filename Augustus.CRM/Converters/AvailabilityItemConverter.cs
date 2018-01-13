using Augustus.CRM.Entities;
using Augustus.Domain.Objects;

namespace Augustus.CRM.Converters
{
    public static class AvailabilityItemConverter
    {
        public static AvailabilityItem ConvertToDomain(this AvailabilityItemEntity entity)
        {
            return ToDomain(entity);
        }

        public static AvailabilityItem ToDomain(AvailabilityItemEntity entity)
        {
            return new AvailabilityItem
            {
                Id = entity.Id,
                Created = entity.Created,
                AccountId = entity.AccountId,
                AvailabilityDate = entity.AvailabilityDate,
                Cost = entity.Cost,
                AvailableDays = entity.AvailableDays
            };
        }
    }
}
