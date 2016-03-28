using System;

namespace Augustus.CRM
{
    public class BaseQuery
    {
        protected CrmContext Context;

        public BaseQuery(CrmContext context)
        {
            this.Context = context;
        }
    }
}
