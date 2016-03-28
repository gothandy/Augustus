using Augustus.CRM.Entities;
using Augustus.Domain.Enums;
using Augustus.Domain.Objects;

namespace Augustus.CRM.Converters
{
    public static class InvoiceConverter
    {
        public static void SetUsingDomain(this InvoiceEntity entity, Invoice domain)
        {
            entity.Name = domain.Name;
            // AccountId set using lookup on Opportunity in query.
            entity.OpportunityId = domain.OpportunityId;
            entity.InvoiceNo = domain.InvoiceNo;
            entity.PONumber = domain.PONumber;
            entity.Cost = domain.Cost;
            entity.Revenue = domain.Revenue;
            entity.Margin = domain.Margin;
            entity.InvoiceDate = domain.InvoiceDate;
            entity.ClientApprovedDate = domain.ClientApprovedDate;
        }

        public static Invoice ConvertToDomain(this InvoiceEntity entity)
        {
            return InvoiceConverter.ToDomain(entity);
        }

        public static Invoice ToDomain(InvoiceEntity invoice, OpportunityEntity opportunity)
        {
            var domain = ToDomain(invoice);
            domain.Opportunity = OpportunityConverter.ToDomainObject(opportunity);
            return domain;
        }

        public static Invoice ToDomain(InvoiceEntity entity)
        {
            var domain = new Invoice();

            domain.Id = entity.Id;
            domain.Name = entity.Name;
            domain.AccountId = entity.AccountId;
            domain.OpportunityId = entity.OpportunityId;
            domain.ClientApprovedDate = entity.ClientApprovedDate;
            domain.Created = entity.Created;
            domain.InvoiceDate = entity.InvoiceDate;
            domain.InvoiceNo = entity.InvoiceNo;
            domain.PONumber = entity.PONumber;
            domain.Revenue = entity.Revenue;
            domain.Cost = entity.Cost;
            //Margin is calculated
            domain.Status = (InvoiceStatus)entity.Status;

            return domain;
        }
    }
}
