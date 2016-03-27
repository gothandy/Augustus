using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Augustus.CRM.Extensions
{
    public static class StatusAttributeExtension
    {
        public static int? GetAttributeStatus(this BaseEntity entity, [CallerMemberName] string caller = "")
        {
            var attributeLogicalName = AttributeHelper.GetLogicalName(entity, caller);
            var statusLookup = AttributeHelper.GetStatusLookup(entity, caller);

            OptionSetValue statusCode = entity.GetAttributeValue<OptionSetValue>(attributeLogicalName);

            if (statusCode == null)
            {
                return null;
            }
            else
            {
                return Array.FindIndex(statusLookup, a => statusCode.Value == 100000000 + a);
            }
        }
    }
}
