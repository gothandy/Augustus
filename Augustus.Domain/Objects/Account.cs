using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Augustus.Domain.Objects
{
    [DataContract]
    public class Account
    {
        [DataMember]
        public Guid? Id { get; set; }

        [DataMember]
        [Required]
        public string Name { get; set; }

        [DataMember]
        public DateTime? Created { get; set; }

        [DataMember]
        public IEnumerable<Opportunity> Opportunities { get; set; }

        public IEnumerable<Invoice> Invoices { get; set; }

    }
}
