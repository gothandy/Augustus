using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Augustus.CRM.Attributes
{
    public static class IdAttribute
    {
        public static Guid GetAttributeId(this BaseEntity entity, string attributeLogicalName)
        {
            return entity.GetAttributeValue<Guid>(attributeLogicalName);
        }

        public static void SetAttributeId(this BaseEntity entity, string attributeLogicalName, Guid value)
        {
            entity.SetBaseAttributeValue(attributeLogicalName, (Guid?)value);
            entity.SetBaseIdValue(value);
        }
    }
}
