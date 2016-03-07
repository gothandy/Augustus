using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Augustus.Console.InvoiceWipCsv
{
    public class Invoice
    {
        public string Customer { get; set; }
        public string Name { get; set; }
        public decimal? Margin { get; set; }
        public decimal? PreviousWIP { get; set; }
        public decimal? CurrentWIP { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string Status { get; set; }
    }
}
