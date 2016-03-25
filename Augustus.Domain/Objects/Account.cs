using System;
using System.Collections.Generic;

namespace Augustus.Domain.Objects
{
    public class Account
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public DateTime? Created { get; set; }
        public IEnumerable<Opportunity> Opportunities { get; set; }
        public IEnumerable<Invoice> Invoices { get; set; }
    }
}
