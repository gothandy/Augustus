using System;

namespace Augustus.Domain.Objects
{
    public class Account
    {
        public DateTime? Created { get; set; }
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
