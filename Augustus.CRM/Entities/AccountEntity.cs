using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using System.ComponentModel.DataAnnotations;
using System;

namespace Augustus.CRM.Entities
{

    [EntityLogicalName("account")]
    public class AccountEntity : BaseEntity
    {

        public AccountEntity() : base(EntityLogicalName) { }

        public const string EntityLogicalName = "account";
        public const int EntityTypeCode = 1;

        [AttributeLogicalName("accountid")]
        public Guid? AccountId
        {
            get
            {
                return GetAttributeValue<Guid?>("accountid");
            }
            set
            {
                SetAttributeValue("accountid", value);
                base.Id = value.GetValueOrDefault();
            }
        }

        [AttributeLogicalName("accountid")]
        public override Guid Id
        {
            get
            {
                return base.Id;
            }
            set
            {
                this.AccountId = value;
            }
        }

        [Required]
        [AttributeLogicalName("name")]
        public string Name
        {
            get
            {
                return GetAttributeValue<string>("name");
            }
            set
            {
                SetAttributeValue("name", value);
            }
        }
    }
}
