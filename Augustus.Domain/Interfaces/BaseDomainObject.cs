using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Augustus.Domain.Interfaces
{
    public class BaseDomainObject
    {
        [DataMember]
        public Guid? Id { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;

            BaseDomainObject o = obj as BaseDomainObject;
            if ((System.Object)o == null) return false;

            return this.Id == o.Id;
        }
        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }
    }
}
