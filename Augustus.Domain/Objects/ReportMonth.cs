using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Augustus.Domain.Objects
{
    public class ReportMonth
    {
        public DateTime Month { get; set; }
        public Decimal Invoice { get; set; }
        public Decimal WorkDone { get; set; }
    }
}
