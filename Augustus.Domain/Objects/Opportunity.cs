using Augustus.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Augustus.Domain.Objects
{
    [DataContract]
    public class Opportunity : BaseDomainObject
    {
        [DataMember]
        [Required]
        public string Name { get; set; }

        [DataMember]
        public Guid? AccountId { get; set; }

        [DataMember]
        public DateTime? Created { get; set; }

        [DataMember]
        public IEnumerable<Invoice> Invoices { get; set; }

    }
}
