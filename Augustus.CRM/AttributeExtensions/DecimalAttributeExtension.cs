using Microsoft.Xrm.Sdk;
using System;
using System.Runtime.CompilerServices;

namespace Augustus.CRM.AttributeExtensions
{
    public static class DecimalAttributeExtension
    {
        public static decimal? GetAttributeDecimal(this BaseEntity entity, [CallerMemberName] string caller = "")
        {
            string attributeLogicalName = AttributeHelper.GetLogicalName(entity, caller);

            return entity.GetAttributeValue<Decimal>(attributeLogicalName);
        }

        public static void SetAttributeDecimal(this BaseEntity entity, decimal? value, [CallerMemberName] string caller = "")
        {
            if (value.HasValue)
            {
                throw new NotImplementedException();
            }
        }
    }
}
