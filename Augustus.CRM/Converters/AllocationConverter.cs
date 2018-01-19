using Augustus.CRM.Entities;
using Augustus.Domain.Objects;

namespace Augustus.CRM.Converters
{
    public static class AllocationConverter
    {
        public static Allocation ConvertToDomain(this AllocationEntity entity)
        {
            return ToDomain(entity);
        }

        public static Allocation ToDomain(AllocationEntity entity)
        {
            return new Allocation
            {
                Id = entity.Id,
                Created = entity.Created,
                AccountId = entity.AccountId,
                Month = entity.Month,
                Days = entity.Days,
                Cost = entity.Cost,
                DayRate = entity.DayRate
            };
        }
    }
}
