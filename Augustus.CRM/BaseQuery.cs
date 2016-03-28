using System;

namespace Augustus.CRM
{
    public class BaseQuery : IDisposable
    {
        public CrmContext Organization { get; set; }

        public void Dispose()
        {
            Organization.Dispose();
        }
    }
}
