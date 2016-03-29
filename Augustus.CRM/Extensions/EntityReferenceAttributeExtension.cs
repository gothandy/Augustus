using Microsoft.Xrm.Sdk;
using System;
using System.Runtime.CompilerServices;

namespace Augustus.CRM.Extensions
{
    public static class EntityReferenceAttributeExtension
    {
        public static Guid? GetAttributeEntityReference(this BaseEntity entity, [CallerMemberName] string caller = "")
        {
            string attributeLogicalName = AttributeHelper.GetLogicalName(entity, caller);

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

        public static void SetAttributeEntityReference(this BaseEntity entity, Guid? id, [CallerMemberName] string caller = "")
        {
            if (id.HasValue)
            {
                string attributeLogicalName = AttributeHelper.GetLogicalName(entity, caller);
                string entityReferenceLogicalName = AttributeHelper.GetEntityReferenceLogicalName(entity, caller);

                var entRef = new EntityReference(entityReferenceLogicalName, id.Value);
                entity.SetBaseAttributeValue(attributeLogicalName, entRef);
            }
        }
    }
}
