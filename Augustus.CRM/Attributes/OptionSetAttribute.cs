using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Augustus.CRM.Attributes
{
    public static class OptionSetAttribute
    {
        public static int? GetAttributeOptionSet(this BaseEntity entity, string attributeLogicalName, int[] statusLookup)
        {
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
