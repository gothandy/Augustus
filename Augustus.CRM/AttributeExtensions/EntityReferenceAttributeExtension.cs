using Microsoft.Xrm.Sdk;
using System;
using System.Runtime.CompilerServices;

namespace Augustus.CRM.AttributeExtensions
{
    public static class EntityReferenceAttributeExtension
    {
        public static Guid? GetAttributeEntityReference(this BaseEntity entity, [CallerMemberName] string caller = "")
        {
            string attributeLogicalName = AttributeHelper.GetLogicalName(entity, caller);

            var entRef = entity.GetAttributeValue<EntityReference>(attributeLogicalName);

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

                var newEntRef = new EntityReference(entityReferenceLogicalName, id.Value);
                var oldEntRef = entity.GetAttributeValue<EntityReference>(attributeLogicalName);

                if (newEntRef.Id != oldEntRef.Id)
                {
                    entity.SetBaseAttributeValue(attributeLogicalName, newEntRef);
                }
            }
        }
    }
}
