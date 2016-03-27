using System;

namespace Augustus.CRM.Attributes
{
    public class StatusLookupAttribute : Attribute
    {
        private int[] statusLookup;
        public StatusLookupAttribute(int[] statusLookup)
        {
            this.statusLookup = statusLookup;
        }

        public int[] StatusLookup
        {
            get
            {
                return statusLookup;
            }
        }
    }
}
