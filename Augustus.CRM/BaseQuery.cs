using System;

namespace Augustus.CRM
{
    public class BaseQuery : IDisposable
    {
        protected CrmContext Context;

        public BaseQuery(CrmContext context)
        {
            this.Context = context;
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
