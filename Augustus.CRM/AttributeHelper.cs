using Augustus.CRM.Attributes;
using Microsoft.Xrm.Sdk;
using System;
using System.Reflection;

namespace Augustus.CRM
{
    public static class AttributeHelper
    {
        public static string GetLogicalName(BaseEntity entity, string caller)
        {
            return GetAttribute< AttributeLogicalNameAttribute>(entity, caller).LogicalName;
        }
        public static int[] GetStatusLookup(BaseEntity entity, string caller)
        {
            return GetAttribute<StatusLookupAttribute>(entity, caller).StatusLookup;
        }

        public static string GetEntityReferenceLogicalName(BaseEntity entity, string caller)
        {
            return GetAttribute<EntityReferenceAttribute>(entity, caller).LogicalName;
        }

        private static T GetAttribute<T>(BaseEntity entity, string caller) where T : Attribute
        { 
            var type = entity.GetType();

            var key = string.Concat(type.Name, caller, typeof(T).Name);

            if (entity.AttributeCache.ContainsKey(key))
            {
                return (T)entity.AttributeCache[key];
            }
            else
            {
                var prop = type.GetProperty(caller);
                var attr = prop.GetCustomAttribute<T>();
                entity.AttributeCache.Add(key, attr);

                return attr;
            }
        }
    }
}
