using Augustus.CRM.Entities;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using System;
using System.Linq;

[assembly: ProxyTypesAssemblyAttribute()]

namespace Augustus.CRM
{
    public class CrmContext : IDisposable
    {
        private OrganizationServiceContext context;

        public CrmContext(IOrganizationService service)
        {
            context = new OrganizationServiceContext(service);
        }

        public void Dispose()
        {
            context.Dispose();
        }

        public IQueryable<AccountEntity> Accounts
        {
            get { return context.CreateQuery<AccountEntity>(); }
        }

        public IQueryable<InvoiceEntity> Invoices
        {
            get { return context.CreateQuery<InvoiceEntity>(); }
        }

        public IQueryable<WorkDoneItemEntity> WorkDoneItems
        {
            get { return context.CreateQuery<WorkDoneItemEntity>(); }
        }

        public IQueryable<AllocationEntity> Allocations
        {
            get { return context.CreateQuery<AllocationEntity>(); }
        }

        public IQueryable<OpportunityEntity> Opportunities
        {
            get { return context.CreateQuery<OpportunityEntity>(); }
        }

        public DateTime ActiveDate { get; set; }
        public DateTime NewDate { get; set; }

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
