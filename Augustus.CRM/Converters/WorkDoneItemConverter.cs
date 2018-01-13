using Augustus.CRM.Entities;
using Augustus.Domain.Objects;

namespace Augustus.CRM.Converters
{
    public static class WorkDoneItemConverter
    {
        public static WorkDoneItem ConvertToDomain(this WorkDoneItemEntity entity)
        {
            return ToDomain(entity);
        }

        public static WorkDoneItem ToDomain(WorkDoneItemEntity entity)
        {
            return new WorkDoneItem
            {
                Id = entity.Id,
                Created = entity.Created,
                AccountId = entity.AccountId,
                InvoiceId = entity.InvoiceId,
                WorkDoneDate = entity.WorkDoneDate,
                Margin = entity.Margin,
                Forecast = entity.Forecast
            };
        }
    }
}
