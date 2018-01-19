using System;

namespace Augustus.Domain.Objects
{
    public class Allocation
    {
        public Guid? Id { get; set; }
        public DateTime? Created { get; set; }

        public Guid? AccountId { get; set; }
        public DateTime? Month { get; set; }
        public decimal? Days { get; set; }
        public decimal? Cost { get; set; }
        public decimal? DayRate { get; set; }
    }
}
