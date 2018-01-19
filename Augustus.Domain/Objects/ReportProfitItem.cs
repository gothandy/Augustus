namespace Augustus.Domain.Objects
{
    public class ReportProfitItem
    {
        public Account Account { get; set; }
        public Allocation Allocation { get; set; }
        public decimal? WorkDoneMargin { get; set; }
    }
}
