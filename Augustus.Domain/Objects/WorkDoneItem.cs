using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Augustus.Domain.Objects
{
    public class WorkDoneItem
    {
        public Guid? AccountId { get; set; }
        public DateTime? Created { get; set; }
        public Guid? Id { get; set; }
        public Guid? InvoiceId { get; set; }
        public decimal? Margin { get; set; }
        public DateTime? WorkDoneDate { get; set; }
    }
}
