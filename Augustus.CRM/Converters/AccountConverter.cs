using Augustus.CRM.Entities;
using Augustus.Domain.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Augustus.CRM.Converters
{
    public static class AccountConverter
    {
        public static Account ToDomainObject(AccountEntity a)
        {

            return new Account
            {
                Id = a.Id,
                Name = a.Name,
                Created = a.Created
            };
        }
    }
}
