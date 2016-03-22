using System;
using System.Collections.Generic;

namespace Augustus.Domain.Objects
{
    public class Opportunity
    {
        public Guid? AccountId { get; set; }
        public DateTime? Created { get; set; }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<Invoice> Invoices { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;

            Opportunity o = obj as Opportunity;
            if ((System.Object)o == null) return false;

            return this.Id == o.Id;
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }
    }
}
