using Microsoft.Xrm.Sdk;
using System;
using System.Linq;

namespace Augustus.CRM
{

    public class OrgQueryable : BaseOrganization
    {
        public OrgQueryable(string connectionString) : base(connectionString) { }
        public OrgQueryable(Uri crmUrl, string accessToken) : base(crmUrl, accessToken) { }

        public IQueryable<Account> Accounts
        {
            get
            {
                return context.CreateQuery<Account>();
            }
        }

        public IQueryable<Invoice> Invoices
        {
            get
            {
                return context.CreateQuery<Invoice>();
            }
        }

        public IQueryable<WorkDoneItem> WorkDoneItems
        {
            get
            {
                return context.CreateQuery<WorkDoneItem>();
            }
        }

        public void Create<T>(T entity) where T : BaseEntity
        {
            context.AddObject(entity);
        }

        public void Update<T>(T entity) where T : BaseEntity
        {
            context.UpdateObject(entity);
        }

        public void Delete<T>(T entity) where T : BaseEntity
        {
            context.DeleteObject(entity);
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
