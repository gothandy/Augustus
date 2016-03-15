using System;

namespace Augustus.CRM
{
    public class BaseQuery : IDisposable
    {
        public OrgQueryable Organization { get; set; }

        public void Dispose()
        {
            Organization.Dispose();
        }
    }
}
