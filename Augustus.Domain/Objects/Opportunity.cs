using System;

namespace Augustus.Domain.Objects
{
    public class Opportunity
    {
        public Guid? AccountId { get; set; }
        public DateTime? Created { get; set; }
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
