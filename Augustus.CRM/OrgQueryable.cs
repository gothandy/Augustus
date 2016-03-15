using Augustus.CRM.Entities;
using System;
using System.Linq;

namespace Augustus.CRM
{
    public class OrgQueryable : BaseOrganization
    {
        public OrgQueryable(string connectionString) : base(connectionString) { }
        public OrgQueryable(Uri crmUrl, string accessToken) : base(crmUrl, accessToken) { }

        public IQueryable<AccountEntity> Accounts
        {
            get
            {
                return context.CreateQuery<AccountEntity>();
            }
        }

        public IQueryable<InvoiceEntity> Invoices
        {
            get
            {
                return context.CreateQuery<InvoiceEntity>();
            }
        }

        public IQueryable<WorkDoneItemEntity> WorkDoneItems
        {
            get
            {
                return context.CreateQuery<WorkDoneItemEntity>();
            }
        }

        public IQueryable<OpportunityEntity> Opportunities
        {
            get
            {
                return context.CreateQuery<OpportunityEntity>();
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

        public void SaveChanges()
        {
            context.SaveChanges();
        }
    }
}
