using Augustus.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Augustus.Domain.Objects
{
    [DataContract]
    public class Opportunity : IDomainObject
    {
        [DataMember]
        public Guid? AccountId { get; set; }

        [DataMember]
        public DateTime? Created { get; set; }

        [DataMember]
        public Guid? Id { get; set; }

        [DataMember]
        [Required]
        public string Name { get; set; }

        [DataMember]
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
