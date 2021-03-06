﻿using Augustus.CRM.AttributeExtensions;
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
            get { return this.GetAttributeId(); }
            set { this.SetAttributeId(value); }
        }

        [AttributeLogicalName("name")]
        public string Name
        {
            get { return this.GetAttributeString(); }
            set { this.SetAttributeString(value); }
        }

        [AttributeLogicalName("new_full_name")]
        public string FullName
        {
            get { return this.GetAttributeString(); }
            set { this.SetAttributeString(value); }
        }

        [AttributeLogicalName("parentaccountid")]
        [EntityReference(AccountEntity.EntityLogicalName)]
        public Guid? ParentAccountId
        {
            get { return this.GetAttributeEntityReference(); }
            set { this.SetAttributeEntityReference(value); }
        }
    }
}
