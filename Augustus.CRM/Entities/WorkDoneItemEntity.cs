using Augustus.Domain.Objects;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using System;
using System.Runtime.Serialization;

namespace Augustus.CRM.Entities
{
    [DataContract()]
    public enum WorkDoneItemState
    {

        [EnumMember()]
        Active = 0,

        [EnumMember()]
        Inactive = 1,
    }

    [EntityLogicalName("new_workdoneitem")]
    public partial class WorkDoneItemEntity : BaseEntity
    {

        public WorkDoneItemEntity() : base(EntityLogicalName) { }

        public const string EntityLogicalName = "new_workdoneitem";
        public const int EntityTypeCode = 10049;

        [AttributeLogicalName("new_invoiceid")]
        public Guid? InvoiceId
        {
            get
            {
                return GetAttributeEntityReferenceId("new_invoiceid");
            }
            set
            {
                if (value.HasValue)
                {
                    SetAttributeValue("new_invoiceid",
                        new EntityReference(
                            InvoiceEntity.EntityLogicalName,
                            value.Value));
                }
            }
        }

        [AttributeLogicalName("new_margin")]
        public decimal? Margin
        {
            get
            {
                return GetAttributeValueMoney("new_margin");
            }
            set
            {
                if (value.HasValue)
                {
                    SetAttributeValue("new_margin", new Money(value.Value));
                }
            }
        }

        public static WorkDoneItem ToDomainObject(WorkDoneItemEntity entity)
        {
            return new WorkDoneItem
            {
                Id = entity.Id,
                Created = entity.Created,
                InvoiceId = entity.InvoiceId,
                WorkDoneDate = entity.WorkDoneDate,
                Margin = entity.Margin
            };
        }

        [AttributeLogicalName("new_workdonedate")]
        public DateTime? WorkDoneDate
        {
            get
            {
                return GetAttributeValue<DateTime?>("new_workdonedate");
            }
            set
            {
                SetAttributeValue("new_workdonedate", value);
            }
        }

        [AttributeLogicalName("new_workdoneitemid")]
        public Guid? WorkDoneItemId
        {
            get
            {
                return GetAttributeValue<Guid?>("new_workdoneitemid");
            }
            set
            {
                SetAttributeValue("new_workdoneitemid", value);
                if (value.HasValue)
                {
                    base.Id = value.Value;
                }
                else
                {
                    base.Id = System.Guid.Empty;
                }
            }
        }

        [AttributeLogicalName("new_workdoneitemid")]
        public override Guid Id
        {
            get
            {
                return base.Id;
            }
            set
            {
                WorkDoneItemId = value;
            }
        }



        [AttributeLogicalName("statecode")]
        public WorkDoneItemState? State
        {
            get
            {
                OptionSetValue optionSet = GetAttributeValue<OptionSetValue>("statecode");
                if ((optionSet != null))
                {
                    return ((WorkDoneItemState)(System.Enum.ToObject(typeof(WorkDoneItemState), optionSet.Value)));
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if ((value == null))
                {
                    SetAttributeValue("statecode", null);
                }
                else
                {
                    SetAttributeValue("statecode", new OptionSetValue(((int)(value))));
                }
            }
        }
    }
}
