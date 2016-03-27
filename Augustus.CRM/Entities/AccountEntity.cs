using Augustus.CRM.Attributes;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
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
        public override Guid Id
        {
            get { return this.GetAttributeId("accountid"); }
            set { this.SetAttributeId("accountid", value); }
        }

        [AttributeLogicalName("name")]
        public string Name
        {
            get { return this.GetAttributeString("name"); }
            set { this.SetAttributeString("name", value); }
        }
    }
}
