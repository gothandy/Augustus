namespace Augustus.Domain.Objects
{
    public class ReportProfitItem
    {
        public Account Account { get; set; }
        public AvailabilityItem AvailabilityItem { get; set; }
        public decimal? WorkDoneMargin { get; set; }
    }
}
