using Augustus.CRM.Attributes;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Augustus.CRM
{
    public static class AttributeHelper
    {
        private static Dictionary<string, object> AttributeCache = new Dictionary<string, object>();

        public static string GetLogicalName(BaseEntity entity, string caller)
        {
            return GetAttribute< AttributeLogicalNameAttribute, string>(entity, caller, a => a.LogicalName);
        }
        public static int[] GetStatusLookup(BaseEntity entity, string caller)
        {
            return GetAttribute<StatusLookupAttribute, int[]>(entity, caller, a => a.StatusLookup);
        }

        public static string GetEntityReferenceLogicalName(BaseEntity entity, string caller)
        {
            return GetAttribute<EntityReferenceAttribute, string>(entity, caller, a => a.LogicalName);
        }

        private static U GetAttribute<T, U>(BaseEntity entity, string caller, Func<T, U> result) where T : Attribute
        { 
            var type = entity.GetType();

            var key = string.Format("{0} {1} {2}", type.Name, caller, typeof(T).Name);

            if (AttributeCache.ContainsKey(key))
            {
                return (U)AttributeCache[key];
            }
            else
            {
                var prop = type.GetProperty(caller);
                var attr = prop.GetCustomAttribute<T>();
                var obj = (U)result(attr);
                AttributeCache.Add(key, obj);

                return obj;
            }
        }
    }
}
