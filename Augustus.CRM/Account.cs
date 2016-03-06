using Augustus.Interfaces;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using System.Runtime.Serialization;

namespace Augustus.CRM
{

    [DataContract()]
    [EntityLogicalName("account")]
    public class Account : Entity, IAccount
    {

        public Account() : base(EntityLogicalName) { }

        public const string EntityLogicalName = "account";
        public const int EntityTypeCode = 1;

        [AttributeLogicalName("accountid")]
        public System.Guid? AccountId
        {
            get
            {
                return this.GetAttributeValue<System.Nullable<System.Guid>>("accountid");
            }
            set
            {
                this.SetAttributeValue("accountid", value);
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

        [AttributeLogicalName("accountid")]
        public override System.Guid Id
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

        [AttributeLogicalName("name")]
        public string Name
        {
            get
            {
                return this.GetAttributeValue<string>("name");
            }
            set
            {
                this.SetAttributeValue("name", value);
            }
        }
    }
}
