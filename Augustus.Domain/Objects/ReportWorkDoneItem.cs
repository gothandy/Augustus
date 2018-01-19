namespace Augustus.Domain.Objects
{
    public class ReportWorkDoneItem
    {
        public Account Account { get; set; }
        public Invoice Invoice { get; set; }
        public Allocation Allocation { get; set; }
        public WorkDoneItem WorkDoneItem { get; set; }
    }
}
