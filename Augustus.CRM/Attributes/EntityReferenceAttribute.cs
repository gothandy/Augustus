using Microsoft.Xrm.Sdk;
using System;

namespace Augustus.CRM.Attributes
{
    public static class EntityReferenceAttribute
    {
        public static Guid? GetAttributeEntityReference(this BaseEntity entity, string attributeLogicalName)
        {
            EntityReference entRef = entity.GetAttributeValue<EntityReference>(attributeLogicalName);

            if (entRef == null)
            {
                return null;
            }
            else
            {
                return entRef.Id;
            }
        }

        public static void SetAttributeEntityReference(this BaseEntity entity, string attributeLogicalName, string entityLogicalName, Guid? id)
        {
            if (id.HasValue)
            {
                var entRef = new EntityReference(entityLogicalName, id.Value);
                entity.SetBaseAttributeValue(attributeLogicalName, entRef);
            }
        }
    }
}
