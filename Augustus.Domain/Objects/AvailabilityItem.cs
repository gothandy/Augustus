using System;

namespace Augustus.Domain.Objects
{
    public class AvailabilityItem
    {
        public Guid? Id { get; set; }
        public DateTime? Created { get; set; }

        public Guid? AccountId { get; set; }
        public DateTime? AvailabilityDate { get; set; }
        public decimal? AvailableDays { get; set; }
        public decimal? Cost { get; set; }
        public decimal? Forecast { get; set; }
    }
}
